using System;

namespace SoapExtensionLib
{
    public delegate void ProgressDelegate(object sender, ProgressEventArgs e);

    /// <summary>
    /// Interface to be implemented by a web service proxy class to get progress notification.
    /// </summary>
    public interface IProxyProgressExtension
    {
        /// <summary>
        /// Use the RequestGuid to differentiate between multiple background calls.
        /// </summary>
        string RequestGuid { get; set; }

        /// <summary>
        /// The size in bytes of the stream we are reading back from the web server
        /// </summary>
        long ResponseContentLength { get; }

        /// <summary>
        /// Callback to report progress
        /// </summary>
        ProgressDelegate Callback { get; }
    }
}
