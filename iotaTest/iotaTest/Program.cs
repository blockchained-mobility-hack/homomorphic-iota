using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            string text = File.ReadAllText(Path.Combine(basePath, "result.txt"));
            var repository = new RestIotaRepository(new RestClient("https://nodes.testnet.iota.org:443"));
            const string address = "THEBLOCKCHAINEDMOBILITYHACKATHONTHEBLOCKCHAINEDMOBILITYHACKATHONTHEBLOCKCHAINEDMO";

            Seed seed = new Seed(address);
            Transfer transfer = new Transfer
            {
                Address = new Address(address),
                Message = TryteString.FromUtf8String("blubber" + DateTime.Now.Ticks),
                //Message = TryteString.FromUtf8String("1Homomorphic Iota ist alles super duper cool, aber es geht nicht so wie ich will. Homomorphic Iota ist alles super duper cool, aber es geht nicht so wie ich will. Homomorphic Iota ist alles super duper cool, aber es geht nicht so wie ich will. Homomorphic Iota ist alles super duper cool, aber es geht nicht so wie ich will." +
                //                                     //"2Homomorphic Iota ist alles super duper cool, aber es geht nicht so wie ich will. Homomorphic Iota ist alles super duper cool, aber es geht nicht so wie ich will. Homomorphic Iota ist alles super duper cool, aber es geht nicht so wie ich will. Homomorphic Iota ist alles super duper cool, aber es geht nicht so wie ich will." +
                //                                     //"3Homomorphic Iota ist alles super duper cool, aber es geht nicht so wie ich will. Homomorphic Iota ist alles super duper cool, aber es geht nicht so wie ich will. Homomorphic Iota ist alles super duper cool, aber es geht nicht so wie ich will. Homomorphic Iota ist alles super duper cool, aber es geht nicht so wie ich will." +
                //                                     //"4Homomorphic Iota ist alles super duper cool, aber es geht nicht so wie ich will. Homomorphic Iota ist alles super duper cool, aber es geht nicht so wie ich will. Homomorphic Iota ist alles super duper cool, aber es geht nicht so wie ich will. Homomorphic Iota ist alles super duper cool, aber es geht nicht so wie ich will." +
                //                                     //"5Homomorphic Iota ist alles super duper cool, aber es geht nicht so wie ich will. Homomorphic Iota ist alles super duper cool, aber es geht nicht so wie ich will. Homomorphic Iota ist alles super duper cool, aber es geht nicht so wie ich will. Homomorphic Iota ist alles super duper cool, aber es geht nicht so wie ich will." +
                //                                     //"6Homomorphic Iota ist alles super duper cool, aber es geht nicht so wie ich will. Homomorphic Iota ist alles super duper cool, aber es geht nicht so wie ich will. Homomorphic Iota ist alles super duper cool, aber es geht nicht so wie ich will. Homomorphic Iota ist alles super duper cool, aber es geht nicht so wie ich will." +
                //                                     "7Homomorphic Iota ist alles super duper cool, aber es geht nicht so wie ich will. Homomorphic Iota ist alles super duper cool, aber es geht nicht so wie ich will. Homomorphic Iota ist alles super duper cool, aber es geht nicht so wie ich will. Homomorphic Iota ist alles super duper cool, aber es geht nicht so wie ich will."),
                ValueToTransfer = 0
            };

            Bundle bundle = new Bundle();
            bundle.AddTransfer(transfer);


            int depth = 3;
            int minWeightMagnitude = 9;
            try
            {
                var sendTransfer = repository.SendTransfer(seed, bundle, SecurityLevel.Medium, depth, minWeightMagnitude);

                TransactionHashList bundleTransactions = repository.FindTransactionsByBundles(new List<Hash> {bundle.Hash});
                foreach (Hash transactionsHash in bundleTransactions.Hashes)
                {
                    Hash hash = new Hash(transactionsHash.Value);
                    var transactionsTrytes = repository.GetTrytes(bundleTransactions.Hashes);
                    var transaction = Transaction.FromTrytes(transactionsTrytes[0], hash);
                    
                }

                TransactionHashList transactions = repository.FindTransactionsByAddresses(new List<Address> {new Address(address)});

                //var transactionsTrytes = repository.GetTrytes(transactions.Hashes).First();
                //Hash hash = new Hash(transactions.Hashes[0].Value);
                //var transaction = Transaction.FromTrytes(transactionsTrytes, hash);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                //throw;
            }

            Console.ReadKey();
        }
    }
}
