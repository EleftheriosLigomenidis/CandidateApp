namespace CandidateApp.Business.Exceptions
{
    public class ValidationException
    {
        public ValidationException()
        {
            StatusCode = 400;
            Message = "Validation Errors";
        }
        public int StatusCode { get; private set; }
        public string Message { get; private set; }
        public IEnumerable<string> Errors { get; set; } = Enumerable.Empty<string>();
    }
}
