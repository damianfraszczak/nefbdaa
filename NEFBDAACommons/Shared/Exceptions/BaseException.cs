using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace NEFBDAACommons.Shared.Exceptions
{
    [Serializable]
    public class BaseException : Exception
    {
        public BaseException()
        {
        }

        public BaseException(string message) : base(message)
        {
        }

        public BaseException(string message, Exception exception) : base(message, exception)
        {
        }
        public BaseException(string message, ExceptionUiConfig config) : base(message)
        {
            this.Config = config;

        }
        public BaseException(string message, Exception exception, ExceptionUiConfig config) : base(message, exception)
        {
            this.Config = config;
        }
        protected BaseException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            ResourceReferenceProperty = info.GetString(nameof(ResourceReferenceProperty));
        }

        public string ResourceReferenceProperty { get; set; }
        public ExceptionUiConfig Config { get; set; }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            info.AddValue(nameof(ResourceReferenceProperty), ResourceReferenceProperty);

            base.GetObjectData(info, context);
        }
    }
}
