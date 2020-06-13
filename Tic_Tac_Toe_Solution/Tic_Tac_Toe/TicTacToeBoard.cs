using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Tic_Tac_Toe
{

    public delegate void MyAction(object sender, TicTacToeEventArgs e);
    
    public class TicTacToeEventArgs 
    {
        public string Massage { get; }
        public int CountComp { get; }
        public int CountPlayer { get; }

        public TicTacToeEventArgs(string massage, int countComp, int countPlayer)
        {
            Massage = massage;
            CountComp = countComp;
            CountPlayer = countPlayer;
        }

    }
    public class TicTacToeBoard
    {

        // Событие определяющее запись результатов игры в файл
        public event MyAction WriteToFile;

        private string board = @"
              |     |     
           0  |  1  |  2  
        ------------------  
              |     |     
           3  |  4  |  5  
        ------------------
              |     |     
           6  |  7  |  8  
              |     |      ";

        private List<int> listPositions = new List<int>();
        private readonly Dictionary<int, int> map = null;
        private int _countWinComp = 0;
        private int _countWinPlayer = 0;
        
        // Свойство, определяющее заполненность доски фишками-символами
        public bool IsFullBoard { get; set; } = false;

        private char _chip1;
        private char _chip2;
        public char Chip1 { get; private set; }
        public char Chip2 { get; private set; }
        public char ChipComp { get; private set; } = 'y';



        // Конструтор выполняющий создание карты-позиционирования
        public TicTacToeBoard()
        {
            this.map = new Dictionary<int, int>()
            {
                { 0, this.board.IndexOf('0') },
                { 1, this.board.IndexOf('1') },
                { 2, this.board.IndexOf('2') },
                { 3, this.board.IndexOf('3') },
                { 4, this.board.IndexOf('4') },
                { 5, this.board.IndexOf('5') },
                { 6, this.board.IndexOf('6') },
                { 7, this.board.IndexOf('7') },
                { 8, this.board.IndexOf('8') }

            };
        }

        
        public void showBoard()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(board);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("========================================");
            /*WriteToFile?.Invoke(this, new TicTacToeEventArgs("Recording completed!", _countWinComp, _countWinPlayer));*/
        }

        // Метод выбора режима игры(2 игрока или игрок против компьютера)
        public int ModeOfGame()
        {
            Console.WriteLine("Select game mode:\n" +
                              "1. Two players\n" +
                              "2. Single player\n" +
                              "3. Exit");
            int mode;


            while (!int.TryParse(Console.ReadLine(), out mode))
            {
                Console.WriteLine("Invalid mode! Press 1 or 2.");
            }

            while (mode != 1 && mode != 2 && mode != 3)
            {
                Console.WriteLine("No such game mode!");
                while (!int.TryParse(Console.ReadLine(), out mode))
                {
                    Console.WriteLine("Invalid mode! Press 1 or 2.");
                }
            }


            switch (mode)
            {
                case 1:
                    Console.WriteLine("Your choice: Two players");
                    ChoiceСhip("Player 1", ref _chip1);
                    ChoiceСhip("Player 2", ref _chip2);
                    Chip1 = _chip1;
                    Chip2 = _chip2;
                    if (Chip1 == Chip2) 
                    {
                        Chip2 = 'z';
                        Console.WriteLine($"You cannot play with the same chips. Player 2 will play: {Chip2}\n");
                    }
                    break;

                case 2:
                    Console.WriteLine($"Computer plays chip: {ChipComp}");
                    ChoiceСhip("Player 1", ref _chip1);
                    Chip1 = _chip1;
                    break;

                case 3:
                    Environment.Exit(0);
                    break;

                default:
                    Console.WriteLine("Error");
                    break;

            }
            
            return mode;
        }

        private void ChoiceСhip(string numbOfPlayers, ref char сhip)
        {
            Console.WriteLine($"{numbOfPlayers}, please select a chip to play (one character):");

            char choice;
            while (!char.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine("Invalid character! Follow the instructions.");
            }
            while (char.IsDigit(choice) || char.IsWhiteSpace(choice) || choice == ChipComp)
            {
                Console.WriteLine("Invalid character! Follow the instructions.");
                while (!char.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Invalid character! Follow the instructions.");
                }
            }
            сhip = choice;
        }

        // Метод, следящий за правильной логикой изменения положений на доске
        public void changeBoard(char symbol, int position)
        {

            

            if (!IsFullBoard)
            {
                while (position < 0 || position > 8 || this.listPositions.Contains(position))
            {
                Console.WriteLine("Wrong position! Try one more time!");
                while(!int.TryParse(Console.ReadLine(), out position))
                {
                    Console.WriteLine("This is not a number! Follow the instructions!");
                }
            }

            
                this.listPositions.Add(position);
                
                if (listPositions.Count == 9) { IsFullBoard = true; }

                this.board = RebuildBoard(symbol, position);
            }
            else
            {
                Console.WriteLine("The board is full. No changes");
            }
            
           

        }

        // Метод, перестраивающий доску-строку с учётом установленной фишки-символа
        private string RebuildBoard(char symbol, int position)
        {
            string beginBoard = this.board.Substring(0, map[position]);
            string symbolInBoard = symbol.ToString();
            string endBoard = this.board.Substring(map[position] + 1);

            string newBoard = beginBoard + symbolInBoard + endBoard;
            return newBoard;
        }

        // Метод, определяющий победителя по фишке-символу игрока
        public bool IsWinner(char symbol)
        {
            // Определим выигрышные комбинации
            int[,] combWin = new int[8, 3]  
            {
                { 0, 1, 2}, { 3, 4, 5}, { 6, 7, 8}, { 0, 3, 6},
                { 1, 4, 7}, { 2, 5, 8}, { 0, 4, 8}, { 2, 4, 6}
            };

            int first = 0, second = 0, third = 0;
            for (int i = 0; i < combWin.GetLength(0); i++)
            {
                int j = 0;
              
                 first = combWin[i, j++];
                 second = combWin[i, j++];
                 third = combWin[i, j++];
                
                if (board[map[first]] == board[map[second]] && board[map[second]] == board[map[third]]
                    && board[map[third]] == symbol)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\n*************Congratulation!!!**************");

                    Console.WriteLine($"Winner is: {symbol}!\n");
                    
                    
                        if (symbol == ChipComp)
                        {
                            _countWinComp++;
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("This is a computer victory! A shame!");
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                        else
                        {
                            _countWinPlayer++;
                        }


                    Console.ForegroundColor = ConsoleColor.Gray;
                    WriteToFile?.Invoke(this, new TicTacToeEventArgs("Recording completed!", _countWinComp, _countWinPlayer));  //  если событие для обработки добавлено (т.е. не равно null) то обработаем его
                    Console.WriteLine("**********************************************");

                    
                    return true;
                }
            }

            return false;
        }
        // Метод, определяющий предвыирышную ситуацию и возвращающий позицию, которая
        // приведёт к победе, либо -1, если таковой нет (метод для поддержания логики игры компьютера)
        private int AlmostWinnner()
        {
            int[,] combWin = new int[8, 3]
            {
                { 0, 1, 2}, { 3, 4, 5}, { 6, 7, 8}, { 0, 3, 6},
                { 1, 4, 7}, { 2, 5, 8}, { 0, 4, 8}, { 2, 4, 6}
            };

            int first = 0, second = 0, third = 0;
            for (int i = 0; i < combWin.GetLength(0); i++)
            {
                int j = 0;

                first = combWin[i, j++];
                second = combWin[i, j++];
                third = combWin[i, j++];

                if (board[map[first]] == board[map[second]] && board[map[third]] != Chip1 && board[map[third]] != ChipComp) return third;
                else if (board[map[second]] == board[map[third]] && board[map[first]] != Chip1 && board[map[first]] != ChipComp) return first;
                else if (board[map[first]] == board[map[third]] && board[map[second]] != Chip1 && board[map[second]] != ChipComp) return second;
                else continue;

            }
            return -1;
        }

        // Метод Логики игры компьютера
        public int CompMove()
        {
           /* int posReturn = AlmostWinnner();*/
            // Если есть предвыигрышная ситуация, то возвращаем её
            if(AlmostWinnner() != -1)
            {
                return AlmostWinnner();
            }
            // Если метод вернул -1, то ищем рандомную позицию, путём создания списка
            // из всех позиций. Затем вычитание тех позиций, что уже есть в глобальном
            // списке состояния. И у получившегося списка по индексу, выбранному рандомно
            // возвращаем позицию.
            else
            {
                int rand = new Random().Next(0, 9);
                while(this.listPositions.Contains(rand))
                {
                    rand = new Random().Next(0, 9);
                }
                return rand;
                /*var allPos = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
                var result = allPos.Except(listPositions).ToList();

                int index = new Random().Next(0, result.Count);
                
                return result[index];*/
                                       
            }
        }

        // Метод, возвращающий доску в исходное состояние
        public void ResetBoard()
        {
            string primaryBoard = @"
              |     |     
           0  |  1  |  2  
        ------------------  
              |     |     
           3  |  4  |  5  
        ------------------
              |     |     
           6  |  7  |  8  
              |     |      ";

            this.board = primaryBoard;
        }

        public void ClearListPositionsAndCharArray()
        {
            this.listPositions.Clear();       
        }


    }

}
