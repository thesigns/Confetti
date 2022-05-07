using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Window;
using SFML.Graphics;
using SFML.System;

namespace Confetti {

    public class Console: Canvas {

        private Sprite Sprite { get; }
        private RenderTexture RenderTexture { get; }
        private Font Font { get; }

        public uint Width { get => (uint)(Columns * Font.GlyphWidth * _scale); }
        public uint Height { get => (uint)(Rows * Font.GlyphHeight * _scale); }

        public uint Frame { get; private set; }

        public Vector2i CursorPhysicalPosition { get; private set; } = new(0, 0);
        public bool Cursor { get; set; } = true;

        private float _scale = 1;
        public float Scale {
            get { return _scale; }
            set {
                _scale = value;
                Sprite.Scale = new Vector2f(_scale, _scale);
            }
        }

        public Console(Font font, int columns, int rows) : base(columns, rows) {
            Font = font;
            RenderTexture = new RenderTexture(Width, Height);
            Sprite = new Sprite(RenderTexture.Texture);
        }

        private void RenderCells() {
            for (int y = 0; y < Rows; y++) {
                for (int x = 0; x < Columns; x++) {
                    var posXY = new Vector2f(x * Font.GlyphWidth, y * Font.GlyphHeight);
                    Font.Glyph[Buffer[x, y].BackgroundCharacter].Color = Buffer[x, y].BackgroundColor;
                    Font.Glyph[Buffer[x, y].BackgroundCharacter].Position = posXY;
                    RenderTexture.Draw(Font.Glyph[Buffer[x, y].BackgroundCharacter]);
                    Font.Glyph[Buffer[x, y].ForegroundCharacter].Color = Buffer[x, y].ForegroundColor;
                    Font.Glyph[Buffer[x, y].ForegroundCharacter].Position = posXY;
                    RenderTexture.Draw(Font.Glyph[Buffer[x, y].ForegroundCharacter]);
                    if (Cursor && (Frame % 60 < 30)) {
                        Font.Glyph['▂'].Color = new Color(255, 255, 255, 80);
                        Font.Glyph['▂'].Position = (Vector2f)CursorPhysicalPosition;
                        RenderTexture.Draw(Font.Glyph['▂']);
                    }
                }
            }
        }

        public void CopyTo(RenderWindow window, float x = 0, float y = 0) {
            RenderTexture.Clear();
            RenderCells();
            RenderTexture.Display();
            Sprite.Position = new Vector2f(x, y);
            window.Draw(Sprite);
            Frame++;
        }
    }
}
