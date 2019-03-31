namespace PrimeWarEngine.Domain.Components.Map
{
    public struct Coordinates
    {
        public static Coordinates origin = new Coordinates(0, 0);
        public int q;
        public int r;
        public Coordinates(int leftToRight, int topRightToBottomLeft)
        {
            q = leftToRight;
            r = topRightToBottomLeft;
        }

        public override string ToString()
        {
            return "Q:" + q + ", R:" + r;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Coordinates))
            {
                return false;
            }

            var coordinates = (Coordinates)obj;
            return q == coordinates.q &&
                   r == coordinates.r;
        }

        public override int GetHashCode()
        {
            var hashCode = 1939305425;
            hashCode = hashCode * -1521134295 + q.GetHashCode();
            hashCode = hashCode * -1521134295 + r.GetHashCode();
            return hashCode;
        }

        public static Coordinates operator +(Coordinates a, Coordinates b)
        {
            return new Coordinates(a.q + b.q, a.r + b.r);
        }

        public static Coordinates operator -(Coordinates a, Coordinates b)
        {
            return new Coordinates(a.q - b.q, a.r - b.r);
        }

        public static bool operator ==(Coordinates a, Coordinates b)
        {
            return (a.q == b.q && a.r == b.r);
        }

        public static bool operator !=(Coordinates a, Coordinates b)
        {
            return (a.q != b.q || a.r != b.r);
        }

        public static bool operator <(Coordinates a, Coordinates b)
        {
            return MapMath.DistanceBetween(Coordinates.origin, a) < MapMath.DistanceBetween(Coordinates.origin, b);
        }

        public static bool operator >(Coordinates a, Coordinates b)
        {
            return MapMath.DistanceBetween(Coordinates.origin, a) > MapMath.DistanceBetween(Coordinates.origin, b);
        }

        public static bool operator <=(Coordinates a, Coordinates b)
        {
            return MapMath.DistanceBetween(Coordinates.origin, a) <= MapMath.DistanceBetween(Coordinates.origin, b);
        }

        public static bool operator >=(Coordinates a, Coordinates b)
        {
            return MapMath.DistanceBetween(Coordinates.origin, a) >= MapMath.DistanceBetween(Coordinates.origin, b);
        }

        public static Coordinates[] operator +(Coordinates start, Coordinates[] list)
        {
            Coordinates[] results = new Coordinates[list.Length];
            for(int i = 0; i < list.Length; i++)
            {
                results[i] = list[i] + start;
            }
            return results;
        }
    }
}
