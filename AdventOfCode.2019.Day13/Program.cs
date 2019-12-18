using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2019.Day13
{
    class Program
    {
        enum TileType { Empty = 0, Wall = 1, Block = 2, HorizontalPaddle = 3, Ball = 4 }

        static void Main(string[] args)
        {
            long[] inputProgram = InputReader.Read();

            Func<long?> inputReader = () => throw new NotSupportedException();

            var outputs = new Queue<long>(3);
            var tiles = new Dictionary<long, Dictionary<long, TileType>>();

            Action<long> outputWriter = output =>
            {
                outputs.Enqueue(output);

                if (outputs.Count == 3)
                {
                    long posX = outputs.Dequeue();
                    long posY = outputs.Dequeue();
                    TileType tileType = (TileType)outputs.Dequeue();

                    if (!tiles.ContainsKey(posX))
                    {
                        tiles[posX] = new Dictionary<long, TileType>();
                    }

                    tiles[posX][posY] = tileType;
                }
            };

            var gameProgram = new IntcodeProgram(inputProgram, inputReader, outputWriter);
            gameProgram.Execute();

            if (!gameProgram.Halted)
            {
                throw new InvalidOperationException();
            }

            Console.WriteLine(tiles.Values.SelectMany(py => py.Values).Count(t => t == TileType.Block));
        }
    }
}
