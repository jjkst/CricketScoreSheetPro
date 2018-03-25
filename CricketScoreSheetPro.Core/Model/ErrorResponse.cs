namespace CricketScoreSheetPro.Core.Model
{
    public enum ErrorTypes
    {
        None, Error, Warning, Info
    }

    public class ErrorResponse
    {
        public string Message { get; set; }

        public ErrorTypes ErrorType { get; set; }
    }
}
