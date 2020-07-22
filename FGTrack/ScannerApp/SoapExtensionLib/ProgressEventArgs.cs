using System;

namespace SoapExtensionLib
{
    /// <summary>
    /// The reported states during progress notification
    /// </summary>
    public enum ProgressState
    {
        Sending,
        ServerProcessing,
        Retrieving
    }

    /// <summary>
    /// When progress is reported a new instance of this class containing progress information is passed to the client
    /// </summary>
    public class ProgressEventArgs : EventArgs
    {
        private int m_processedSize;
        private long m_totalSize;

        // Unique ID of this call (Must be provided by the caller)
        private string m_guid;

        // the current state of this call
        private ProgressState m_state;

        public ProgressEventArgs(int processedSize, long totalSize, string guid, ProgressState status)
        {
            m_processedSize = processedSize;
            m_totalSize = totalSize;
            m_guid = guid;
            m_state = status;
        }

        /// <summary>
        /// Cummulative size of data processed during this call
        /// </summary>
        public int ProcessedSize
        {
            get { return m_processedSize; }
            set { m_processedSize = value; }
        }

        /// <summary>
        /// Total size of data of this call
        /// If the size is unknkown it will be set to SoapExtensionLib.ProgressExtension.TotalSizeUnknown
        /// </summary>
        public long TotalSize
        {
            get { return m_totalSize; }
            set { m_totalSize = value; }
        }

        /// <summary>
        /// Unique identifier for this call. (provided by caller to differentiate between multiple background calls)
        /// </summary>
        public string Guid
        {
            get { return m_guid; }
            set { m_guid = value; }
        }

        /// <summary>
        /// The kind of progress is being reported. (Sending, Waiting, Retrieving)
        /// e.g Use this property to show progress only during upload ( m_state == ProgressState.Sending )
        /// </summary>
        public ProgressState State
        {
            get { return m_state; }
            set { m_state = value; }
        }
    }
}
