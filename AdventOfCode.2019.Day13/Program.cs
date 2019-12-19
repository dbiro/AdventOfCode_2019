using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace AdventOfCode._2019.Day13
{
    class Program
    {
        enum TileType { Empty = 0, Wall = 1, Block = 2, HorizontalPaddle = 3, Ball = 4 }

        static char PrintTileType(TileType tileType)
        {
            switch (tileType)
            {
                case TileType.Empty:
                    return ' ';
                case TileType.Wall:
                    return 'W';
                case TileType.Block:
                    return 'B';
                case TileType.HorizontalPaddle:
                    return 'P';
                case TileType.Ball:
                    return 'O';
                default:
                    throw new ArgumentException();
            }
        }
                
        static void Main(string[] args)
        {
            long[] inputProgram = InputReader.Read();
            inputProgram[0] = 2;
                        
            long userScore = 0;

            var outputs = new Queue<long>(3);
            var tiles = new Dictionary<long, Dictionary<long, TileType>>();

            Func<long?> inputReader = () =>
            {                
                Thread.Sleep(50);
                return 0;
            };            

            Action<long> outputWriter = output =>
            {
                outputs.Enqueue(output);

                if (outputs.Count == 3)
                {
                    long posX = outputs.Dequeue();
                    long posY = outputs.Dequeue();

                    if (posX == -1 && posY == 0)
                    {
                        userScore = outputs.Dequeue();
                        Console.SetCursorPosition(0, (int)tiles.Values.SelectMany(py => py.Keys).Max() + 1);
                        
                        Console.Write($"Score: {userScore}");
                    }
                    else
                    {
                        TileType tileType = (TileType)outputs.Dequeue();

                        if (!tiles.ContainsKey(posX))
                        {
                            tiles[posX] = new Dictionary<long, TileType>();
                        }

                        tiles[posX][posY] = tileType;

                        Console.SetCursorPosition((int)posX, (int)posY);
                        Console.Write(PrintTileType(tileType));                                                
                    }                    
                }
            };

            var gameProgram = new IntcodeProgram(inputProgram, inputReader, outputWriter);
            gameProgram.Execute();                        

            if (!gameProgram.Halted)
            {
                throw new InvalidOperationException();
            }

            //Console.WriteLine(tiles.Values.SelectMany(py => py.Values).Count(t => t == TileType.Block));
        }
    }
}
