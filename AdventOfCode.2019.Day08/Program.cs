using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode._2019.Day08
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = File.ReadAllText("input.txt");
            const int pictureWidth = 25;
            const int pictureHeight = 6;

            //string input = "123456789012";
            //const int pictureWidth = 3;
            //const int pictureHeight = 2;

            //string input = "0222112222120000";
            //const int pictureWidth = 2;
            //const int pictureHeight = 2;

            Image image = Image.Load(input, pictureWidth, pictureHeight);

            int checksum = image.CalculateChecksum();
            Console.WriteLine(checksum);

            Layer renderedLayer = image.Render();
            renderedLayer.Render(Console.Out);
        }                
    }
}
