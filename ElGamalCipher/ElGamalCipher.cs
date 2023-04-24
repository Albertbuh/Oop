namespace ElGamalC
{
    public class ElGamalCipher 
    {
        public byte[]? message_bytes = null;
        public byte[]? cipher_bytes = null;
        protected internal HashSet<int> prime_numbers = new HashSet<int>() {
            2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61, 67, 71, 73, 79, 83, 89, 97,
            101, 103, 107, 109, 113, 127, 131, 137, 139, 149, 151, 157, 163, 167, 173, 179, 181, 191, 193, 197, 199,
            211, 223, 227, 229, 233, 239, 241, 251, 257, 263, 269, 271, 277, 281, 283, 293,
            307, 311, 313, 317, 331, 337, 347, 349, 353, 359, 367, 373, 379, 383, 389, 397,
            401, 409, 419, 421, 431, 433, 439, 443, 449, 457, 461, 463, 467, 479, 487, 491, 499,
            503, 509, 521, 523, 541, 547, 557, 563, 569, 571, 577, 587, 593, 599,
            601, 607, 613, 617, 619, 631, 641, 643, 647, 653, 659, 661, 673, 677, 683, 691,
            701, 709, 719, 727, 733, 739, 743, 751, 757, 761, 769, 773, 787, 797,
            809, 811, 821, 823, 827, 829, 839, 853, 857, 859, 863, 877, 881, 883, 887,
             907, 911, 919, 929, 937, 941, 947, 953, 967, 971, 977, 983, 991, 997,
        };
        public int p, g, k; //Open key
        public int x; //Secret key
        public int a, b, y; //params for encyphering
        public List<int> g_list = new List<int>();
        protected internal int block_size = 2;
        protected internal string cipher_text = "";
        protected internal string message_text = "";
        public void SetCipherText(string text) => cipher_text = text;
        public void SetMessageText(string text) => message_text = text;
        protected internal Random rand = new Random();
        
        /// <summary>
        /// Fast way to set all neccessary variables
        /// </summary>
        /// <param name="p"></param>
        /// <param name="g"></param>
        /// <param name="k"></param>
        /// <param name="x"></param>
        public void SetParameters(int p, int g = 0, int k = 0, int x = 0)
        {
            this.p = p;
            Random rand = new Random();
            if (g == 0)
            {
                CountGRoots(p);
                int ind = rand.Next(0, g_list.Count);
                g = g_list[ind];
            }
            else
                this.g = g;
            if( k == 0 || k >= p-1 || GCD(k, p-1) != 1)
            {
                k = p - 2;
                while (GCD(k, p - 1) != 1)
                    k--;
            }
            if (x >= p - 1 || x <= 0)
                x = rand.Next(0, p - 1);

            this.g = g;
            this.k = k;
            this.x = x;
        }
        /// <summary>
        /// Check if number is prime
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        private bool IsPrimeNumber(int num)
        {
            for (int i = 2; i * i <= num; i++)
            {
                if (num % i == 0)
                    return false;
            }
            return true;
        }
        /// <summary>
        /// x^z mod m = ?
        /// </summary>
        /// <returns></returns>
        private int PowMod(int x, int z, int m)
        {
            int res = 1;
            while (z != 0)
            {
                while (z % 2 == 0)
                {
                    z >>= 1; //z = z div 2
                    x = (x * x) % m;
                }
                z = z - 1;
                res = (res * x) % m;
            }
            return res;
        }
        /// <summary>
        /// Factorization of number
        /// </summary>
        /// <param name="p"></param>
        /// <returns> a = p1^a1*p2^a2*p3^a3...pn^an</returns>
        private int[] Factorization(int p)
        {
            List<int> factorization = new List<int>();
            int n = p;
            for (int i = 2; i * i <= n; i++)
            {
                if (n % i == 0)
                {
                    factorization.Add(i);
                    while (n % i == 0)
                        n = n / i;
                }
            }
            if (n > 1)
                factorization.Add(n);
            return factorization.ToArray();
        }
        /// <summary>
        /// Get list of primordial roots
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        private List<int>? CountGRoots(int p)
        {
            int phi = CountEulerFunc(p);
            int[] dividers = Factorization(phi);
            g_list.Clear();
            for (int i = 2; i <= p; i++)
            {
                bool IsOk = true;
                for (int j = 0; j < dividers.Length; j++)
                    IsOk &= PowMod(i, phi / dividers[j], p) != 1;
                if (IsOk)
                    g_list.Add(i);
            }

            return g_list.Count > 0 ? g_list : null;
        }

        public List<int>? GetGRoots(int p)
        {
            if (p > 0)
                return CountGRoots(p);
            else
                return null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns>GCD(a,b)</returns>
        private int GCD(int a, int b)
        {
            int res = 1;
            int max = a > b ? a : b;
            for (int i = 2; i * i <= max; i++)
            {
                while (a % i == 0 && b % i == 0)
                {
                    res = res * i;
                    a /= i;
                    b /= i;
                }
                while (a % i == 0)
                    a = a / i;
                while (b % i == 0)
                    b = b / i;
            }
            return res;
        }
        /// <summary>
        /// Get Euler Function of "p"
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        private int CountEulerFunc(int p)
        {
            int result = p;
            for (int i = 2; i * i <= p; i++)
            {
                if (p % i == 0)
                {
                    while (p % i == 0)
                        p = p / i;
                    result = result - result / i;
                }
            }
            if (p > 1)
                result = result - result / p;
            return result;
        }
        /// <summary>
        /// y = g^x mod p
        /// </summary>
        /// <param name="g"></param>
        /// <param name="x"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        private int CountFunc(int g, int x, int p)
        {
            return PowMod(g, x, p);
        }
        /// <summary>
        /// Return size of cipher block according to "p" value
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        private int GetSize(int p)
        {
            const uint one_byte_max = Byte.MaxValue;
            const uint two_byte_max = UInt16.MaxValue;
            const uint four_byte_max = UInt32.MaxValue;
            return (uint)p switch
            {
                <= one_byte_max => 1,
                <= two_byte_max => 2,
                <= four_byte_max => 4
            };
        }
        public int SetSize(int p)
        {
            block_size = GetSize(p);
            return block_size;
        }

        /// <summary>
        /// Return array of bytes from Int value
        /// </summary>
        /// <param name="num"></param>
        /// <param name="size"></param>
        /// <returns>bytes[0] = 1-8bits, bytes[1] = 9-16[bits], bytes[2] = 17-24bits, bytes[3] = 25-32bits</returns>
        private byte[] GetBytes(int num, int size)
        {
            byte[] bytes = new byte[size];
            for (int i = 0; i < size; i++)
            {
                bytes[i] = (byte)num;
                num >>= 8;
            }
            return bytes;
        }

        /// <summary>
        /// Check if user is enable to cypher
        /// </summary>
        /// <returns></returns>
        public bool Validation()
        {
            bool validation_flag = false;

            if (!prime_numbers.Contains(p) && !IsPrimeNumber(p))
                Console.WriteLine($"P number \"{p}\" is not prime number");
            else if (p < 255)
              Console.WriteLine($"P number \"{p}\" is less than bit size, it can lead to data loss");
            else if (x >= p - 1 || x <= 1)
              Console.WriteLine($"X number \"{x}\" is not correct");
            else if (k >= p - 1 || k <= 1)
              Console.WriteLine($"K number \"{k}\" is not correct");
            else if (GCD(p - 1, k) != 1)
              Console.WriteLine($"K number \"{k}\" doesn't satisfy condition: GCD(p-1,k) = 1");
            else if (message_bytes == null)
              Console.WriteLine($"Error: Please, choose the file for encription");
            else
                validation_flag = true;

            return validation_flag;
        }
        /// <summary>
        /// Encryption
        /// </summary>
        /// <returns> array ob ciphered bytes</returns>
        public byte[]? Encryption(string path_to_file = "")
        {
            if(!path_to_file.Equals(""))
              message_bytes = File.ReadAllBytes(path_to_file);

            if (Validation() && message_bytes != null)
            {
                y = CountFunc(g, x, p);
                a = PowMod(g, k, p);
                int bY = PowMod(y, k, p); //b = bY * (m % p);
                int ind = 0;
                cipher_bytes = new byte[message_bytes.Length * 2 * block_size];
                foreach (byte m in message_bytes)
                {
                    byte[] bytes = GetBytes(a, block_size);
                    for (int i = bytes.Length - 1; i >= 0; i--)
                        cipher_bytes[ind++] = bytes[i];

                    b = (bY * m) % p;

                    bytes = GetBytes(b, block_size);
                    for (int i = bytes.Length - 1; i >= 0; i--)
                        cipher_bytes[ind++] = bytes[i];
                }
                return cipher_bytes;
            }
            else
                return null;
        }
        /// <summary>
        /// m = (b/a^x) mod p
        /// </summary>
        /// <param name="m"></param>
        private byte CountM(int phi)
        {
            int m = PowMod(a, x * (phi - 1), p);
            m = b * m % p;
            return (byte)m;
        }
        public byte[]? Decryption(string path_to_file = "")
        {
            if(!path_to_file.Equals(""))
              message_bytes = File.ReadAllBytes(path_to_file);

            if (Validation() && message_bytes != null)
            {
                y = CountFunc(g, x, p);
                a = PowMod(g, k, p);
                int phi = CountEulerFunc(p);
                int ind = 0;
                int deblock_size = 2 * block_size;
                cipher_bytes = new byte[message_bytes.Length / deblock_size];
                for (int i = 0; i < message_bytes.Length; i += deblock_size)
                {
                    b = 0;
                    for (int j = block_size; j < deblock_size; j++)
                    {
                        b += message_bytes[i + j] << (8 * (deblock_size - 1 - j));
                    }
                    cipher_bytes[ind++] = CountM(phi);
                }
                return cipher_bytes;
            }
            else
                return null;
        }
    }
}
