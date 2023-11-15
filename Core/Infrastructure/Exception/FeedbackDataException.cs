using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infrastructure.Exception
{
    public class FeedbackDataException<T>:System.Exception
    {
        public EnumFeedbackDataType DataType { get; }
        public T ExceptionData { get; }

        public FeedbackDataException(EnumFeedbackDataType dataType, string message, T exceptionData) : base(message)
        {
            DataType = dataType;
            ExceptionData = exceptionData;
        }
    }
    public enum EnumFeedbackDataType {
        ShiftAutoFil_SpecifyExtendedWorkHourEmployees = 1,
        ShiftExtended_SpecifyExtendedWorkHourEmployees = 2
    }
}
