using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using RestSharp;
using Tangle.Net.Cryptography;
using Tangle.Net.Entity;
using Tangle.Net.Repository;
using Tangle.Net.Repository.DataTransfer;

namespace iotaTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string basePath = Path.Combine(Environment.CurrentDirectory, "..", "..");
            string resultText = File.ReadAllText(Path.Combine(basePath, "result.txt"));
            var repository = new RestIotaRepository(new RestClient("https://nodes.testnet.iota.org:443"));
            
            //Message = TryteString.FromUtf8String(resultText),
            //string message = "blubber" + DateTime.Now.Ticks;
            string message =
                "1These homomorphic encryption schemes are based on a hard computation problem known as Ring Learning with Errors. Here we will unpack what the Ring part of that title means. Essentially, data in these schemes is represented by polynomials both when it is encrypted (the ciphertext) and when it is unencrypted (the plaintext)." +
                "2These are almost the normal polynomials that everyone studies at school. Things like There are some differences, the first being that the coefficients are all whole numbers and are the remainder relative to some other whole number (that is ). Say , then this is like a 24 hour clock, where adding 6 hours to 21 takes you to 3. All the coefficients of the polynomials are treated like this." +
                "3Alternately, we can consider the numbers to range from -11 to 12, allowing us to negate numbers conveniently. Note that this is just a convenience factor - there is no difference between a \"remainder\" of -1 and a remainder of 23 (when dividing by 24)." +
                "4The second, and trickier, thing that is different about these polynomials is that this idea of using remainders applies not just to the coefficients of the polynomials, but also to the polynomials themselves." +
                "5We define a special polynomial called the polynomial modulus, and only consider the remainder of polynomials when they have been divided by this polynomial modulus. The specific form of this polynomial modulus in the FV scheme is where for some . For illustration here we will take , so the polynomial is ." +
                "6Because we are considering remainders with respect to , we only have to consider polynomials with powers from to . Any larger powers will be reduced by division by the polynomial modulus. This can also be understood by considering that , meaning that can be replaced by -1 to reduce the larger powers of into the range 0 to 15.";

            //const string address = "FHEBLOCKCHAINEDMOBILITYHACKATHONTHEBLOCKCHAINEDMOBILITYHACKATHONTHEBLOCKCHAINEDMO";
            //SendToChain(address, repository, message);

            const string address = "IHEBLOCKCHAINEDMOBILITYHACKATHONTHEBLOCKCHAINEDMOBILITYHACKATHONTHEBLOCKCHAINEDMO";
           // SendToChain(address, repository, message);
            GetFromChain(address, repository);
            Console.ReadKey();
        }

        private static void GetFromChain(string address, RestIotaRepository repository)
        {
            int depth = 3;
            int minWeightMagnitude = 9;
            try
            {
                TransactionHashList transactionsByAdress =
                    repository.FindTransactionsByAddresses(new List<Address> { new Address(address) });

                List<TransactionTrytes> transactionsTrytesList = repository.GetTrytes(transactionsByAdress.Hashes);

                Dictionary<int, Transaction> transactionPosition = new Dictionary<int, Transaction>();
                int lastIndex = 0;
                //foreach (Hash hash in transactionsByAdress.Hashes)
                //{
                    foreach (TransactionTrytes transactionTrytes in transactionsTrytesList)
                    {
                        Transaction transactionOne = Transaction.FromTrytes(transactionTrytes, transactionsByAdress.Hashes[0]);
                        transactionPosition.Add(transactionOne.CurrentIndex, transactionOne);
                        lastIndex = transactionOne.LastIndex;
                    }
                //}

                string combined = string.Empty;
                for (int i = 0; i < lastIndex; i++)
                {
                    combined  += transactionPosition[i].Fragment.Value;
                }

                string fertisch = TrytesToString(combined);
                Console.WriteLine(fertisch + " positiv");
            




            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                //throw;
            }

            //Hash hashAddress = new Hash(transactionsByAdress.Hashes[0].Value);
            //TransactionHashList bundleTransactions = repository.FindTransactionsByBundles(new List<Hash> {bundle.Hash});
            //foreach (Hash transactionsHash in bundleTransactions.Hashes)
            //{
            //    Hash hash = new Hash(transactionsHash.Value);
            //    var transactionsTrytes = repository.GetTrytes(bundleTransactions.Hashes);
            //    var transaction = Transaction.FromTrytes(transactionsTrytes[0], hash);
            //}
        }

        public static string TrytesToString(string inputTrytes)
        {
            var builder = new StringBuilder();

            for (var i = 0; i < inputTrytes.Length-1; i += 2)
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

        private static void SendToChain(string address, RestIotaRepository repository, string message)
        {
            Seed seed = new Seed(address);
            Transfer transfer = new Transfer
            {
                Address = new Address(address),
                Message = TryteString.FromUtf8String(message),
                ValueToTransfer = 0
            };

            Bundle bundle = new Bundle();
            bundle.AddTransfer(transfer);


            int depth = 3;
            int minWeightMagnitude = 9;
            try
            {
                var sendTransfer = repository.SendTransfer(seed, bundle, SecurityLevel.Medium, depth, minWeightMagnitude);

                //TransactionHashList bundleTransactions = repository.FindTransactionsByBundles(new List<Hash> {bundle.Hash});
                //foreach (Hash transactionsHash in bundleTransactions.Hashes)
                //{
                //    Hash hash = new Hash(transactionsHash.Value);
                //    var transactionsTrytes = repository.GetTrytes(bundleTransactions.Hashes);
                //    var transaction = Transaction.FromTrytes(transactionsTrytes[0], hash);
                //}

                TransactionHashList transactionsByAdress =
                    repository.FindTransactionsByAddresses(new List<Address> {new Address(address)});

                var transactionsTrytesAddress = repository.GetTrytes(transactionsByAdress.Hashes).First();
                Hash hashAddress = new Hash(transactionsByAdress.Hashes[0].Value);

                Transaction transactionOne = Transaction.FromTrytes(transactionsTrytesAddress, hashAddress);
                TransactionTrytes transactionTrytes = new TransactionTrytes("");
                TransactionTrytes test = transactionOne.ToTrytes();
                var text = test.Value;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                //throw;
            }
        }
    }
}
