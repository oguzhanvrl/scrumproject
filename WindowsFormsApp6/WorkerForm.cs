using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp6
{
    public partial class WorkerForm : Form
    {
        string type;
        public WorkerForm(string type)
        {
            InitializeComponent();
            this.type = type;
        }
        // Düzenleme ve oluşturma aynı form ekranında olabilir . Bu yüzden bu formun ismi Çalışan Oluştur veya Çalışan Düzenle olacak .


        private void WorkerForm_Load(object sender, EventArgs e)
        {
            Workers_Fetch();
            btnDo.Text = type;
            this.Text = "Çalışan " + type;
            if(type==MainBoard.type.Edit)
            {
                cmbWorkers.Visible = true;
                pnlWorker.Visible = false;
            }
            else if(type==MainBoard.type.New)
            {
                cmbWorkers.Visible = false;
                pnlWorker.Visible = true; ;
            }
            else if(type==MainBoard.type.Delete)
            {
                cmbWorkers.Visible = true;
                pnlWorker.Visible = false;
            }
        }

        private void Workers_Fetch()
        {
            cmbWorkers.Items.Clear();
            using (ProjectDBEntities db_entitiy = new ProjectDBEntities())
            {
                var workerQuery = from w in db_entitiy.Workers
                                  select w.name;
                foreach (var worker in workerQuery)
                    cmbWorkers.Items.Add(worker);
            }
        }

        public static Color ColorConvert (String colorName)
        {
            var color_props = typeof(Color).GetProperties();
            foreach (var c in color_props)
                if (colorName.Equals(c.Name, StringComparison.OrdinalIgnoreCase))
                    return (Color)c.GetValue(new Color(), null);
            return Color.Transparent;
        }

        void pnlWorker_Clear()
        {
            cmbWorkers.Text = "";
            txtWorkerName.Clear();
            btnColor.BackColor = Color.Transparent;
        }

        private void cmbWorkers_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnlWorker.Visible = true;
            string workerName = cmbWorkers.SelectedItem.ToString();
            using (ProjectDBEntities db_entitiy = new ProjectDBEntities())
            {
                if (type == MainBoard.type.Delete) pnlWorker.Enabled = false;
                Workers workers = db_entitiy.Workers.FirstOrDefault(w => w.name == workerName);
                txtWorkerName.Text = workers.name;
                btnColor.BackColor = ColorConvert(workers.color);                
                //btnColor.BackColor = System.Drawing.ColorTranslator.FromHtml(workers.color);    
            }
        }

        private void btnColor_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                btnColor.BackColor = colorDialog.Color;
            }
        }

        private void btnDo_Click(object sender, EventArgs e)
        {
            using (ProjectDBEntities db_entity = new ProjectDBEntities())
            {
                if (type == MainBoard.type.New && txtWorkerName != null)
                {
                    Worker work = new Worker(txtWorkerName.Text, btnColor.BackColor);
                    Workers w = new Workers();
                    w.name = work.GetName();
                    w.color = work.GetColor().Name;
                    w.experience = work.GetExp();
                    db_entity.Workers.Add(w);                
                }
                else
                {
                    Workers worker = db_entity.Workers.FirstOrDefault(w => w.name == cmbWorkers.SelectedItem.ToString());
                    if (type == MainBoard.type.Edit)
                    {
                        worker.name = txtWorkerName.Text;
                        worker.color = btnColor.BackColor.Name;
                    }
                    else
                    {
                        pnlWorker.Enabled = false;
                        db_entity.Workers.Remove(worker);
                    }
                }
                db_entity.SaveChanges();
                pnlWorker_Clear();
                MessageBox.Show("İşlem BAŞARILI");
            }
        }
    }
}
