namespace BenchmarkingApp.Benchmarks.Hosts {
    partial class RadVirtualGrid {
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.radVirtualGrid1 = new Telerik.WinControls.UI.RadVirtualGrid();
            ((System.ComponentModel.ISupportInitialize)(this.radVirtualGrid1)).BeginInit();
            this.SuspendLayout();
            // 
            // radVirtualGrid1
            // 
            this.radVirtualGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radVirtualGrid1.Location = new System.Drawing.Point(0, 0);
            this.radVirtualGrid1.Name = "radGridView1";
            this.radVirtualGrid1.Size = new System.Drawing.Size(800, 600);
            this.radVirtualGrid1.TabIndex = 0;
            this.radVirtualGrid1.Text = "radVirtualGrid1";
            this.radVirtualGrid1.AllowAddNewRow = false;
            this.radVirtualGrid1.AllowDelete = false;
            this.radVirtualGrid1.AllowMultiColumnSorting = true;
            this.radVirtualGrid1.AutoSizeColumnsMode = Telerik.WinControls.UI.VirtualGridAutoSizeColumnsMode.Fill;
            // 
            // RadVirtualGrid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.radVirtualGrid1);
            this.Name = "RadVirtualGrid";
            this.Size = new System.Drawing.Size(800, 600);
            ((System.ComponentModel.ISupportInitialize)(this.radVirtualGrid1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadVirtualGrid radVirtualGrid1;
    }
}