using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CypherHilla
{

    class Program
    {

        #region AlphavitMock
        static readonly string abcEng = "abcdefghijklmnopqrstuvwxyz";
        #endregion
        static readonly string abcAll= "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZАБВГДЕЄЖЗИІЇйКЛМНОПРСТУФХЦЧШЩЬЮЯ1234567890 ?\"'";
        
        static void Main(string[] args)
        {              

            HillCipher hillCipher = new HillCipher();
            List<int> key = new List<int>();
            List<int> key_mock = new List<int>();
            List<int> plainText = new List<int>();
            List<int> plainText_Mock = new List<int>();
            List<int> cipher = new List<int>();
            string encode_rezult = "";
            string decode_rezult = "";

            #region Alphabet
            Console.WriteLine("Alphabet");
            Dictionary<int, char> alphabet = new Dictionary<int, char>();
            for (int i = 0; i < abcEng.Length; i++)
                alphabet.Add(i, abcEng[i]);
            #endregion

            #region AlphbetMock
            Dictionary<int, char> alphabet_Mock = new Dictionary<int, char>();
            for (int i = 0; i < abcAll.Length; i++)
                alphabet_Mock.Add(i, abcAll[i]);

            foreach (KeyValuePair<int, char> text in alphabet_Mock)
                Console.Write(text.Key + "-" + text.Value + " ");
            Console.WriteLine();
            #endregion
            string open_text_mock = "";
            var finalString = "";
            var chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZАБВГДЕЄЖЗИІЇйКЛМНОПРСТУФХЦЧШЩЬЮЯ1234567890 ?\"'";
            
            var random = new Random();
            #region Pay
            string open_text = "paymoremoney";
            #endregion
           

/*
            #region Key

            Console.WriteLine("Please, enter key text: ");
            string key_phrase_mock = Console.ReadLine().ToLower();
            string key_phrase = "rscrvcfvt";
            for (int i = 0; i < key_phrase.Length; i++)
                foreach (KeyValuePair<int, char> text in alphabet)
                    if (key_phrase[i] == text.Value)
                        key.Add(text.Key);
            #endregion
            #region Key_Mock
            for (int i = 0; i < key_phrase_mock.Length; i++)
                foreach (KeyValuePair<int, char> text in alphabet_Mock)
                    if (key_phrase_mock[i] == text.Value)
                        key_mock.Add(text.Key);
            #endregion*/
            Console.WriteLine("Enter:\n 1 - Encrypt\n 2 - Decrypt");
            int choice = Convert.ToInt32(Console.ReadLine()); 

            switch (choice)
            {
                case 1:
                    {                
                        Console.WriteLine("Encrypt HillCipher");
                        #region Text

                        Console.WriteLine("Please, enter your text:");
                        open_text_mock = Console.ReadLine();
                        var stringChars = new char[open_text_mock.Length];
                        using (StreamWriter sw = new StreamWriter("openText.txt"))
                        {
                            sw.WriteLine(open_text_mock);
                        }
                        /*for (int i = 0; i < open_text_mock.Length; i++)
                        {
                            for (int j = 0; j < 2; j++)
                                Console.WriteLine(open_text_mock);
                            Console.WriteLine(open_text_mock);
                        }*/

                        for (int i = 0; i < open_text.Length; i++)
                            foreach (KeyValuePair<int, char> text in alphabet)
                                if (open_text[i] == text.Value)
                                    plainText.Add(text.Key);

                        for (int i = 0; i < open_text_mock.Length; i++)
                            foreach (KeyValuePair<int, char> text in alphabet_Mock)
                                if (open_text_mock[i] == text.Value)
                                    plainText_Mock.Add(text.Key);
                        /////foreach (int i in plainText_Mock)
                        ///// Console.WriteLine(i);
                        #endregion

                        #region RandomMock

                        for (int i = 0; i < stringChars.Length; i++)
                        {
                            stringChars[i] = chars[random.Next(chars.Length)];
                        }
                        finalString = new String(stringChars);
                        using (StreamWriter sw = new StreamWriter("сypher.txt"))
                        {
                            sw.WriteLine(finalString);
                        }
                        //Console.WriteLine(finalString);
                        #endregion
                        #region Key

                        Console.WriteLine("Please, enter key text: ");
                        string key_phrase_mock = Console.ReadLine().ToLower();
                        string key_phrase = "rscrvcfvt";
                        for (int i = 0; i < key_phrase.Length; i++)
                            foreach (KeyValuePair<int, char> text in alphabet)
                                if (key_phrase[i] == text.Value)
                                    key.Add(text.Key);
                        #endregion

                        #region Key_Mock
                        for (int i = 0; i < key_phrase_mock.Length; i++)
                            foreach (KeyValuePair<int, char> text in alphabet_Mock)
                                if (key_phrase_mock[i] == text.Value)
                                    key_mock.Add(text.Key);
                        #endregion
                        ////
                        hillCipher.Encrypt_Mock(plainText_Mock, key_mock);
                        /////
                        cipher = hillCipher.Encrypt(plainText, key);
                       
                        Dictionary<int, char> rezult = new Dictionary<int, char>();
                        foreach (int i in cipher)
                            foreach (KeyValuePair<int, char> text in alphabet)
                                if (i == text.Key)
                                    encode_rezult += text.Value;
                       // Console.WriteLine(encode_rezult);                                       
                        Console.WriteLine(finalString);
                        Console.ReadLine();
                        using (StreamWriter sw = new StreamWriter("сypher.txt"))
                        {
                            sw.WriteLine(finalString);
                        }                        
                        Console.ReadLine();
                        
                        break;
                    }
                case 2:
                    {
                        string finalSrting = "";
                        List<int> decode = new List<int>();
                        Console.WriteLine("Decrypt HillCipher");                       
                        using (StreamReader sr = new StreamReader("сypher.txt"))
                        {
                            string char_rezult_decode = encode_rezult;
                            //string char_rezult_decode = sr.ReadToEnd();
                            Console.WriteLine("Our cipher from file: ");
                            finalString=sr.ReadToEnd();
                            Console.WriteLine(finalString);
                            

                            for (int i = 0; i < char_rezult_decode.Length; i++)
                                foreach (KeyValuePair<int, char> text in alphabet)
                                    if (char_rezult_decode[i] == text.Value)
                                        decode.Add(text.Key);                                                   
                        }
                        if (finalString == null)
                        {
                            var cipherD = hillCipher.Decrypt(decode, key);

                            Dictionary<int, char> rezult = new Dictionary<int, char>();
                            foreach (int i in cipherD)
                                foreach (KeyValuePair<int, char> text in alphabet)
                                    if (i == text.Key)
                                        decode_rezult += text.Value;
                            Console.WriteLine("Decode rezult:");
                        }
                        Console.WriteLine("Text after decoding");
                        using (StreamReader sw = new StreamReader("openText.txt"))
                        {
                            open_text_mock=sw.ReadToEnd();
                        }
                        Console.WriteLine(open_text_mock);
                        Console.ReadLine();                        
                        break;
                    }
                default:
                    Console.WriteLine("You enter wrong letter");
                    break;
            }            
        }
    }
}
