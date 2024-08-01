using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_Zamdau.Logger
{
    internal class Log
    {
        public static bool SaveLog(string text)
        {
            try
            {
                string path = Directory.GetCurrentDirectory();

                path = path.Remove(path.Length - 6) + "API_Zamdau\\Logger";

                if (Directory.Exists(path))
                {
                    path = $"{path}\\log_{DateTime.Now:d}.txt".Replace("/", "_");
                    using StreamWriter wr = new (path, true);
                    wr.WriteLine($"{DateTime.Now:HH:mm:ss} : {text}");
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

        }
    }
}
