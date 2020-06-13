using System;
using System.Collections.Generic;
using System.IO;

namespace Tic_Tac_Toe
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("********Welcome to the game Tic-Tac-Toe!**********");
            TicTacToeBoard tttBoard = new TicTacToeBoard();

            int mode = tttBoard.ModeOfGame();

            switch (mode)
            {
                case 1:
                    ModePlayer1VsPlayer2(tttBoard);
                    break;

                case 2:
                    Console.WriteLine("\nDo you want to write the results of the game to a file (y - Yes): ");
                    string yesOrNo = Console.ReadLine();

                    // регистрируем событие
                    if (yesOrNo.ToLower().Equals("y"))
                    {
                        tttBoard.WriteToFile += ResultToFile;
                    }
                    ModePlayer1VsComp(tttBoard);
                    break;

                default:
                    Console.WriteLine("Error of choice!"); 
                    break;
            }
        }

        

        static void ModePlayer1VsPlayer2(TicTacToeBoard board)
        {
          
            do
            {
                Console.WriteLine("Determine who goes first: press your character or press \'r\' and it will be chosen randomly:");

                char prime;
                while (!char.TryParse(Console.ReadLine(), out prime) && !char.IsLetter(prime))
                {
                    Console.WriteLine("Invalid character! Follow the instructions.");
                }

                if (prime == board.Chip1)
                {
                    prime = board.Chip1;
                }
                else if(prime == board.Chip2)
                {
                    prime = board.Chip2;
                }
                else if (prime == 'r')
                {
                    Random rand = new Random();
                    int r = rand.Next(1, 1000);
                    if (r % 2 == 0)  prime = board.Chip1; 
                    else  prime = board.Chip2; 
                }
                else
                {
                    Console.WriteLine($"Such a chip does not exist! Default state, first: {board.Chip1}");
                    prime = board.Chip1;

                }

                Console.WriteLine("~~~~~~~~~~~The game started~~~~~~~~~~~");
                while (!board.IsFullBoard)
                {
                    board.showBoard();
                    Console.WriteLine($"Puts the chip player \'{prime}\':");
                    Console.WriteLine($"Select the position (0 - 8) where to insert the chip: ");
                    int pos;
                    while (!int.TryParse(Console.ReadLine(), out pos))
                    {
                        Console.WriteLine("This is not a number!. Follow the instructions!");
                    }
                    board.changeBoard(prime, pos);
                    if (board.IsWinner(prime))
                    {
                        board.showBoard();
                        break;
                    }

                    if (prime == board.Chip1) { prime = board.Chip2; }
                    else
                    { prime = board.Chip1; }
                }

                if (board.IsFullBoard)
                {
                    Console.WriteLine("The board is full. No changes");
                    board.showBoard();
                }

            } while (QuestionOnRepeat(board));

            Console.WriteLine("Goodbye!!!");
            Console.ReadLine();
        }

        static void ModePlayer1VsComp(TicTacToeBoard board)
        {
            
            do
            {

                Console.WriteLine("\nFirst to play Player 1");
                char prime = board.Chip1;
                Console.WriteLine("~~~~~~~~~~~The game started~~~~~~~~~~~");
                while (!board.IsFullBoard)
                {
                    board.showBoard();
                    Console.WriteLine($"Puts the chip player \'{prime}\':");
                    Console.WriteLine($"Select the position (0 - 8) where to insert the chip: ");
                    int pos;
                    while (!int.TryParse(Console.ReadLine(), out pos))
                    {
                        Console.WriteLine("This is not a number!. Follow the instructions!");
                    }
                    board.changeBoard(prime, pos);
                    board.showBoard();
                    if (board.IsWinner(prime))
                    {
                        board.showBoard();
                        break;
                    }

                    if (board.IsFullBoard)
                    {
                        Console.WriteLine("The board is full.");
                        break;
                    }

                    Console.WriteLine("The computer puts its chip");
                    int compM = board.CompMove();
                    Console.WriteLine($"CompMove = {compM}");
                    board.changeBoard(board.ChipComp, compM);
                    if (board.IsWinner(board.ChipComp))
                    {
                        board.showBoard();
                        break;
                    }

                    prime = board.Chip1;

                    if (board.IsFullBoard)
                    {
                        Console.WriteLine("The board is full. No changes");
                        board.showBoard();
                    }

                }

                

            } while (QuestionOnRepeat(board));
        }

        static bool QuestionOnRepeat(TicTacToeBoard board)
        {
            string answer;
            Console.WriteLine("Want to play one more time?(y/n)");
            answer = Console.ReadLine();

            if (answer.ToLower().Equals("y"))
            {
                board.ResetBoard();
                board.ClearListPositionsAndCharArray();
                board.IsFullBoard = false;
                return true;
            }
            else
                Console.WriteLine("This is NO!");
                return false;

        }

        // Метод для обработки события записи на диск результатов
        public static void ResultToFile(object sender, TicTacToeEventArgs ev)
        {
            Console.WriteLine(ev.Massage);
            TicTacToeBoard newBoard = sender as TicTacToeBoard;

            DriveInfo[] drives = DriveInfo.GetDrives();
            string dirName = drives[0].Name;

            if(drives[0].AvailableFreeSpace > 10000  && drives[0].IsReady)
            {
                var dir = Directory.CreateDirectory(dirName + "ResultOfTic-Tac-Toe");
                string path = dir.ToString() + "\\result.txt";

                string result = $"Побед компьютера: {ev.CountComp}\nПобед игрока: {ev.CountPlayer}";

                File.WriteAllText(path, result);
            }

        }
    }
}
