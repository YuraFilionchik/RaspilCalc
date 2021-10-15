using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RaspilCalc
{
 public   class Config
    {
        private IniFile cfg;

        #region keys

        private string materialsPath = "MaterialPath";
        private string bordsPath = "BordsPath";
        private string ServicePricePath = "ServicePricePath";
        private string LastPathSection = "LastPaths";
     private string LastFilePro100 = "LastFile";
        private string ManufactorsSection = "Manufactors";
        private string SizeSection = "Sizes";
     private string LastProject = "LastProjectFile";
     private string LastWorkDir = "WorkDir";
      //  private string LastSave = "LastSavesPath";

        #endregion
        public Config()
        {
            cfg = new IniFile("config.ini");
        }

        public string LastMaterialPaths()
        {
            string path = "";
            var methodName = new StackTrace(false).GetFrame(0).GetMethod().Name;
            try
            {
                if (cfg.KeyExists(materialsPath, LastPathSection)) path = cfg.ReadINI(LastPathSection, materialsPath);
               return path;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, methodName);
                return path;
            }
        }
        public void SaveMaterialPath(string path)
        {

            var methodName = new StackTrace(false).GetFrame(0).GetMethod().Name;
            try
            {
                cfg.Write(LastPathSection, materialsPath, path);
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, methodName);

            }
        }

     public List<string> GetManufactors()
     {
         var methodName = new StackTrace(false).GetFrame(0).GetMethod().Name;
         try
         {
             var keys = cfg.GetAllKeys(ManufactorsSection);
             if(keys.Length==0) return new List<string>();
             List<string> manufs=new List<string>();
             foreach (string key in keys)
             {
                 manufs.Add(cfg.ReadINI(ManufactorsSection,key));
             }
             return manufs;
         }
         catch (Exception ex)
         {
             MessageBox.Show(ex.Message, methodName);
             return new List<string>();
         }
     }

     public void AddManufactor(string manufactor)
     {
         var methodName = new StackTrace(false).GetFrame(0).GetMethod().Name;
         try
         {
             var keys = cfg.GetAllKeys(ManufactorsSection);
             string newkey = (int.Parse(keys.Last()) + 1).ToString();
             foreach (string key in keys)
             {
                 if(cfg.ReadINI(ManufactorsSection,key)==manufactor) return;
             }
             cfg.Write(ManufactorsSection,newkey,manufactor);
         }
         catch (Exception ex)
         {
             MessageBox.Show(ex.Message, methodName);
         }
     }
        public List<int[]> GetSizes()
        {

            var methodName = new StackTrace(false).GetFrame(0).GetMethod().Name;
            try
            {
                var sizeKeys=cfg.GetAllKeys(SizeSection);
                if(sizeKeys.Length==0) return new List<int[]>();
                List<int[]>  Sizes=new List<int[]>();
                foreach (string key in sizeKeys)
                {
                    int[] size = new int[2];
                    var value = cfg.ReadINI(SizeSection, key);
                    var words = value.Split('x');
                    if(words.Length!=2) continue;
                    if(!int.TryParse(words[0],out size[0]) || !int.TryParse(words[1], out size[1])) continue;
                    Sizes.Add(size);

                }
                return Sizes;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, methodName);
                return new List<int[]>();
            }
        }

        public void AddSize(string size)
        {

            var methodName = new StackTrace(false).GetFrame(0).GetMethod().Name;
            try
            {
                var sizeKeys = cfg.GetAllKeys(SizeSection);
                string keyName = (int.Parse(sizeKeys.Last()) + 1).ToString();
                //check if size exist
                foreach (string key in sizeKeys)
                {
                    if(cfg.ReadINI(SizeSection,key)==size) return;
                }
                //size not found
                //add new
                cfg.Write(SizeSection, keyName, size);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, methodName);
            }
        }

     public void SaveLastFilePro100(string file)
     {
         cfg.Write(LastPathSection,LastFilePro100,file);
     }

     public string GetLastProjectFile()
     {
         string path = "";
         var methodName = new StackTrace(false).GetFrame(0).GetMethod().Name;
         try
         {
             if (cfg.KeyExists(LastProject, LastPathSection)) path = cfg.ReadINI(LastPathSection, LastProject);
             return path;
         }
         catch (Exception ex)
         {
             MessageBox.Show(ex.Message, methodName);
             return path;
         }
     }
     public string GetLastFilePro100()
     {
         if (cfg.KeyExists(LastFilePro100, LastPathSection))
             return cfg.ReadINI(LastPathSection, LastFilePro100);
         else return "";
     }

     public void SaveLastProjectFile(string file)
     {
         cfg.Write(LastPathSection, LastProject, file);
     }

     public string GetWorkDir()
     {
         var methodName = new StackTrace(false).GetFrame(0).GetMethod().Name;
         try
         {
             if (cfg.KeyExists(LastWorkDir, LastPathSection)) return cfg.ReadINI(LastPathSection, LastWorkDir);
             else return "";
         }
         catch (Exception ex)
         {
             MessageBox.Show(ex.Message, methodName);
             return "";
         }
     }
     public void SaveWorkDir(string dir)
     {
 cfg.Write(LastPathSection,LastWorkDir,dir);        
     } 
 
 }

    public class IniFile
    {
        string Path; //Имя файла.

        [DllImport("kernel32", CharSet = CharSet.Unicode)] // Подключаем kernel32.dll и описываем его функцию WritePrivateProfilesString
        static extern long WritePrivateProfileString(string Section, string Key, string Value, string FilePath);

        [DllImport("kernel32", CharSet = CharSet.Unicode)] // Еще раз подключаем kernel32.dll, а теперь описываем функцию GetPrivateProfileString
        static extern int GetPrivateProfileString(string Section, string Key, string Default, StringBuilder RetVal, int Size, string FilePath);

        [DllImport("kernel32", CharSet = CharSet.Unicode)] // Еще раз подключаем kernel32.dll, а теперь описываем функцию GetPrivateProfileString
        static extern int GetPrivateProfileString(string Section, string Key, string Default, IntPtr RetVal, int Size, string FilePath);

        public string[] GetAllKeys(string Section)
        {
            IntPtr RetVal = Marshal.AllocHGlobal(4096 * sizeof(char));
            // GetPrivateProfileString(Section, null, "", RetVal, 255, Path);
            string t = "";
            List<string> result = new List<string>();
            int n = GetPrivateProfileString(Section, null, null, RetVal, 4096 * sizeof(char), Path) - 1;
            if (n > 0)
                t = Marshal.PtrToStringUni(RetVal, n);

            Marshal.FreeHGlobal(RetVal);

            return t.Split('\0');

        }

        // С помощью конструктора записываем пусть до файла и его имя.
        public IniFile(string IniPath)
        {

            Path = new FileInfo(IniPath).FullName.ToString();
        }

        //Читаем ini-файл и возвращаем значение указного ключа из заданной секции.
        public string ReadINI(string Section, string Key)
        {
            var RetVal = new StringBuilder(255);
            GetPrivateProfileString(Section, Key, "", RetVal, 255, Path);

            return RetVal.ToString();
        }
        //Записываем в ini-файл. Запись происходит в выбранную секцию в выбранный ключ.
        public void Write(string Section, string Key, string Value)
        {
            WritePrivateProfileString(Section, Key, Value, Path);
        }

        //Удаляем ключ из выбранной секции.
        public void DeleteKey(string Key, string Section = null)
        {
            Write(Section, Key, null);
        }
        //Удаляем выбранную секцию
        public void DeleteSection(string Section = null)
        {
            Write(Section, null, null);
        }
        //Проверяем, есть ли такой ключ, в этой секции
        public bool KeyExists(string Key, string Section = null)
        {
            return ReadINI(Section, Key).Length > 0;
        }
    }
}
