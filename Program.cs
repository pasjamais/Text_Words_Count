using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Text_Words_Count
{
    internal class Program
    {
        static Dictionary<string, int> words_count = new Dictionary<string, int>();
        static string text_file_name;
        static string outputfile_name;

        /// <summary>
        /// Loops through the lines of a text file, presenting them to the Line_Processing method
        /// </summary>
        private static void Process_Input_File()
        {
            int i = 1; // line number in the file

            using (StreamReader reader = new StreamReader(text_file_name))
            {
                while (Line_Processing(reader.ReadLine()) != -1)
                {
                    Console.Write($"File lines processed:\t\t{i}\r");
                    i++;
                }
             Console.WriteLine();
            }
        }

        /// <summary>
        /// Writes word-count pairs sorted in descending order to the output file
        /// </summary>
        private static void Write_Output_File()
        {
            using (StreamWriter sw = new StreamWriter(outputfile_name, false))
                foreach (var item in words_count.OrderByDescending(key => key.Value))
                {
                    sw.WriteLine(item.Key + "\t" + item.Value);
                }
            Console.WriteLine($"Pairs \"word - quantity\" are written to the file {outputfile_name}");
        }

        /// <summary>
        /// Takes the string s as an argument, parses it into words and counts their number,
        /// writing this information into the words_count dictionary
        /// </summary>
        private static int Line_Processing(string? s)
        {
            if (s == null)
                return -1;
            else
            {
                char[] delimiterCharsArray = $"1234567890!\"#%&()*,–\'+-=^~`|<>./:;?@[\\]_{{}} «»\t\r\n".ToCharArray();
                int position;
                StringBuilder line = new(s.Trim(delimiterCharsArray).ToLower());
                do
                {
                    string word;
                    position = line.ToString().IndexOfAny(delimiterCharsArray);
                    if (position >= 0)
                    {
                        word = line.ToString().Substring(0, position + 1).Trim(delimiterCharsArray);
                        line = new(line.ToString().Substring(position).Trim(delimiterCharsArray));
                    }
                    else
                    {
                        word = line.ToString();
                    }
                    if (word == "")
                    {
                        continue;
                    }
                    if (words_count.ContainsKey(word))
                    {
                        words_count[word]++;
                    }
                    else
                    {
                        words_count.Add(word, 1);
                    }

                } while (position > -1 && line.Length > 0);
                return 0;
            }
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            if (args.Length == 0)
            {
                System.Console.WriteLine("Enter the name (path and name) of the file as an argument.");
                System.Console.WriteLine("Example: Text_Words_Count Myfile.txt");
            }
            else
            {
                text_file_name = args[0];
                Console.WriteLine($"Selected file: {text_file_name}");
                if (File.Exists(text_file_name))
                {
                    outputfile_name = Path.GetFileNameWithoutExtension(text_file_name) + "_output.txt";
                    Process_Input_File();
                    Write_Output_File();
                }
                else
                {
                    System.Console.WriteLine("The specified file was not found.");
                }
            }
            System.Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}

