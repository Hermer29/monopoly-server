using GameServerParts;

namespace MonopolyGameServer.Game.Process
{
    public struct Response
    {
        private CommandText _commandText;

        public Response(string text)
        {
            _commandText = CommandText.Parse(text);
        }

        public Rule Rule => _commandText.GetCommand().ParseRule();

        public string[] Arguments => _commandText.GetArguments();
    }
}
