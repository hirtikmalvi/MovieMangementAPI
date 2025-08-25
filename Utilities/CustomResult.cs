namespace MovieManagementAPI.Utilities
{
    public class CustomResult<T>
    {
        public bool Status { get; set; } = true;
        public T? Data { get; set; }
        public string Message { get; set; } = string.Empty;
        public int StatusCode { get; set; }
        public List<string>? Errors { get; set; }

        public static CustomResult<T> Ok(T data, int statusCode, string message)
        {
            return new CustomResult<T>
            {
                Status = true,
                Data = data,
                StatusCode = statusCode,
                Message = message,
                Errors = null
            };
        }
        public static CustomResult<T> Fail(int statusCode, string message, List<string> errors = null)
        {
            return new CustomResult<T>
            {
                Status = false,
                StatusCode = statusCode,
                Message = message,
                Errors = errors
            };
        }
    }
}
