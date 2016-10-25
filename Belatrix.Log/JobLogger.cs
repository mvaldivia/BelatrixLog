using System;
using System.IO;
using System.Configuration;
using System.Data.SqlClient;

namespace Belatrix.Log
{

    public class JobLogger
    {

        private static bool _logToFile;
        private static bool _logToConsole;
        private static bool _logMessage;
        private static bool _logWarning;
        private static bool _logError;
        private static bool _logToDatabase;
        private bool _initialized;

        public JobLogger(bool logToFile, bool logToConsole, bool logToDatabase, bool logMessage, bool logWarning, bool logError)
        {
            _logError = logError;
            _logMessage = logMessage;
            _logWarning = logWarning;
            _logToDatabase = logToDatabase;
            _logToFile = logToFile;
            _logToConsole = logToConsole;
        }

        public static void LogMessage(string message, bool isMessage, bool warning, bool error)
        {
            message.Trim();
            if (message == null || message.Length == 0)
            {
                return;
            }
            if (!_logToConsole && !_logToFile && !_logToDatabase)
            {
                throw new Exception("Invalid configuration");
            }
            if ((!_logError && !_logMessage && !_logWarning) || (!isMessage && !warning && !error))
            {
                throw new Exception("Error or Warning or Message must be specified");
            }

            int typemessage = 0;

            if (_logToDatabase)
            {
                SqlConnection connection = new
                SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]);
                connection.Open();

                if (isMessage && _logMessage)
                {
                    typemessage = 1;
                }

                if (error && _logError)
                {
                    typemessage = 2;
                }

                if (warning && _logWarning)
                {
                    typemessage = 3;
                }

                SqlCommand command = new
                SqlCommand("Insert into Log Values('" + message + "', " + typemessage.ToString() + ")");
                command.ExecuteNonQuery();
            }

            string fileData = string.Empty;
            string fullPathFile = ConfigurationManager.AppSettings["LogFileDirectory"] + "LogFile_" + DateTime.Now.ToString("yyyyMMdd") + ".txt";

            if (_logToFile)
            {
                if (!File.Exists(fullPathFile))
                {
                    File.Create(fullPathFile).Close();
                }

                fileData = File.ReadAllText(fullPathFile);

                if (error && _logError)
                {
                    fileData += DateTime.Now.ToShortDateString() + " Error: " + message;
                }
                if (warning && _logWarning)
                {
                    fileData += DateTime.Now.ToShortDateString() + " Warning: " + message;
                }
                if (isMessage && _logMessage)
                {
                    fileData += DateTime.Now.ToShortDateString() + " Message: " + message;
                }
                File.WriteAllText(fullPathFile, fileData);
            }

            if (_logToConsole)
            {
                if (error && _logError)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                if (warning && _logWarning)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                }
                if (isMessage && _logMessage)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.WriteLine(DateTime.Now.ToShortDateString() + " " + message);
            }
        }

    }

}