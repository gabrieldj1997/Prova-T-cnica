using Source__API_.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;

namespace Source__API_.Utils
{
    public class IO
    {
        public IO()
        {
            _path = GetDataPath();
            _tempFile = _path + "clientes\\client_temp.txt";
            _finalFile = _path + "clientes\\client_final.txt";
            _logPath = _path + "log";
            _logFile = _logPath + "\\logConsole.txt";
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
            if (Directory.Exists(_path + "clientes"))
            {
                if (File.Exists(_path + "clientes\\client_final.txt")) { Console.WriteLine("Existe temporaria"); }
                else { File.Create(_path + "clientes\\client_final.txt"); Console.WriteLine("Tabela criada"); }
            }
            else
            {
                Directory.CreateDirectory(_path + "\\clientes");
                PathExist();
            }
            if (Directory.Exists(_logPath))
            {
                if (File.Exists(_logFile)) { Console.WriteLine("Log existente"); }
                else { File.Create(_logFile); Console.WriteLine("Log criado"); }
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
                File.Create(_logFile);
                Log(log);
            }
        }

    }
}
