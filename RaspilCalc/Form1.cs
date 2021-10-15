using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RaspilCalc
{
    public partial class MainForm : Form
    {
        private string materialsfile = "Materials.csv";
        private string servicepriceFile = "ServicePrice.csv";
        public ServicePrice Services;
        public Config cfg;
        public MaterialManager MManager;
        public BordManager BManager;
        private ImportMat AddMaterials;
        public Thread MakingStat;
       // public List<Bord> SelectedBords; 
        public MainForm()
        {
            InitializeComponent();
            
           MManager=new MaterialManager(materialsfile);
             cfg=new Config();
            BManager=new BordManager();
            Services=new ServicePrice(servicepriceFile);
             #region config DGV

            dgvMat.AutoGenerateColumns = false;
             dgvMat.DataSource = bsMaterials;
             bsMaterials.DataSource = MManager.AllMaterials;
             bsMaterials.AllowNew = false;
            
             #endregion
            DisplayServices(Services);
 
             AddMaterials = new ImportMat(MManager);
            tbConsole.Text = MakeStat(BManager.AllBords, Services);

           menuBordSelectMat.DropDownClosed += materialSelected;
           bsBords.DataSourceChanged += bsBords_DataSourceChanged;
           dgvService.RowsAdded += dgvService_RowsAdded;
           dgvService.CellValueChanged += dgvService_CellValueChanged;
            выбратьМатериалToolStripMenuItem.MouseHover += выбратьМатериалToolStripMenuItem_Click;
        }

        //edit row service
        void dgvService_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            var methodName = new StackTrace(false).GetFrame(0).GetMethod().Name;
            try
            {
                Services.Catalog.Clear();
                foreach (DataGridViewRow row in dgvService.Rows)
                {
                     double d = 0.0;
                    if(row==null) continue;
                    if(row.Cells.Count==0)continue;
                    if(!row.Displayed) continue;
                    if (row.Cells[0].Value==null)continue;
                if (string.IsNullOrEmpty(row.Cells[0].Value.ToString()) ||
                    row.Cells[1].Value==null ||
                    !double.TryParse(row.Cells[1].Value.ToString(),out d)) continue;
                Services.AddToCatalog(row);
                }
                Services.SavePrice();
                //DisplayServices(Services);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, methodName);
            }
        }


        //add row service
        void dgvService_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
         
        }

        //изменение содержимого DGV BORDS
        void bsBords_DataSourceChanged(object sender, EventArgs e)
        {
          
            var methodName = new StackTrace(false).GetFrame(0).GetMethod().Name;
            try
            {
                
               RefreshStat();
                lbFilter.Items.Clear();
                lbFilter.Items.Add("Показать все");
                foreach (List<Bord> bords in BManager.SelectByZsize())
                {
                    lbFilter.Items.Add("Толщина, H ="+bords[0].Z);
                }
                foreach (List<Bord> bords in BManager.selectByMat())
                {
                    lbFilter.Items.Add(bords[0].MatName);
                }
                bsBords.Sort = "Z DESC";
                ((List<Bord>)bsBords.DataSource).Sort(delegate(Bord us1, Bord us2)
            { return us2.Z.CompareTo(us1.Z); });

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, methodName);
            }
        }

        //load Services in dgv
        public void DisplayServices(ServicePrice service)
        {
            var methodName = new StackTrace(false).GetFrame(0).GetMethod().Name;
            try
            {
                dgvService.Rows.Clear();
                foreach (KeyValuePair<string, double> pair in service.Catalog)
                {
                    var row = new DataGridViewRow();
                    row.Cells.Add(new DataGridViewTextBoxCell() {Value = pair.Key});
                    row.Cells.Add(new DataGridViewTextBoxCell() {Value = pair.Value});
                    dgvService.Rows.Add(row);
                }
              
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, methodName);
            }
        }
        void materialSelected(object sender, EventArgs e)
        {
            var methodName = new StackTrace(false).GetFrame(0).GetMethod().Name;
            try
            {
                if(dgvBords.SelectedRows.Count==0)return;
                
                var selected = menuBordSelectMat.SelectedItem.ToString();
                var words = selected.Split(':');
                if(words.Length!=3)return;
                var name = words[1].Trim();
                var manufactor = words[2].Trim();
                foreach (DataGridViewRow row in dgvBords.SelectedRows)
                {
                    var mats =
                        MManager.AllMaterials.Where(x => x.NameMaterial == name && x.Manufactor == manufactor && x.Z == int.Parse(words[0].Split('м').First()));
                    if(mats.Count()!=1) throw new Exception("Не найден нужный материал");
                    BManager.AllBords.First(x => x==new Bord(row)).
                        Mat=mats.First();
                }
                bsBords.ResetBindings(false);
              
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, methodName);
            }
        }

        private void splitContainer3_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void импортМатериаловToolStripMenuItem_Click(object sender, EventArgs e)
        {
              
            var methodName = new StackTrace(false).GetFrame(0).GetMethod().Name;
            try
            {
                //if (!string.IsNullOrEmpty(cfg.LastMaterialPaths()))
                //    openFileDialog1.InitialDirectory = cfg.LastMaterialPaths();
                //else openFileDialog1.InitialDirectory = Directory.GetCurrentDirectory();

                //DialogResult dr=openFileDialog1.ShowDialog();
                //if(dr!=DialogResult.OK) return;
                //cfg.SaveMaterialPath(openFileDialog1.FileName);
                
                AddMaterials.ShowDialog();


            }
            catch (Exception ex)
            {
                
                MessageBox.Show(ex.Message, methodName);
            }
        }

        private void удалитьМатериалToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var methodName = new StackTrace(false).GetFrame(0).GetMethod().Name;
            try
            {
                if(dgvMat.SelectedRows.Count==0)return;
             
                MManager.RemoveMaterial(dgvMat.SelectedRows[0].Cells["NameMaterial"].Value.ToString(), dgvMat.SelectedRows[0].Cells["Article"].Value.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, methodName);
            }
        }

        private void открытьCSVPro100ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var methodName = new StackTrace(false).GetFrame(0).GetMethod().Name;
            try
            {

                openFileDialog1.FileName = cfg.GetLastFilePro100();
                DialogResult dr=openFileDialog1.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    //read csvfile
                   BManager=new BordManager(openFileDialog1.FileName);
                    bsBords.DataSource = BManager.AllBords;
                    //tbConsole.Text = MakeStat(BManager.AllBords, Services);
                    this.Text = openFileDialog1.FileName;
                    cfg.SaveLastFilePro100(openFileDialog1.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, methodName);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bords">all boards in list</param>
        /// <param name="catalog"></param>
        /// <returns></returns>
        public string MakeStat(List<Bord> bords, ServicePrice catalog)
        {
            var methodName = new StackTrace(false).GetFrame(0).GetMethod().Name;
            string result = "";
            try
            {
                double totalcoast = 0.0;    //общая стоимость по листам
               // double totalcoastm2 = 0.0;  //обзая стоимость по М2
                double servicecoast = 0.0;  //стоимость услуг распила и оклейки кромки
                double kromka = 0.0;        //стоимость кромки общая
                double krLength = 0;      //общая длина кромки
                double totaRaspil = 0.0;        //стоимость услуги распила общая
                double materialCoast = 0.0; //общая стоимость материала
                double materialcoastm2 = 0.0;//общая стоимость материала по м2
                double S = 0.0;             //общая площадь 
                int listcount = 0;          //кол-во листов материала
                int bordcount = 0;              //кол-во досок общее
                double ostatok = 0.0;       //остаток материала от целого листа
                
                List<Material> types=new List<Material>(); //all materials in project
                foreach (Bord bord in bords)
                {
                    //how match materials
                    if(!types.Contains(bord.Mat)) types.Add(bord.Mat);
                }
                foreach (Material material in types)//TODO find bug materialcoast
                {
                    var material1 = material; //тип листа материала
                    var selectbords = bords.Where(x => x.Mat == material1); //все детали данного материала
                    var info = BManager.GetInfoBords(selectbords.ToList()); //статистика по набору деталей
                    totaRaspil += catalog.Catalog["rasp"]*info.DSPcount; //стоимость распила 1 листа * кол-во листов материала
                    kromka += catalog.Catalog["kromka"]*info.KromkaLength;
                    S += info.S;
                    listcount += info.DSPcount; //кол-во листов всех материалов
                    bordcount += info.Count;
                    ostatok += info.RestS;
                    krLength += info.KromkaLength;
                    materialCoast += material.ListPrice*info.DSPcount; //полная стоимость листов материала
                    materialcoastm2 += material.M2Price*info.S;
                }
                
                servicecoast = totaRaspil + kromka;
                totalcoast = servicecoast + materialCoast;
                result = "Итоговая стоимость проекта по целым листам = " + totalcoast + "руб"+"\t(100%)"+
                         "\r\n\tСтоимость работ = " + servicecoast + "руб\t("+(100*servicecoast/totalcoast).ToString("F1")+"%),\t из них:" +
                         "\r\n\t\tРаспил = " + totaRaspil +"руб\t\t("+(100*totaRaspil/totalcoast).ToString("F1")+"%)"+
                         "\r\n\t\tКромка = " + kromka + "руб\t(" + (100 * kromka / totalcoast).ToString("F1") + "%)" +
                         "\r\n\tСтоимость листов материала = " + materialCoast + "руб\t(" + (100 * materialCoast / totalcoast).ToString("F1") + "%)" +
                         "\r\n\tСтоимость по м2            = " + materialcoastm2.ToString("F1") + "руб\t(" + (100 * materialcoastm2 / totalcoast).ToString("F1") + "%)" +
                         "\r\n=======================================================" +
                         "\r\nОбщая длина кромки = " + krLength +"м"+
                         "\r\nКоличество листов  = " + listcount +"шт"+
                         "\r\nОбщая площадь материалов = " + S +"м2"+
                         "\r\nОстаток материла от целого листа = " + ostatok +"м2"+
                         "\r\nОбщее кол-во досок = " + bordcount;

                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, methodName);
                return result;
            }
            
    
        }

        private void dgvBords_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

     private void выбратьМатериалToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var methodName = new StackTrace(false).GetFrame(0).GetMethod().Name;
            try
            {
                menuBordSelectMat.Items.Clear();
                foreach (Material material in MManager.AllMaterials)
                {

                    menuBordSelectMat.Items.Add(material.Z + "мм : " + material.NameMaterial + " : " +
                                                material.Manufactor);
                }
                if (menuBordSelectMat.Items.Count != 0) menuBordSelectMat.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, methodName);
            }
        }

        private void lbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            var methodName = new StackTrace(false).GetFrame(0).GetMethod().Name;
            try
            {
                if(lbFilter.SelectedItems.Count==0)return;
                var selected = lbFilter.SelectedItem.ToString();
                if (selected.Contains("Показать все"))
                {
                    bsBords.DataSource = BManager.AllBords;
                }else if (selected.Contains("Толщина"))
                {
                    var words = selected.Split('=');
                    if(words.Length!=2) throw new Exception("Не удалось распознать текст фильтра");
                    int Z = int.Parse(words[1].Trim());
                    bsBords.DataSource = BManager.AllBords.FindAll(x => x.Z == Z);
                }
                else //materials
                {
                    bsBords.DataSource = BManager.AllBords.FindAll(x => x.MatName == selected);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, methodName);
            }
        }

        private void пересчитатьСтоимостьToolStripMenuItem_Click(object sender, EventArgs e)
        {
           RefreshStat();
        }

        public void RefreshStat()
        {
            if (MakingStat != null && MakingStat.IsAlive) return;
            var methodName = new StackTrace(false).GetFrame(0).GetMethod().Name;
            try
            {
                MakingStat = new Thread(() =>
                {
                    int A = 1;
                    int B = 0;
                    while (A != B)
                    {
                        A = dgvBords.Rows.Count;
                        Thread.Sleep(600);
                        B = dgvBords.Rows.Count;
                    }

                    Log.invokeControlText(tbConsole, MakeStat((List<Bord>)bsBords.DataSource, Services));
                });
                MakingStat.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, methodName);
            }
        }

        private void добавитьМатериалToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddMaterials.ShowDialog();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(MakingStat!=null && MakingStat.IsAlive) MakingStat.Abort();
            Close();
        }

        public void SaveForExcel()
        {
            var methodName = new StackTrace(false).GetFrame(0).GetMethod().Name;
            try
            {
               // saveFileDialog1.FileName = cfg.GetLastFilePro100();
                var dr = saveFileDialog1.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    string file = saveFileDialog1.FileName;
                    BManager.SaveBordsToCsv(file, (List<Bord>)bsBords.DataSource);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, methodName);
            }
        }

       
        private void сохранитьТекущийПроектToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var methodName = new StackTrace(false).GetFrame(0).GetMethod().Name;
            try
            {

                List<Bord> savelist = BManager.AllBords;
                if (savelist.Count == 0) throw new Exception("Нечего сохранять.");
                saveProjectDialog.FileName = cfg.GetLastProjectFile();
                var dr = saveProjectDialog.ShowDialog();
                if (dr != DialogResult.OK) return;
                SaveProject(saveProjectDialog.FileName);
                cfg.SaveLastProjectFile(saveProjectDialog.FileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, methodName);
            }
        }
        /// <summary>
        /// формат сохранения:
        /// -заголовок столбцов
        /// -список досок
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public bool SaveProject(string file)
        {
            var methodName = new StackTrace(false).GetFrame(0).GetMethod().Name;
            try
            {
                List<Bord> savelist = BManager.AllBords;
               List<string> context = new List<string>();
                context.Add(Bord.BordHeader(';'));
                context.AddRange(savelist.ConvertAll(x=>x.ToString(';')));
                File.WriteAllLines(file,context,Encoding.Default);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, methodName);
                return false;
            }
        }

        public void LoadProject(string file)
        {
            var methodName = new StackTrace(false).GetFrame(0).GetMethod().Name;
            try
            {
                BManager.AllBords=new List<Bord>();
                var lines = File.ReadAllLines(file, Encoding.Default);
                for (int i = 1; i < lines.Length; i++)
                {
                    
                    BManager.AllBords.Add(new Bord().FromString(lines[i],';',MManager)); //new bord in Allbords
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, methodName);
            }
                    
        }
        private void открытьСохраненныйПроектToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var methodName = new StackTrace(false).GetFrame(0).GetMethod().Name;
            try
            {
                openProjectDialog.FileName = cfg.GetLastProjectFile();
                var dr = openProjectDialog.ShowDialog();
                if(dr!=DialogResult.OK) return;
                LoadProject(openProjectDialog.FileName);
                cfg.SaveLastProjectFile(openProjectDialog.FileName);
                cfg.SaveWorkDir(openProjectDialog.InitialDirectory);
                bsBords.DataSource = BManager.AllBords;
                this.Text = openProjectDialog.FileName;
                bsBords.ResetBindings(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, methodName);
            }
        }

        private void toolStripComboBox1_Click(object sender, EventArgs e)
        {
            SaveForExcel();
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var methodName = new StackTrace(false).GetFrame(0).GetMethod().Name;
            try
            {
                var rows = dgvBords.SelectedRows;
                if (rows.Count == 0) return;
                for (int i = 0; i < rows.Count; i++)
                {
                    DataGridViewRow row = rows[i];
                    var bord = BManager.AllBords.First(x => x == new Bord(row));
                    BManager.AllBords.Remove(bord);
                }
                bsBords.ResetBindings(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, methodName);
            }
        }
        private void добавитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var methodName = new StackTrace(false).GetFrame(0).GetMethod().Name;
            try
            {
                BManager.AllBords.Add(new Bord());
                bsBords.ResetBindings(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, methodName);
            }
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //todo about form
        }
    }
}
