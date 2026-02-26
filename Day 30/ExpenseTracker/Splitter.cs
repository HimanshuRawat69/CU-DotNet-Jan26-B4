using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker
{
    internal class Splitter
    {
        static List<string> SettleShare(Dictionary<string, double> expenses)
        {
            List<string> repo = new List<string>();//csv string

            //Creditors and Debitors
            Queue<KeyValuePair<string, double>> receivers = new Queue<KeyValuePair<string, double>>();
            Queue<KeyValuePair<string, double>> payers = new Queue<KeyValuePair<string, double>>();

            var totalexp = expenses.Values.Sum();
            var persons = expenses.Count;
            var equalShare = totalexp / persons;
            //populate payers and receivers queues
            foreach (var p in expenses)
            {
                if (p.Value > equalShare)
                {
                    receivers.Enqueue(new KeyValuePair<string, double>(p.Key, p.Value - equalShare));
                }
                else if (p.Value < equalShare)
                {
                    payers.Enqueue(new KeyValuePair<string, double>(p.Key, equalShare - p.Value));
                }
            }
            //settlement
            while (payers.Count > 0 && receivers.Count > 0)
            {
                var payer = payers.Dequeue();
                var receiver = receivers.Dequeue();

                var amount = Math.Min(payer.Value, receiver.Value);
                repo.Add($"{payer.Key}, {receiver.Key}, {amount}");

                if (payer.Value > amount)
                {
                    payers.Enqueue(new KeyValuePair<string, double>(payer.Key, Math.Abs(equalShare - payer.Value)));
                }
                if (receiver.Value > amount)
                {
                    receivers.Enqueue(new KeyValuePair<string, double>(receiver.Key, Math.Abs(equalShare - receiver.Value)));
                }

                //Console.WriteLine("jhbjhbhhbvh");
            }
            return repo;
        }
        static void Main(string[] args)
        {
            Dictionary<string, double> expenses = new Dictionary<string, double>
            {
                {"Sneha", 900},
                {"Chirag", 0},
                {"Apurva", 1290}
            };
            List<string> repo = SettleShare(expenses);
            //settlement item - from to amount
            foreach (var payment in repo)
            {
                Console.WriteLine(payment);
            }

        }
    }
}