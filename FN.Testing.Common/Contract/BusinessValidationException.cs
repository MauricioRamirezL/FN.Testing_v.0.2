using System;
using System.Data;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace FN.Testing.Common.Contract
{
    [Serializable]
    public class BusinessValidationException : DataException
    {
        public string PropertyName { get; }
        public string ValidationKey { get; }
        public BusinessValidationException() : base()
        {
            PropertyName = string.Empty;
            ValidationKey = string.Empty;
        }
        public BusinessValidationException(string propertyName, string validationKey)
            : this("The property: " + propertyName + " is not valid. Reason: " + validationKey)
        {
            PropertyName = propertyName;
            ValidationKey = validationKey;
        }
        public BusinessValidationException(string message)
          : base(message)
        {
            PropertyName = string.Empty;
            ValidationKey = string.Empty;
        }
        public BusinessValidationException(string propertyName, string validationKey, Exception innerException)
            : this("The property: " + propertyName + " is not valid. Reason: " + validationKey, innerException)
        {
            PropertyName = propertyName;
            ValidationKey = validationKey;
        }
        public BusinessValidationException(string message, Exception innerException)
          : base(message, innerException)
        {
            PropertyName = string.Empty;
            ValidationKey = string.Empty;
        }
        protected BusinessValidationException(SerializationInfo serializationInfo, StreamingContext streamingContext)
            : base(serializationInfo, streamingContext)
        {
            PropertyName = (string)serializationInfo.GetValue(nameof(PropertyName), typeof(string));
            ValidationKey = (string)serializationInfo.GetValue(nameof(ValidationKey), typeof(string));
        }
        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            info.AddValue(nameof(PropertyName), PropertyName);
            info.AddValue(nameof(ValidationKey), ValidationKey);
            base.GetObjectData(info, context);
        }
    }
}
