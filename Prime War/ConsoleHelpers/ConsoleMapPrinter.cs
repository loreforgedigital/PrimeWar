using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrimeWarEngine.Domain.Components.Map;
using PrimeWarEngine.Domain.Controllers;

namespace PrimeWarEngine.ConsoleHelpers
{
    class ConsoleMapPrinter
    {
        public static void printBlankBoard()
        {
            AsciiBoard board = new AsciiBoard(0, 12, -6, 15, new SmallFlatAsciiHexPrinter());
            var mapcoords = MapMath.GetCoordinatesForScenarioMap();
            foreach(var c in mapcoords)
            {
                board.printHex(c);
            }            
            string mapString = board.prettyPrint(false);
            Console.Write(mapString);
        }

        public static void printBoardWithPath(IEnumerable<Coordinates> path)
        {
            AsciiBoard board = new AsciiBoard(0, 12, -6, 15, new SmallFlatAsciiHexPrinter());
            var mapcoords = MapMath.GetCoordinatesForScenarioMap();
            foreach (var c in mapcoords)
            {
                board.printHex(c, path.Contains(c)? "PATH" : "");
            }
            string mapString = board.prettyPrint(false);
            Console.Write(mapString);
        }

        public static void printBoardWithTargets(IEnumerable<TargetController> targets)
        {
            IDictionary<Coordinates, TargetController> occupiedHexes = targets.ToDictionary(tc => tc.Position);
            AsciiBoard board = new AsciiBoard(0, 12, -6, 15, new LargeFlatAsciiHexPrinter());
            var mapcoords = MapMath.GetCoordinatesForScenarioMap();
            foreach (var c in mapcoords)
            {
                board.printHex(c, occupiedHexes.Keys.Contains(c) ? occupiedHexes[c].Target.Name : "");
            }
            string mapString = board.prettyPrint(false);
            Console.Write(mapString);
        }

        public Stack<Coordinates> GetMovementPath(string TargetName, Coordinates pathStart)
        {
            Random random = new Random();
            var totalCoordinates = MapMath.GetCoordinatesForScenarioMap();
            Stack<Coordinates> movementPath = new Stack<Coordinates>();
            bool continueTravel = true;
            while (continueTravel)
            {
                ConsoleMapPrinter.printBoardWithPath(movementPath);
                Console.WriteLine("Continue your journey? Y/N");
                string continueResponse = Console.ReadLine();
                continueTravel = !(continueResponse.Contains("N") || continueResponse.Contains("n"));
                if (continueTravel)
                {
                    var nextCoord = MapMath.relativeAdjacents[random.Next(0, 6)] + movementPath.Peek();
                    while (!totalCoordinates.Contains(nextCoord) || movementPath.Contains(nextCoord) || MapMath.relativeAdjacents.All(a => movementPath.Contains(a + movementPath.Peek())))
                    {
                        nextCoord = MapMath.relativeAdjacents[random.Next(0, 6)] + movementPath.Peek();
                    }
                    movementPath.Push(nextCoord);
                }
            }
            return movementPath;
        }
    }

    public class CharGrid
    {

        private static string LINE_BREAK = "\n";

        private int width;
        private int height;
        private char[,] grid;

        public CharGrid(int width, int height)
        {
            this.width = width;
            this.height = height;
            grid = new char[this.height,this.width];//[this.width];
            prefillGrid();
        }

