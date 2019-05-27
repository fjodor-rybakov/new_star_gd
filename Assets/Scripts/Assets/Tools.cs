using System.Collections.Generic;
using System.Linq;

namespace Assets
{
    public static class Tools
    {
        private const int CountRows = 4;
        private const int CountCols = 6;
        public static readonly List<Coord> AlreadyExistCoors = new List<Coord>();
    
        public static Coord GetCoordsPair()
        {
            var rand = new System.Random();
            int xPos = rand.Next(CountCols), yPos = rand.Next(CountRows);
            var coords = new Coord {X = xPos, Y =  yPos};

            while (AlreadyExistCoors.FirstOrDefault(i => i.X == xPos && i.Y == yPos) != null)
            {
                xPos = rand.Next(CountCols - 1);
                yPos = rand.Next(CountRows - 1);
                coords = new Coord {X = xPos, Y = yPos};
            }

            AlreadyExistCoors.Add(coords);

            return coords;
        }
    }
}