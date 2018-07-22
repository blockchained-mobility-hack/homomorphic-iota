using Microsoft.Research.SEAL;

namespace Homomorphic_Iota
{
    public class PositionEncoded
    {
        public PositionEncoded()
        {
            Lon = new Plaintext();    
            Lat = new Plaintext();
        }

        public Plaintext Lon { get; set; }
        public Plaintext Lat { get; set; }
    }
}
