using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RaspilCalc
{
    public static class Log
    {
        public static void Errors(string who, string text)
        {
            AddLine("Errors.txt", who + " => " + text);
        }
        public static void Events(string who, string text)
        {
            AddLine("Events.txt", who + " => " + text);
        }
        private static void AddLine(string filename, string line)
        {
            try
            {
                int log_size = 100000;
                //пишем все сообщения, генерируемые службой во время работы, в локальный файл на диске
                FileStream fs1 = new FileStream(filename, FileMode.Append);
                long lenght = fs1.Length;
                fs1.Dispose();
                if (lenght >= log_size) //log_size - предельный размер лог-файла в байтах
                {
                    File.Move(filename,
                        filename + "_" + DateTime.Now.ToShortDateString() + "." + DateTime.Now.Hour + "." +
                        DateTime.Now.Minute + "." + DateTime.Now.Second + @".old");
                }
                FileStream fs2 = new FileStream(filename, FileMode.Append);
                StreamWriter sw = new StreamWriter(fs2);
                sw.WriteLine(DateTime.Now.ToString() + ": " + line);
                sw.Close();
                fs2.Dispose();
            }
            catch (Exception ex)
            {
                // MessageBox.Show("Ошибка записи лога");
            }
        }

        public static void invokeControlText(Control control, string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {

            }
            else
            {
                if (control.InvokeRequired) control.Invoke(new Action<string>(s => control.Text = s), text);
                else control.Text = text;
            }

        }
    }
    
}
