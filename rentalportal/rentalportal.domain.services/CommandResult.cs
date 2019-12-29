namespace rentalportal.domain.services
{
    public class CommandResult<T>
    {
        private CommandResult(T payload) => Payload = payload;
        private CommandResult(object reason) => FailureReason = reason;

        public T Payload { get; private set; }
        public object FailureReason { get; }
        public bool IsSuccess => FailureReason == null;

        public static CommandResult<T> Success(T payload) => new CommandResult<T>(payload);
        public static CommandResult<T> Fail(object reason) => new CommandResult<T>(reason);
        public static CommandResult<T> NotFound(object identifier = null)
        {
            string reason = $"{typeof(T).Name} was not found.";
            if (identifier != null)
            {
                reason = $"{typeof(T).Name} with an identifier '{identifier}' was not found.";
            }

            return Fail(reason.Replace("ViewModel", ""));
        }

        public static implicit operator bool(CommandResult<T> result) => result.IsSuccess;
    }
}
