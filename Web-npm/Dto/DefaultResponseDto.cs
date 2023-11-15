namespace Web0.Dto
{
    public class DefaultResponseDto
    {
        public bool Ok { get; set; }
        public string Message { get; set; }

        public DefaultResponseDto()
        {

        }

        public DefaultResponseDto(bool ok)
        {
            Ok = ok;
        }

        public DefaultResponseDto(bool ok, string message)
        {
            Ok = ok;
            Message = message;
        }
    }

    public class DefaultResponseDto<T> : DefaultResponseDto
    {
        public T Data { get; set; }

        public DefaultResponseDto(T data)
        {
            Ok = true;
            Data = data;
        }

        public DefaultResponseDto(T data, bool ok)
        {
            Data = data;
            Ok = ok;
        }

        public DefaultResponseDto(T data, bool ok, string message)
        {
            Data = data;
            Ok = ok;
            Message = message;
        }
    }
}
