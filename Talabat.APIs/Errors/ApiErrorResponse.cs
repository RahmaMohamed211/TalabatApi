namespace Talabat.APIs.Errors
{
    public class ApiErrorResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }

        public ApiErrorResponse(int StatusCode,string? message=null)
        {
            this.StatusCode = StatusCode;
            Message = message ?? GetDefaultMessageForStatusCode(StatusCode);
        }
        private string? GetDefaultMessageForStatusCode(int StatusCode)
        {
            return StatusCode switch
            {
                400 => "A bad Requst,You Have Made",
                401 => "Authorized ,You are not",
                404 => "Resourses Not Found",
                500 => "There is Server Error",
                _=>null


            };
        }
    }
}
