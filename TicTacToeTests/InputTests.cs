using NUnit.Framework;
using TicTacToe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TicTacToe.Tests
{
    [TestFixture()]
    public class InputTests
    {
        [TestCase("1", ExpectedResult = 1)]
        [TestCase("2", ExpectedResult = 2)]
        public int RequestGameModeTest_CorrectInputReturnsCorrectChoise(string str)
        {
            var stringReader = new StringReader(str);
            Console.SetIn(stringReader);
            int result = InputValidator.RequestGameMode();
            return result;
        }

        [TestCase("3", ExpectedResult = "Enter the number of your choice\n1. Vs Player\n2. Vs Bot")]
        [TestCase("abc", ExpectedResult = "This is not a number. Please enter the correct answer:")]
        public string RequestGameModeTest_InCorrectInputReturnsMessage(string str)
        {
            var stringReader = new StringReader(str);
            Console.SetIn(stringReader);
            _ = InputValidator.RequestGameMode();
            TextWriter tmpResult = Console.Out;
            string result = tmpResult.NewLine;
            return result;
        }


    }
}