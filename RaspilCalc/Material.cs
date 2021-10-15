using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaspilCalc
{
    /// <summary>
    /// S - площадь листа материала
    /// Size - строковое представление размера листа
    /// </summary>
    public class Material
    {
        public string NameMaterial
        {
            get { return _nameMaterial; }
            set { _nameMaterial = value; }
        }

        public string Article
        {
            get { return _article; }
            set { _article = value; }
        }

        public string Size
        {
            get { return _size; }
            set { _size = value; }
        }

        public int Z
        {
            get { return _z; }
            set { _z = value; }
        }

        public double ListPrice
        {
            get { return _listPrice; }
            set { _listPrice = value; }
        }

        public string Manufactor
        {
            get { return _manufactor; }
            set { _manufactor = value; }
        }

        private string _article = "";
        private string _manufactor = "";
        private int X;
        private int Y;
        private int _z;
        private double _listPrice;
        public double M2Price;
        private string _size;
        public double S;
        private string _nameMaterial;
       
        public Material()
        {
            this.NameMaterial = "default";
            this._article = "";
            this._manufactor = "";
            this.X = 2800;
            this.Y = 2070;
            this._z = 18;
            this.S = (X * Y) / 1000000.00;
            this._listPrice = 60;
           this.M2Price = _listPrice / this.S;
            this.Size = this.X.ToString() + "x" + this.Y.ToString();
        }
        public Material(string name, int x, int y, int z, double listPrice, double m2Price=0, string article = "", string manufactor = "")
        {
            this.NameMaterial = name;
            this._article = article;
            this._manufactor = manufactor;
            this.X = x;
            this.Y = y;
            this._z = z;
            this.S = (X * Y) / 1000000.00;
            this._listPrice = listPrice;
            if (m2Price == 0 && S != 0)
            {
                this.M2Price = _listPrice / this.S;
            }
            else this.M2Price = m2Price;
            this.Size = this.X.ToString() + "x" + this.Y.ToString();

        }
        public Material(string StringCSV)
        {
            try
            {
                if (!GetFromString(StringCSV)) throw new Exception("Ошибка чтения строки материала.");
            }
            catch (Exception ex)
            {
                Log.Errors("Material(stringCSV)",ex.Message);
                throw;
            }
        }
        public override string ToString()
        {
            return string.Format("Name={0}; Article={1}; Manufactor={2}; X={3}; Y={4}; Z={5}; ListPrice={6}; M2Price={7}\r\n", NameMaterial, _article, _manufactor, X, Y, _z, _listPrice, M2Price);
        }

        /// <summary>
        /// Получение материала из строки csv файла, сохраненного в программе
        /// </summary>
        /// <param name="csvString"></param>
        /// <returns>успех или нет</returns>
        private bool GetFromString(string csvString)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(csvString)) return false;
                var words = csvString.Split(';');
                if (words.Length < 8) return false;
                foreach (string word in words)
                {
                    var pair = word.Split('=');
                    if (pair.Length != 2) continue;
                    string value = pair[1].Trim();
                    switch (pair[0].Trim())
                    {
                        case "Name":
                            this.NameMaterial = value;
                            break;
                        case "Article":
                            this._article = value;
                            break;
                        case "Manufactor":
                            this._manufactor = value;
                            break;
                        case "X":
                            int.TryParse(value, out this.X);
                            break;
                        case "Y":
                            int.TryParse(value, out this.Y);
                            break;
                        case "Z":
                            int.TryParse(value, out this._z);
                            break;
                        case "ListPrice":
                            double.TryParse(value, out this._listPrice);
                            break;
                        case "M2Price":
                            double.TryParse(value, out this.M2Price);
                            break;
                    }
                }
                this.S = (X * Y) / 1000000.00;
                if (this.M2Price == 0 && S != 0) this.M2Price = this._listPrice / S;
                this.Size = this.X.ToString() + "x" + this.Y.ToString();
                return true;
            }
            catch (Exception ex)
            {
                Log.Errors("Парсинг строки CSV мвтериала", ex.Message);
                return false;
            }
        }


        public override bool Equals(object obj)
        {
            if ( obj is Material)
                return Equals((Material)obj); // use Equals method below
            else
                return false;
        }
        public bool Equals(Material other)
        {
          // add comparisions for all members here
             return this.NameMaterial == other.NameMaterial &&
                   this.Article == other.Article &&
                   this.Manufactor == other.Manufactor&&
                   this.Z==other.Z;
        }
        public static bool operator ==(Material A, Material B)
        {
            try
            {
                return A.Equals(B);
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static bool operator !=(Material A, Material B)
        {
            return !(A == B);
        }
    }

}
