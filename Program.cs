using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;
namespace encrypt
{
    class Program
    {
        class Protection
        {
            public byte[] encode(string text, byte[] key, byte[] IV)
            {
                // Check user input
                if (text == null || text.Length <= 0)
                {
                    throw new ArgumentException("Text");
                }
                if (key == null || text.Length <= 0)
                {
                    throw new ArgumentException("text");
                }
                if (IV == null || text.Length <= 0)
                {
                    throw new ArgumentException("text");
                }
                byte[] data;
                // create a new instance of a class and key
                // also this set the vector for algorthim
                using (AesManaged code = new AesManaged())
                {
                    code.Key = key;
                    code.IV = IV;
                    using (MemoryStream mem = new MemoryStream())
                    {
                        using (CryptoStream crypto = new CryptoStream(mem, code.CreateEncryptor(code.Key,code.IV),CryptoStreamMode.Write))
                        {
                            using (StreamWriter swrite = new StreamWriter(crypto))
                            {
                                // This write the data to the stream
                                swrite.Write(text);
                            }
                            data = mem.ToArray();
                        }
                    }
                }
                // return the encrypted bytes from the memory stream
                return data;
            }

            public string Decrypt(byte[] ctext, byte[] key, byte[] IV)
            {
                // check values same as Encrypt
                if (ctext == null || ctext.Length <= 0)
                {
                    throw new Exception("text");
                }
                if (key == null || ctext.Length <= 0)
                {
                    throw new Exception("text");
                }
                if (IV == null || ctext.Length <= 0)
                {
                    throw new Exception("text");
                }
                string dData;
                // creating an instance for key and vector for alogorithim 
                using (AesManaged decode = new AesManaged())
                {
                    decode.Key = key;
                    decode.IV = IV;
                    using (MemoryStream mem = new MemoryStream(ctext))
                    {
                        using (CryptoStream decrypt = new CryptoStream(mem, decode.CreateDecryptor(decode.Key, decode.IV), CryptoStreamMode.Read))
                        {
                            using (StreamReader read = new StreamReader(decrypt))
                            {
                                // This read the decrypted bytes and place them in a string
                                dData = read.ReadToEnd();
                            }
                        }
                        
                    }
                }

                return dData;
            }
        }
        static void Main(string[] args)
        {
            Protection protect = new Protection();
            try
            {
                using (AesManaged aes = new AesManaged())
                {
                    Console.Write("please enter some text you want to Encrypt:");
                    string text = Convert.ToString(Console.ReadLine());
                    // encrypt the string to an array 
                    byte[] encrypt = protect.encode(text, aes.Key, aes.IV);
                    string eText = String.Empty;
                    foreach (var b in encrypt)
                    {
                        eText += b.ToString() + ", ";
                    }
                    Console.WriteLine(Environment.NewLine + $"Encrypted text: {eText}");
                    Console.WriteLine("Please hit enter to Continue");
                    Console.ReadLine();
                    // Now decrypt the bytes to string
                    string decrypted = protect.Decrypt(encrypt, aes.Key, aes.IV);
                    Console.WriteLine(Environment.NewLine + $"Decrypted text : {decrypted}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(Environment.NewLine + $"Error:{e.Message}");
            }

            
            Console.ReadLine();

        }
    }
}
