﻿using System;
using System.Runtime.Serialization;

namespace DustInTheWind.SharpKinoko
{
    /// <summary>
    /// Base exception used by "Kinoko" framework.
    /// </summary>
    [Serializable]
    public class KinokoException : ApplicationException
    {
        private const string MESSAGE = "Internal error in Kinoko.";

        /// <summary>
        /// Initializes a new instance of the <see cref="KinokoException"/> class.
        /// </summary>
        public KinokoException()
            : base(MESSAGE)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="KinokoException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">A message that describes the error.</param>
        public KinokoException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="KinokoException"/> class with a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public KinokoException(Exception innerException)
            : base(MESSAGE, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="KinokoException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public KinokoException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="KinokoException"/> class with serialized data.
        /// </summary>
        /// <param name="info">The object that holds the serialized object data.</param>
        /// <param name="context">The contextual information about the source or destination.</param>
        public KinokoException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}