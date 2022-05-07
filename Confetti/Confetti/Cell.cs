using SFML.Graphics;

namespace Confetti {

    public class Cell {

        public ushort ForegroundCharacter { get; set; }
        public Color ForegroundColor { get; set; }
        public ushort BackgroundCharacter { get; set; }
        public Color BackgroundColor { get; set; }
        public bool Cursor { get; set; }

        public Cell(ushort foregroundCharacter, Color foregroundColor, ushort backgroundCharacter, Color backgroundColor, bool cursor = false) {
            ForegroundCharacter = foregroundCharacter;
            ForegroundColor = foregroundColor;
            BackgroundCharacter = backgroundCharacter;
            BackgroundColor = backgroundColor;
            Cursor = cursor;
        }

    }
}
