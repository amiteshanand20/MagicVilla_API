using MagicVilla_VillaAPI.Logger;

namespace MagicVilla_VillaAPI.Logs
{
    public class Logging : ILogging
    {
        public void LogInformation(string message, string type)
        {
            if(type == "Error")
            {
                Console.WriteLine("Error: " + message);
            }
            else
            {
                Console.WriteLine("Info: " + message);
            }   
        }
    }
}
