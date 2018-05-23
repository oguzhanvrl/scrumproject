namespace WindowsFormsApp6
{
    partial class WorkerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.cmbWorkers = new System.Windows.Forms.ComboBox();
            this.pnlWorker = new System.Windows.Forms.Panel();
            this.lblRengi = new System.Windows.Forms.Label();
            this.btnColor = new System.Windows.Forms.Button();
            this.txtWorkerName = new System.Windows.Forms.TextBox();
            this.lblCalisanAdi = new System.Windows.Forms.Label();
            this.btnDo = new System.Windows.Forms.Button();
            this.pnlWorker.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbWorkers
            // 
            this.cmbWorkers.FormattingEnabled = true;
            this.cmbWorkers.Location = new System.Drawing.Point(84, 29);
            this.cmbWorkers.Name = "cmbWorkers";
            this.cmbWorkers.Size = new System.Drawing.Size(226, 24);
            this.cmbWorkers.TabIndex = 5;
            this.cmbWorkers.SelectedIndexChanged += new System.EventHandler(this.cmbWorkers_SelectedIndexChanged);
            // 
            // pnlWorker
            // 
            this.pnlWorker.Controls.Add(this.lblRengi);
            this.pnlWorker.Controls.Add(this.btnColor);
            this.pnlWorker.Controls.Add(this.txtWorkerName);
            this.pnlWorker.Controls.Add(this.lblCalisanAdi);
            this.pnlWorker.Location = new System.Drawing.Point(12, 59);
            this.pnlWorker.Name = "pnlWorker";
            this.pnlWorker.Size = new System.Drawing.Size(338, 136);
            this.pnlWorker.TabIndex = 6;
            // 
            // lblRengi
            // 
            this.lblRengi.AutoSize = true;
            this.lblRengi.Location = new System.Drawing.Point(47, 85);
            this.lblRengi.Name = "lblRengi";
            this.lblRengi.Size = new System.Drawing.Size(45, 17);
            this.lblRengi.TabIndex = 9;
            this.lblRengi.Text = "Rengi";
            // 
            // btnColor
            // 
            this.btnColor.Location = new System.Drawing.Point(128, 65);
            this.btnColor.Name = "btnColor";
            this.btnColor.Size = new System.Drawing.Size(100, 56);
            this.btnColor.TabIndex = 8;
            this.btnColor.UseVisualStyleBackColor = true;
            this.btnColor.Click += new System.EventHandler(this.btnColor_Click);
            // 
            // txtWorkerName
            // 
            this.txtWorkerName.Location = new System.Drawing.Point(98, 33);
            this.txtWorkerName.Name = "txtWorkerName";
            this.txtWorkerName.Size = new System.Drawing.Size(226, 22);
            this.txtWorkerName.TabIndex = 6;
            // 
            // lblCalisanAdi
            // 
            this.lblCalisanAdi.AutoSize = true;
            this.lblCalisanAdi.Location = new System.Drawing.Point(14, 38);
            this.lblCalisanAdi.Name = "lblCalisanAdi";
            this.lblCalisanAdi.Size = new System.Drawing.Size(78, 17);
            this.lblCalisanAdi.TabIndex = 5;
            this.lblCalisanAdi.Text = "Çalışan Adı";
            // 
            // btnDo
            // 
            this.btnDo.Location = new System.Drawing.Point(140, 220);
            this.btnDo.Name = "btnDo";
            this.btnDo.Size = new System.Drawing.Size(100, 56);
            this.btnDo.TabIndex = 7;
            this.btnDo.Text = "Oluştur";
            this.btnDo.UseVisualStyleBackColor = true;
            this.btnDo.Click += new System.EventHandler(this.btnDo_Click);
            // 
            // WorkerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(373, 320);
            this.Controls.Add(this.pnlWorker);
            this.Controls.Add(this.cmbWorkers);
            this.Controls.Add(this.btnDo);
            this.Name = "WorkerForm";
            this.Text = "Çalışan";
            this.Load += new System.EventHandler(this.WorkerForm_Load);
            this.pnlWorker.ResumeLayout(false);
            this.pnlWorker.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ColorDialog colorDialog;
        private System.Windows.Forms.ComboBox cmbWorkers;
        private System.Windows.Forms.Panel pnlWorker;
        private System.Windows.Forms.Label lblRengi;
        private System.Windows.Forms.Button btnColor;
        private System.Windows.Forms.Button btnDo;
        private System.Windows.Forms.TextBox txtWorkerName;
        private System.Windows.Forms.Label lblCalisanAdi;
    }
}