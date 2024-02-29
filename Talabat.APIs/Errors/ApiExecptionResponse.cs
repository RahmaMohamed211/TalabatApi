namespace Talabat.APIs.Errors
{
    public class ApiExecptionResponse :ApiErrorResponse
    {
        public string? Details { get; set; }

        public ApiExecptionResponse(int StatusCode,string? Message=null,string? Details=null):base(StatusCode,Message)
        {
            this.Details = Details;
        }



    }
}
