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
               
        private Layer FindLayerWithFewestZeroDigits()
        {
            Layer foundLayer = layers.First();
            foreach (var layer in layers)
            {
                if (layer.NumberOfZeroDigits < foundLayer.NumberOfZeroDigits)
                {
                    foundLayer = layer;
                }
            }
            return foundLayer;
        }

        public int CalculateChecksum()
        {
            Layer layerWithFewestZeroDigits = FindLayerWithFewestZeroDigits();
            return layerWithFewestZeroDigits.NumberOfOneDigits * layerWithFewestZeroDigits.NumberOfTwoDigits;
        }

        public Layer Render()
        {
            // create empty (transparent) layer
            char[] renderedLayerDigits = Enumerable.Repeat(Layer.TransparentDigit, width * height).ToArray();

            foreach (var layer in layers.Select(l => l.ToArray()))
            {
                for (int i = 0; i < layer.Length; i++)
                {
                    if (renderedLayerDigits[i] == Layer.BlackDigit || renderedLayerDigits[i] == Layer.WhiteDigit)
                    {
                        continue;
                    }
                    else
                    {
                        renderedLayerDigits[i] = layer[i];
                    }
                }
            }

            return new Layer(width, height, renderedLayerDigits);
        }

        public IEnumerator<Layer> GetEnumerator()
        {
            return layers.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return layers.GetEnumerator();
        }
    }
}
