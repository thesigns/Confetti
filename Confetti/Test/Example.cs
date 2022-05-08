using SFML.Window;
using SFML.Graphics;


public static class Example {

    public static void Main(string[] args) {

        var font = new Confetti.Font("Confetti.png", "Confetti.txt");

        var console = new Confetti.Console(font, 80, 25);
        console.Scale = 2;
        console.Cursor = false;

        var mode = new VideoMode(console.Width, console.Height);
        var window = new RenderWindow(mode, "Confetti Example");

        window.SetVerticalSyncEnabled(true);
        window.SetFramerateLimit(60);
        window.Closed += (sender, args) => window.Close();
        window.KeyPressed += (sender, args) => window.Close();

        var art = new AsciiAnim(80, 25);

        while (window.IsOpen) {
            window.DispatchEvents();
            window.Clear();
            art.Mix();
            art.CopyTo(console, 0, 0);
            console.CopyTo(window, 0, 0);
            window.Display();
        }

    }




}