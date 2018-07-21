using Microsoft.Research.SEAL;
using System;
using System.IO;

namespace Homomorphic_Iota
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string destinationFile = "result.txt";
            string basePath =Path.Combine(Environment.CurrentDirectory, "..", "..");
            string[] lines = File.ReadAllLines(Path.Combine(basePath, "longlat_data.xml"));

            Homomorphic homomorphic = new Homomorphic(basePath);

            const string finder = "lat=";
            foreach (var line in lines)
            {
                if (!line.Contains("lat="))
                    continue;

                var positionLat = line.IndexOf("lat=");
                var lonString = line.Substring("lon=".Length, positionLat - finder.Length).Replace("\"", string.Empty).Trim();
                var startLat = positionLat + finder.Length;
                var latString = line.Substring(startLat, line.Length-1- startLat).Replace("\"", string.Empty).Trim();

                Position position = new Position
                {
                    Lon = double.Parse(lonString),
                    Lat = double.Parse(latString)
                };

                homomorphic.EncryptValues(position, out PositionEncrypted positionEncrypted);

                MemoryStream encryptedLon = new MemoryStream();
                positionEncrypted.Lon.Save(encryptedLon);

                MemoryStream encryptedLat = new MemoryStream();
                positionEncrypted.Lat.Save(encryptedLat);

                //File.Delete(Path.Combine(basePath, destinationFile));
                File.AppendAllText(Path.Combine(basePath, destinationFile), $"lon: {Environment.NewLine}");
                AppendAllBytes(Path.Combine(basePath, destinationFile), encryptedLon.ToArray());
                File.AppendAllText(Path.Combine(basePath, destinationFile),Environment.NewLine);
                File.AppendAllText(Path.Combine(basePath, destinationFile), $"lat: {Environment.NewLine}");
                AppendAllBytes(Path.Combine(basePath, destinationFile), encryptedLat.ToArray());
                File.AppendAllText(Path.Combine(basePath, destinationFile), $"{Environment.NewLine}");

                break;
            }           
        }

        public static void AppendAllBytes(string path, byte[] bytes)
        {
            //argument-checking here.

            using (var stream = new FileStream(path, FileMode.Append))
            {
                stream.Write(bytes, 0, bytes.Length);
            }
        }
    }
}