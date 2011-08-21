using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BitbucketBackup
{
    /// <summary>
    /// custom exception, message will always be displayed to the user
    /// </summary>
    internal class ClientException : Exception
    {
        /// <summary>
        /// Creates a new instance of the ClientException class.
        /// </summary>
        /// <param name="message">The error message</param>
        /// <param name="innerException">The inner exception or null</param>
        public ClientException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
