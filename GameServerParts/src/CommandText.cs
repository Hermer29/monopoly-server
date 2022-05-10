using System.Text.RegularExpressions;

namespace GameServerParts
{
    public class CommandText
    {
        private string? _command;
        private string[]? _args;
        private string[] _delimitedLine;

        private CommandText(string[] delimitedLine)
        {
            _delimitedLine = delimitedLine;
        }

        public int ArgumentsCount => GetArguments().Count();

        public static CommandText Parse(string line)
        {
            string formatted = ExcludeRepeatingSpaces(line);
            int wordsCount = CountOfWords(formatted);
            if(wordsCount == 1)
            {
                return new CommandText(new string[] { formatted });
            }
            var splittedCommand = formatted.Split();
            return new CommandText(splittedCommand);
        }

        private static string ExcludeRepeatingSpaces(string text)
        {
            var filtered = Regex.Replace(text, @"\s+", " ");
            var deleteInStart = Regex.Replace(filtered, @"^\s", "");
            return deleteInStart;
        }

        private static int CountOfWords(string words)
        {
            var matches = Regex.Matches(words, @"\s");
            var wordsCount = matches.Count + 1;
            return wordsCount;
        }

        public string GetCommand()
        {
            if (_command != null)
                return _command;

            _command = _delimitedLine[0];
            return _command;
        }

        public string[] GetArguments()
        {
            if (_args != null)
                return _args;

            _args = _delimitedLine.Skip(1).Select(x => x.Trim('\0')).ToArray();
            return _args;
        }

        public override string ToString()
        {
            var args = GetArguments().Any() ? GetArguments().Aggregate((a, b) => a + " " + b) : string.Empty;
            return $"{GetCommand()} {args}";
        }
    }
}
