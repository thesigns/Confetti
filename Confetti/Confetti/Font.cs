using SFML.Graphics;
using System;
using System.IO;
using System.IO.Compression;

namespace Confetti {

    public class Font {

        public Sprite[] Glyph { get; }
        public int GlyphWidth { get; }
        public int GlyphHeight { get; }
        private ushort[] CharSetTable;

        public Font(string pngPath, string txtPath) {

            Image image = new Image(pngPath);
            ConvertColor(image, Color.Black, Color.Transparent);
            Texture imageTexture = new Texture(image);

            var lines = File.ReadAllLines(txtPath);
            var rows = lines.Length;
            var cols = lines[0].Length;

            Glyph = new Sprite[char.MaxValue];
            GlyphWidth = (int)imageTexture.Size.X / cols;
            GlyphHeight = (int)imageTexture.Size.Y / rows;
            CharSetTable = new ushort[ushort.MaxValue];

            int index = 0;
            for (int y = 0; y < rows; y++) {
                for (int x = 0; x < cols; x++) {
                    char c = lines[y][x];
                    Glyph[c] = new Sprite(imageTexture);
                    Glyph[c].TextureRect = new IntRect(x * GlyphWidth, y * GlyphHeight, GlyphWidth, GlyphHeight);
                    CharSetTable[index] = c;
                    index++;
                }
            }
        }

        public ushort CharSet(ushort index) {
            if (Glyph[CharSetTable[index]] == null) {
                return CharSetTable[0];
            } else {
                return CharSetTable[index];
            }
        }

        private List<string>? ConvertToLines(string text) {
            if(text.Length == 0) {
                return null;
            };
            List<string> lines = new List<string>() { "" };
            var lineNum = 0;
            foreach (var character in text) {
                if (character.ToString() == "\n") {
                    lineNum++;
                    lines.Add("");
                } else {
                    lines[lineNum] += character;
                }
            }
            return null;
        }

        private void ConvertColor(Image image, Color fromColor, Color toColor) {
            for (uint y = 0; y < image.Size.Y; y++) {
                for (uint x = 0; x < image.Size.X; x++) {
                    Color pixelColor = image.GetPixel(x, y);
                    if (pixelColor == fromColor) {
                        image.SetPixel(x, y, toColor);
                    }
                }
            }
        }

    }
}
