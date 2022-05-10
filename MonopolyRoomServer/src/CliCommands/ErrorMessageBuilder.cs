namespace MonopolyRoomServer.CliCommands
{
    public class ErrorMessageBuilder
    {
        private int[] _requiredArgsCount;
        private string _commandName;

        public ErrorMessageBuilder(string commandName, params int[] requiredArgsCount)
        {
            _requiredArgsCount = requiredArgsCount;
            _commandName = commandName;
        }

        public string ArgumentAmountError(int actual)
        {
            var requiredArgsCountString = _requiredArgsCount.Select(x => x.ToString()).Aggregate((a, x) => a += ", " + x).Trim(' ').Trim(',');
            return $"{_commandName}: Command cannot be used with {actual} amount of arguments ({requiredArgsCountString} expected)";
        }

        public string WithHeader(string text)
        {
            return $"{_commandName}: {text}";
        }
    }
}
