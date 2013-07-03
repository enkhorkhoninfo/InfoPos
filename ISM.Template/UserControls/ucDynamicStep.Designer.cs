namespace ISM.Template.UserControls
{
    partial class ucDynamicStep
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucDynamicStep));
            this.imageCollection1 = new DevExpress.Utils.ImageCollection();
            this.timer1 = new System.Windows.Forms.Timer();
            this.checkEdit1 = new DevExpress.XtraEditors.CheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "FillUpHS.png");
            this.imageCollection1.Images.SetKeyName(1, "FillDownHS.png");
            // 
            // timer1
            // 
            this.timer1.Interval = 50;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // checkEdit1
            // 
            this.checkEdit1.Dock = System.Windows.Forms.DockStyle.Right;
            this.checkEdit1.Location = new System.Drawing.Point(528, 0);
            this.checkEdit1.Name = "checkEdit1";
            this.checkEdit1.Properties.AllowFocused = false;
            this.checkEdit1.Properties.Caption = "";
            this.checkEdit1.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.UserDefined;
            this.checkEdit1.Properties.GlyphAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.checkEdit1.Properties.ImageIndexChecked = 1;
            this.checkEdit1.Properties.ImageIndexUnchecked = 0;
            this.checkEdit1.Properties.Images = this.imageCollection1;
            this.checkEdit1.Properties.UseParentBackground = true;
            this.checkEdit1.ShowToolTips = false;
            this.checkEdit1.Size = new System.Drawing.Size(20, 20);
            this.checkEdit1.TabIndex = 1;
            this.checkEdit1.CheckedChanged += new System.EventHandler(this.checkEdit1_CheckedChanged);
            // 
            // ucDynamicStep
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.checkEdit1);
            this.Name = "ucDynamicStep";
            this.Size = new System.Drawing.Size(548, 72);
            this.Load += new System.EventHandler(this.ucDynamicStep_Load);
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit1.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.Utils.ImageCollection imageCollection1;
        private System.Windows.Forms.Timer timer1;
        private DevExpress.XtraEditors.CheckEdit checkEdit1;


    }
}
