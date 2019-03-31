using System;
using System.Collections.Generic;
using System.Linq;

namespace PrimeWarEngine.Domain.Components.Map
{
    public class MapMath
    { 
        public static int DistanceBetween(Coordinates a, Coordinates b)
        {
            return DistanceBetween(ConvertToCube(a), ConvertToCube(b));
        }

        public static Coordinates[] relativeAdjacents = {new Coordinates(1, 0), new Coordinates(1, -1), new Coordinates(0, -1),
                                                        new Coordinates(-1, 0), new Coordinates(-1, 1), new Coordinates(0, 1) };

        public static List<Coordinates> RotateCoordinates(List<Coordinates> toRotate, bool clockwise, int numberOfTimes, Coordinates aboutPoint = default(Coordinates))
        {
            IEnumerable<CubeCoordinates> cubed = toRotate.Select(a => MapMath.ConvertToCube(a-aboutPoint));
            IEnumerable<CubeCoordinates> rotatedCubes = cubed.Select(c => clockwise ? new CubeCoordinates(-c.Z, -c.X, -c.Y) : new CubeCoordinates(-c.Y, -c.Z, -c.X));
            for(int i=1;i<numberOfTimes;i++)
            {
                rotatedCubes = rotatedCubes.Select(c => clockwise ? new CubeCoordinates(-c.Z, -c.X, -c.Y) : new CubeCoordinates(-c.Y, -c.Z, -c.X));
            }
            List<Coordinates> rotated = rotatedCubes.Select(rc => MapMath.ConvertToAxial(rc)+aboutPoint).ToList();
            return rotated;
        }
        public static List<Coordinates> CardinalHexDirectionToRadius(int radius)
        {
            List<Coordinates> cardinalSouth = new List<Coordinates>() { Coordinates.origin };
            for(int i = 1; i<=radius; i++)
            {
                cardinalSouth.Add(new Coordinates(0, i));
            }
            return cardinalSouth;
        }

        public static List<Coordinates> GetShape(Shapes shape, int radius)
        {
            List<Coordinates> cardinal = CardinalHexDirectionToRadius(radius);
            List<Coordinates> totalCoordinates = new List<Coordinates>() { Coordinates.origin };
            switch(shape)
            {
                case Shapes.Cone:
                    {
                        for(int r = 1; r<=radius; r++)
                        {
                            Coordinates coordToAdd = ScaleCoordinateBy(relativeAdjacents[5], r);
                            totalCoordinates.Add(coordToAdd);
                            for(int i = 0; i< r;i++)
                            {
                                coordToAdd += relativeAdjacents[1];
                                totalCoordinates.Add(coordToAdd);
                            }
                        }
                        break;
                    }
                case Shapes.Sweep:
                    {
                        for (int r = 1; r <= radius; r++)
                        {
                            Coordinates coordToAdd = ScaleCoordinateBy(relativeAdjacents[5], r);
                            totalCoordinates.Add(coordToAdd);
                            for (int n = 1; n <= 3; n++)
                            {
                                for (int i = 0; i < r; i++)
                                {
                                    coordToAdd += relativeAdjacents[n];
                                    totalCoordinates.Add(coordToAdd);
                                }
                            }
                        }
                        break;
                    }
                case Shapes.Radius:
                    {
                        for (int r = 1; r <= radius; r++)
                        {
                            Coordinates coordToAdd = ScaleCoordinateBy(relativeAdjacents[4], r);
                            for (int n = 0; n < 6; n++)
                            {
                                for (int i = 0; i < r; i++)
                                {
                                    coordToAdd += relativeAdjacents[n];
                                    totalCoordinates.Add(coordToAdd);
                                }
                            }
                        }
                        break;
                    }
                case Shapes.Column:
                    {
                        totalCoordinates = cardinal;
                        break;
                    }
                case Shapes.Beam:
                    {
                        for (int r = 1; r <= radius; r++)
                        {
                            totalCoordinates.Add(cardinal[r]);
                            totalCoordinates.Add(cardinal[r] + relativeAdjacents[1]);
                            totalCoordinates.Add(cardinal[r] + relativeAdjacents[3]);
                        }
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
            return totalCoordinates;
        }

        static Coordinates ScaleCoordinateBy(Coordinates coord, int scale)
        {
            return new Coordinates(coord.q * scale, coord.r * scale);
        }

        static int DistanceBetween(CubeCoordinates a, CubeCoordinates b)
        {
            return (Math.Abs(a.X - b.X)+ Math.Abs(a.Y - b.Y) + Math.Abs(a.Z - b.Z))/2;
        }
        static CubeCoordinates ConvertToCube(Coordinates axial)
        {
            return new CubeCoordinates(axial.q, -(axial.q) - axial.r, axial.r);
        }
        static Coordinates ConvertToAxial(CubeCoordinates cube)
        {
            return new Coordinates(cube.X, cube.Z);
        }
        struct CubeCoordinates
        {
            public int X { get; private set; }
            public int Y { get; private set; }
            public int Z { get; private set; }
            public CubeCoordinates(int _x,int _y,int _z)
            {
                X = _x;
                Y = _y;
                Z = _z;
            }
        }
    }
}
