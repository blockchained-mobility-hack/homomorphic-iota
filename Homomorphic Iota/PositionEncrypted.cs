using Microsoft.Research.SEAL;

namespace Homomorphic_Iota
{
    public class PositionEncrypted
    {
        public PositionEncrypted()
        {
            Lon = new Ciphertext();
            Lat = new Ciphertext();
        }

        public Ciphertext Lon { get; set; }
        public Ciphertext Lat { get; set; }
    }
}
