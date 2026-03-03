

using System.Collections.Specialized;

namespace GameOfLife
{
    class Helper
    {
        public static char[,] FillArray()
        {
            int height = Console.WindowHeight - 1;
            int width = Console.WindowWidth;
            char[,] landscape = new char[height, width];

            for (int y = 0; y < height; y++)
            {

                for (int x = 0; x < width; x++)
                {
                    int random = Random.Shared.Next(100);
                    landscape[y, x] = (random < 30) ? '#' : ' ';
                }
            }
            return landscape;
        }
        public static char[,] NextGeneration(char[,] current)
        {
            int height = current.GetLength(0);
            int width = current.GetLength(1);
            char[,] nextGeneration = new char[height, width];
            int[] dy = { -1, -1, -1, 0, 0, 1, 1, 1 };
            int[] dx = { -1, 0, 1, -1, 1, -1, 0, 1 };
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int countOfLive = 0;
                    for (int z = 0; z < dy.Length; z++)
                    {
                        int ny = y + dy[z];
                        int nx = x + dx[z];
                        if (ny < 0 || ny >= height || nx < 0 || nx >= width)
                            continue;
                        else if (current[ny, nx] == '#')
                        {
                            countOfLive++;
                        }

                    }
                    if (current[y, x] == '#' && (countOfLive == 0 || countOfLive == 1))//dies
                        nextGeneration[y, x] = ' ';
                    else if (current[y, x] == '#' && countOfLive >= 4)//overpopulation.
                        nextGeneration[y, x] = ' ';
                    else if (current[y, x] == '#' && (countOfLive == 2 || countOfLive == 3))//survives
                        nextGeneration[y, x] = '#';
                    else if (current[y, x] == ' ' && countOfLive == 3)//populated
                        nextGeneration[y, x] = '#';
                    else
                        nextGeneration[y, x] = current[y, x];

                }
            }
            return nextGeneration;
        }
        public static void Draw(char[,] current)
        {
            Console.SetCursorPosition(0, 0);
            for (int y = 0; y < current.GetLength(0); y++)
            {
                
                for (int x = 0; x < current.GetLength(1); x++)
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.SetCursorPosition(x,y);
                    Console.Write(current[y, x]);
                }
            }
    }
        class Program
        {
            static void Main(string[] args)
            {
                Console.Clear();
                char[,] current = Helper.FillArray();
                Console.BackgroundColor = ConsoleColor.Black;
                while (!Console.KeyAvailable)
                {
                    
                    Helper.Draw(current);
                    current = Helper.NextGeneration(current);
                    Thread.Sleep(100);

                }
            }
        }
    }

}   
