using SFML.Graphics;
using SFML.System;

namespace Confetti {
    public class Canvas {


        public int Columns { get; }
        public int Rows { get; }

        protected Cell[,] Buffer { get; }

        protected Vector2i cursorPosition;
        private Color foregroundColor;
        private Color backgroundColor;

        public Canvas(int columns, int rows) {
            cursorPosition = new(0, 0);
            foregroundColor = new Color(160, 160, 160);
            backgroundColor = Color.Black;
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

        public void SetColor(Color foregroundColor) {
            this.foregroundColor = foregroundColor;
        }

        public void SetColor(Color foregroundColor, Color backgroundColor) {
            this.foregroundColor = foregroundColor;
            this.backgroundColor = backgroundColor;
        }

        public void SetColor(byte r, byte g, byte b) {
            Color color = new Color(r, g, b);
            SetColor(color);
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

        public void Clear() {
            for (int y = 0; y < Rows; y++) {
                for (int x = 0; x < Columns; x++) {
                    Buffer[x, y] = new Cell(' ', foregroundColor, '█', backgroundColor);
                }
            }
            Locate(0, 0);
        }

        public void Print() {
            Locate(cursorPosition.X, cursorPosition.Y + 1);
        }

        public void Print(string? text, bool newLine = true) {
            if (text == null) {
                return;
            }
            int textLength = text.Length;
            Vector2i startPosition = cursorPosition;
            for (int i = 0; i < textLength; i++) {
                AddCharacter(text[i]);
            }
            if (newLine) Locate(startPosition.X, cursorPosition.Y + 1);
        }

        public void Print<T>(T value, bool newLine = true) {
            Print(Convert.ToString(value), newLine);
        }

        public void CenterPrint(string text, bool newLine = true) {
            int textLength = text.Length;
            Vector2i startPosition = cursorPosition;
            Locate(cursorPosition.X - textLength / 2, cursorPosition.Y);
            for (int i = 0; i < textLength; i++) {
                AddCharacter(text[i]);
            }
            if (newLine) Locate(startPosition.X, cursorPosition.Y + 1);
        }

        public void CopyTo(Canvas destination, int x, int y) {
            for (int row = 0; row < Rows; row++) {
                int targetRow = row + y;
                for (int col = 0; col < Columns; col++) {
                    int targetCol = col + x;
                    if (targetCol < 0 || targetCol > destination.Columns - 1 || targetRow < 0 || targetRow > destination.Rows - 1) {
                        continue;
                    }
                    destination.Buffer[targetCol, targetRow] = Buffer[col, row];
                }
            }
        }

    }
}
