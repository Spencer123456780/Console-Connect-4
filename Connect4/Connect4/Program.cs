using System;

namespace Connect4
{
    class Board
    {
        public const int rows = 6;
        public const int columns = 7;
        public static char[,] board = new char[rows, columns]; //Making the 2d array
        public static bool[] columnIsFull = new bool[columns];

        public static void InitializeBoard()
        {
            for (int i = 0; i < rows; i++)//Rows
            {
                columnIsFull[i] = false;
                for (int ix = 0; ix < columns; ix++)//Columns
                {
                    board[i, ix] = '#'; //Initialize each entry in the board to #
                }
            }
        }


        public static void displayBoard()
        {
            Console.WriteLine();
            for (int i = 0; i < rows; i++)
            {
                Console.Write("     |");
                for (int ix = 0; ix < columns; ix++)
                {
                    Console.Write(Board.board[i, ix]);//This will Update as the game progress
                }
                Console.Write("|");
                Console.WriteLine();
            }
            Console.WriteLine("      0123456");
        }

        public static int BoardIsFull() //Stops game if Board is compleately full AKA Draw
        {
           int full = 0;
            for (int i = 0; i <columns; i++)
            {
                if (board[0, i] != '#')
                    full++;
            }
           return full;
        }
    }


    class Solution
    {
        public static bool Finished = false;
        public static bool Win = false;
        public static bool CheckFour(char activePlayer)
        {   
            //Horizontal Win
            for (int i = 0; i < 6; i++) //Rows
            {
                for (int ix = 0; ix < 4; ix++) //Columns
                {
                    if (Board.board[i, ix] == activePlayer && Board.board[i, ix + 1] == activePlayer && Board.board[i, ix + 2] == activePlayer &&
                        Board.board[i, ix + 3] == activePlayer)
                    {
                        Console.WriteLine("{0} Has Won The Game!", Connect4.Symbol);
                        Win = true;
                        Finished = true;
                    }
                }
            }

            //Vertical Win
            for (int i = 0; i < 3; i++)//Rows
            {
                for (int ix = 0; ix < 7; ix++)//Columns
                {
                    if (Board.board[i, ix] == activePlayer && Board.board[i + 1, ix] == activePlayer && Board.board[i + 2, ix] == activePlayer &&
                        Board.board[i + 3, ix] == activePlayer)
                    {
                        Console.WriteLine("{0} Has Won The Game!", Connect4.Symbol);
                        Win = true;
                        Finished = true;
                    }
                }
            }

            //Diagnoal Down
            for (int i = 0; i < 3; i++) //Rows
            {
                for (int ix = 0; ix < 4; ix++) //Columns
                {
                    if (Board.board[i, ix] == activePlayer && Board.board[i + 1, ix + 1] == activePlayer && Board.board[i + 2, ix + 2] == activePlayer &&
                        Board.board[i + 3, ix + 3] == activePlayer)
                    {
                        Console.WriteLine("{0} Has Won The Game!", Connect4.Symbol);
                        Win = true;
                        Finished = true;
                    }
                }
            }

            //Diagonal Up
            for (int i = 0; i < 3; i++) //Rows
            {
                for (int ix = 6; ix > 3; ix--) //Columns
                {
                    if (Board.board[i, ix] == activePlayer && Board.board[i + 1, ix - 1] == activePlayer && Board.board[i + 2, ix - 2] == activePlayer &&
                        Board.board[i + 3, ix - 3] == activePlayer)
                    {
                        Console.WriteLine("{0} Has Won The Game!", Connect4.Symbol);
                        Win = true;
                        Finished = true;
                    }
                }
            } 
            return Win;
        }
    }




    class Connect4
    {
        public static char Symbol;
        private static int ColumnPicked = 0;
        static void Main(string[] args)
         {
            Console.WriteLine("Wellcome To Connect 4");
            Console.WriteLine("Your objective is to get 4 of your Tiles in a row from any direction");
            Console.WriteLine("Good luck :)");
            Board.InitializeBoard();
            Board.displayBoard();

            int firstPlayer = new Random().Next() % 2; //Picks a random number between 0 and 1
            if (firstPlayer == 0)
            {
                Symbol = 'X';  
                Console.WriteLine("X will go first");
            }
            else
            {
                Symbol = 'O';
                Console.WriteLine("O will go first");
            }

            do
            {
                if (Symbol == 'X') 
                {
                    Console.Write("X");
                }
                else
                {
                    Console.Write("O");
                }
                Console.Write(" Pick a Column from 0 - 6 ");
                try 
                { 
                    int input = int.Parse(Console.ReadLine());
                    ColumnPicked = input;
                    if (ColumnPicked >= 0 && ColumnPicked <= 6) //Makes you pick a column from 0-6 in the array so 7 columns total
                    {

                        placeInColumn(ColumnPicked,Symbol); //Makes the Column you picked replace the # with your Symbol
                        if (Solution.CheckFour(Symbol) == true) //Check if the win is true or not
                        {
                            Console.WriteLine("Player {0} has Won!!!", Symbol);
                        }
                        //Switch Players If Column is NOT FULL
                        else if (!Board.columnIsFull[ColumnPicked]) //Column is not full
                        {
                            Symbol = (Symbol == 'X' ? 'O' : 'X'); //Swichs Players
                        }
                        if (Board.BoardIsFull() == 7)
                        {
                            Console.WriteLine("The game ended in a draw!!!");
                            Solution.Finished = true;
                        }
                    }
                    else //Someone entered a number outside of 0-6
                    {
                        Console.WriteLine("Please Enter a number between 0-6 ");
                    }
                }
                catch (Exception) { }
            }
            while (!Solution.Finished);
         }

        private static void placeInColumn(int columnNumber, char newSymbol)//Player symbol is droped into the column
        {
            int newRow = Board.rows - 1;
            char op = Board.board[newRow, columnNumber];
            while ((op == 'X' || op == 'O') && newRow >= 0)
            {
                newRow--;//Heads up in the column by row
                if (newRow >= 0) op = Board.board[newRow, columnNumber];//Run up the rows and columns to find first #
            }
            if (newRow < 0) Board.columnIsFull[columnNumber] = true;//drops until column is full

            if (!Board.columnIsFull[columnNumber])//column is not full
            {
                Board.board[newRow, columnNumber] = newSymbol; //Sets the empty slot # to the players symbol
                Board.displayBoard(); //shows the board again
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("!!!!COLUMN IS FULL!!!!");
                Console.WriteLine();
            }
        }
    }
}