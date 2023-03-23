using System;
using static System.Console;
using static System.Convert;

namespace TicTacToe
{
    class Program
    {
        static int RandomNum()
        {
            Random random = new Random();
            int rndNum = random.Next(1, 10);
            return rndNum;
        }

        static void DisplayField(char[,] arr)
        {
            char lineStraight = '|';
            int c = 0;
            for (int r = 0; r < arr.GetLength(0); r++)
            {
                for (c = 0; c < arr.GetLength(1); c++)
                {
                    if (arr[r, c] != 0)
                    {
                        Write($"{arr[r, c]}");
                        if (r != arr.GetLength(0))
                        {
                            if (c != arr.GetLength(1) - 1)
                            {
                                Write(lineStraight);
                            }
                        }
                    }
                    else
                    {
                        Write(' ');
                        if (r != arr.GetLength(0))
                        {
                            if (c != arr.GetLength(1) - 1)
                            {
                                Write(lineStraight);
                            }
                        }
                    }
                }
                WriteLine();
                if (r != arr.GetLength(0)-1)
                {
                    WriteLine("-----");
                }               
            }
        }

        static bool PlaceMarker(char[,] arr, int num, char c)
        {
            num--;
            int rH = 0, cH = 0;

            switch (num)
            {
                case 0:
                    rH = 0; cH = 0;
                    break;
                case 1:
                    rH = 0; cH = 1;
                    break;
                case 2:
                    rH = 0; cH = 2;
                    break;
                case 3:
                    rH = 1; cH = 0;
                    break;
                case 4:
                    rH = 1; cH = 1;
                    break;
                case 5:
                    rH = 1; cH = 2;
                    break;
                case 6:
                    rH = 2; cH = 0;
                    break;
                case 7:
                    rH = 2; cH = 1;
                    break;
                case 8:
                    rH = 2; cH = 2;
                    break;
            }

            for (int rows = 0; rows < arr.GetLength(0); rows++)
            {
                for (int colms = 0; colms < arr.GetLength(1); colms++)
                {
                    if (rows == rH && colms == cH)
                    {
                        if (arr[rows, colms] != 'x' && arr[rows,colms] != 'o')
                        {
                            arr[rows, colms] = c;
                            return true;
                        }
                        else
                            return false;
                    }               
                }
            }

            return false;
        }

        static void ComputersTurn(char[,] arr, int num, char c)
        {
            int compNum, counterTries = 1;
            do
            {
                compNum = RandomNum();
                while (compNum == num)
                {
                    compNum = RandomNum();
                }
                counterTries++;
            } while (!PlaceMarker(arr, compNum, c) && counterTries <= 9);
        }

        static bool IsWinner(char[,] arr, char charBoth)
        {
            bool winner = false;
            int counterChar;

            // For horizontal:
            for (int r = 0; r < arr.GetLength(0); r++)
            {
                counterChar = 0; // Counter reset.
                for (int c = 0; c < arr.GetLength(1); c++)
                {
                    if (arr[r, c] == charBoth)
                    {
                        counterChar++;
                    }
                }

                if (counterChar == 3)
                {
                    winner = true;
                    return winner;
                }
            }

            // For vertical:
            for (int c = 0; c < arr.GetLength(1); c++)
            {
                counterChar = 0; // Counter reset.
                for (int r = 0; r < arr.GetLength(0); r++)
                {
                    if (arr[r, c] == charBoth)
                    {
                        counterChar++;
                    }
                }

                if (counterChar == 3)
                {
                    winner = true;
                    return winner;
                }
            }

            // For diagonal:
            counterChar = 0;  //Counter reset.
            for (int r = 0; r < arr.GetLength(0); r++)
            {
                for (int c = 0; c < arr.GetLength(1); c++)
                {
                    if (r == c)
                    {
                        if (arr[r, c] == charBoth)
                        {
                            counterChar++;
                        }
                    }
                }
            }

            if (counterChar == 3)
            {
                winner = true;
                return winner;
            }

            // For diagonal-reversed:
            counterChar = 0;  //Counter reset.
            int colmsDegrade = 0;
            for (int r = 0; r < arr.GetLength(0); r++)
            {
                for (int c = 0; c < arr.GetLength(1) - colmsDegrade; c++)
                {
                    if (c == arr.GetLength(1) - colmsDegrade - 1)
                    {
                        if (arr[r, c] == charBoth)
                        {
                            counterChar++;
                        }
                    }
                }
                colmsDegrade++;
            }

            if (counterChar == 3)
            {
                winner = true;
                return winner;
            }

            return winner;
        }

