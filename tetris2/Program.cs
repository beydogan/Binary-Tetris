using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
namespace tetris2
{
    class Program
    {
        static string[,] gameArea = new string[22, 15];
        static string[] blockTypes = new string[7] { "01:11:21", "11:12:21:22", "10:11:12:21", "10:11:21:22", "01:11:21:22", "01:11:21:20", "11:12:20:21"}; // block coordinates

        static string[,] block = new string[3, 3];
        static Random rand = new Random();
        static int sleepTime = 300;
        static string pattern = "0000";
        static string patternRev = "";
        static int userScore = 0;
        static int patternVal = 0;

        // ======= High Score ==========
        struct Date
        {
            public int day;
            public int month;
            public int year;
        };

        struct User
        {
            public string name;
            public string pattern;
            public int score;
            public Date time;
        }
        // ======= High Score ==========

        static void highScore()
        {
            DateTime CurrTime = DateTime.Now;
            User us;
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(15, 13);
            Console.Write("                          ");
            Console.SetCursorPosition(15, 14);
            Console.Write("Enter Your Name:          ");
            Console.SetCursorPosition(15, 15);
            Console.Write("                          ");
            Console.SetCursorPosition(33, 14);
            us.name = Console.ReadLine();
            us.pattern = pattern;
            us.score = userScore;

            us.time.day = CurrTime.Day;
            us.time.month = CurrTime.Month;
            us.time.year = CurrTime.Year;

            string[,] scoretable = new string[10, 6];

            StreamReader f2 = File.OpenText(".\\highest.txt");

            int n = 0;

            do
            {
                string satir1 = Convert.ToString(f2.ReadLine());

                string[] result1 = satir1.Split(' ');

                for (int i = 0; i < 6; i++)
                {
                    scoretable[n, i] = result1[i];
                }
                Console.WriteLine();
                n = n + 1;
            } while (!f2.EndOfStream);

            f2.Close();

            if (us.score > Convert.ToInt16(scoretable[9, 5]))
            {
                scoretable[9, 5] = Convert.ToString(us.score);
                scoretable[9, 0] = us.name;
                scoretable[9, 1] = us.pattern;
                scoretable[9, 2] = Convert.ToString(us.time.day);
                scoretable[9, 3] = Convert.ToString(us.time.month);
                scoretable[9, 4] = Convert.ToString(us.time.year);
            }

            int sayi;

            string sayi1;
            string sayi2;
            int sayi3;
            int sayi4;
            int sayi5;
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (Convert.ToInt16(scoretable[i, 5]) > Convert.ToInt16(scoretable[j, 5]))
                    {

                        sayi = Convert.ToInt16(scoretable[i, 5]);
                        (scoretable[i, 5]) = (scoretable[j, 5]);
                        (scoretable[j, 5]) = Convert.ToString(sayi);

                        sayi1 = (scoretable[i, 0]);
                        (scoretable[i, 0]) = (scoretable[j, 0]);
                        (scoretable[j, 0]) = sayi1;

                        sayi2 = (scoretable[i, 1]);
                        (scoretable[i, 1]) = (scoretable[j, 1]);
                        (scoretable[j, 1]) = (sayi2);

                        sayi3 = Convert.ToInt16(scoretable[i, 2]);
                        (scoretable[i, 2]) = (scoretable[j, 2]);
                        (scoretable[j, 2]) = Convert.ToString(sayi3);

                        sayi4 = Convert.ToInt16(scoretable[i, 3]);
                        (scoretable[i, 3]) = (scoretable[j, 3]);
                        (scoretable[j, 3]) = Convert.ToString(sayi4);

                        sayi5 = Convert.ToInt16(scoretable[i, 4]);
                        (scoretable[i, 4]) = (scoretable[j, 4]);
                        (scoretable[j, 4]) = Convert.ToString(sayi5);
                    }
                }
            }

