using System;

namespace TicTacToe
{
    public static class UI
    {
        /// <summary>
        /// Print game logo to console.
        /// </summary>
        public static void PrintLogo()
        {
            Console.WriteLine("          +------------------------------+\n          |\\* <><><><><><><><><><><><> */|\n          | +--------------------------+ |");
            Console.WriteLine("          |*||------------------------||*| \n          + ||* THE TIC TAC TOE GAME *|| +\n           \\||------------------------||/");
            Console.WriteLine("            +--------------------------+ ");
        }

        /// <summary>
        /// A message that the opponent is a bot.
        /// </summary>
        public static void MessageIfOpponentIsBot()
        {
            Console.WriteLine("Your opponent is Computer!");
        }

        /// <summary>
        /// A message with number of the turn.
        /// </summary>
        public static void MessageAboutTurn(int currentTurnNumber)
        {
            Console.WriteLine($"\n*** Current turn: {currentTurnNumber} ***");
        }

        /// <summary>
        /// A message that the game has started..
        /// </summary>
        public static void MessageAboutGameStarted()
        {
            Console.WriteLine($"\n***** Game started! *****\n");
        }

        /// <summary>
        /// A message about whose turn it is
        /// </summary>
        /// <param name="playerName">The name of the player whose turn it is</param>
        public static void MessageWhoseTurn(string playerName)
        {
            Console.WriteLine($"\n{playerName}, now it's your turn!");
        }

        /// <summary>
        /// A message that the bot made a move.
        /// </summary>
        /// <param name="playerName">The name of the bot</param>
        public static void MessageBotMadeTurn(string playerName)
        {
            Console.WriteLine($"\n{playerName} makes turn...");
        }

        /// <summary>
        /// A message about the number of combinations of the player.
        /// </summary>
        /// <param name="playerName">The name of the player</param>
        /// <param name="countOfCombinationsMade">The number of combinations of the player</param>
        public static void MessageAbountPlayerCombinations(string playerName, int countOfCombinationsMade)
        {
            Console.WriteLine($"*** {playerName}, now you have {countOfCombinationsMade} points! ***");
        }

        /// <summary>
        /// A message that the field is already occupied by another player.
        /// </summary>
        /// <param name="coordinateX">The X coordinate</param>
        /// <param name="coordinateY">The Y coordinate</param>
        public static void MessageThatFieldAlreadyOccupied(int coordinateX, int coordinateY)
        {
            Console.WriteLine($"\n*** The field [{coordinateX},{coordinateY}] already flagged! You have to choose another. ***");
        }

        /// <summary>
        /// A message that there are no more empty fields on the board
        /// </summary>
        public static void MessageThatNoMoreFields()
        {
            Console.WriteLine("*** There are no more empty fields on the board... ***");
        }

        /// <summary>
        /// A message about the number of new combinations.
        /// </summary>
        /// <param name="numberOfCombinationsFounded"></param>
        public static void MessageNewCombinationsCount(int numberOfCombinationsFounded)
        {
            if (numberOfCombinationsFounded == 1)
            {
                Console.WriteLine($"\n*** {numberOfCombinationsFounded} new combination have been created! ***");
            }
            else
            {
                Console.WriteLine($"\n*** {numberOfCombinationsFounded} new combinations have been created! ***");
            }
        }

        /// <summary>
        /// A message that the player no longer has options for creating a combination.
        /// </summary>
        /// <param name="playerName">The name of player</param>
        public static void MessageThatNoMoreCombinationsPossible(string playerName)
        {
            Console.WriteLine($"*** {playerName} no longer has options to create a combination. ***");
        }

        /// <summary>
        /// A message that there is no chance of winning.
        /// </summary>
        /// <param name="playerName">The name of player</param>
        public static void MessageThatNoChanseToWin(string playerName)
        {
            Console.WriteLine($"*** Sorry {playerName}, but you have no chance of winning... ***");
        }

        /// <summary>
        /// A message with the results of the players.
        /// </summary>
        /// <param name="firstPlayerName">The name of the first player</param>
        /// <param name="firstPlayerCombinationsCount">Number of points for the first player</param>
        /// <param name="secondPlayerName">The name of the second player</param>
        /// <param name="secondPlayerCombinationsCount">Number of points for the second player</param>
        public static void MessageWithScores(string firstPlayerName, int firstPlayerCombinationsCount, string secondPlayerName, int secondPlayerCombinationsCount)
        {
            Console.WriteLine($"\n*** {firstPlayerName}, you earned {firstPlayerCombinationsCount} points!");
            Console.WriteLine($"\n*** {secondPlayerName}, you earned {secondPlayerCombinationsCount} points!");
        }

