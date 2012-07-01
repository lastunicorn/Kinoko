using System;
using System.Runtime.Serialization;

namespace DustInTheWind.SharpKinoko
{
    /// <summary>
    /// Exception raised by <see cref="Kinoko"/> class if the <see cref="Kinoko.Run"/> method has been called without
    /// previously setting a task to be tested.
    /// </summary>
    [Serializable]
    public class TaskNotSetException : KinokoException
    {
        private const string MESSAGE = "No task has been set to be tested by Kinoko.";

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskNotSetException"/> class.
        /// </summary>
        public TaskNotSetException()
            : base(MESSAGE)
        {
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="TaskNotSetException"/> class with a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public TaskNotSetException(Exception innerException)
            : base(MESSAGE, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskNotSetException"/> class with serialized data.
        /// </summary>
        /// <param name="info">The object that holds the serialized object data.</param>
        /// <param name="context">The contextual information about the source or destination.</param>
        public TaskNotSetException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
