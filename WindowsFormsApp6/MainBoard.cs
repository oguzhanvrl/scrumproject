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
    public partial class MainBoard : Form
    {
        public MainBoard()
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.Fixed3D;
            this.Size = new Size(1500,620);
        }
        string currType;
        Control currControl;
        public class type
        {
            public const string
                New = "Oluştur",
                Edit = "Düzenle",
                Details="Detayları",
                Delete="Sil";
        }

        int taskCounter = 0;
        string cStry;// comboboxtan seçilen story
        // panel oluşturma ve flowlayout panelleri uyarlama

        void tasks_fetch()
        {
            using (ProjectDBEntities db_entity = new ProjectDBEntities())
            {
                FLPClear();
                var task_query = from t in db_entity.Tasks
                                 select t;

                foreach (var item in task_query)
                {
                    Task t = new Task(item.story_id.Value, item.taskName, item.details, item.commitmentDate.Value, item.deadline.Value, item.worker_id.Value);
                    t.EditState(item.state);
                    CreateTaskPanel(t);
                }

            }
        }

        public string GetStoryName(int story_id)
        {
            using (ProjectDBEntities db_entity = new ProjectDBEntities())
            {
                Storys s = db_entity.Storys.FirstOrDefault(sid => sid.story_id == story_id);
                return s.name;
            }
        }

        private void MainBoard_Load(object sender, EventArgs e)
        {
            tasks_fetch();
            Storys_Fetch();
            EnableDrop();
            gboxTask.Visible = false;
            gboxStory.Visible = false;
            gboxTask.Visible = false;
            txtTaskName.Enter += new EventHandler(textbox_enter);
            txtTaskDetails.Enter += new EventHandler(textbox_enter);
            txtStoryName.Enter += new EventHandler(textbox_enter);
            txtStoryDescription.Enter += new EventHandler(textbox_enter);
            txtStoryName.Leave += new EventHandler(textbox_leave);
            txtTaskName.Leave += new EventHandler(textbox_leave);
            txtStoryDescription.Leave += new EventHandler(textbox_leave);
            txtTaskDetails.Leave += new EventHandler(textbox_leave);
        }

        //oluşturulan panellerin sol ve sağ tık olayları
        void c_MouseDown(object sender, MouseEventArgs e)
        {
            Control currentControl = sender as Control;
            currControl = currentControl;
            if(e.Button==MouseButtons.Right)
            {
                Point panelLocation = new Point(currentControl.Location.X, currentControl.Location.Y);
                ctMenuTask.Show(currentControl, e.X,e.Y);              
            }
            else if(e.Button==MouseButtons.Left)
            {
                currentControl.DoDragDrop(currentControl, DragDropEffects.Move);
            }
        }

        //flowlayout panellerin içine atmayı kabul ettirme
        void panel_DragDrop(object sender, DragEventArgs e)
        {
            Control c = e.Data.GetData(e.Data.GetFormats()[0]) as Control;
            FlowLayoutPanel destination = sender as FlowLayoutPanel;
            FlowLayoutPanel source = (FlowLayoutPanel)c.Parent;
            if (c != null)
            {
                using (ProjectDBEntities db_entity = new ProjectDBEntities())
                {
                    Tasks currTask = db_entity.Tasks.FirstOrDefault(t => t.taskName == currControl.Name);
                    c.Location = destination.PointToClient(new Point(e.X, e.Y));

                    if (destination == flaypnlBacklog)
                    {
                        currTask.state = TaskState.Backlog;
                    }
                    else if (destination == flaypnlDoing)
                    {
                        currTask.state = TaskState.Doing;
                    }
                    else if (destination == flaypnlDone)
                    {
                        currTask.state = TaskState.Done;
                    }
                    else if (destination == flaypnlSprint)
                    {
                        currTask.state = TaskState.Sprint;
                    }
                    else if (destination == flaypnlTesting)
                    {
                        currTask.state = TaskState.Testing;
                    }
                    else if (destination == flaypnlToDo)
                    {
                        currTask.state = TaskState.ToDo;
                    }
                    destination.Controls.Add(c);
                    db_entity.SaveChanges();
                }
            }
        }

        void panel_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        void EnableDrop()
        {
            flaypnlBacklog.AllowDrop = true;
            flaypnlBacklog.DragOver += new DragEventHandler(panel_DragOver);
            flaypnlBacklog.DragDrop += new DragEventHandler(panel_DragDrop);
            flaypnlSprint.AllowDrop = true;
            flaypnlSprint.DragOver += new DragEventHandler(panel_DragOver);
            flaypnlSprint.DragDrop += new DragEventHandler(panel_DragDrop);
            flaypnlDoing.AllowDrop = true;
            flaypnlDoing.DragOver += new DragEventHandler(panel_DragOver);
            flaypnlDoing.DragDrop += new DragEventHandler(panel_DragDrop);
            flaypnlToDo.AllowDrop = true;
            flaypnlToDo.DragOver += new DragEventHandler(panel_DragOver);
            flaypnlToDo.DragDrop += new DragEventHandler(panel_DragDrop);
            flaypnlTesting.AllowDrop = true;
            flaypnlTesting.DragOver += new DragEventHandler(panel_DragOver);
            flaypnlTesting.DragDrop += new DragEventHandler(panel_DragDrop);
            flaypnlDone.AllowDrop = true;
            flaypnlDone.DragOver += new DragEventHandler(panel_DragOver);
            flaypnlDone.DragDrop += new DragEventHandler(panel_DragDrop);
        }

        void FLPClear()
        {
            flaypnlBacklog.Controls.Clear();
            flaypnlDoing.Controls.Clear();
            flaypnlDone.Controls.Clear();
            flaypnlSprint.Controls.Clear();
            flaypnlTesting.Controls.Clear();
            flaypnlToDo.Controls.Clear();
        }
        
        Color GetColor(int workerID)
        {
            using (ProjectDBEntities db_entity = new ProjectDBEntities())
            {
                var worker = db_entity.Workers.FirstOrDefault(x => x.worker_id == workerID);
                return System.Drawing.ColorTranslator.FromHtml(worker.color);
            }
        }

        void CreateTaskPanel(Task task)
        {
            FlowLayoutPanel p = FindPanel(task.GetTaskState());
            if (p != null)
            {
                System.Windows.Forms.Panel pnl = new System.Windows.Forms.Panel
                {
                    Name = task.GetTaskName(),
                    BackColor = GetColor(task.GetWorker()),// GetColor metodu bu formda
                    Size = new Size(190, 80),
                    Top = 20,
                    Left = 20,                    
                };
                PictureBox pb = new PictureBox
                {
                    Location = new Point(100, 3),
                    Image = Image.FromFile(@"C:\Users\MONSTER\Desktop\YazılımYap2\WindowsFormsApp6\WindowsFormsApp6\Resources\rap.png"),
                    SizeMode=PictureBoxSizeMode.StretchImage,
                    Size=new Size(30,30)
                };
                pnl.Controls.Add(pb);
                pnl.MouseDown += new MouseEventHandler(c_MouseDown);
                System.Windows.Forms.Label lbl = new System.Windows.Forms.Label
                {
                    Text = task.GetTaskName(),
                    Font = new Font("Montserrat", 12),
                    AutoSize = true
                };
                pnl.Controls.Add(lbl);
                lbl.Location = new Point(3, 5);
                pnl.Font = new Font("Montserrat", 12);
                System.Windows.Forms.Label lbl2 = new System.Windows.Forms.Label
                {
                    Text = GetStoryName(task.GetStory()),
                    Font = new Font("Montserrat", 9),
                    AutoSize = true
                };
                lbl2.Location = new Point(3, 35);
                pnl.Controls.Add(lbl2);
                System.Windows.Forms.Label lbl3 = new System.Windows.Forms.Label
                {
                    Text = task.GetCommitmentDate().ToShortDateString(),
                    Font = new Font("Montserrat", 9),
                    AutoSize = true
                };
                lbl3.Location = new Point(3, 55);
                pnl.Controls.Add(lbl3);
                p.Controls.Add(pnl);
            }
        }

        FlowLayoutPanel FindPanel(string state)
        {
            if (state == TaskState.Backlog) { return flaypnlBacklog; }
            else if (state == TaskState.Doing) { return flaypnlDoing; }
            else if (state == TaskState.Done) { return flaypnlDone; }
            else if (state == TaskState.Sprint) { return flaypnlSprint; }
            else if (state == TaskState.Testing) { return flaypnlTesting; }
            else if (state == TaskState.ToDo) { return flaypnlToDo; }
            return null;
        }

        private void btnNewTask_Click(object sender, EventArgs e)
        {
            using (ProjectDBEntities db_entity = new ProjectDBEntities())
            {
                if (currType == type.New)
                {
                    string currStory = cmbStories.SelectedItem.ToString();
                    string currWorker = cmbWorkers.SelectedItem.ToString();
                    Storys story = db_entity.Storys.FirstOrDefault(s => s.name == currStory);
                    Workers worker = db_entity.Workers.FirstOrDefault(w => w.name == currWorker);
                    Tasks tsk = new Tasks();
                    Task task = new Task(story.story_id, txtTaskName.Text, txtTaskDetails.Text, dtTaskDate.Value.Date, Convert.ToInt16(txtTaskDeadline.Text), worker.worker_id);
                    tsk.story_id = task.GetStory();
                    tsk.worker_id = task.GetWorker();
                    tsk.taskName = task.GetTaskName();
                    tsk.details = task.GetDetails();
                    tsk.commitmentDate = task.GetCommitmentDate();
                    tsk.endingDate = task.GetEndingDate();
                    tsk.deadline = task.GetDeadline();
                    tsk.state = task.GetTaskState();
                    db_entity.Tasks.Add(tsk);
                    CreateTaskPanel(task);
                }
                else
                {
                    Tasks task = db_entity.Tasks.FirstOrDefault(t => t.taskName == currControl.Name);
                    if (currType == type.Edit)
                    {
                        task.taskName = txtTaskName.Text;
                        task.details = txtTaskDetails.Text;
                        task.deadline = Convert.ToInt16(txtTaskDeadline.Text);
                        task.commitmentDate = dtTaskDate.Value.Date;
                        task.endingDate = task.commitmentDate.Value.AddDays(task.deadline.Value);
                        Workers w = db_entity.Workers.FirstOrDefault(v => v.name == cmbWorkers.SelectedItem.ToString());
                        task.worker_id = w.worker_id;
                    }
                    if (currType == type.Delete)
                    {
                        db_entity.Tasks.Remove(task);
                    }
                }
                db_entity.SaveChanges();
                MessageBox.Show("İşlem Başarıyla Gerçekleşti.");
                ResetTaskGroupBox();
                Storys_Fetch();
            }
        }       

        void textbox_enter(object sender, System.EventArgs e)
        {
            TextBox t = sender as TextBox;
            string s = t.Text;
            if(s=="Görev Detayları"||s=="Görev Adı"||s=="Story Tanımı"||s=="Story Adı")
            t.Clear();
        }

        void textbox_leave(object sender, System.EventArgs e)
        {
            TextBox t = sender as TextBox;
            if(t.Text=="")
            {
                if (t == txtTaskDetails)
                {
                    t.Text = "Görev Detayları";
                }
                else if (t == txtTaskName)
                {
                    t.Text = "Görev Adı";
                }
                else if (t == txtStoryDescription)
                {
                    t.Text = "Story Tanımı";
                }
                else if (t == txtStoryName)
                {
                    t.Text = "Story Adı";
                }
            }           
        }

        private void btnCreateStory_Click(object sender, EventArgs e)
        {
            using (ProjectDBEntities db_entity = new ProjectDBEntities())
            {                
                if (currType == type.New)
                {
                    Story st = new Story(txtStoryName.Text, txtStoryDescription.Text, int.Parse(txtStoryDeadline.Text));
                    st.EditEndingDate(st.GetStartingDate(), st.GetDeadline());
                    Storys st_db = new Storys();
                    st_db.name = st.GetName();
                    st_db.description = st.GetDescription();
                    st_db.startingDate = st.GetStartingDate();
                    st_db.endingDate = st.GetEndingDate();
                    st_db.state = st.GetState();
                    st_db.deadline = st.GetDeadline();
                    db_entity.Storys.Add(st_db);
                }
                else
                {
                    string currStory = cmbStories.SelectedItem.ToString();
                    Storys story = db_entity.Storys.FirstOrDefault(w => w.name == currStory);
                    if (currType == type.Edit && currStory != null)
                    {
                        lblDateNow.Text = story.startingDate.ToString();
                        story.name = txtStoryName.Text;
                        story.description = txtStoryDescription.Text;
                        story.deadline = Convert.ToInt16(txtStoryDeadline.Text);
                        story.endingDate = story.startingDate.Value.AddDays(story.deadline.Value);
                    }
                    if (currType == type.Delete && currStory != null)
                    {
                        db_entity.Storys.Remove(story);
                    }
                }
                db_entity.SaveChanges();                
                MessageBox.Show("İşlem Başarıyla Gerçekleşti.");               
                ResetStoryGroupBox();
                Storys_Fetch();
            }
        }

        void StoryDetails()
        {
            using (ProjectDBEntities db_entity = new ProjectDBEntities())
            {
                string currStory = cmbStories.SelectedItem.ToString();
                if (currStory != null)
                {                  
                    Storys story = db_entity.Storys.FirstOrDefault(w => w.name == currStory);
                    txtStoryName.Text = story.name;
                    txtStoryDescription.Text = story.description;
                    txtStoryDeadline.Text = story.deadline.ToString();
                    lblDateNow.Text = story.startingDate.Value.ToShortDateString() + "-" + story.endingDate.Value.ToShortDateString();
                }
            }
        }

        void TaskDetails(string taskName)
        {
            using (ProjectDBEntities db_entity = new ProjectDBEntities())
            {
                Tasks currTask = db_entity.Tasks.FirstOrDefault(w => w.taskName == taskName);

                txtTaskName.Text = currTask.taskName;
                txtTaskDetails.Text = currTask.details;
                txtTaskDeadline.Text = currTask.deadline.ToString();

                Storys story = db_entity.Storys.FirstOrDefault(s => s.story_id == currTask.story_id);
                Workers worker = db_entity.Workers.FirstOrDefault(w => w.worker_id == currTask.worker_id);

                cmbWorkers.Text = worker.name;
                lblStoryAdi.Text = "Story : " + story.name;
                dtTaskDate.Value = currTask.commitmentDate.Value.Date;
            }
        }

        private void MainBoard_MouseClick(object sender, MouseEventArgs e)
        {
            if(e.Button==MouseButtons.Right)
            {
                ctMenuStory.Show(this,e.X,e.Y);
            }
        }

        private void btnCancelTask_Click(object sender, EventArgs e)
        {
            gboxTask.Visible = false;
            ResetTaskGroupBox();
        }

        private void taskDetayli_Click(object sender, EventArgs e)
        {
            currType = type.Details;
            TaskDetails(currControl.Name);
            btnCreateTask.Visible = false;
            btnCancelTask.Visible = false;
            txtCommitter.Visible = true;
            gboxTask.Text = "Task " + currType;
            gboxTask.Visible = true;           
        }

        private void taskDuzenle_Click(object sender, EventArgs e)
        {
            currType = type.Edit;
            TaskDetails(currControl.Name);
            btnCreateTask.Text = "Task " + currType;
            gboxTask.Text = btnCreateTask.Text;
            btnCreateTask.Visible = true;
            btnCancelTask.Visible = false;
            cmbWorkers.Visible = true;
            gboxTask.Visible = true;
        }

        private void taskSil_Click(object sender, EventArgs e)
        {
            currType = type.Delete;
            TaskDetails(currControl.Name);
            btnCreateTask.Text = "Task " + currType;
            btnCreateTask.Visible = true;
            btnCancelTask.Visible = false;
        }

        private void yeniStory_Click(object sender, EventArgs e)
        {
            ResetStoryGroupBox();
            currType = type.New;
            btnCreateTask.Text = "Story " + currType;
            btnCreateStory.Visible = true;
            btnCancelStory.Visible = true;
            gboxStory.Text = btnCreateStory.Text;
            gboxStory.Visible = true;
            lblDateNow.Text = "Tarih : " + DateTime.Now.ToShortDateString();
        }

        private void storyDetayli_Click(object sender, EventArgs e)
        {
            ResetStoryGroupBox();
            StoryDetails();
            currType = type.Details;
            btnCreateTask.Text = "Story " + currType;
            gboxStory.Text = btnCreateStory.Text;
            btnCreateStory.Visible = false;
            btnCancelStory.Visible = false;
            gboxStory.Visible = true;
            gboxStory.Enabled = false;          
        }

        private void storyDuzenle_Click(object sender, EventArgs e)
        {
            ResetStoryGroupBox();
            StoryDetails();
            currType = type.Edit;
            btnCreateTask.Text = "Story " + currType;
            btnCreateStory.Visible = true;
            btnCancelStory.Visible = true;
            gboxStory.Text = btnCreateStory.Text;
            gboxStory.Visible = true;
        }

        private void storySil_Click(object sender, EventArgs e)
        {
            ResetStoryGroupBox();
            StoryDetails();
            currType = type.Delete;
            btnCreateTask.Text = "Story " + currType;
            btnCreateStory.Visible = true;
            btnCancelStory.Visible = false;
            gboxStory.Text = btnCreateStory.Text;
            gboxStory.Visible = true;          
        }

        private void storyYeniTask_Click(object sender, EventArgs e)
        {
            ResetStoryGroupBox();
            Workers_Fetch();
            lblStoryAdi.Text = "Story : "+cmbStories.SelectedItem.ToString();
            currType = type.New;
            btnCreateTask.Text = "Task " + currType;
            btnCreateTask.Visible = true;
            btnCancelTask.Visible = true;
            gboxTask.Text = btnCreateTask.Text;
            cmbWorkers.Visible = true;
            txtCommitter.Visible = false;
            gboxTask.Visible = true;
        }

        private void calisanYeni_Click(object sender, EventArgs e)
        {
            WorkerFormAc(type.New);
        }

        private void calisanDuzenle_Click(object sender, EventArgs e)
        {
            WorkerFormAc(type.Edit);
        }

        private void calisanSil_Click(object sender, EventArgs e)
        {
            WorkerFormAc(type.Delete);
        }

        private void btnCancelStory_Click(object sender, EventArgs e)
        {
            gboxStory.Visible = false;
            ResetStoryGroupBox();
        }

        void ResetTaskGroupBox()
        {
            txtTaskName.Text = "Görev Adı";
            txtTaskDetails.Text = "Görev Detayları";
            txtTaskDeadline.Text = "Deadline";
            dtTaskDate.Value = DateTime.Now;
            txtCommitter.Visible = false;
            txtCommitter.Text = "";
            cmbWorkers.SelectedItem = null;
            btnCreateStory.Visible = true;
            btnCancelStory.Visible = true;
        }

        void ResetStoryGroupBox()
        {
            gboxStory.Enabled = true;
            txtStoryName.Text = "Story Adı";
            txtStoryDescription.Text = "Story Tanımı";
            lblDateNow.Text = "Tarih";
            txtStoryDeadline.Clear();
            btnCreateStory.Visible = true;
            btnCancelStory.Visible = true;
        }

        void WorkerFormAc(string typeInfo)
        {
            WorkerForm workerForm = new WorkerForm(typeInfo);
            workerForm.Show();
        }

        private void Storys_Fetch()
        {
            cmbStories.Items.Clear();
            using (ProjectDBEntities db_entitiy = new ProjectDBEntities())
            {
                var storyQuery = from w in db_entitiy.Storys
                                  select w.name;
                foreach (var story in storyQuery)
                    cmbStories.Items.Add(story);
            }
        }

        private void cmbStories_SelectedIndexChanged(object sender, EventArgs e)
        {
            string currStory = cmbStories.SelectedItem.ToString();
            cStry = currStory;
            using (ProjectDBEntities db_entitiy = new ProjectDBEntities())
            {
                Storys cStory = db_entitiy.Storys.FirstOrDefault(s => s.name == currStory);
                txtCurrStoryDesc.Text = cStory.description;
                lblCurrStoryDate.Text = cStory.startingDate.Value.ToShortDateString() + " -Deadline [" + cStory.deadline.ToString() + " Gün]- " + cStory.endingDate.Value.ToShortDateString();
            }
            tasks_fetch();
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

        private void cmbWorkers_SelectedIndexChanged(object sender, EventArgs e)
        {
         
        }
    }
}
