using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SourceConsole.Utils
{
    public class IO
    {
        public IO()
        {
            _path = GetDataPath(); 
            _tempFile = _path + @"\clientes\client_temp.txt";
            _finalFile = _path+ @"\clientes\client_final.txt";
            _logPath = _path + @"\logs";
            _logFile = _path + @"\logs\log.txt";
            PathExist();
        }
        public string _path;
        public string _tempFile;
        public string _finalFile;
        public string _logPath;
        public string _logFile;
        public string GetDataPath()
        {
            String[] pathTemp = Directory.GetCurrentDirectory().Split("\\");
            string path = "";
            for (int i = 0; i < pathTemp.Length - 1; i++)
            {
                path += pathTemp[i];
                if(i < pathTemp.Length - 1)
                {
                    path += "\\";
                }
            }
            return path;
        }
        public void PathExist()
        {
            if (Directory.Exists(_path))
            {
                if (File.Exists(_finalFile))
                {
                    Console.WriteLine("Existe a tabela");
                }
                else
                {
                    using (var myFile = File.Create(_finalFile))
                    {
                        Console.WriteLine("Tabela criada");
                    }
                    File.WriteAllText(_finalFile, "[]");
                }
            }
            else
            {
                Directory.CreateDirectory(_path);
                PathExist();
            }
            if (Directory.Exists(_logPath))
            {
                if (File.Exists(_logFile)) { Console.WriteLine("Log existente"); }
                else 
                { 
                    var file = File.Create(_logFile);
                    file.Close();
                    Console.WriteLine("Log criado"); 
                }
            }
            else
            {
                Directory.CreateDirectory(_logPath);
                PathExist();
            }
        }
        public void Log(string log)
        {
            if (File.Exists(_logFile))
            {
                StreamWriter sw = File.AppendText(_logFile);
                sw.WriteLine(log);
                sw.Close();
            }
            else
            {
                var file = File.Create(_logFile);
                file.Close();
                Log(log);
            }
        }
        public int MinutesToMiliseconds(int min)
        {
            return (min * 60 * 100);
        }
    }
}
