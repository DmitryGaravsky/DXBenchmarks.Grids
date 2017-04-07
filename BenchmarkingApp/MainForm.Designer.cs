namespace BenchmarkingApp {
    partial class MainForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.btnCold = new DevExpress.XtraEditors.SimpleButton();
            this.btnWarmup = new DevExpress.XtraEditors.SimpleButton();
            this.btnRun = new DevExpress.XtraEditors.SimpleButton();
            this.leHosts = new DevExpress.XtraEditors.LookUpEdit();
            this.hostItemBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.leBenchmarks = new DevExpress.XtraEditors.LookUpEdit();
            this.benchmarkItemBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.mvvmContext1 = new DevExpress.Utils.MVVM.MVVMContext(this.components);
            this.hostPanel = new System.Windows.Forms.Panel();
            this.result = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.leHosts.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hostItemBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.leBenchmarks.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.benchmarkItemBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mvvmContext1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCold
            // 
            this.btnCold.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCold.Location = new System.Drawing.Point(817, 12);
            this.btnCold.Name = "btnCold";
            this.btnCold.Size = new System.Drawing.Size(120, 23);
            this.btnCold.TabIndex = 0;
            this.btnCold.Text = "Cold Run";
            // 
            // btnWarmup
            // 
            this.btnWarmup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnWarmup.Location = new System.Drawing.Point(943, 12);
            this.btnWarmup.Name = "btnWarmup";
            this.btnWarmup.Size = new System.Drawing.Size(120, 23);
            this.btnWarmup.TabIndex = 0;
            this.btnWarmup.Text = "Warm Up";
            // 
            // btnRun
            // 
            this.btnRun.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRun.Location = new System.Drawing.Point(1069, 12);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(120, 23);
            this.btnRun.TabIndex = 0;
            this.btnRun.Text = "Run";
            // 
            // leHosts
            // 
            this.leHosts.Location = new System.Drawing.Point(8, 14);
            this.leHosts.Name = "leHosts";
            this.leHosts.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.leHosts.Properties.DataSource = this.hostItemBindingSource;
            this.leHosts.Properties.DisplayMember = "Name";
            this.leHosts.Properties.ShowHeader = false;
            this.leHosts.Size = new System.Drawing.Size(205, 20);
            this.leHosts.TabIndex = 1;
            // 
            // hostItemBindingSource
            // 
            this.hostItemBindingSource.DataSource = typeof(BenchmarkingApp.HostItem);
            // 
            // leBenchmarks
            // 
            this.leBenchmarks.Location = new System.Drawing.Point(219, 14);
            this.leBenchmarks.Name = "leBenchmarks";
            this.leBenchmarks.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.leBenchmarks.Properties.DataSource = this.benchmarkItemBindingSource;
            this.leBenchmarks.Properties.DisplayMember = "Name";
            this.leBenchmarks.Properties.ShowHeader = false;
            this.leBenchmarks.Size = new System.Drawing.Size(205, 20);
            this.leBenchmarks.TabIndex = 1;
            // 
            // benchmarkItemBindingSource
            // 
            this.benchmarkItemBindingSource.DataSource = typeof(BenchmarkingApp.BenchmarkItem);
            // 
            // mvvmContext1
            // 
            this.mvvmContext1.ContainerControl = this;
            this.mvvmContext1.ViewModelType = typeof(BenchmarkingApp.MainViewModel);
            // 
            // hostPanel
            // 
            this.hostPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hostPanel.Location = new System.Drawing.Point(8, 40);
            this.hostPanel.Name = "hostPanel";
            this.hostPanel.Size = new System.Drawing.Size(1184, 792);
            this.hostPanel.TabIndex = 2;
            // 
            // result
            // 
            this.result.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.result.Location = new System.Drawing.Point(430, 17);
            this.result.Name = "result";
            this.result.ReadOnly = true;
            this.result.Size = new System.Drawing.Size(381, 13);
            this.result.TabIndex = 3;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 840);
            this.Controls.Add(this.result);
            this.Controls.Add(this.hostPanel);
            this.Controls.Add(this.leBenchmarks);
            this.Controls.Add(this.leHosts);
            this.Controls.Add(this.btnRun);
            this.Controls.Add(this.btnWarmup);
            this.Controls.Add(this.btnCold);
            this.Name = "MainForm";
            this.Padding = new System.Windows.Forms.Padding(8, 40, 8, 8);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.leHosts.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hostItemBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.leBenchmarks.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.benchmarkItemBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mvvmContext1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnCold;
        private DevExpress.XtraEditors.SimpleButton btnWarmup;
        private DevExpress.XtraEditors.SimpleButton btnRun;
        private DevExpress.Utils.MVVM.MVVMContext mvvmContext1;
        private System.Windows.Forms.BindingSource hostItemBindingSource;
        private DevExpress.XtraEditors.LookUpEdit leHosts;
        private DevExpress.XtraEditors.LookUpEdit leBenchmarks;
        private System.Windows.Forms.BindingSource benchmarkItemBindingSource;
        private System.Windows.Forms.Panel hostPanel;
        private System.Windows.Forms.TextBox result;
    }
}