        /**
         * Prefill grid with spaces.
         */
        private void prefillGrid()
        {
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    addChar(j, i, ' ');
                }
            }
        }

        /**
         * Add a string to the grid.
         *
         * @param x Starting x coordinate.
         * @param y Starting y coordinate.
         * @param input string put input. string will not wrap, but throws IndexOutOfBounds if to long.
         */
        public void addstring(int x, int y, string input)
        {
            if (string.IsNullOrEmpty(input)) return;
            for (int i = 0; i < input.Length; i++)
            {
                this.addChar(x + i, y, input[i]);
            }
        }

        /**
         * Add a string to the grid.
         *
         * @param x Starting x coordinate.
         * @param y Starting y coordinate.
         * @param input Char to insert. Trows IndexOutOfBounds if outside grid.
         */
        public void addChar(int x, int y, char input)
        {
            if (x < 0 || x >= width || y < 0 || y >= height)
            {
                int maxWidth = width - 1;
                int maxHeight = height - 1;
                throw new IndexOutOfRangeException("(" + x + "," + y + ") is outside (" + maxWidth + "," + maxHeight + ")");
            }

            grid[y,x] = input;
        }

        /**
         * Returns a char from the grid
         */
        public char getChar(int x, int y)
        {
            return grid[y,x];
        }

        /**
         * Returns the char grid as a string, ready for output.
         *
         * @param trimToBoundingBox If true, the grid is trimmed to it's contents bounding box. If not grid is printet as is.
         */
        public string print(bool trimToBoundingBox)
        {

            int leftBound = trimToBoundingBox ? width - 1 : 0;
            int rightBound = trimToBoundingBox ? 0 : width - 1;
            int topBound = trimToBoundingBox ? height - 1 : 0;
            int bottomBound = trimToBoundingBox ? 0 : height - 1;

            // Find bounding box
            if (trimToBoundingBox)
            {
                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        char c = grid[i,j];
                        if (c != ' ')
                        {
                            leftBound = Math.Min(leftBound, j);
                            rightBound = Math.Max(rightBound, j);
                            topBound = Math.Min(topBound, i);
                            bottomBound = Math.Max(bottomBound, i);
                        }
                    }
                }
            }

            // Print grid
            StringBuilder builder = new StringBuilder((width + LINE_BREAK.Length) * height);
            for (int i = topBound; i <= bottomBound; i++)
            {
                for (int j = leftBound; j <= rightBound; j++)
                {
                    builder.Append(grid[i,j]);
                }
                builder.Append(LINE_BREAK);
            }
            return builder.ToString();
        }
    }

    public class SmallFlatAsciiHexPrinter : AsciiHexPrinter
    {

    public static string TEMPLATE =
             "   _ _   \n"  // 0 - 9
           + " /# # #\\ \n" // 9 - 18
           + "/# XXX #\\\n" // 18 - 27
           + "\\# YYY #/\n" // 27 - 36
           + " \\#_#_#/ ";  // 36 - 45

    private int width = 9;
    private int height = 5;
    private int sideLength = 2;


    public override string getHex(string line1, string line2, char filler)
    {
        string hex = TEMPLATE;

        line1 = restrictToLength(line1, 3);
        line2 = restrictToLength(line2, 3);

        hex = hex.Replace("XXX", line1);
        hex = hex.Replace("YYY", line2);

        return hex.Replace('#', filler);
    }

    
    public override int[] mapHexCoordsToCharCoords(int q, int r)
    {
        int[] result = new int[2];

        result[0] = 7 * q;        // q * (width - side)
        result[1] = 2 * q + 4 * r;  // height/2 * q + (height - 1) * r

        return result;
    }

    
    public override int[] getMapSizeInChars(int hexWidth, int hexHeight)
    {
        int widthInChars = hexWidth * (width - sideLength) + sideLength;
        int heightInChars = (hexWidth - 1) * height / 2 + hexHeight * height;
        return new int[] { widthInChars, heightInChars };
    }

    }

    public class LargeFlatAsciiHexPrinter :AsciiHexPrinter
    {

    public static string TEMPLATE =
              "   _ _ _ _   \n" // 0 - 13
            + "  / # # # \\  \n" // 12 - 24
            + " /# # # # #\\ \n" // 24 - 36
            + "/# XXXXXXX #\\\n" // 36 - 48
            + "\\# YYYYYYY #/\n" // 48 - 60
            + " \\# # # # #/ \n" // 60 - 72
            + "  \\_#_#_#_/  \n"; // 72 - 84

    private int width = 13;
    private int height = 7;
    private int sideLength = 3;
    private int sideHeight = 3;

    
    public override string getHex(string line1, string line2, char filler)
    {
        string hex = TEMPLATE;

        line1 = restrictToLength(line1, 7);
        line2 = restrictToLength(line2, 7);

        hex = hex.Replace("XXXXXXX", line1);
        hex = hex.Replace("YYYYYYY", line2);

        return hex.Replace('#', filler);
    }

    
    public override int[] mapHexCoordsToCharCoords(int q, int r)
    {
        int[] result = new int[2];

        result[0] = (width - sideLength) * q;
        result[1] = sideHeight * q + (height - 1) * r;

        return result;
    }

    
    public override int[] getMapSizeInChars(int hexWidth, int hexHeight)
    {
        int widthInChars = hexWidth * (width - sideLength) + sideLength;
        int heightInChars = (hexWidth - 1) * height / 2 + hexHeight * height;
        return new int[] { widthInChars, heightInChars };
    }
    }

    public class AsciiBoard
    {

        private int width;
        private int height;

        private AsciiHexPrinter printer;
        private CharGrid grid;

        /**
         * Constructs the virtual hex board.
         *
         * @param printer Reference to the hex printer used
         */
        public AsciiBoard(int minQ, int maxQ, int minR, int maxR, AsciiHexPrinter printer)
        {
            this.width = maxQ - minQ + 1;
            this.height = maxR - minR + 1;
            this.printer = printer;
            this.grid = createGrid();
        }

        private CharGrid createGrid()
        {
            // This potentially creates the grid ½ a hexagon to heigh or wide, as we do not now given the max coordinates
            // (0,0,1,1) if both (0,1) or (1,1) is filled. This is OK, as we can fix it when outputting the grid.
            int[] gridSize = printer.getMapSizeInChars(width, height);
            return new CharGrid(gridSize[0], gridSize[1]);
        }

        public void printHex(Coordinates c, string targetName ="", FeatureType terrain = FeatureType.None)
        {
            char terrainChar = ' ';
            switch(terrain)
            {
                case FeatureType.Cover:
                    terrainChar = '#';
                    break;
                case FeatureType.HighGround:
                    terrainChar = '^';
                    break;
                case FeatureType.Concealment:
                    terrainChar = '@';
                    break;
                case FeatureType.Wall:
                    terrainChar = '%';
                    break;
                default:
                    break;
            }
            this.printHex(c.ToString(), targetName, terrainChar, c.q, c.r);
        }

        /**
         *
         * @param line1 First line of text
         * @param line2 2nd line of
         * @param fillerChar Character used as filler, may be ' '
         * @param hexQ Q coordinate for the hex in the hex grid.
         * @param hexR R coordinate for the hex in the hex grid.
         */
        public void printHex(string line1, string line2, char fillerChar, int hexQ, int hexR)
        {

            string hex = printer.getHex(line1, line2, fillerChar);
            int[] charCoordinates = printer.mapHexCoordsToCharCoords(hexQ, hexR);
            string[] lines = hex.ToString().Split('\n');

            for (int i = 0; i < lines.Length; i++)
            {
                string content = lines[i];
                for (int j = 0; j < content.Length; j++)
                {
                    int x = charCoordinates[0] + j;
                    int y = charCoordinates[1] + i;

                    // Only override empty spaces
                    if (grid.getChar(x, y) == ' ')
                    {
                        grid.addChar(x, y, content[j]);
                    }
                }
            }
        }

        /**
         * Prints the Hexagonal map as a string.
         *
         * @param wrapInBox If true, output is wrapped in a Ascii drawn box.
         */
        public string prettyPrint(bool wrapInBox)
        {
            return printBoard(wrapInBox);
        }

        /**
         * Returns the Hexagonal map as a string. Any extra empty lines at the end are trimmed away,
         * but map still starts at (0,0), so eg. having a hex at (0,1) will produce whitespace at the top.
         *
         * @param wrapInBox If true, the hex map is wrapped in a ASCII bounding box.
         */
        private string printBoard(bool wrapInBox)
        {
            if (wrapInBox)
            {
                StringBuilder sb = new StringBuilder();

                // Get content
                string[] lines = grid.print(true).Split('\n');
                int contentLength = (lines.Length > 0) ? lines[0].Length : 0;
                string verticalLine = getVerticalLine('=', contentLength);
                string spacerLine = getVerticalLine(' ', contentLength);

                // Build output
                sb.Append(verticalLine);
                for (int i = 0; i < lines.Length; i++)
                {
                    string line = lines[i];
                    sb.Append("| ");
                    sb.Append(line);
                    sb.Append(" |");
                    sb.Append('\n');
                }

                // Flat hexes have to little bottom space as they use the _ char
                // so add a extra filler line.
                
                    sb.Append(spacerLine);
                

                sb.Append(verticalLine);
                return sb.ToString();

            }
            else
            {
                return grid.print(true);
            }
        }

        private string getVerticalLine(char filler, int contentLength)
        {
            StringBuilder verticalLine = new StringBuilder("| ");
            for (int i = 0; i < contentLength; i++)
            {
                if (i % 2 == 0)
                {
                    verticalLine.Append(filler);
                }
                else
                {
                    verticalLine.Append(' ');
                }
            }
            return verticalLine.Append(" |\n").ToString();
        }
    }

    public abstract class AsciiHexPrinter
    {

        /**
         * Returns the hex
         */
        public abstract string getHex(string line1, string line2, char fillerChar);

        /**
         * Viewing the board as a grid of hexes. Each hex has a bounding box. Map top-left of bounding box given by hex
         * coordinates to same area viewed as char grid.
         *
         * @returns A int[2] with (x,y) char coordinates. (top,left) is (0,0)
         */
        public abstract int[] mapHexCoordsToCharCoords(int q, int r);

        /**
         * Returns the bounding box in chars for a map of the given size
         *
         * @param hexWidth  Size of board in hexes horisontally.
         * @param hexHeight Size of board in hexes verticall.
         * @return A int[2]: int[0] gives the width in chars and int[1] gives the height.
         */
        public abstract int[] getMapSizeInChars(int hexWidth, int hexHeight);



    protected string restrictToLength(string str, int length)
    {
        string result = "  ";
        if (str != null)
        {
            if (str.Length > length)
            {
                result = str.ToUpper().Substring(0, length);
            }
            else if (str.Length < length)
            {
                result = pad(str.ToUpper(), length - str.Length);
            }
            else
            {
                result = str;
            }
        }

        return result;
    }

    /**
     * Pads whitespace to both sides, effectively centering the text.
     * Padding starts at the left side
     * @param s
     * @param n
     * @return
     */
    private string pad(string s, int n)
    {
        while (n > 0)
        {
            if (n % 2 == 0)
            {
                s = " " + s;
            }
            else
            {
                s = s + " ";
            }
            n--;
        }

        return s;
    }
}
}