            StreamWriter f = new StreamWriter(".\\highest.txt");
            for (int i = 0; i < 10; i++)
            {
                f.Write(scoretable[i, 0] + " ");
                f.Write(scoretable[i, 1] + " ");
                f.Write(scoretable[i, 2] + " ");
                f.Write(scoretable[i, 3] + " ");
                f.Write(scoretable[i, 4] + " ");
                f.WriteLine(scoretable[i, 5]);
            }
            f.Close();

            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.Red;
            
        }

        public static void writeHighScore()
        {
            Console.Clear();
            
            string[,] scoretable = new string[10, 6];
            StreamReader f2 = File.OpenText(".\\highest.txt");
            int n = 0;

            do
            {
                string satir1 = Convert.ToString(f2.ReadLine());
                string[] result1 = satir1.Split(' ');
                for (int i = 0; i < 6; i++)
                {
                    scoretable[n, i] = result1[i];
                }
                Console.WriteLine();
                n = n + 1;
            } while (!f2.EndOfStream);

            f2.Close();

            Console.SetCursorPosition(9, 4);
            Console.WriteLine("┌───────────────High Scores──────────────┐");
            Console.SetCursorPosition(9, 5);
            Console.Write("│");
            Console.SetCursorPosition(50, 5);
            Console.Write("│");
            Console.SetCursorPosition(9, 6);
            Console.WriteLine("│ #    Name    Pattern     Date     Point│");
            Console.SetCursorPosition(9, 7);
            Console.WriteLine("│---  ------   -------  ----------  -----│");
            //                 10   15       24       33          45
            
            for (int i = 0; i < 10; i++)
            {

                Console.SetCursorPosition(9, 8 + i);
                Console.Write("│");
                Console.SetCursorPosition(50, 8 + i);
                Console.Write("│");
                Console.SetCursorPosition(11, 8+i);
                Console.Write(i + 1);
                Console.SetCursorPosition(15, 8 + i);
                Console.Write(scoretable[i, 0].ToUpper());
                Console.SetCursorPosition(25, 8 + i);
                Console.Write(scoretable[i, 1].ToUpper());
                Console.SetCursorPosition(33, 8 + i);
                Console.Write(scoretable[i, 2] +"."+ scoretable[i, 3] +"."+ scoretable[i, 4]);               
                Console.SetCursorPosition(46, 8 + i);
                Console.Write(scoretable[i, 5].ToUpper());
            }

            Console.SetCursorPosition(9, 18);
            Console.WriteLine("└────────────────────────────────────────┘");
            Console.SetCursorPosition(17, 19);
            Console.WriteLine("Press any Key to Continue");
            Console.ReadKey();
        }
        public static void fillGameArea()
        {
            // This procedure fills game area with #s and spaces.
            for (int i = 0; i < 22; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    if (i == 0)
                        gameArea[i, j] = "#";
                    else if (j == 0 || j == 14)
                        gameArea[i, j] = "#";
                    else if (i == 21)
                        gameArea[i, j] = "#";
                    else
                        gameArea[i, j] = " ";
                }
            }
        }

        public static string[,] fillBlock(int typeNo)
        {
            //filling block with given type no
            string [,] curBlock = new string[3, 3];

            if (typeNo == 7)
            {
                curBlock[1, 1] = "X";
            }
            else
            {
                string[] coordinates;
                coordinates = blockTypes[typeNo].Split(':');

                for (int i = 0; i < coordinates.Length; i++)
                {
                    curBlock[Convert.ToInt32(coordinates[i].Substring(0, 1)), Convert.ToInt32(coordinates[i].Substring(1, 1))] = Convert.ToString(rand.Next(0, 2));
                }
            }
            return curBlock;
        }

        public static void writeAreaBorder()
        {
            //writing game area borders, just #s.
            for (int i = 0; i < 22; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    Console.SetCursorPosition(j, i);
                    if (i == 0)
                        Console.Write(gameArea[i, j]);
                    else if (j == 0 || j == 14)
                        Console.Write(gameArea[i, j]);
                    else if (i == 21)
                        Console.Write(gameArea[i, j]);
                }
                Console.WriteLine();
            }
        }

        public static void writeGameArea()
        {
            for (int i = 1; i < 21; i++)
            {
                for (int j = 1; j < 15; j++)
                {
                    Console.SetCursorPosition(j, i);
                    Console.Write(gameArea[i, j]);
                }
                Console.WriteLine();
            }
        }

        public static void writeBlockToArea(int x, int y)
        {
            /* writing block to given coordinates
             * X,Y is middle point of block
             *
             *         Y
             *   +---+---+---+
             *   |   |   |   |
             *   +---+---+---+
             * X |   | O |   |
             *   +---+---+---+
             *   |   |   |   |
             *   +---+---+---+
            */
            int blockX = 0;
            int blockY = 0;
            for (int i = x-1; i <= x+1; i++)
            {
                for (int j = y-1; j <= y+1; j++)
                {
                    if (block[blockX, blockY] != null)
                    {
                        gameArea[i, j] = block[blockX, blockY];
                    }
                    blockY++;
                }
                blockX++;
                blockY = 0;
            }
        }
       
        public static void clearPrevious(int x, int y)
        {
            x--;
            int blockX = 0;
            int blockY = 0;
            for (int i = x - 1; i <= x + 1; i++)
            {
                for (int j = y - 1; j <= y + 1; j++)
                {
                    if (block[blockX, blockY] != null)
                    {
                        gameArea[i, j] = " ";
                    }
                    blockY++;
                }
                blockX++;
                blockY = 0;
            }
        }

        public static bool checkNextLine(int x, int y)
        {
            //
            bool flag = true;
            int yCoor = y;
            int xCoor = x;
            for (int i = 2; i > -1; i--)
            {
                for (int j = 2; j > -1; j--)
                {
                    if (block[i, j] != null)
                        {                           
                                if (j == 0)
                                    yCoor--;
                                if (j == 2) 
                                    yCoor++;

                                if (i == 2)
                                    xCoor += 2;
                                if (i == 1)
                                    xCoor++;

                                if (gameArea[xCoor, yCoor] != " ")
                                {
                                    if (i < 2 && block[i + 1, j] != null)
                                    {

                                    }
                                    else
                                    {
                                        flag = false;
                                    }
                                }

                                if(gameArea[xCoor, yCoor] == "#")
                                {
                                    if (i < 2 && block[i + 1, j] != null)
                                    {

                                    }
                                    else
                                    {
                                        flag = false;
                                    }
                                }
                            
                        }
                    yCoor = y;
                    xCoor = x;
                }
            }


            return flag;
        }

        public static bool checkRight(int x, int y)
        {
            
            bool flag = true;
            int yCoor = y;
            int xCoor = x;
            for (int i = 2; i > -1; i--)
            {
                for (int j = 2; j > -1; j--)
                {
                    if (block[i, j] != null)
                    {
                        if (j == 1)                            
                            yCoor++;

                        if (j == 2)
                            yCoor += 2;                            

                        if (i == 2)
                            xCoor++;
                        if (i == 0)
                            xCoor--;

                        if (gameArea[xCoor, yCoor] != " ")
                        {
                            if (j < 2 && block[i, j + 1 ] != null)
                            {

                            }
                            else
                            {
                                flag = false;
                            }
                        }

                    }
                    yCoor = y;
                    xCoor = x;
                }
            }

            return flag;

        }

        public static bool checkLeft(int x, int y)
        {

            bool flag = true;
            int yCoor = y;
            int xCoor = x;
            for (int i = 2; i > -1; i--)
            {
                for (int j = 2; j > -1; j--)
                {
                    if (block[i, j] != null)
                    {
                        if (j == 1)
                            yCoor--;

                        if (j == 0)
                            yCoor -= 2;

                        if (i == 2)
                            xCoor++;
                        if (i == 0)
                            xCoor--;

                        if (gameArea[xCoor, yCoor] != " ")
                        {
                            if (j > 0 && block[i, j - 1] != null)
                            {

                            }
                            else
                            {
                                flag = false;
                            }
                        }
                    }
                    yCoor = y;
                    xCoor = x;
                }
            }

            return flag;

        }

        public static void patternSrcHor(string[,] gameArea) // searching game area horizontal
        {

            string line = "";
            string[] gameAreaLines = new string[20];
            for (int i = 1; i < 21; i++)
            {
                line = "";
                for (int j = 1; j < 14; j++)
                {
                    line += gameArea[i, j]; // adding game area char by char to string "line"
                }
                gameAreaLines[i - 1] = line; // then adding string "line" to an array
            }

            int startPoint = -1;
            for (int j = 0; j < 20; j++)
            {
                if (gameAreaLines[j].Contains("1111111111111") || gameAreaLines[j].Contains("0000000000000"))
                {
                    for (int i = 1; i < 14; i++)
                    {
                        int row = j;

                        Thread.Sleep(100);
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.SetCursorPosition(i, row + 1);
                        Console.Write(gameArea[row + 1, i]);
                        Console.ResetColor();
                        
                        gameArea[row + 1, i] = " ";
                        while (row + 1 > 0 && gameArea[row, i] != " ")
                        {
                            gameArea[row + 1, i] = gameArea[row, i];
                            Console.SetCursorPosition(i, row + 1);
                            Console.Write(gameArea[row + 1, i]);
                            row--;
                        }

                        gameArea[row + 1, i] = " ";
                        Console.SetCursorPosition(i, row + 1);
                        Console.Write(gameArea[row + 1, i]);
                    }
                }
                if (gameAreaLines[j].Contains(pattern))
                {
                    startPoint = gameAreaLines[j].IndexOf(pattern);
                    patternDelHor(startPoint, j);
                    break;
                }

            }
            /*
            if (patternRev != pattern)
            {
                for (int j = 0; j < 20; j++)
                {
                    if (gameAreaLines[j].Contains(patternRev))
                    {
                        startPoint = gameAreaLines[j].IndexOf(patternRev);
                        patternDelHor(startPoint, j); // j is row
                        break;
                    }

                }
            }
            */
        }

        public static void patternSrcHorRev(string[,] gameArea) // searching game area horizontal and for reverse patter
        {
            string line = "";
            string[] gameAreaLines = new string[20];
            for (int i = 1; i < 21; i++)
            {
                line = "";
                for (int j = 1; j < 14; j++)
                {
                    line += gameArea[i, j]; // adding game area char by char to string "line"
                }
                gameAreaLines[i - 1] = line; // then adding string "line" to an array
            }

            int startPoint = -1;
            for (int j = 0; j < 20; j++)
            {
                if (gameAreaLines[j].Contains(patternRev))
                {
                    startPoint = gameAreaLines[j].IndexOf(patternRev);
                    patternDelHor(startPoint, j);
                    break;
                }

            }
        }

        public static void patternSrcVer() // searching game area vertical
        {
            string column = "";
            string[] gameAreaCols = new string[13];
            for (int i = 1; i < 14; i++)
            {
                column = "";
                for (int j = 1; j < 21; j++)
                {
                    column += gameArea[j, i];
                }
                gameAreaCols[i - 1] = column;
            }

            int startPoint = -1;
            for (int j = 0; j < 13; j++)
            {
                if (gameAreaCols[j].Contains(pattern))
                {
                    startPoint = gameAreaCols[j].IndexOf(pattern);
                    patternDelVer(startPoint, j); // j is column
                    break;
                }
            }

            if (patternRev != pattern)
            {
                for (int j = 0; j < 13; j++)
                {
                    if (gameAreaCols[j].Contains(patternRev))
                    {
                        startPoint = gameAreaCols[j].IndexOf(patternRev);
                        patternDelVer(startPoint, j); // j is column
                        break;
                    }
                }
            }

        }

        public static void patternDelVer(int startPoint, int column)
        {
            userScore += patternVal;
            gameArea[startPoint + 1, column +1] = " ";
            gameArea[startPoint + 2, column +1] = " ";
            gameArea[startPoint + 3, column +1] = " ";
            gameArea[startPoint + 4, column +1] = " ";

            Thread.Sleep(100);
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.SetCursorPosition(column + 1, startPoint + 1);
            Console.Write(gameArea[startPoint + 1, column + 1]);

            Thread.Sleep(100);
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.SetCursorPosition(column + 1, startPoint + 2);
            Console.Write(gameArea[startPoint + 2, column + 1]);

            Thread.Sleep(100);
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.SetCursorPosition(column + 1, startPoint + 3);
            Console.Write(gameArea[startPoint + 3, column + 1]);

            Thread.Sleep(100);
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.SetCursorPosition(column + 1, startPoint + 4);
            Console.Write(gameArea[startPoint + 4, column + 1]);
            Console.ResetColor();
          
            
            for (int k = startPoint + 4; k > 4; k--)
            {
                gameArea[k, column +1] = gameArea[k - 4, column +1];
            }
            
            patternSrcVer();


        }

        public static void patternDelHor(int startPoint, int row)
        {
            gameArea[row + 1 ,startPoint + 1] = " ";
            gameArea[row + 1, startPoint + 2] = " ";
            gameArea[row + 1, startPoint + 3] = " ";
            gameArea[row + 1, startPoint + 4] = " ";
            userScore += patternVal;

            for (int i = 1; i <= 4; i++)
            {
                Thread.Sleep(100);
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.SetCursorPosition(startPoint + i, row + 1);
                Console.Write(gameArea[row + 1, startPoint + i]);
                Console.ResetColor();
            }
           
            int temp = row;
            while (row + 1 > 0 && gameArea[row, startPoint + 1] != " ")
            {
                gameArea[row + 1, startPoint + 1] = gameArea[row, startPoint + 1];
                Console.SetCursorPosition(startPoint + 1, row + 1);
                Console.Write(gameArea[row + 1, startPoint + 1]);
                row--;
            }

            gameArea[row+1, startPoint + 1] = " ";
            Console.SetCursorPosition(startPoint + 1, row + 1);
            Console.Write(gameArea[row + 1, startPoint + 1]);

            row = temp;
            while (row + 1 > 0 && gameArea[row, startPoint + 2] != " ")
            {
                gameArea[row + 1, startPoint + 2] = gameArea[row, startPoint + 2];
                Console.SetCursorPosition(startPoint + 2, row + 1);
                Console.Write(gameArea[row + 1, startPoint + 2]);
                row--;
            }

            gameArea[row + 1, startPoint + 2] = " ";
            Console.SetCursorPosition(startPoint + 2, row + 1);
            Console.Write(gameArea[row + 1, startPoint + 2]);

            row = temp;
            while (row + 1 > 0 && gameArea[row, startPoint + 3] != " ")
            {
                gameArea[row + 1, startPoint + 3] = gameArea[row, startPoint + 3];
                Console.SetCursorPosition(startPoint + 3, row + 1);
                Console.Write(gameArea[row + 1, startPoint + 3]);
                row--;
            }

            gameArea[row + 1, startPoint + 3] = " ";
            Console.SetCursorPosition(startPoint + 3, row + 1);
            Console.Write(gameArea[row + 1, startPoint + 3]);

            row = temp;
            while (row + 1 > 0 && gameArea[row, startPoint + 4] != " ")
            {
                gameArea[row + 1, startPoint + 4] = gameArea[row, startPoint + 4];
                Console.SetCursorPosition(startPoint + 4, row + 1);
                Console.Write(gameArea[row + 1, startPoint + 4]);
                row--;
            }

            gameArea[row + 1, startPoint + 4] = " ";
            Console.SetCursorPosition(startPoint + 4, row + 1);
            Console.Write(gameArea[row + 1, startPoint + 4]);

          
            /*
            for (int k = row + 1; k > 1; k--)
            {

                gameArea[k, startPoint + 1] = gameArea[k - 1, startPoint + 1];
                Console.SetCursorPosition(startPoint + 1, k);
                Console.Write(gameArea[k, startPoint + 1]);

                gameArea[k, startPoint + 2] = gameArea[k - 1, startPoint + 2];
                Console.SetCursorPosition(startPoint + 2, k);
                Console.Write(gameArea[k, startPoint + 2]);

                gameArea[k, startPoint + 3] = gameArea[k - 1, startPoint + 3];
                Console.SetCursorPosition(startPoint + 3, k);
                Console.Write(gameArea[k, startPoint + 3]);

                gameArea[k, startPoint + 4] = gameArea[k - 1, startPoint + 4];
                Console.SetCursorPosition(startPoint + 3, k);
                Console.Write(gameArea[k, startPoint + 3]);
            }
            */
            patternSrcHor(gameArea);

            // can be put recursive here. patternSrcHor() ??
        }

        public static string reversePattern(string pattern)
        {
            string patternRev = "";
            for (int i = 0; i < 4; i++)
            {
                patternRev += pattern.Substring(3 - i,1);
            }
            return patternRev;
        }

        public static string[,] rotateBlock(string[,] curBlock, int typeNo, int X, int Y) // takes current block, type no and position
        {
            string[,] newBlock = new string[3, 3];
         
            string coords = "";

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (block[i, j] != null)
                    {
                        coords += i.ToString() + j.ToString() + ":";
                    }
                }
            }

            string[] curCoordinates;
            coords = coords.Remove(coords.Length - 1, 1);
            curCoordinates = coords.Split(':');
            string[] newCoordinates = new string[curCoordinates.Length];


            if (typeNo != 1)
            {
                for (int i = 0; i < curCoordinates.Length; i++)
                {
                    switch (curCoordinates[i])
                    {
                        case "10":
                            newCoordinates[i] = "01";
                            break;
                        case "01":
                            newCoordinates[i] = "12";
                            break;
                        case "21":
                            newCoordinates[i] = "10";
                            break;
                        case "12":
                            newCoordinates[i] = "21";
                            break;
                        case "00":
                            newCoordinates[i] = "02";
                            break;
                        case "02":
                            newCoordinates[i] = "22";
                            break;
                        case "11":
                            newCoordinates[i] = "11";
                            break;
                        case "22":
                            newCoordinates[i] = "20";
                            break;
                        case "20":
                            newCoordinates[i] = "00";
                            break;
                    }
                }
            }
            else
            {
                for (int i = 0; i < curCoordinates.Length; i++)
                {
                    switch (curCoordinates[i])
                    {
                        case "11":
                            newCoordinates[i] = "12";
                            break;
                        case "12":
                            newCoordinates[i] = "22";
                            break;
                        case "21":
                            newCoordinates[i] = "11";
                            break;
                        case "22":
                            newCoordinates[i] = "21";
                            break;
                    }
                }
            }


            for (int i = 0; i < curCoordinates.Length; i++)
            {
                newBlock[Convert.ToInt32(newCoordinates[i].Substring(0, 1)), Convert.ToInt32(newCoordinates[i].Substring(1, 1))] = curBlock[Convert.ToInt32(curCoordinates[i].Substring(0, 1)), Convert.ToInt32(curCoordinates[i].Substring(1, 1))];
            }
            if (block[0, 1] != null)
            {
                if (gameArea[X, Y + 1] != " ")
                {
                    newBlock = block;
                }
            }
            if (block[0, 2] != null)
            {
                if (gameArea[X + 1, Y + 1] != " ")
                {
                    newBlock = block;
                }
            }
            if (block[0, 0] != null)
            {
                if (gameArea[X - 1, Y + 1] != " ")
                {
                    newBlock = block;
                }
            }

            if (block[2, 0] != null)
            {
                if (gameArea[X - 1, Y - 1 ] != " ")
                {
                    newBlock = block;
                }
            }         
            if (block[2, 1] != null)
            {
                if (gameArea[X, Y -1 ] != " ")
                {
                    newBlock = block;
                }
            }
            if (block[2, 2] != null)
            {
                if (gameArea[X + 1, Y-1] != " ")
                {
                    newBlock = block;
                }
            }

            if (block[1, 2] != null)
            {
                if (gameArea[X + 1, Y] != " ")
                {
                    newBlock = block;
                }
            }

            if (block[0, 2] != null)
            {
                if (gameArea[X + 1, Y + 1] != " ")
                {
                    newBlock = block;
                }
            } 
            return newBlock;
        }

        public static void writeNextBlock(string[,] nextBlock)
        {
            Console.SetCursorPosition(22, 1);
            Console.Write("Next Block");
            Console.SetCursorPosition(24, 2);
            Console.Write("┌───┐");
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Console.SetCursorPosition(24, 3 + i);
                    Console.Write("│");
                    Console.SetCursorPosition(28, 3 + i);
                    Console.Write("│");
                    Console.SetCursorPosition(25+j, 3+i);
                    if (nextBlock[i, j] == null)
                    {
                        Console.Write(" ");
                    }
                    else
                    {
                        Console.Write(nextBlock[i, j]);
                    }
                }
            }
            Console.SetCursorPosition(24, 6);
            Console.Write("└───┘");

        }

        public static void writeUserScore(int userScore)
        {
            Console.SetCursorPosition(22, 8);            
            Console.Write("Your Score");
            Console.SetCursorPosition(23, 9);
            Console.Write("┌──────┐");
            Console.SetCursorPosition(23, 10);
            Console.Write("│");
            Console.SetCursorPosition(30, 10);
            Console.Write("│");
            Console.SetCursorPosition(23, 11);
            Console.Write("└──────┘");
            Console.SetCursorPosition(24, 10);
            Console.Write(userScore);

        }

        public static void writePattern()
        {
            Console.SetCursorPosition(21, 13);
            Console.Write("Your Pattern");
            Console.SetCursorPosition(24, 14);
            Console.Write("┌────┐");
            Console.SetCursorPosition(24, 15);
            Console.Write("│");
            Console.SetCursorPosition(29, 15);
            Console.Write("│");
            Console.SetCursorPosition(24, 16);
            Console.Write("└────┘");
            Console.SetCursorPosition(25, 15);
            Console.Write(pattern);

        }

        public static void writeTimePlayed(DateTime start)
        {
            DateTime simdi = DateTime.Now;
            TimeSpan sonuc = simdi - start;

            Console.SetCursorPosition(21, 18);
            Console.Write("Time Played");
            Console.SetCursorPosition(23, 19);
            Console.Write("┌─────┐");
            Console.SetCursorPosition(23, 20);
            Console.Write("│");
            Console.SetCursorPosition(29, 20);
            Console.Write("│");
            Console.SetCursorPosition(23, 21);
            Console.Write("└─────┘");
            Console.SetCursorPosition(24, 20);
            Console.Write("     ");   
            Console.SetCursorPosition(24, 20);
            Console.Write(sonuc.Minutes + ":" +sonuc.Seconds);            

        }

        public static int calculateValue(string pattern)
        {
            int patternValue = 0 ;
            char[] patternChar = pattern.ToCharArray();
            for (int i = 0; i < 4; i++)
            {
                patternValue += Convert.ToInt32(patternChar[3 - i].ToString()) * Convert.ToInt32(Math.Pow(2.0, Convert.ToDouble(i)));
            }
            return patternValue;
        }

        public static void wait()
        {
            Console.ReadLine();
        }

        static void Main(string[] args)
        {
            Console.SetWindowSize(1, 1);
            Console.SetBufferSize(60, 23);
            Console.SetWindowSize(60, 23);            
            Console.CursorVisible = false;
            Console.Title = "Binary Tetris";            

            ConsoleKeyInfo key;
         
            int choice;
            choice = 0;
            short c;
            string[] menuItems = { "Play Game", "High Scores", "Help", "Exit" };
            while (true)
            {
                // Starting Menu
                do
                {
                    Console.BackgroundColor = ConsoleColor.DarkRed; // bgcolor of all screen
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine(" ____  _                          _______   _        _     ");
                    Console.WriteLine("|  _ \\(_)                        |__   __| | |      (_)    ");
                    Console.WriteLine("| |_) |_ _ __   __ _ _ __ _   _     | | ___| |_ _ __ _ ___ ");
                    Console.WriteLine("|  _ <| | '_ \\ / _` | '__| | | |    | |/ _ \\ __| '__| / __|");
                    Console.WriteLine("| |_) | | | | | (_| | |  | |_| |    | |  __/ |_| |  | \\__ \\");
                    Console.WriteLine("|____/|_|_| |_|\\__,_|_|   \\__, |    |_|\\___|\\__|_|  |_|___/");
                    Console.WriteLine("                           __/ |                           ");
                    Console.WriteLine("                          |___/                            ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.SetCursorPosition(20, 8);                    
                    Console.WriteLine("┌───── Menu ─────┐");
                    
                   
                    Console.ResetColor();
                    for (c = 0; c < menuItems.Length; c++)
                    {
                        if (choice == c)
                        {
                            Console.BackgroundColor = ConsoleColor.DarkRed;
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.CursorLeft = 20;
                            Console.Write("│");
                            Console.ForegroundColor = ConsoleColor.Gray;
                            Console.BackgroundColor = ConsoleColor.DarkRed;
                            Console.Write(">>");
                            Console.Write(menuItems[c].PadRight(14));
                            Console.BackgroundColor = ConsoleColor.DarkRed;
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("│");                            
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.BackgroundColor = ConsoleColor.DarkRed;
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.CursorLeft = 20;
                            Console.WriteLine("│" + menuItems[c].PadRight(16) + "│");
                            Console.ResetColor();
                        }
                    }

                    Console.BackgroundColor = ConsoleColor.DarkRed;
                    Console.ForegroundColor = ConsoleColor.White;                    
                    Console.CursorLeft = 20;
                    Console.WriteLine("└────────────────┘");
                   
                    key = Console.ReadKey(true);
                    if (key.Key.ToString() == "DownArrow")
                    {
                        choice++;
                        if (choice > menuItems.Length - 1) choice = 0;
                    }
                    else if (key.Key.ToString() == "UpArrow")
                    {
                        choice--;
                        if (choice < 0) choice = Convert.ToInt16(menuItems.Length - 1);
                    }
                } while (key.KeyChar != 13);
                // End of Menu


                //Play Game
                if (choice == 0)
                {
                    Console.ResetColor();
                    Console.Clear();
                    userScore = 0;
                    Console.BackgroundColor = ConsoleColor.DarkRed;
                    Console.Clear();
                    bool patternFlag = false;
                    int patternLength = -1;
                    while (patternLength != 4 | !patternFlag)
                    {
                        Console.SetCursorPosition(20, 8);
                        Console.BackgroundColor = ConsoleColor.DarkRed;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("┌──────────────┐");
                        Console.SetCursorPosition(20, 9);
                        Console.WriteLine("│Enter  Pattern│");
                        Console.SetCursorPosition(20, 10);
                        Console.WriteLine("│              │");
                        Console.SetCursorPosition(20, 11);
                        Console.WriteLine("│              │");
                        Console.SetCursorPosition(20, 12);
                        Console.WriteLine("└──────────────┘");
                        Console.SetCursorPosition(26, 11);
                        Console.CursorVisible = true;
                        string patternStr = Console.ReadLine();
                        Console.CursorVisible = false;
                        patternLength = patternStr.Length;

                        for (int i = 0; i < patternLength; i++)
                        {
                            if (patternStr.Substring(i, 1) != "0" && patternStr.Substring(i, 1) != "1")
                            {
                                patternFlag = false;
                                break;
                            }
                            else
                            {
                                patternFlag = true;
                                pattern = patternStr;
                            }
                        }
                    }

                    patternVal = calculateValue(pattern);
                    Console.ResetColor();
                    Console.Clear();

                    fillGameArea();
                    int typeNo = 0, nextTypeNo = 0 ;
                    int X = 2;
                    int Y = 7;

                    string[,] nextBlock = new string[3, 3];

                    typeNo = rand.Next(0, blockTypes.Length +1);
                    nextTypeNo = rand.Next(0, blockTypes.Length +1);
                    block = fillBlock(typeNo);
                    nextBlock = fillBlock(nextTypeNo);

                    writeAreaBorder();
                    writePattern();
                    patternRev = reversePattern(pattern); // reversing pattern
                    DateTime startTime = DateTime.Now;

                    while (X < 22)
                    {                       
                        bool nextLineFlag = checkNextLine(X - 1, Y);
                        if (!nextLineFlag)
                        {
                            if (typeNo == 7)
                            {
                                if (Y != 1)
                                {
                                    gameArea[X - 2, Y - 1] = " ";
                                    gameArea[X - 1, Y - 1] = " ";
                                }

                                if (Y != 13)
                                {
                                    gameArea[X - 2, Y + 1] = " ";
                                    gameArea[X - 1, Y + 1] = " ";
                                }

                                gameArea[X - 1, Y] = " ";

                                gameArea[X - 2, Y] = " ";
                                if (X != 21)
                                {
                                    if (Y != 1)
                                    {
                                        gameArea[X, Y - 1] = " ";
                                    }
                                    gameArea[X, Y] = " ";
                                    if (Y != 13)
                                    {
                                        gameArea[X, Y + 1] = " ";
                                    }
                                }
                                if (X != 21)
                                {
                                    for (int k = X; k > 4; k--)
                                    {
                                        gameArea[k, Y - 1] = gameArea[k - 3, Y - 1];
                                    }
                                    for (int k = X; k > 4; k--)
                                    {
                                        gameArea[k, Y] = gameArea[k - 3, Y];
                                    }
                                    for (int k = X; k > 4; k--)
                                    {
                                        gameArea[k, Y + 1] = gameArea[k - 3, Y + 1];
                                    }
                                }
                            }
                            sleepTime = 350;
                            patternSrcHor(gameArea);
                            if (patternRev != pattern)
                            {
                                patternSrcHorRev(gameArea);
                            }
                            patternSrcVer();
                            X = 2;
                            Y = 7;

                            typeNo = nextTypeNo;
                            block = nextBlock;
                            nextTypeNo = rand.Next(0, blockTypes.Length +1);
                            nextBlock = fillBlock(nextTypeNo);
                            if (gameArea[2, 7] != " ")
                            {
                                break;
                            }

                            while (Console.KeyAvailable) Console.ReadKey(true);
                        }
                        writeUserScore(userScore);
                        writeNextBlock(nextBlock);
                        writeTimePlayed(startTime);

                        clearPrevious(X, Y);
                        writeBlockToArea(X, Y);
                        writeGameArea();

                        if (Console.KeyAvailable)
                        {
                            key = Console.ReadKey(true);
                            if (key.Key== ConsoleKey.LeftArrow && checkLeft(X, Y))
                            {
                                Y--;
                                clearPrevious(X + 1, Y + 1);
                                writeBlockToArea(X, Y);
                            }
                            if (key.Key== ConsoleKey.RightArrow && checkRight(X, Y))
                            {
                                Y++;
                                clearPrevious(X + 1, Y - 1);
                                writeBlockToArea(X, Y);
                            }
                            if (key.Key== ConsoleKey.DownArrow)
                            {
                                sleepTime = 100;
                            }

                            if (key.Key == ConsoleKey.UpArrow)
                            {

                                clearPrevious(X + 1, Y);
                                block = rotateBlock(block, typeNo, X,Y);
                                writeBlockToArea(X, Y);

                            }

                            if (key.Key== ConsoleKey.P)
                            {
                                Console.SetCursorPosition(2, 9);
                                Console.BackgroundColor = ConsoleColor.DarkRed;
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.WriteLine("Game Paused");
                                Console.ResetColor();
                                Console.ReadKey(true);
                            }

                            while (Console.KeyAvailable) Console.ReadKey(true);
                        }
                        Thread.Sleep(sleepTime/2);
                        X++;
                    }
                    highScore();
                }
                //End Of Play Game

                // High Scores
                if (choice == 1)
                {
                    writeHighScore();
                }

                // Help
                if (choice == 2)
                {
                    Console.Clear();
                    Console.SetCursorPosition(0,5);
                    Console.WriteLine("┌───────────────────────Help Content──────────────────────┐");
                    for (int i = 0; i < 12; i++)
                    {
                        Console.SetCursorPosition(0, 6+i);
                        Console.Write("│");
                        Console.SetCursorPosition(58, 6+i);
                        Console.Write("│");
                    }
                    Console.SetCursorPosition(3, 6);
                    Console.WriteLine("Keys:");
                    Console.SetCursorPosition(3, 7);                    
                    Console.WriteLine("  Left-Right Arrow: To move the block to right and left");
                    Console.SetCursorPosition(3, 8);
                    Console.WriteLine("  Down Arrow: To fall the block quickly");
                    Console.SetCursorPosition(3, 9);
                    Console.WriteLine("  Up Arrow: To rotate the block");
                    Console.SetCursorPosition(3, 10);
                    Console.WriteLine("  P: To pause the game");
                    Console.SetCursorPosition(0, 18);
                    Console.WriteLine("└─────────────────────────────────────────────────────────┘");
                    Console.WriteLine("Press any Key to Continue");
                    Console.ReadKey();
                }

                // Exit
                if (choice == 3)
                {
                    break;
                }
            }
        }
    }
}
