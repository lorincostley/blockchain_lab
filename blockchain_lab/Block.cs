using System.Text;
using System.Security.Cryptography;

namespace blockchain_lab
{
    internal class Block
    {
        public int Index { get; set; }
        public string PreviousHash { get; set; }
        public string TimeStamp { get; set; }
        public List<Transaction> Transactions { get; set; }
        public string Hash { get; set; }
        public int Nonce { get; set; }

        public Block(int index, string timestamp, List<Transaction> transactions, string previousHash = "")
        {
            this.Index = index;
            this.PreviousHash = previousHash;
            this.TimeStamp = timestamp;
            this.Transactions = transactions;
            this.Nonce = 0;
            this.Hash = CalculateHash();
        }

        public string CalculateHash()
        {
            string blockData = this.Index + this.PreviousHash + this.TimeStamp + this.Transactions.ToString() + Nonce;
            byte[] blockBytes = Encoding.ASCII.GetBytes(blockData);
            byte[] hashBytes = SHA256.Create().ComputeHash(blockBytes);
            return BitConverter.ToString(hashBytes).Replace("-", "");
        }

        public void Mine(int difficulty)
        {
            while (this.Hash.Substring(0, difficulty) != new string('0', difficulty))
            {
                this.Nonce++;
                this.Hash = this.CalculateHash();
                Console.WriteLine("Mining: " + this.Hash);
            }
            Console.WriteLine("Block has been Mined: " + this.Hash);
        }
    }
}
