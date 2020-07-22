using System;
using System.IO;

using System.Web.Services.Protocols;

namespace SoapExtensionLib
{
    public class ProgressExtension : SoapExtension
    {
        private Stream m_wireStream;
        private Stream m_applicationStream;
        private long m_totalSize;
        private string m_requestGuid;
        private ProgressState m_state;
        private ProgressDelegate m_progressCallback;

        // How much data should we write/read at each access to the data stream
        private const int ChunkSize = 8192;

        // Could not figure out the size of data to be processed.
        public const int TotalSizeUnknown = -1;

        /// <summary>
        /// We don't use the initializers.
        /// For more information see:
        /// http://msdn2.microsoft.com/en-us/library/system.web.services.protocols.soapextension.getinitializer(VS.80).aspx
        /// </summary>
        /// <param name="serviceType"></param>
        /// <returns></returns>
        public override object GetInitializer(Type serviceType)
        {
            return null;
        }

        public override object GetInitializer(LogicalMethodInfo methodInfo, SoapExtensionAttribute attribute)
        {
            return null;
        }

        public override void Initialize(object initializer)
        {
        }

        /// <summary>
        /// Get our Soap extension access to the memory buffer containing the SOAP request or response. 
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public override Stream ChainStream(Stream stream)
        {
            m_wireStream = stream;
            m_applicationStream = new MemoryStream();
            return m_applicationStream;
        }

        /// <summary>
        /// Intercept the writing/reading of the message streams as they are sent/received
        /// </summary>
        /// <param name="message"></param>
        public override void ProcessMessage(SoapMessage message)
        {
            switch (message.Stage)
            {
                case SoapMessageStage.BeforeSerialize:
                    break;
                case SoapMessageStage.AfterSerialize:
                    WriteToWire(message);
                    break;
                case SoapMessageStage.BeforeDeserialize:
                    ReadFromWire(message);
                    break;
                case SoapMessageStage.AfterDeserialize:
                    break;
                default:
                    System.Diagnostics.Trace.Assert(false, "Unknown stage reported in ProgressExtension::ProcessMessage()");
                    break;
            }
        }

        void ReadFromWire(SoapMessage message)
        {
            SoapClientMessage clientMessage = message as SoapClientMessage;
            m_state = ProgressState.Retrieving;
            InitNotification(clientMessage);
            try
            {
                CopyStream(m_wireStream, m_applicationStream);
            }
            finally
            {
                m_applicationStream.Position = 0;
            }
        }

        void WriteToWire(SoapMessage message)
        {
            SoapClientMessage clientMessage = message as SoapClientMessage;
            m_state = ProgressState.Sending;
            InitNotification(clientMessage);
            m_applicationStream.Position = 0;
            CopyStream(m_applicationStream, m_wireStream);
        }

        void CopyStream(Stream fromStream, Stream toStream)
        {
            int processedSize = 0;
            // buffer used to copy data between the streams
            byte[] buffer = new byte[ChunkSize];

            while (true)
            {
                int bytesRead = fromStream.Read(buffer, 0, ChunkSize);
                if (bytesRead == 0)
                {
                    break;
                }
                toStream.Write(buffer, 0, bytesRead);
                processedSize += bytesRead;
                ReportProgress(processedSize);
            }
        }

        void InitNotification(SoapClientMessage clientMessage)
        {
            if (clientMessage.Client is IProxyProgressExtension)
            {
                IProxyProgressExtension proxy = clientMessage.Client as IProxyProgressExtension;
                m_requestGuid = proxy.RequestGuid;
                GetContentLength(clientMessage, proxy);
                m_progressCallback = proxy.Callback;
            }
        }

        /// <summary>
        /// Store the size of data to be processed.
        /// The way to obtain the size differs depending on whether we are sending or receiving data.
        /// * When we are reading from the web server, the web server reports the size in through the web response.
        /// * When we are sending data our stream has the size to be sent.
        /// </summary>
        /// <param name="clientMessage"></param>
        /// <param name="proxy"></param>
        void GetContentLength(SoapClientMessage clientMessage, IProxyProgressExtension proxy)
        {
            if (clientMessage.Stage == SoapMessageStage.BeforeDeserialize)
            {
                m_totalSize = proxy.ResponseContentLength;
            }
            else if (clientMessage.Stage == SoapMessageStage.AfterSerialize)
            {
                m_totalSize = clientMessage.Stream.Length;
            }
            else
            {
                m_totalSize = TotalSizeUnknown;
            }
        }

        void ReportProgress(int processedSize)
        {
            if (m_progressCallback != null)
            {
                ProgressEventArgs args = new ProgressEventArgs(processedSize, m_totalSize, m_requestGuid, m_state);
                m_progressCallback.Invoke(this, args);
            }
        }
    }
}
