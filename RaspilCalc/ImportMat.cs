using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RaspilCalc
{
   
    public partial class ImportMat : Form
    {
        public List<int[]> Sizes;
        public List<string> Manufactors;
        Config cfg=new Config();
        private MaterialManager mng;
        public ImportMat()
        {
            InitializeComponent();
      
        }

        public ImportMat(MaterialManager mng)
        {
            
                InitializeComponent();
                Sizes = cfg.GetSizes();
                Manufactors = cfg.GetManufactors();
                if (Manufactors != null) cbManufactor.Items.AddRange(Manufactors.ToArray());
                if (cbManufactor.Items.Count != 0) cbManufactor.SelectedIndex = 0;
            if (Size != null)
                foreach (int[] size in Sizes)
                {
                    string sizeString = size[0] + "x" + size[1];
                    cbSize.Items.Add(sizeString);
                }
                if (cbSize.Items.Count != 0) cbSize.SelectedIndex = 0;
            this.mng = mng;
            tbPriceM2.TextChanged += tbPriceM2_TextChanged;
            tbPrice.TextChanged += tbPrice_TextChanged;
            tbZ.TextChanged+=tbZ_TextChanged;
        }

        void tbPrice_TextChanged(object sender, EventArgs e)
        {
            double PriceM2 = 0;
            if (!double.TryParse(tbPrice.Text, out PriceM2)) errorProvider.SetError(tbPrice, "Неверный формат числа. Дробную часть нужно вводить через запятую");
            else errorProvider.SetError(tbPrice, null);
        }

        void tbPriceM2_TextChanged(object sender, EventArgs e)
        {
            double PriceM2 = 0;
            if (!string.IsNullOrEmpty(tbPriceM2.Text) && !double.TryParse(tbPriceM2.Text, out PriceM2)) errorProvider.SetError(tbPriceM2, "Неверный формат числа. Дробную часть нужно вводить через запятую");
            else errorProvider.SetError(tbPriceM2,null);

        }


        private void tbZ_TextChanged(object sender, EventArgs e)
        {
            int Z = 0;
            if (!int.TryParse(tbZ.Text, out Z)) errorProvider.SetError(tbZ, "Должно быть целое число");
            else errorProvider.SetError(tbZ, null);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void ImportMat_Load(object sender, EventArgs e)
        {

        }

        private void cbSize_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var methodName = new StackTrace(false).GetFrame(0).GetMethod().Name;
            try
            { errorProvider.Clear();
                double PriceM2 =0;
                double price;
                double.TryParse(tbPriceM2.Text, out PriceM2);
                string[] size; 
                if(cbSize.Text.ToLower().Contains("x"))
                size= cbSize.Text.ToLower().Split('x');
                else if (cbSize.Text.ToLower().Contains("х"))
                size = cbSize.Text.ToLower().Split('х');
                else {errorProvider.SetError(cbSize,"Неверный формат размера, пример: 2800x2070");return;}
                int x, y,z=-1;
                if(!int.TryParse(size[0].Trim(),out x) || !int.TryParse(size[1].Trim(),out y))
                { errorProvider.SetError(cbSize, "Неверный формат размера, пример: 2800x2070"); return;}
                if (!int.TryParse(tbZ.Text, out z) || z<0)
                { errorProvider.SetError(tbZ, "Должно быть целое положительное цисло"); return; }
                if (!double.TryParse(tbPrice.Text, out price))
                { errorProvider.SetError(tbPrice, "Неверный формат числа. пример 60,1"); return; }
                mng.AddMaterial(tbName.Text,x,y,z,price,
                    PriceM2,tbArticle.Text,cbManufactor.Text);
                cfg.AddSize(cbSize.Text.Trim());
                cfg.AddManufactor(cbManufactor.Text);
                lbConsole.Text = tbName.Text + "\nдобавлен.";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, methodName);
            }
        }
    }
}
