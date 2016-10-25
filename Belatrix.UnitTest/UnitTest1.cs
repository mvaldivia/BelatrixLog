using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Belatrix.Log;

namespace Belatrix.UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
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

            int isComplete = 0;
            try
            {
                JobLogger log = new JobLogger(_logToFile, _logToConsole, _logToDatabase, _logMessage, _logWarning, _logError);
                JobLogger.LogMessage(message, isMessage, warning, error);
                isComplete = 1;
            }
            catch (Exception)
            {
                isComplete = -1;
            }
            Assert.AreEqual(1, isComplete);
        }
    }
}