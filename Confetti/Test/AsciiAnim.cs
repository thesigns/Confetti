
public class AsciiAnim : Confetti.Canvas {

    protected Confetti.Cell[,] TargetBuffer { get; }

    public AsciiAnim(int columns, int rows) : base(columns, rows) {

        TargetBuffer = new Confetti.Cell[columns, rows];

        Clear();
        Locate(40, 5);
        CenterPrint("@@@ @@@      @@@@@@      @@@@@@@ ");
        CenterPrint("@@@ @@@     @@@@@@@@     @@@@@@@@");
        CenterPrint("@@! !@@     @@!  @@@     @@!  @@@");
        CenterPrint("!@! @!!     !@!  @!@     !@!  @!@");
        CenterPrint(" !@!@!      @!@!@!@!     @!@!!@! ");
        CenterPrint("  @!!!      !!!@!!!!     !!@!@!  ");
        CenterPrint("  !!:       !!:  !!!     !!: :!! ");
        CenterPrint("  :!:       :!:  !:!     :!:  !:!");
        CenterPrint("   ::       ::   :::     ::   :::");
        CenterPrint("   :         :   : :      :   : :");
        CenterPrint("                                 ");
        CenterPrint(":::::: Yet Another Rewrite ::::::");
        CenterPrint("");
        CenterPrint("Press any key to close the window");


        Random random = new Random();

        for (int x = 0; x < columns; x++) {
            for (int y = 0; y < rows; y++) {
                TargetBuffer[x, y] = new Confetti.Cell(
                    Buffer[x, y].ForegroundCharacter,
                    Buffer[x, y].ForegroundColor,
                    Buffer[x, y].BackgroundCharacter,
                    Buffer[x, y].BackgroundColor
                );

                Locate(x, y);
                ushort ch = (ushort)random.Next(32, 127);
                SetColor((byte)ch, 80, 80);
                AddCharacter(ch);
            }
        }

    }

    public void Mix() {
        Random random = new Random();

        for (int x = 0; x < Columns; x++) {
            for (int y = 0; y < Rows; y++) {

                if (Buffer[x, y].ForegroundCharacter == TargetBuffer[x, y].ForegroundCharacter) {
                    continue;
                }

                Locate(x, y);
                ushort ch = (ushort)random.Next(32, 127);
                if (random.Next(1, 100) < 20) {
                    ch = 32;
                }
                SetColor((byte)ch, 80, 80);
                AddCharacter(ch);

            }
        }

    }

}