        /// <summary>
        /// A message with congratulations to the player.
        /// </summary>
        /// <param name="winnerName"></param>
        public static void MessageCongratsToPlayer(string winnerName)
        {
            Console.WriteLine($"\n***** GAME OVER! *****\nCongratulations {winnerName}, you won!");
        }

        /// <summary>
        /// A message when there is a draw.
        /// </summary>
        public static void MessageIsDraw()
        {
            Console.WriteLine($"\n***** GAME OVER! *****\nIncredibly, the players scored the same number of points! This is a draw!");
        }

        /// <summary>
        /// A message that players have scored the same number of points.
        /// </summary>
        public static void MessageNotScored()
        {
            Console.WriteLine($"\n***** GAME OVER! *****\nUnbelievable, but the players didn't score any points! You are both losers!");
        }

        /// <summary>
        /// Prints the board to the console.
        /// </summary>
        public static void DrawBoardInConsole(Board currentBoard, bool withBorder)
        {
            if (withBorder)
            {
                PrintWithBorder(currentBoard);
            }
            else
            {
                PrintWithoutBorder(currentBoard);
            }
        }

        /// <summary>
        /// Prints a board without borders.
        /// </summary>
        private static void PrintWithoutBorder(Board currentBoard)
        {
            var maxRowIndex = currentBoard.BoardMatrix.GetUpperBound(0);
            var maxColIndex = currentBoard.BoardMatrix.GetUpperBound(1);
            for (int rowIndex = 0; rowIndex <= maxRowIndex; rowIndex++)
            {
                for (int colIndex = 0; colIndex <= maxColIndex; colIndex++)
                {
                    Console.Write(currentBoard.BoardMatrix[rowIndex, colIndex]);
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Prints a board with borders.
        /// </summary>
        private static void PrintWithBorder(Board currentBoard)
        {
            var prefixForBorder = "|";
            var postfixForBorder = "|";
            var prefixForBoardFields = " ";
            var postfixForBoardFields = " ";
            var cornerField = "+";
            var standartField = "-";
            var spacerForBoard = "";
            var spacerForFields = "";
            if (currentBoard.Cols > 9 || currentBoard.Rows > 9)
            {
                spacerForBoard = "-";
                spacerForFields = " ";
            }
            var maxRowIndex = currentBoard.BoardMatrix.GetUpperBound(0);
            var maxColIndex = currentBoard.BoardMatrix.GetUpperBound(1);
            for (int rowIndex = -2; rowIndex <= maxRowIndex + 2; rowIndex++)
            {
                for (int colIndex = -2; colIndex <= maxColIndex + 2; colIndex++)
                {
                    if ((colIndex >= 0 && colIndex <= maxRowIndex) && (rowIndex >= 0 && rowIndex <= maxColIndex))
                    {
                        Console.Write(prefixForBoardFields + spacerForFields + currentBoard.BoardMatrix[rowIndex, colIndex] + spacerForFields + postfixForBoardFields);
                    }
                    else if ((colIndex == -1 || colIndex == maxRowIndex + 1) || (rowIndex == -1 || rowIndex == maxColIndex + 1))
                    {
                        Console.Write(prefixForBorder + spacerForBoard + standartField + spacerForBoard + postfixForBorder);
                    }
                    else if (colIndex == -2 || colIndex == maxColIndex + 2)
                    {
                        if (rowIndex == -2 || rowIndex == maxRowIndex + 2)
                        {
                            Console.Write(prefixForBorder + spacerForBoard + cornerField + spacerForBoard + postfixForBorder);
                        }
                        else
                        {
                            if (rowIndex <= 8)
                            {
                                Console.Write(prefixForBorder + spacerForBoard + (rowIndex + 1) + spacerForBoard + postfixForBorder);
                            }
                            else
                            {
                                Console.Write(prefixForBorder + spacerForBoard + (rowIndex + 1) + postfixForBorder);
                            }
                        }
                    }
                    else if (rowIndex == -2 || rowIndex == maxRowIndex + 2)
                    {
                        if (colIndex <= 8)
                        {
                            Console.Write(prefixForBorder + spacerForBoard + (colIndex + 1) + spacerForBoard + postfixForBorder);
                        }
                        else
                        {
                            Console.Write(prefixForBorder + spacerForBoard + (colIndex + 1) + postfixForBorder);
                        }
                    }
                }
                Console.Write("\n");
            }
        }
    }
}