        static void Main(string[] args)
        {
            char[,] field = new char[3, 3];

            WriteLine("Welcome to TicTacToe!");
            WriteLine("---------------------");

            WriteLine("\nStart new game?");

            string? answer = ReadLine();
            if (answer == "yes" || answer == "Yes")
            {             
                string? input;
                do
                {
                    field = new char[3, 3]; //Field reset.
                    WriteLine("\nFirst please choose a symbol between 'x' and 'o'.");
                    WriteLine("(Once you have selected a symbol you can no longer change it until you start a new game!)");
                    DisplayField(field);
                    Write("Symbol: ");
                    char charPlayer, charComp;
                    bool charTest, numTest, winner;
                    int num, winnerDecide = 0;
                    do
                    {
                        input = ReadLine();
                        char.TryParse(input, out charPlayer);
                        if (charPlayer != 'x' && charPlayer != 'o')
                        {
                            charTest = false;
                            WriteLine("Wrong Symbol! Please choose between 'x' and 'o' only!");
                            Write("\nSymbol:");
                        }
                        else
                            charTest = true;
                    } while (!charTest);

                    if (charPlayer == 'x')
                    {
                        charComp = 'o';
                    }
                    else
                        charComp = 'x';

                    WriteLine("And now a number between 1 - 9 please.");
                    do
                    {
                        Write("\nNumber: ");
                        do
                        {
                            do
                            {
                                input = ReadLine();
                                numTest = int.TryParse(input, out num);
                                if (!numTest)
                                {
                                    WriteLine("False input! Please enter a number between 1 and 9 only!");
                                    Write("\nNumber: ");
                                }
                            } while (!numTest);

                            if (num < 1 || num > 9)
                            {
                                numTest = false;
                                WriteLine("False number! Please choose between 1 and 9 only!");
                                Write("\nNumber: ");
                            }
                            else
                                numTest = true;
                        } while (!numTest);

                        while (!PlaceMarker(field, num, charPlayer)) //Mach solange bis feld besetzt ist.
                        {
                            WriteLine("Field slot already taken! Please enter an empty field slot!");
                            Write("\nNumber: ");
                            num = ToInt32(ReadLine());
                        }

                        DisplayField(field);
                        WriteLine("Press any key to continue.");
                        ReadKey();
                        winner = IsWinner(field, charPlayer);
                        Clear();
                        winnerDecide = 1;
                        if (!winner)
                        {
                            WriteLine("Computer's turn:");
                            ComputersTurn(field, num, charComp);
                            DisplayField(field);
                            winner = IsWinner(field, charComp);
                            if (!winner)
                            {
                                WriteLine("Your turn!");
                            }
                            winnerDecide = 0;
                        }
                    } while (!winner);

                    if (winnerDecide == 0)
                    {
                        WriteLine("Computer has won.");
                    }
                    else if (winnerDecide == 1 && winner)
                    {
                        WriteLine("You've won!");
                    }
                    else
                        WriteLine("It's a draw!");
                        
                    WriteLine("\nPlay again?");
                    input = ReadLine();
                } while (input == "yes" || input == "Yes");

                WriteLine("Goodbye!");
                ReadKey();
            }
            else
                WriteLine("Goodbye!");
            ReadKey();
        }
    }
}
