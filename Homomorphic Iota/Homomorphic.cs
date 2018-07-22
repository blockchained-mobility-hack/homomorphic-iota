using System;
using System.IO;
using Microsoft.Research.SEAL;

namespace Homomorphic_Iota
{
    public class Homomorphic
    {
        private string _BasePath;

        public Homomorphic(string basePath)
        {
            _BasePath = basePath;
        }

        public void EncryptValues(Position position, out PositionEncrypted positionEncrypted)
        {
            var encryptor = new Encryptor(GetContext(), GetPublicKey());

            PositionEncoded positionEncoded = GetEncodedPosition(position);
            positionEncrypted = GetEncrpytedPosition(positionEncoded, encryptor);
        }

        private PublicKey GetPublicKey()
        {
            var stream = new StreamReader(Path.Combine(_BasePath, "public.key"));
            PublicKey publicKey = new PublicKey();
            publicKey.Load(stream.BaseStream);
            return publicKey;
        }

        private SecretKey GetSecretKey()
        {
            var stream = new StreamReader(Path.Combine(_BasePath, "secret.key"));
            SecretKey secretKey = new SecretKey();
            secretKey.Load(stream.BaseStream);
            return secretKey;
        }

        private PositionEncrypted GetEncrpytedPosition(PositionEncoded position, Encryptor encryptor)
        {
            PositionEncrypted positionEncrypted = new PositionEncrypted();
            encryptor.Encrypt(position.Lon, positionEncrypted.Lon);
            encryptor.Encrypt(position.Lat, positionEncrypted.Lat);

            return positionEncrypted;
        }

        private PositionEncoded GetEncodedPosition(Position position)
        {
            var context = GetContext();

            var encoder = GetEncoder(context);

            //gerate key
            //var keygen = new KeyGenerator(context);
            //PublicKey publicKey = keygen.PublicKey;
            //SecretKey secretKey = keygen.SecretKey;

            //var stream = new StreamWriter(Path.Combine(_BasePath, "public.key"));
            //publicKey.Save(stream.BaseStream);
            //stream = new StreamWriter(Path.Combine(_BasePath, "secret.key"));
            //secretKey.Save(stream.BaseStream);

            var positionEncoded = new PositionEncoded
            {
                Lon = encoder.Encode(position.Lon),
                Lat = encoder.Encode(position.Lat)
            };

            return positionEncoded;
        }

        private FractionalEncoder GetEncoder(SEALContext context)
        {
            var encoder = new FractionalEncoder(context.PlainModulus, context.PolyModulus, 64, 32);
            return encoder;
        }

        private SEALContext GetContext()
        {
            var parms = new EncryptionParameters();
            parms.PolyModulus = "1x^2048 + 1";
            parms.CoeffModulus = DefaultParams.CoeffModulus128(2048);
            parms.PlainModulus = 1 << 8;
            var context = new SEALContext(parms);
            return context;
        }

        public bool IsIn(Position searchPosition)
        {
            var encoded = GetEncodedPosition(searchPosition);
            var evaluator = new Evaluator(GetContext());

            PositionEncrypted positionEncrypted = GetFromIota();

            evaluator.SubPlain(positionEncrypted.Lon, encoded.Lon);
            evaluator.SubPlain(positionEncrypted.Lat, encoded.Lat);

            var decryptor = new Decryptor(GetContext(), GetSecretKey());

            Plaintext plainResultLon = new Plaintext();
            Plaintext plainResultLat = new Plaintext();
            decryptor.Decrypt(positionEncrypted.Lon, plainResultLon);
            decryptor.Decrypt(positionEncrypted.Lat, plainResultLat);

            var encoder = GetEncoder(GetContext());
            double resultLon = encoder.Decode(plainResultLon);
            double resultLat = encoder.Decode(plainResultLat);

            var diff = Math.Abs(resultLon) + Math.Abs(resultLat);
            if (diff < 0.007)
                return true;

            return false;
        }

        private PositionEncrypted GetFromIota()
        {
            const string address = "IOTAFFFBLOCKCHAINEDMOBILITYHACKATHONTHEBLOCKCHAINEDMOBILITYHACKATHONTHEBLOCKCHAIN";

            IotaHelperWithSpecialPower iotaHelper = new IotaHelperWithSpecialPower();
            PositionEncrypted encrypted = iotaHelper.GetPosition(address);

            return encrypted;
        }
    }
}