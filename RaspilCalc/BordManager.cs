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
 public   class BordManager
 {
     public List<Bord> AllBords;

     public BordManager()
     {
         AllBords=new List<Bord>();
     }
     public BordManager(string file)
     {
         var methodName = new StackTrace(false).GetFrame(0).GetMethod().Name;
         try
         {
             AllBords=new List<Bord>();
             var lines = File.ReadAllLines(file, System.Text.Encoding.Default);
             for (int i = 0; i < lines.Count(); i++)
             {
                 Bord bord = new Bord();
                 var words = lines[i].Split(';'); //subwords
                 if (words.Length < 1) { MessageBox.Show("file format error..."); return; }
                 //"";"800";"—";"375";"—";"18";"1";""
                 bord.BordName = words[0].Trim('"');
                 bord.X = int.Parse(words[1].Trim('"'));
                 bord.Y = int.Parse(words[3].Trim('"'));
                 bord.Z = int.Parse(words[5].Trim('"'));
                 bord.KromkaX = words[2].Trim('"');
                 bord.KromkaY = words[4].Trim('"');
                 bord.Count = int.Parse(words[6].Trim('"'));
                 AllBords.Add(bord);
             }
         }
         catch (Exception ex)
         {
             Log.Errors(methodName,ex.Message);
             MessageBox.Show(ex.Message, methodName);
         }
     }
        /// <summary>
        /// Подсчет статистики для указанного списка деталей
        /// </summary>
        /// <param name="bords">список деталей</param>
        /// <returns></returns>
     public BordsInfoStruct GetInfoBords(List<Bord> bords)
     {
         var methodName = new StackTrace(false).GetFrame(0).GetMethod().Name;
         BordsInfoStruct bordsInfo=new BordsInfoStruct();
         try
         {  double s = bords[0].Mat.S; //площадь одного листа материала
             foreach (Bord bord in bords)
             {
                 bordsInfo.S += bord.S; //подсчет суммарной площади деталей
                 bordsInfo.Count += bord.Count; //общее кол-во деталей
                 bordsInfo.KromkaLength += bord.KromkaLength; //обшая длина кромки всех деталей
             }
             bordsInfo.DSPcount=(int)Math.Ceiling((bordsInfo.S + bordsInfo.S * 0.05) / s); //примерная оценка кол-ва листов материала
             bordsInfo.RestS = bordsInfo.DSPcount * s - (bordsInfo.S * 1.05); //примерное кол-во остатков материала от целых листов
             return bordsInfo;
         }
         catch (Exception ex)
         {
             MessageBox.Show(ex.Message, methodName);
             return bordsInfo;
         }
        
     }

     public List<Bord> GetBordsFromRows(DataGridViewRowCollection rows)
     {
         var methodName = new StackTrace(false).GetFrame(0).GetMethod().Name;
            try
            {
                List<Bord> newList=new List<Bord>();
                foreach (DataGridViewRow row in rows)
                {
                   // if(row.)
                    newList.Add(new Bord(row));
                }
                return newList;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, methodName);
                return new List<Bord>();
            }
     }

     public List<List<Bord>> SelectByZsize()
     {
         var methodName = new StackTrace(false).GetFrame(0).GetMethod().Name;
         List<List<Bord>> resultList=new List<List<Bord>>();
         try
         {
             List<int> sizes=new List<int>();
             foreach (Bord bord in AllBords)
             if(!sizes.Contains(bord.Z)) sizes.Add(bord.Z);

             foreach (int size in sizes)
             {
                 resultList.Add(AllBords.FindAll(x=>x.Z==size));
             }
             return resultList;
         }
         catch (Exception ex)
         {
             MessageBox.Show(ex.Message, methodName);
             return resultList;
         }
     }

     public List<List<Bord>> selectByMat()
     {
         var methodName = new StackTrace(false).GetFrame(0).GetMethod().Name;
         List<List<Bord>> resultList = new List<List<Bord>>();
         try
         {
             List<Material> materials = new List<Material>();
             foreach (Bord bord in AllBords)
                 if (!materials.Contains(bord.Mat)) materials.Add(bord.Mat);

             foreach (Material mat in materials)
             {
                 resultList.Add(AllBords.FindAll(x => x.Mat == mat));
             }
             return resultList;
         }
         catch (Exception ex)
         {
             MessageBox.Show(ex.Message, methodName);
             return resultList;
         }
     }

     public void SaveBordsToCsv(string file, List<Bord> bords)
     {
         var methodName = new StackTrace(false).GetFrame(0).GetMethod().Name;
         try
         {
             string context = Bord.BordHeader(';')+Environment.NewLine;
             foreach (Bord bord in bords)
             {
                 context += bord.ToString(';') + Environment.NewLine;
             }
             File.WriteAllText(file, context,Encoding.Default);
         }
         catch (Exception ex)
         {
             MessageBox.Show(ex.Message, methodName);
         }
     }
 }
          

    public struct BordsInfoStruct
    {
        /// <summary>
        /// Общая площадь досок, м2
        /// </summary>
        public double S;
        /// <summary>
        /// общая длина кромки, м
        /// </summary>
        public double KromkaLength;
        public int Count;
        /// <summary>
        ///  //кол-во целых дсп листов
        /// </summary>
        public int DSPcount;
        /// <summary>
        /// Остаток от целого листа ДСП, м2
        /// </summary>
        public double RestS;
    }
   

}

