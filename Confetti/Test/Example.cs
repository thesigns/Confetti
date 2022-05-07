using SFML.Window;
using SFML.Graphics;
using SFML.System;


public static class Example {

    static Confetti.Font font;
    static Confetti.Console console;
    static Confetti.Console console2;
    static Confetti.Chunk camera;
    static Confetti.Chunk map;

    static int cameraX = 0;
    static int cameraY = 0;

    static Example() {
        string fontPath = "../../../../../Fonts/";
        font = new Confetti.Font(fontPath + "Confetti.png", fontPath + "Confetti.txt");
        console = new Confetti.Console(font, 40, 20);
        console2 = new Confetti.Console(font, 40, 20);
        console.Scale = 2;
        camera = new Confetti.Chunk(console, 12, 12);
        map = new Confetti.Chunk(camera, 256, 256);
    }

    public static void Main(string[] args) {



        var mode = new VideoMode(1500, 700);
        var window = new RenderWindow(mode, "Confetti Example");

        window.SetVerticalSyncEnabled(true);
        window.SetFramerateLimit(60);
        window.Closed += (sender, args) => window.Close();
        window.KeyPressed += (sender, args) => {

            switch (args.Code) {
                case Keyboard.Key.Up: cameraY--; break;
                case Keyboard.Key.Down: cameraY++; break;
                case Keyboard.Key.Left: cameraX--; break;
                case Keyboard.Key.Right: cameraX++; break;
            }

        };

        Random rnd = new Random();
        for (int i = 0; i < map.Columns * map.Rows; i++) {
            map.AddCharacter(font.CharSet[rnd.Next(0, 256)]);
            console2.AddCharacter(font.CharSet[rnd.Next(0, 256)]);
        }

        while (window.IsOpen) {
            window.DispatchEvents();
            window.Clear();
            Update();
            console.Draw(window);
            console2.Draw(window, 450, 30);
            window.Display();
        }

    }

    private static void Update() {
        
        camera.Clear();
        map.Transfer(cameraX, cameraY);
        camera.Transfer(10, 10);

    }



}