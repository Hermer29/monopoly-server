namespace MonopolyRoomServer.CliCommands
{
    public class CliMessenger
    {
        public void SendLine(string text)
        {
            Console.WriteLine(text);
        }

        public void Send(string text)
        {
            Console.Write(text);
        }

        public string Receive()
        {
            return Console.ReadLine() ?? "";
        }
    }
}
