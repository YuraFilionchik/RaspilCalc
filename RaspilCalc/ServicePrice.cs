using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RaspilCalc
{
public    class ServicePrice
{
    public Dictionary<string, double> Catalog;
    private string filepath = "";

    public ServicePrice(string file)
    {var methodName = new StackTrace(false).GetFrame(0).GetMethod().Name;
        try
        {
            if(!File.Exists(file)) throw new Exception("File prices not found");
            var lines = File.ReadAllLines(file, Encoding.Default);
            Catalog=new Dictionary<string, double>();
            filepath = file;
            foreach (string line in lines)
            {
                var words = line.Split('=');
                if (words.Length != 2) continue;
                double D = 0.0;
                if(double.TryParse(words[1],out D))
                Catalog.Add(words[0],D);
            }

        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, methodName);

        }        
    }

    public void AddToCatalog(DataGridViewRow row)
    {
        var methodName = new StackTrace(false).GetFrame(0).GetMethod().Name;
        try
        {
            if(row.Cells.Count!=2)return;
            if(Catalog==null)return;
            double C = 0.0;
            if (row.Cells[1].Value!=null &&
                double.TryParse(row.Cells[1].Value.ToString(), out C) &&
               !Catalog.ContainsKey(row.Cells[0].Value.ToString()))

                Catalog.Add( row.Cells[0].Value.ToString(),C);
        }
        catch (Exception ex)
        {
           MessageBox.Show(ex.Message, methodName);
        }
    }

    public void SavePrice()
    {
        var methodName = new StackTrace(false).GetFrame(0).GetMethod().Name;
        try
        {
            File.WriteAllText(filepath, "");
            foreach (KeyValuePair<string, double> pair in Catalog)
            {
                File.AppendAllText(filepath, pair.Key + "=" + pair.Value + Environment.NewLine);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, methodName);
        }
    }
    public void SavePrice(Dictionary<string, double> catalog)
    {
        var methodName = new StackTrace(false).GetFrame(0).GetMethod().Name;
        try
        {
            File.WriteAllText(filepath,"");
            foreach (KeyValuePair<string, double> pair in catalog)
            {
                File.AppendAllText(filepath, pair.Key+"="+pair.Value+Environment.NewLine);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, methodName);
        }
    }
}
}
