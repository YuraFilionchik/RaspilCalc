using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RaspilCalc
{
    public class MaterialManager
    {
        public List<Material> AllMaterials;
        public string file = "Materials.csv";
        private Config Cfg=new Config();
        public MaterialManager(string filepath)
        {
            var methodName = new StackTrace(false).GetFrame(0).GetMethod().Name;
            try
            {
                AllMaterials=new List<Material>();
                file = filepath;
                if (!File.Exists(filepath)) throw new Exception("Файл " + filepath + " не найден");

                var lines = File.ReadAllLines(filepath);
                if (lines.Length == 0) throw new Exception("Файл материалов пуст");
                foreach (var line in lines)
                {
//из каждой строки создаем материал и добавляем в общую коллекцию AllMaterials
                    Material mat=new Material(line);
                    AllMaterials.Add(mat);
                }
            }
            catch (Exception ex)
            {
                Log.Errors(methodName,ex.Message);
                MessageBox.Show(ex.Message, methodName);
            }
        }

        public MaterialManager()
        {
            var methodName = new StackTrace(false).GetFrame(0).GetMethod().Name;
            try
            {
                AllMaterials = new List<Material>();
                
                if (!File.Exists(file)) throw new Exception("Файл " + file + " не найден");

                var lines = File.ReadAllLines(file);
                if (lines.Length == 0) throw new Exception("Файл материалов пуст");
                foreach (var line in lines)
                {
                    //из каждой строки создаем материал и добавляем в общую коллекцию AllMaterials
                    Material mat = new Material(line);
                    AllMaterials.Add(mat);
                }
            }
            catch (Exception ex)
            {
                Log.Errors(methodName, ex.Message);
                MessageBox.Show(ex.Message, methodName);
            }
        }
        public void AddMaterial(string name, int x, int y, int z, double listPrice, double m2Price=0, string article = "",
            string manufactor = "")
        {
            var methodName = new StackTrace(false).GetFrame(0).GetMethod().Name;
            try
            {
                Material M = new Material(name, x, y, z, listPrice, m2Price, article, manufactor);
                if(AllMaterials.Contains(M)) return;
                File.AppendAllText(file, M.ToString());
                AllMaterials.Add(M);
                Cfg.AddManufactor(manufactor);
                Program.MF.bsMaterials.ResetBindings(false);
                }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, methodName);
            }
        }

        public void RemoveMaterial(string name, string article)
        {
            var methodName = new StackTrace(false).GetFrame(0).GetMethod().Name;
            try
            {
                var M = AllMaterials.First(x => x.NameMaterial == name && x.Article == article);
                AllMaterials.Remove(M);
               //write Allmaterials replase file
               File.WriteAllText(file,"");
                foreach (Material material in AllMaterials)
                {
                    File.AppendAllText(file,material.ToString());
                }
                Program.MF.bsMaterials.ResetBindings(false);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, methodName);
            }
        }
        public void ImportMaterials(string filepath)
        {
            var methodName = new StackTrace(false).GetFrame(0).GetMethod().Name;
            try
            {
                var lines = File.ReadAllLines(filepath);
                if(lines.Length==0) throw new Exception("Файл пустой");
                foreach (string line in lines)
                {
                    var words = line.Split('\t');
                    if(words.Length<6) continue;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, methodName);
            }
        }
    }
}
