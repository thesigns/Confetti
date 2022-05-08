using SFML.Graphics;
using SFML.System;

namespace Confetti {

    public class Console : Canvas {

        private Sprite Sprite { get; }
        private RenderTexture RenderTexture { get; }
        private Font Font { get; }

        public uint Width { get => (uint)(Columns * Font.GlyphWidth * scale); }
        public uint Height { get => (uint)(Rows * Font.GlyphHeight * scale); }

        public Vector2i CursorPhysicalPosition { get; private set; } = new(0, 0);
        public bool Cursor { get; set; } = true;

        private DateTime creationDate = DateTime.Now;

        private float scale = 1;
        public float Scale {
            get { return scale; }
            set {
                scale = value;
                Sprite.Scale = new Vector2f(scale, scale);
            }
        }

        public Console(Font font, int columns, int rows) : base(columns, rows) {
            Font = font;
            RenderTexture = new RenderTexture(Width, Height);
            Sprite = new Sprite(RenderTexture.Texture);
        }

        private void RenderCells() {
            TimeSpan ts = DateTime.Now - creationDate;
            for (int y = 0; y < Rows; y++) {
                for (int x = 0; x < Columns; x++) {
                    var posXY = new Vector2f(x * Font.GlyphWidth, y * Font.GlyphHeight);
                    Font.Glyph[Buffer[x, y].BackgroundCharacter].Color = Buffer[x, y].BackgroundColor;
                    Font.Glyph[Buffer[x, y].BackgroundCharacter].Position = posXY;
                    RenderTexture.Draw(Font.Glyph[Buffer[x, y].BackgroundCharacter]);
                    var foregroundCharacter = Buffer[x, y].ForegroundCharacter;
                    if (Font.Glyph[foregroundCharacter] == null) {
                        foregroundCharacter = Font.CharSet(0);
                    }
                    Font.Glyph[foregroundCharacter].Color = Buffer[x, y].ForegroundColor;
                    Font.Glyph[foregroundCharacter].Position = posXY;
                    RenderTexture.Draw(Font.Glyph[foregroundCharacter]);
                    if (Cursor && (ts.Milliseconds % 1000 < 500)) {
                        Font.Glyph['▂'].Color = new Color(255, 255, 255, 127);
                        Font.Glyph['▂'].Position = (Vector2f)CursorPhysicalPosition;
                        RenderTexture.Draw(Font.Glyph['▂']);
                    }
                }
            }
        }

        public void CopyTo(RenderWindow window, float x = 0, float y = 0) {
            CursorPhysicalPosition = new Vector2i(cursorPosition.X * Font.GlyphWidth, cursorPosition.Y * Font.GlyphHeight);
            RenderTexture.Clear();
            RenderCells();
            RenderTexture.Display();
            Sprite.Position = new Vector2f(x, y);
            window.Draw(Sprite);
        }
    }
}
