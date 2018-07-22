using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Research.SEAL;
using RestSharp;
using Tangle.Net.Entity;
using Tangle.Net.Repository;
using Tangle.Net.Repository.DataTransfer;

namespace Homomorphic_Iota
{
    public class IotaHelperWithSpecialPower
    {        
        private static RestIotaRepository _Repository;

        public IotaHelperWithSpecialPower()
        {
            _Repository = new RestIotaRepository(new RestClient("https://nodes.testnet.iota.org:443"));
        }

        public PositionEncrypted GetPosition(string address)
        {
            int depth = 3;
            int minWeightMagnitude = 9;
            try
            {
                TransactionHashList transactionsByAdress =
                    _Repository.FindTransactionsByAddresses(new List<Address> { new Address(address) });

                List<TransactionTrytes> transactionsTrytesList = _Repository.GetTrytes(transactionsByAdress.Hashes);

                Dictionary<int, Transaction> transactionPosition = new Dictionary<int, Transaction>();
                int lastIndex = 0;
                foreach (TransactionTrytes transactionTrytes in transactionsTrytesList)
                {
                    Transaction transactionOne = Transaction.FromTrytes(transactionTrytes, transactionsByAdress.Hashes[0]);
                    transactionPosition.Add(transactionOne.CurrentIndex, transactionOne);
                    lastIndex = transactionOne.LastIndex;
                }

                string combined = string.Empty;
                for (int i = 0; i <= lastIndex; i++)
                {
                    combined += transactionPosition[i].Fragment.Value;
                }

                string fertisch = TrytesToString(combined);
                string[] lines = fertisch.Split(new[] {"#"},StringSplitOptions.None);

                PositionEncrypted positionEncrypted = new PositionEncrypted();
                positionEncrypted.Lon = new Ciphertext();
                positionEncrypted.Lon.Load(new MemoryStream(Convert.FromBase64String(lines[0])));

                positionEncrypted.Lat = new Ciphertext();
                positionEncrypted.Lat.Load(new MemoryStream(Convert.FromBase64String(lines[1])));

                return positionEncrypted;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }

        public static string TrytesToString(string inputTrytes)
        {
            var builder = new StringBuilder();

            for (var i = 0; i < inputTrytes.Length - 1; i += 2)
            {
                // get a trytes pair

                var firstValue = TryteToDecimal(inputTrytes[i]);
                var secondValue = TryteToDecimal(inputTrytes[i + 1]);
                var decimalValue = firstValue + secondValue * 27;

                builder.Append((char)decimalValue);
            }

            return builder.ToString();
        }

        private static int TryteToDecimal(char tryte)
        {
            if (tryte == '9')
                return 0;

            return tryte - 'A' + 1;
        }
    }
}
