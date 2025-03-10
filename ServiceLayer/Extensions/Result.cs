namespace ServiceLayer.Extensions
{
    public class Result<T>
    {
        public T? Value { get; }
        public List<string> Errors { get; } = [];
        public bool IsSuccess => Errors.Count == 0;
        public Dictionary<string, string>? Messages { get; }

        private Result(T value, Dictionary<string, string>? messages = null)
        {
            Value = value;
            Messages = messages;
        }
        private Result(List<string> errors) => Errors = errors;

        public static Result<T> Success(T value, Dictionary<string, string> messages) 
            => new(value, messages);
        public static Result<T> Failure(params string[] errors) => new([.. errors]);
    }
}
