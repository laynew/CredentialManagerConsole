namespace CredentialManagerConsole.WindowsCredentialManager.Marshalling
{
    internal class Result<T>
    {
        public T Value { get; }
        public bool IsSuccess { get; }
        public string Message { get; }

        private Result(T value)
        {
            Value = value;
            IsSuccess = true;
        }

        private Result(string message)
        {
            Message = message;
            IsSuccess = false;
        }

        public static Result<T> Fail(string message)
        {
            return new Result<T>(message);
        }

        public static Result<T> Success(T value)
        {
            return new Result<T>(value);
        }
    }
}