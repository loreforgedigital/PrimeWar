

namespace PrimeWarEngine.Domain.Components.Map
{
    public class Hex
    {
        public Coordinates coordinates { get; }
        public FeatureType terrain { get; set; }

        public Hex()
        {
            coordinates = Coordinates.origin;
            terrain = FeatureType.None;
        }
        public Hex(Coordinates coords, FeatureType feature = FeatureType.None)
        {
            coordinates = coords;
            terrain = feature;
        }
    }
}
