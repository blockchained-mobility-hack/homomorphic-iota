using System;
using System.IO;

namespace Homomorphic_Iota
{
    public static class EncryptPositionData
    {
        public static void Main()
        {
            string destinationFile = "result.txt";
            string basePath = Path.Combine(Environment.CurrentDirectory, "..", "..");
            string[] lines = File.ReadAllLines(Path.Combine(basePath, "longlat_data.xml"));

            Homomorphic homomorphic = new Homomorphic(basePath);

            const string finder = "lat=";
            foreach (var line in lines)
            {
                if (!line.Contains("lat="))
                    continue;

                var positionLat = line.IndexOf("lat=");
                var lonString = line.Substring("lon=".Length, positionLat - finder.Length).Replace("\"", string.Empty)
                    .Trim();
                var startLat = positionLat + finder.Length;
                var latString = line.Substring(startLat, line.Length - 1 - startLat).Replace("\"", string.Empty).Trim();

                Position position = new Position
                {
                    Lon = double.Parse(lonString.Replace(".",",")),
                    Lat = double.Parse(latString.Replace(".", ","))
                };

                homomorphic.EncryptValues(position, out PositionEncrypted positionEncrypted);

                MemoryStream encryptedLon = new MemoryStream();
                positionEncrypted.Lon.Save(encryptedLon);

                MemoryStream encryptedLat = new MemoryStream();
                positionEncrypted.Lat.Save(encryptedLat);

                string lon64 = Convert.ToBase64String(encryptedLon.ToArray());
                string lat64 = Convert.ToBase64String(encryptedLat.ToArray());

                string toWrite = lon64 + "#" + lat64;
                string toWriteFinal = toWrite;
                for (int i = 0; i < 1500; i++)
                {
                    toWriteFinal = toWriteFinal + "#";
                }

                File.AppendAllText(Path.Combine(basePath, destinationFile), toWriteFinal);

                break;
            }
        }
    }
}