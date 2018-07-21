using System;
using System.IO;
using Microsoft.Research.SEAL;

namespace Homomorphic_Iota
{
    class Homomorphic
    {
        private string _BasePath;

        public Homomorphic(string basePath)
        {
            _BasePath = basePath;
        }

        public void Do(double lon1, double lat1, out MemoryStream encryptedLon, out MemoryStream encryptedLat)
        {
            var parms = new EncryptionParameters();
            parms.PolyModulus = "1x^2048 + 1";
            parms.CoeffModulus = DefaultParams.CoeffModulus128(2048);
            parms.PlainModulus = 1 << 8;
            var context = new SEALContext(parms);

            var encoder = new FractionalEncoder(context.PlainModulus, context.PolyModulus, 64, 32);

            //gerate key
            //var keygen = new KeyGenerator(context);
            //PublicKey publicKey = keygen.PublicKey;
            //SecretKey secretKey = keygen.SecretKey;

            //var stream = new StreamWriter(Path.Combine(_BasePath, "public.key"));
            //publicKey.Save(stream.BaseStream);
            //stream = new StreamWriter(Path.Combine(_BasePath, "secret.key"));
            //secretKey.Save(stream.BaseStream);

            // read key
            var stream = new StreamReader(Path.Combine(_BasePath, "public.key"));
            PublicKey publicKey = new PublicKey();
            publicKey.Load(stream.BaseStream);

            stream = new StreamReader(Path.Combine(_BasePath, "secret.key"));
            SecretKey secretKey = new SecretKey();
            secretKey.Load(stream.BaseStream);


            var encryptor = new Encryptor(context, publicKey);
            var evaluator = new Evaluator(context);
            var decryptor = new Decryptor(context, secretKey);

            // encode
            var encodedLon = encoder.Encode(lon1);
            var encodedLat = encoder.Encode(lat1);

            Ciphertext clpLon = new Ciphertext();
            Ciphertext clpLat = new Ciphertext();
            encryptor.Encrypt(encodedLon, clpLon);
            encryptor.Encrypt(encodedLat, clpLat);

            encryptedLon = new MemoryStream();
            clpLat.Save(encryptedLon);

            encryptedLat = new MemoryStream();
            clpLat.Save(encryptedLat);


            return;

            /*
            To illustrate the concept of noise budget, we print the budgets in the fresh 
            encryptions.
            */
            //Console.WriteLine("Noise budget in encrypted1: {0} bits",
            //    decryptor.InvariantNoiseBudget(encrypted1));
            //Console.WriteLine("Noise budget in encrypted2: {0} bits",
            //    decryptor.InvariantNoiseBudget(encrypted2));

            /*
            As a simple example, we compute (-encrypted1 + encrypted2) * encrypted2.
            */

            /*
            Negation is a unary operation.
            */
            //evaluator.Negate(encrypted1);

            /*
            Negation does not consume any noise budget.
            */
            //Console.WriteLine("Noise budget in -encrypted1: {0} bits",
            //    decryptor.InvariantNoiseBudget(encrypted1));

            /*
            Addition can be done in-place (overwriting the first argument with the result,
            or alternatively a three-argument overload with a separate destination variable
            can be used. The in-place variants are always more efficient. Here we overwrite
            encrypted1 with the sum.
            */
            //evaluator.Add(encrypted1, encrypted2);

            /*
            It is instructive to think that addition sets the noise budget to the minimum
            of the input noise budgets. In this case both inputs had roughly the same
            budget going on, and the output (in encrypted1) has just slightly lower budget.
            Depending on probabilistic effects, the noise growth consumption may or may 
            not be visible when measured in whole bits.
            */
            //Console.WriteLine("Noise budget in -encrypted1 + encrypted2: {0} bits",
            //    decryptor.InvariantNoiseBudget(encrypted1));

            /*
            Finally multiply with encrypted2. Again, we use the in-place version of the
            function, overwriting encrypted1 with the product.
            */
            //evaluator.Multiply(encrypted1, encrypted2);

            /*
            Multiplication consumes a lot of noise budget. This is clearly seen in the
            print-out. The user can change the PlainModulus to see its effect on the
            rate of
            noise budget consumption.
            */
            //Console.WriteLine("Noise budget in (-encrypted1 + encrypted2) * encrypted2: {0} bits",
            //    decryptor.InvariantNoiseBudget(encrypted1));

            /*
            Now we decrypt and decode our result.
            */

            var plainResult = new Plaintext();
            Console.Write("Decrypting result: ");


            //Ciphertext ziv = new Ciphertext();
            var dataStream = new StreamReader(Path.Combine(Environment.CurrentDirectory, "..", "..", "data"));
            string testString = dataStream.ReadToEnd();
            Plaintext plainencata = new Plaintext("FFx^2047 + FFx^2045 + FFx^2043 + FFx^2041 + FFx^2040 + FFx^2038 + FFx^2036 + FFx^2035 + FFx^2034 + FFx^2033 + FFx^2031 + FFx^2029 + 1x^3 + 1x^1 + 1");
            //plainencata.Load(dataStream.BaseStream);

            //ziv.Load(dataStream.BaseStream);

            double latte = encoder.Decode(plainencata);
            //decryptor.Decrypt(ziv, plainResult);
            //decryptor.Decrypt(encrypted1, plainResult);
            Console.WriteLine($"Done: {plainResult}");

            /*
            Print the result plaintext polynomial.
            */
            Console.WriteLine($"Plaintext polynomial: {plainResult}");

            /*
            Decode to obtain an integer result.
            */
            //Console.WriteLine($"Decoded integer: {encoder.DecodeInt32(plainResult)}");

            /*
            We finish by running garbage collection to make sure all local objects are
            destroyed and memory returned to the memory pool. This is very important to
            ensure correct behavior of the SEAL memory pool in .NET applications.
            */
            GC.Collect();
        }
    }
}
