using System;
using System.Collections.Generic;

namespace Merkle_Hellman
{
    class Program
    {
        static SuperIncreasing GeneratePublicKey()
        {
            int jumlah;
            int temp;
            int p;
            int a;
            List<int> privateKey = new List<int>();
            SuperIncreasing su;
            Console.Write("Masukan Jumlah Key : ");
            jumlah = int.Parse(Console.ReadLine());
            for (int i = 0; i < jumlah; i++)
            {
                Console.Write("S{0} : ", i+1);
                temp = int.Parse(Console.ReadLine());
                privateKey.Add(temp);
            }
            Console.Write("Masukan A : ");
            a = int.Parse(Console.ReadLine());
            Console.Write("Masukan P : ");
            p = int.Parse(Console.ReadLine());
            su = new SuperIncreasing(privateKey, a, p);
            return su;
        }
        static SuperIncreasing GenerateKey()
        {
            int jumlah;
            int temp;
            List<int> privateKey = new List<int>();
            SuperIncreasing su;
            Console.Write("Masukan Jumlah Key : ");
            jumlah = int.Parse(Console.ReadLine());
            for (int i = 0; i < jumlah; i++)
            {
                Console.Write("S{0} : ", i + 1);
                temp = int.Parse(Console.ReadLine());
                privateKey.Add(temp);
            }
            su = new SuperIncreasing(privateKey);
            return su;
        }
        public static string Encrypt()
        {
            int jumlah;
            int temp;
            List<int> publicKey = new List<int>();
            string input;
            List<int> output;
            string outputText = "";
            Console.Write("Masukan Kalimat\t: ");
            input = Console.ReadLine();
            Console.Write("Jumlah Key\t: ");
            jumlah = int.Parse(Console.ReadLine());
            for (int i = 0; i < jumlah; i++)
            {
                Console.Write("T{0}\t: ", i + 1);
                temp = int.Parse(Console.ReadLine());
                publicKey.Add(temp);
            }
            output = SuperIncreasing.Encryption(input, publicKey);
            outputText = "( ";
            foreach (int item in output)
            {
                outputText = outputText + item + " ";
            }
            outputText = outputText + ")";
            return outputText;
        }
        public static string Decrypt()
        {
            int a;
            int p;
            int jumlah;
            int temp;
            List<int> privateKey = new List<int>();
            List<int> input = new List<int>();
            string output;
            Console.Write("Jumlah Cipher\t: ");
            jumlah = int.Parse(Console.ReadLine());
            for (int i = 0; i < jumlah; i++)
            {
                Console.Write("Cipher {0}\t: ", i + 1);
                temp = int.Parse(Console.ReadLine());
                input.Add(temp);
            }
            Console.Write("a\t: ");
            a = int.Parse(Console.ReadLine());
            Console.Write("p\t: ");
            p = int.Parse(Console.ReadLine());
            Console.Write("Jumlah Key\t: ");
            jumlah = int.Parse(Console.ReadLine());
            for (int i=0; i<jumlah; i++)
            {
                Console.Write("S{0}\t: ",i+1);
                temp = int.Parse(Console.ReadLine());
                privateKey.Add(temp);
            }
            output = SuperIncreasing.Decryption(input, privateKey, a, p);
            return output;
        }
        static void Main(string[] args)
        {
            int pilih;
            int jumlahKey;
            SuperIncreasing generated;
            do
            {
                Console.Clear();
                Console.WriteLine("1. Generate Key Automatic");
                Console.WriteLine("2. Generate Public Key");
                Console.WriteLine("3. Encryption");
                Console.WriteLine("4. Decryption");
                Console.WriteLine("5. Exit Program");
                Console.Write("Masukan Pilihan : ");
                pilih = int.Parse(Console.ReadLine());
                switch (pilih)
                {
                    case 1:
                        Console.Write("Masukan Jumlah Key : ");
                        jumlahKey = int.Parse(Console.ReadLine());
                        generated = new SuperIncreasing(jumlahKey);
                        Console.WriteLine(generated.ToString());
                        break;
                    case 2:
                        generated = GenerateKey();
                        Console.WriteLine(generated.ToString());
                        break;
                    case 3:
                        Console.WriteLine("Encrypted\t: " + Encrypt());
                        break;
                    case 4:
                        Console.WriteLine("Decrypted\t: " + Decrypt());
                        break;
                    default:
                        break;
                }
                Console.ReadLine();
            } while (pilih != 5);
        }
    }
}
