using SFML.Window;
using SFML.Graphics;


public static class Example {

    public static void Main(string[] args) {

        var font = new Confetti.Font("Confetti.png", "Confetti.txt");

        var console = new Confetti.Console(font, 80, 25);
        console.Scale = 2;

        var mode = new VideoMode(console.Width, console.Height);
        var window = new RenderWindow(mode, "Confetti Example");

        window.SetVerticalSyncEnabled(true);
        window.SetFramerateLimit(60);
        window.Closed += (sender, args) => window.Close();
        window.KeyPressed += (sender, args) => window.Close();

        var random = new Random();
        var canvas = new Confetti.Canvas(12, 8);

        AsciiAnim art = new AsciiAnim(80, 25);
        console.Cursor = false;

        while (window.IsOpen) {
            window.DispatchEvents();
            window.Clear();
            for (int x = 0; x < canvas.Columns; x++) {
                for (int y = 0; y < canvas.Rows; y++) {
                    canvas.AddCharacter((ushort)random.Next(33, 127));
                }
            }

            art.Mix();
            art.CopyTo(console, 0, 0);
            
            console.CopyTo(window, 0, 0);
            window.Display();
        }

    }




}