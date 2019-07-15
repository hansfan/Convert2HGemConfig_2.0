using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Convert2HGemConfig
{
    class Program
    {
        static void Main(string[] args)
        {


            if (args.Length != 2)
            {
                Console.WriteLine("Please,key in InputFullPath and OutputFullPath.");
            }
            else
            {
                string inputPath = args[0].Trim().ToString();

                MainProcess(inputPath);


            }

            Console.WriteLine("Success!");
            Console.Read();

        }

        static public void MainProcess(string input)
        {
            InputStream.InputContent inputStream = new InputStream.InputContent(input);
            Convertor convertor = new Convertor();
            List<List<string>> HGemconfigList = convertor.Process(inputStream.getInputContent());

            CheckHGemConfig check = new CheckHGemConfig();
            String result = check.Check(HGemconfigList);

            OutputStream outputStream = new OutputStream();
            string output = outputStream.outputFullPath(input);
            outputStream.toHGemConfig(output, result);


        }




    }

    class OutputStream
    {
        public OutputStream()
        {

        }

        public void toHGemConfig(string fullPath, string obj)
        {
            try
            {
                using (StreamWriter output = new StreamWriter(fullPath))
                {
                    output.WriteLine(obj);
                }

            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }


        public string outputFullPath(string input)
        {
            string result = string.Empty;
            string fileName = string.Empty;
            int pPosition = -1;
            try
            {
                string[] arr = input.Split('\\');
                fileName = arr[arr.Length - 1];
                pPosition = fileName.LastIndexOf('.');
                fileName = fileName.Substring(0, pPosition) + ".cfg";

                for (int i = 0; i < arr.Length - 1; i++)
                {
                    result += arr[i]+"\\";
                }
                result += fileName;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return result;
        }


    }


}
