using System;
using System.Collections.Generic;
using System.Linq;

namespace Merkle_Hellman
{
    class SuperIncreasing
    {
        public List<int> s;
        private int a;
        private int p;
        public List<int> t;
        public SuperIncreasing(int jumlah)
        {
            int temp = 1;
            int tempT;
            s = new List<int>();
            t = new List<int>();
            for (int i = 0; i < jumlah; i++)
            {
                temp = (2 * temp) + i;
                s.Add(temp);
            }
            p = primeGen(s.Sum());
            Random rand = new Random();
            a = rand.Next(1, p-1);
            foreach (int i in s)
            {
                tempT = Modulo(a * i, p);
                t.Add(tempT);
            }
        }
        public SuperIncreasing(List<int> s, int a, int p)
        {
            int tempT;
            this.s = s;
            this.p = p;
            this.a = a;
            t = new List<int>();
            foreach (int i in s)
            {
                tempT = Modulo(a * i, p);
                t.Add(tempT);
            }
        }
        public SuperIncreasing(List<int> s)
        {
            int tempT;
            this.s = s;
            this.p = primeGen(s.Sum());
            Console.WriteLine("Bilangan Prima: " + p);
            Random rand = new Random();
            this.a = rand.Next(1, p-1);
            t = new List<int>();
            foreach (int i in s)
            {
                tempT = Modulo(a * i, p);
                t.Add(tempT);
            }
        }
        public static int Modulo(int a, int b)
        {
            return (a % b + b) % b;
        }
        private static int Gcd(int a, int b)
        {
            a = Modulo(a, b);
            int r0 = b;
            int r1 = a;
            int r2;
            int t0 = 0;
            int t1 = 1;
            int t2 = a;
            int q1;
            while (r1 > 1)
            {
                q1 = r0 / r1;
                r2 = r0 % r1;
                t2 = Modulo(t0 - (q1 * t1), b);
                r0 = r1;
                r1 = r2;
                t0 = t1;
                t1 = t2;
            }
            if (r1 != 1)
                throw new Exception("Matrix tidak memiliki GCD");
            return t2;
        }
        public static int primeGen(int angkaDasar)
        {
            bool find = false;
            int cek;
            int prima = angkaDasar - 1;

            while (!find)
            {
                cek = 0;
                prima++;
                for (int i = 2; i <= prima; i++)
                {
                    if (prima % i == 0)
                    {
                        cek++;
                    }
                    if (cek > 1)
                    {
                        break;
                    }
                }
                if (cek == 1)
                {
                    find = true;
                }
            }
            
            return prima;
        }
        public static List<int> Encryption(string text, List<int> publicKey)
        {
            int temp;
            int tempEncryption;
            int keyLength = publicKey.Count;
            int[] binaryTemp = new int[keyLength];
            int[] publicKeyArray = publicKey.ToArray();
            text = text.ToUpper();
            List<int> output = new List<int>();
            foreach (char c in text)
            {
                temp = c;
                tempEncryption = 0;
                for (int i = 0; i < keyLength; i++)
                {
                    binaryTemp[i] = (int)(temp / Math.Pow(2, keyLength - 1 - i));
                    if(binaryTemp[i]==1)
                        temp = (int)(temp - Math.Pow(2, keyLength - 1 - i));
                    tempEncryption = tempEncryption + (binaryTemp[i] * publicKeyArray[i]);
                }
                output.Add(tempEncryption);
            }
            return output;
        }
        public static string Decryption(List<int> input, List<int> privateKey, int a, int p)
        {
            string output = "";
            int temp;
            int tempDecryption;
            int inverseA = Gcd(a, p);
            int[] arrKey = privateKey.ToArray();
            int keyLength = privateKey.Count;
            int[] binaryTemp = new int[keyLength];
            Console.WriteLine();
            foreach (int item in input)
            {
                temp= Modulo(inverseA * item, p);
                tempDecryption = 0;
                for (int i = keyLength - 1; i >= 0; i--)
                {
                    binaryTemp[i] = temp / arrKey[i];
                    if (binaryTemp[i] == 1)
                        temp = (int)(temp - arrKey[i]);
                }
                for (int i = 0; i < keyLength; i++)
                {
                    tempDecryption = tempDecryption + (int)(binaryTemp[i] * Math.Pow(2, keyLength - 1 - i));
                }
                output = output + (char)tempDecryption;
            }
            return output;
        }
        public override string ToString()
        {
            string tempS = "S\t: ( ";
            string tempT = "T\t: ( ";
            foreach (int item in s)
            {
                tempS = tempS + item + " ";
            }
            tempS = tempS + ")";
            foreach (int item in t)
            {
                tempT = tempT + item + " ";
            }
            tempT = tempT + ")";
            return tempS + "\n" + tempT + "\nA\t: " + a + "\nP\t: " + p;
        }
    }
}
