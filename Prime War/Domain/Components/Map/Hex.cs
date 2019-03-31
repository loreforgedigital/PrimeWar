

namespace PrimeWarEngine.Domain.Components.Map
{
    public class Hex
    {
        Coordinates coordinates { get; set; }
        FeatureType terrain { get; set; }

        public Hex()
        {
            coordinates = Coordinates.origin;
            terrain = FeatureType.None;
        }
        public Hex(Coordinates coords )
        {
            coordinates = coords;
            terrain = FeatureType.None;
        }
    }
}
