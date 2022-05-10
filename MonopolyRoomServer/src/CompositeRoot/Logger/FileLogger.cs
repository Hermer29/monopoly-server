namespace MonopolyRoomServer.Loggers
{
    public class FileLogger
    {
        private string _fileName;

        public FileLogger(Configurations configurations)
        {
            _fileName = configurations.GetConnectionsLoggingFilePath();
        }

        public void Log(string text, bool time)
        {
            var timeStamp = time ? $"{DateTime.Now}|" : "";
            using (var writer = File.AppendText(_fileName))
            {
                writer.Write($"{timeStamp}{text}");
                writer.Write("\n\n");
            }
        }

        public void Log(string text)
        {
            Log(text, true);
        }
    }
}
