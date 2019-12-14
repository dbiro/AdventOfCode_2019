using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2019.Day08
{
    class Layer: IEnumerable<char>
    {
        public const char BlackDigit = '0';
        public const char WhiteDigit = '1';
        public const char TransparentDigit = '2';

        private readonly List<char> _digits;
        private readonly int width;
        private readonly int height;

        public int NumberOfZeroDigits { get; private set; }

        public int NumberOfOneDigits { get; private set; }

        public int NumberOfTwoDigits { get; private set; }

        public Layer(int width, int height, IEnumerable<char> digits)
        {
            var inputDigits = digits.ToList();
            if (width * height != inputDigits.Count)
            {
                throw new ArgumentException();
            }

            this.width = width;
            this.height = height;

            _digits = new List<char>(inputDigits.Count);
            AddDigits(inputDigits);
        }

        private void AddDigits(IEnumerable<char> digits)
        {
            foreach (var d in digits)
            {
                AddDigit(d);
            }
        }

        private void AddDigit(char digit)
        {
            switch (digit)
            {
                case '0':
                    NumberOfZeroDigits++;
                    break;
                case '1':
                    NumberOfOneDigits++;
                    break;
                case '2':
                    NumberOfTwoDigits++;
                    break;
                default:
                    throw new ArgumentException($"Invalid digit: {digit}");
            }

            _digits.Add(digit);
        }

        public IEnumerable<string> Render()
        {
            for (int j = 0; j < height; j++)
            {
                char[] rowDigits = _digits.Skip(j * width).Take(width).ToArray();
                string row = new string(rowDigits);
                string formattedRow = row.Replace(BlackDigit, ' ').Replace(WhiteDigit, '#').Replace(TransparentDigit, ' ');
                yield return formattedRow;
            }
        }
                
        public char this[int index] => _digits[index];

        public IEnumerator<char> GetEnumerator()
        {
            return _digits.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _digits.GetEnumerator();
        }
    }
}
