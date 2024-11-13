namespace Talabat.APIs.Errors
{
    public class ApiExceptionResponse : ApiResponse
    {
        public string? Details { get; set; }
        public ApiExceptionResponse(int StatusCode, string? ErrorMessage =null, string? details = null) : base(StatusCode, ErrorMessage)
        {
            Details = details;
        }
    }
}
