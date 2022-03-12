using System.Collections.Generic;

namespace TbxPortal.Web.Dto
{
    public class DefaultResponseDto
    {
        public long Id { get; set; }
        public List<long> IdList { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }

        public string AdditionalInformation { get; set; }

        public DefaultResponseDto()
        {

        }
        public DefaultResponseDto(long id)
        {
            Id = id;
        }
        public DefaultResponseDto(long id, bool success)
        {
            Id = id;
            Success = success;
        }
        public DefaultResponseDto(long id, bool success, string message)
        {
            Id = id;
            Success = success;
            Message = message;
        }

        public DefaultResponseDto(List<long> idList)
        {
            IdList = idList;
        }
        public DefaultResponseDto(List<long> idList, bool success)
        {
            IdList = idList;
            Success = success;
        }
        public DefaultResponseDto(List<long> idList, bool success, string message)
        {
            IdList = idList;
            Success = success;
            Message = message;
        }

        public DefaultResponseDto(List<long> idList, bool success, string message, string additionalInformation)
        {
            IdList = idList;
            Success = success;
            Message = message;
            AdditionalInformation = additionalInformation;
        }

        public DefaultResponseDto(bool success, string message, string additionalInformation)
        {
            Success = success;
            Message = message;
            AdditionalInformation = additionalInformation;
        }
    }

    public class DefaultResponseDto<T> : DefaultResponseDto
    {
        public T Data { get; set; }
        public DefaultResponseDto(long id, T data)
        {
            Id = id;
            Data = data;
        }
        public DefaultResponseDto(long id, bool success, T data)
        {
            Id = id;
            Success = success;
            Data = data;
        }
        public DefaultResponseDto(long id, bool success, string message, T data)
        {
            Id = id;
            Success = success;
            Message = message;
            Data = data;
        }
    }
}