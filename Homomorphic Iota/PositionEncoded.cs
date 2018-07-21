using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
