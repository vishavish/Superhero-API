namespace Superhero.Api.Models
{
    public class Result<T>
    {
        private Result(T data, bool isSuccess, string message)
        {
            Data = data;
            IsSuccess = isSuccess;
            Message = message;
        }

        public T? Data { get; set; }
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }

        public static Result<T> Success(T data) => new(data, true, string.Empty);

        #pragma warning disable CS8604
        public static Result<T> Failure(string message) => new(default, false, message);

    }
}
