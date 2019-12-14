using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2019.Day08
{
    class Image : IEnumerable<Layer>
    {        
        private readonly List<Layer> layers;
        private readonly int width;
        private readonly int height;

        public Image(int width, int height)
        {
            this.width = width;
            this.height = height;
            layers = new List<Layer>();
        }

        public void AddLayer(IEnumerable<char> digits)
        {
            layers.Add(new Layer(width, height, digits));
        }

        public static Image Load(string input, int width, int height)
        {
            var picture = new Image(width, height);

            int i = 0;
            while (i < input.Length)
            {
                List<char> newLayerDigits = new List<char>(width * height);

                for (int j = 0; j < height; j++)
                {
                    int to = i + width;
                    for (; i < to; i++)
                    {
                        newLayerDigits.Add(input[i]);
                    }
                }

                picture.AddLayer(newLayerDigits);
            }

            return picture;
        }

        public IEnumerator<Layer> GetEnumerator()
        {
            return layers.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return layers.GetEnumerator();
        }

        public Layer Render()
        {
            // create empty (dont care) layer
            char[] renderedLayerDigits = Enumerable.Repeat('-', width * height).ToArray();

            for (int j = 0; j < height; j++)
            {
                for (int i = j * width; i < j * width + width; i++)
                {
                    foreach (var layer in layers)
                    {
                        if (renderedLayerDigits[i] == Layer.BlackDigit || renderedLayerDigits[i] == Layer.WhiteDigit)
                        {
                            break;
                        }
                        else
                        {
                            renderedLayerDigits[i] = layer[i];
                        }
                    }
                }
            }

            return new Layer(width, height, renderedLayerDigits);
        }
    }
}
