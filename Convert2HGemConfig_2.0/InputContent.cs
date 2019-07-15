using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace InputStream
{
    class InputContent
    {
        private string _path;
        private CheckInputStream checkInputStream = new CheckInputStream();
        public InputContent(string Path)
        {
            this._path = Path;
        }

        private bool checkEmtpyCategory(string lineString)
        {
            string[] array = lineString.Split(',');

            string value = array[0];

            if (value.Trim() == string.Empty)
            {
                return true;
            }

            return false;
        }

        public List<string> getInputContent()
        {
            List<string> col = new List<string>();
          
            string tmp = string.Empty;

            try
            {
                using (StreamReader reader = new StreamReader(_path))
                {
                    while (reader.Peek() >= 0)
                    {
                        tmp = reader.ReadLine().Trim();

                        if (checkEmtpyCategory(tmp))
                        {
                            continue;
                        }

                        if (!checkInputStream.CheckHGemConfig(tmp))
                        {
                            tmp = ";;;;;;;;" + tmp;
                        }
                        col.Add(tmp);

                    }
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
            return col;

        }

    }


    class CheckInputStream
    {
        private List<string> ecList = new List<string>();
        private List<string> vidList = new List<string>();
        private List<string> eventList = new List<string>();
        private List<string> reportList = new List<string>();
        private List<string> linkList = new List<string>();

        public CheckInputStream()
        {

        }


        public bool CheckHGemConfig(string lineString)
        {
            string[] arraySplit;
            string category = string.Empty;
            string key = string.Empty;
            bool result = false;

            try
            {
                arraySplit = lineString.Split(',');
                category = arraySplit[0].Trim().ToLower();
                key = arraySplit[1].Trim();

                if (category == "ec")
                {
                    if (!ecList.Contains(key))
                    {
                        ecList.Add(key);
                        result = true;
                    }
                }
                else if (category == "vid")
                {
                    if (!vidList.Contains(key))
                    {
                        vidList.Add(key);
                        result = true;
                    }
                }
                else if (category == "event")
                {
                    if (!eventList.Contains(key))
                    {
                        vidList.Add(key);
                        result = true;
                    }
                }
                else if (category == "report")
                {
                    if (!reportList.Contains(key))
                    {
                        reportList.Add(key);
                        result = true;
                    }
                }
                else if (category == "reportlink")
                {
                    if (!linkList.Contains(key))
                    {
                        linkList.Add(key);
                        result = true;
                    }
                }

            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }

            return result;
        }




    }
}
