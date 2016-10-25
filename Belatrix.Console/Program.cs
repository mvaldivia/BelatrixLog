using Belatrix.Log;
using System;

namespace Belatrix.Console
{
    
    class Program
    {
    
        static void Main(string[] args)
        {
            bool _logToDatabase = false;
            bool _logToFile = true;
            bool _logToConsole = false;

            string message = "This is an error message test.";

            bool _logMessage = false;
            bool isMessage = false;

            bool _logWarning = true;
            bool warning = true;

            bool _logError = false;
            bool error = false;


            JobLogger log = new JobLogger(_logToFile, _logToConsole, _logToDatabase, _logMessage, _logWarning, _logError);
            JobLogger.LogMessage(message, isMessage, warning, error);
            
            System.Console.ReadLine();
        }

    }

}
