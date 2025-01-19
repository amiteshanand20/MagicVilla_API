using MagicVilla_VillaAPI.Logger;

namespace MagicVilla_VillaAPI.Logs
{
    public class LoggingV2 : ILogging
    {
        public void LogInformation(string message, string type)
        {
            if (type == "Error")
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine("Error: " + message);
                Console.BackgroundColor = ConsoleColor.Black;
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Info: " + message);
                Console.BackgroundColor = ConsoleColor.Black;       
            }
        }
    }
}
