using SFML.Graphics;

namespace Confetti {

    public class Font {

        public Sprite[] Glyph { get; }
        public int GlyphWidth { get; }
        public int GlyphHeight { get; }
        public char[] CharSet { get; }

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
            CharSet = new char[rows * cols];

            int index = 0;
            for (int y = 0; y < rows; y++) {
                for (int x = 0; x < cols; x++) {
                    char c = lines[y][x];
                    Glyph[c] = new Sprite(imageTexture);
                    Glyph[c].TextureRect = new IntRect(x * GlyphWidth, y * GlyphHeight, GlyphWidth, GlyphHeight);
                    CharSet[index] = c;
                    index++;
                }
            }
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
