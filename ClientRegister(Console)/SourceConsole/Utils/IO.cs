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
            _path = @"C:\Users\gabri\Desktop\projetos\Prova Técnica\Gerenciamento de Cliente\clientes";
            _tempFile = @"C:\Users\gabri\Desktop\projetos\Prova Técnica\Gerenciamento de Cliente\clientes\client_temp.txt";
            _finalFile = @"C:\Users\gabri\Desktop\projetos\Prova Técnica\Gerenciamento de Cliente\clientes\client_final.txt";
            _logPath = @"C:\Users\gabri\Desktop\projetos\Prova Técnica\Gerenciamento de Cliente\logs";
            _logFile = @"C:\Users\gabri\Desktop\projetos\Prova Técnica\Gerenciamento de Cliente\logs\log.txt";
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
                path += pathTemp[i] + "\\";
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
