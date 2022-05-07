using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Window;
using SFML.Graphics;
using SFML.System;

namespace Confetti {
    public class Chunk {


        public int Columns { get; }
        public int Rows { get; }

        protected Cell[,] Buffer { get; }

        private Vector2i oldCursorPosition;
        private Vector2i cursorPosition;
        private Color foregroundColor;
        private Color backgroundColor;

        Chunk? Parent { get; }

        public Chunk(int columns, int rows) : this(null, columns, rows) { }

        public Chunk(Chunk? parent, int columns, int rows) {
            oldCursorPosition = new(0, 0);
            cursorPosition = new(0, 0);
            foregroundColor = new Color(160, 160, 160);
            backgroundColor = Color.Black;
            Parent = parent;
            Columns = columns;
            Rows = rows;
            Buffer = new Cell[Columns, Rows];
            for (int y = 0; y < Rows; y++) {
                for (int x = 0; x < Columns; x++) {
                    Buffer[x, y] = new Cell(' ', new Color(160, 160, 160), '█', Color.Black);
                }
            }
        }

        public void Locate(Vector2i position) {
            position.X = Math.Abs(position.X % Columns);
            position.Y = Math.Abs(position.Y % Rows);
            cursorPosition = position;
        }

        public void Locate(int x, int y) {
            Locate(new Vector2i(x, y));
        }

        public void Clear() {
            for (int y = 0; y < Rows; y++) {
                for (int x = 0; x < Columns; x++) {
                    Buffer[x, y] = new Cell(' ', foregroundColor, '█', backgroundColor);
                }
            }
            Locate(0, 0);
        }



        public void AddCharacter(ushort character, bool moveCursor = true) {
            Buffer[cursorPosition.X, cursorPosition.Y].ForegroundColor = foregroundColor;
            Buffer[cursorPosition.X, cursorPosition.Y].BackgroundColor = backgroundColor;
            Buffer[cursorPosition.X, cursorPosition.Y].ForegroundCharacter = character;
            if (moveCursor) {
                int x = cursorPosition.X;
                int y = cursorPosition.Y;
                x++;
                if (x >= Columns) {
                    x = 0;
                    y++;
                    if (y >= Rows) {
                        y = 0;
                    }
                }
                cursorPosition = new Vector2i(x, y);
            }
        }

        public virtual void Transfer(int x, int y) {
            if (Parent == null) {
                return;
            }
            for (int row = 0; row < Rows; row++) {
                int targetRow = row + y;
                for (int col = 0; col < Columns; col++) {
                    int targetCol = col + x;
                    if (targetCol < 0 || targetCol > Parent.Columns - 1 || targetRow < 0 || targetRow > Parent.Rows - 1) {
                        continue;
                    }
                    Parent.Buffer[targetCol, targetRow] = Buffer[col, row];
                }
            }
        }

    }
}
