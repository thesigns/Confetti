using SFML.Window;
using SFML.Graphics;
using SFML.System;


public static class Example {

    public static void Main(string[] args) {

        var font = new Confetti.Font("Data/Fonts/Confetti.png", "Data/Fonts/Confetti.txt");
        
        var console = new Confetti.Console(font, 80, 25);
        console.Scale = 2;

        var mode = new VideoMode(console.Width, console.Height);
        var window = new RenderWindow(mode, "Confetti Example");

        window.SetVerticalSyncEnabled(true);
        window.SetFramerateLimit(60);
        window.Closed += (sender, args) => window.Close();

        int count = 0;
        while (window.IsOpen) {
            window.DispatchEvents();
            window.Clear();
            console.Print(count, false);
            console.CopyTo(window, 0, 0);
            window.Display();
            count++;
        }

    }




}