using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RaspilCalc
{
   public class Bord
   {
       private string _bordName;
       private int _x;
       private int _y;
       private int _z;
       private int _count;
       private int _kromkaX;
       private int _kromkaY;
       public Material Mat { get; set; }
       private string stringKromka1 = "—";
       private string stringKromka2 = "=";
       public string BordName
       {
           get { return _bordName; }
           set { _bordName = value; }
       }
       public string MatName
       {
           get { return Mat.NameMaterial; }
           
       }
       /// <summary>
       /// Длина кромки для набора досок в миллиметрах
       /// </summary>
       public double KromkaLength
       {
           get { return ((_kromkaY*Y + _kromkaX*X)*Count)/1000.0000000; }
       }
       public int X
       {
           get { return _x; }
           set { _x = value; }
       }
       public int Y
       {
           get { return _y; }
           set { _y = value; }
       }
       public int Z
       {
           get { return _z; }
           set { _z = value; }
       }
       public int Count
       {
           get { return _count; }
           set { _count = value; }
       }
       public string KromkaX
       {
           get
           {
              if (_kromkaX == 1) return stringKromka1;
               if (_kromkaX == 2) return stringKromka2;
               return "";
           }
           set
           {
               if(value==stringKromka1) _kromkaX = 1;else if (value == stringKromka2) _kromkaX = 2;
               else
                   _kromkaX = 0;
           }
       }
       public string KromkaY
       {
           get
           {
               if (_kromkaY == 1) return stringKromka1;
               if (_kromkaY == 2) return stringKromka2;
               return "";
           }
           set
           {
               if (value == stringKromka1) _kromkaY = 1;
               else if (value == stringKromka2) _kromkaY = 2;
               else
                   _kromkaY = 0;
           }
       }

       /// <summary>
       /// суммарная площадь таких досок
       /// </summary>
       public double S
       {
           get { return Count*(X*Y)/1000000.0; }

       }

       /// <summary>
       /// Суммарная стоимость досок с учетом кол-ва. Расчет по метрам квадратным.
       /// </summary>
       public double Coast
       {
           get
           {
               try
               {
                   return S * Mat.M2Price;

               }
               catch (Exception)
               {

                   return 0.0;

               }
               //else return 0.0;
           }
           
       }

       public Bord()
       {
           this.BordName = "empty";
           this.Count = 0;
           this._kromkaX = 0;
           this._kromkaY = 0;
           this.Mat = new Material();
           this.Z = 0;
           X = 0;
           Y = 0;

       }

       public Bord(string name,int x, int krx,int y,int kry,int z,int count)
       {
           this.BordName = name;
           this.X = x;
           Y = y;
           Z = z;
           _kromkaY = kry;
           _kromkaX = krx;
           Count = count;
           Mat = new Material();
           
       }
       public Bord(string name, int x, int krx, int y, int kry, int z, int count,Material m)
       {
           this.BordName = name;
           this.X = x;
           Y = y;
           Z = z;
           _kromkaY = kry;
           _kromkaX = krx;
           Count = count;
          Mat = m;
          
       }
       public Bord(string name, int x, string krx, int y, string kry, int z, int count, Material m)
       {
           this.BordName = name;
           this.X = x;
           Y = y;
           Z = z;
           KromkaY = kry;
           KromkaX = krx;
           Count = count;
           Mat = m;

       }
       public Bord(string name, int x, string krx, int y, string kry, int z, int count)
       {
           this.BordName = name;
           this.X = x;
           Y = y;
           Z = z;
           KromkaY = kry;
           KromkaX = krx;
           Count = count;
           Mat = new Material();

       }
       public Bord(DataGridViewRow row )
       {
           var methodName = new StackTrace(false).GetFrame(0).GetMethod().Name;
           try
           {
               if(row.Cells.Count<6) return;
               this.BordName = row.Cells["BordNameDataGridViewTextBoxColumn"].Value.ToString();
               this.X = int.Parse(row.Cells["xDataGridViewTextBoxColumn"].Value.ToString());
               this.Y = int.Parse(row.Cells["yDataGridViewTextBoxColumn"].Value.ToString());
               this.KromkaX = row.Cells["kromkaXDataGridViewTextBoxColumn"].Value.ToString().Trim();
               this.KromkaY = row.Cells["kromkaYDataGridViewTextBoxColumn"].Value.ToString().Trim();
               this.Z = int.Parse(row.Cells["zDataGridViewTextBoxColumn1"].Value.ToString());
               this.Count = int.Parse(row.Cells["Count"].Value.ToString());
           }
           catch (Exception ex)
           {
               MessageBox.Show(ex.Message, methodName);
           }
       }
       public override bool Equals(object obj)
       {
           if (obj is Bord)
               return Equals((Bord)obj); // use Equals method below
           else
               return false;
       }

       /// <summary>
       /// Возвращает строку заголовков столбцов
       /// </summary>
       /// <returns></returns>
       public static string BordHeader(char sep)
       {
           return "Название" + sep + "X" + sep + "КромкаX" + sep + "Y" + sep + "КромкаY" + sep+"Толщ."+ sep + "Кол-во" + sep +
                  "Материал-Название/Артикл/Производитель";
       }
       public string ToString(char sep)
       {
           if(this.Mat!=null)return BordName+sep+X+sep+KromkaX+sep+Y+sep+KromkaY+sep+Z+sep+Count+sep+Mat.NameMaterial+"/"+Mat.Article+"/"+Mat.Manufactor;
           else return BordName + sep + X + sep + KromkaX + sep + Y + sep + KromkaY + sep + Z + sep + Count + sep;
       }

       public Bord FromString(string line, char sep,MaterialManager mmg)
       {
           var methodName = new StackTrace(false).GetFrame(0).GetMethod().Name;
           try
           {
               var words = line.Split(sep);
               if (words.Length == 8)
               {
                   this.BordName = words[0];
                   this.X = int.Parse(words[1]);
                   this.KromkaX = words[2];
                   this.Y = int.Parse(words[3]);
                   this.KromkaY = words[4];
                   this.Z = int.Parse(words[5]);
                   this.Count=int.Parse(words[6]);
                   Material newMat;
                   var Minfo = words[7].Split('/');
                   if (Minfo.Length < 2) newMat = new Material();
                   else
                   {
                       var mats = mmg.AllMaterials.Where(x => x.Z == this.Z &&
                           x.NameMaterial==Minfo[0] &&
                           x.Article==Minfo[1] &&
                           x.Manufactor==Minfo[2]);
                       if (mats.Count() == 0)
                       {
                           MessageBox.Show("Не найден материал в базе. " + Z + "мм: " + Minfo[0] + " " + Minfo[1] + " " +
                                           Minfo[2]);
                           newMat = new Material();
                       }
                       else
                       {
                           newMat = mats.First();
                       }
                       if (mats.Count() > 1) Log.Events(methodName, "Обнаружено более одного материала в базе по запросу из файла. " 
                           + Z + "мм: " + Minfo[0] + " " + Minfo[1] + " " +Minfo[2]);
                   }
                   this.Mat = newMat;
                  
               }
               return this;
           }
           catch (Exception ex)
           {
               MessageBox.Show(ex.Message, methodName);
               return this;
           }
       }
       public bool Equals(Bord other)
       {
           // add comparisions for all members here
           return this.BordName == other.BordName &&
                  this.X == other.X &&
                  this.Y == other.Y &&
                  this.Z == other.Z &&
                  this.KromkaX == other.KromkaX &&
                  this.KromkaY == other.KromkaY &&
                  this.Count == other.Count;
       }
       public static bool operator ==(Bord A, Bord B)
       {
           try
           {
               return A.Equals(B);
           }
           catch (Exception)
           {
               return A == null && B == null;
           }
       }
       public static bool operator !=(Bord A, Bord B)
       {
           return !(A == B);
       }
   }
}
