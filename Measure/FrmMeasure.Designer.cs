namespace Measure
{
    partial class FrmMeasure
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMeasure));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tbtnLine = new System.Windows.Forms.ToolStripButton();
            this.tbtnArea = new System.Windows.Forms.ToolStripButton();
            this.tbtnFeature = new System.Windows.Forms.ToolStripButton();
            this.tbtnUnit = new System.Windows.Forms.ToolStripSplitButton();
            this.DistanceToolItem = new System.Windows.Forms.ToolStripMenuItem();
            this.kiloToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.metersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.decimetersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.centimetersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.millimetersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.milesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nautToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.yardsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.feetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.inchesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.decimalDegreeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AreaToolItem = new System.Windows.Forms.ToolStripMenuItem();
            this.kiloAreaTSMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.metersAreaTSMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.milesAreaTSMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.feetAreaTSMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.yardsAreaTSMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hectaresTSMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.acresTSMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tbtnClear = new System.Windows.Forms.ToolStripButton();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.tbtnSum = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbtnLine,
            this.tbtnArea,
            this.tbtnFeature,
            this.tbtnSum,
            this.tbtnUnit,
            this.tbtnClear});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(292, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tbtnLine
            // 
            this.tbtnLine.CheckOnClick = true;
            this.tbtnLine.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbtnLine.Image = ((System.Drawing.Image)(resources.GetObject("tbtnLine.Image")));
            this.tbtnLine.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtnLine.Name = "tbtnLine";
            this.tbtnLine.Size = new System.Drawing.Size(23, 22);
            this.tbtnLine.Text = "测量线";
            this.tbtnLine.ToolTipText = "测量线";
            this.tbtnLine.Click += new System.EventHandler(this.tbtnLine_Click);
            // 
            // tbtnArea
            // 
            this.tbtnArea.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbtnArea.Image = ((System.Drawing.Image)(resources.GetObject("tbtnArea.Image")));
            this.tbtnArea.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtnArea.Name = "tbtnArea";
            this.tbtnArea.Size = new System.Drawing.Size(23, 22);
            this.tbtnArea.Text = "测量面";
            this.tbtnArea.Click += new System.EventHandler(this.tbtnArea_Click);
            // 
            // tbtnFeature
            // 
            this.tbtnFeature.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbtnFeature.Image = ((System.Drawing.Image)(resources.GetObject("tbtnFeature.Image")));
            this.tbtnFeature.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtnFeature.Name = "tbtnFeature";
            this.tbtnFeature.Size = new System.Drawing.Size(23, 22);
            this.tbtnFeature.Text = "测量要素";
            this.tbtnFeature.Click += new System.EventHandler(this.tbtnFeature_Click);
            // 
            // tbtnUnit
            // 
            this.tbtnUnit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbtnUnit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DistanceToolItem,
            this.AreaToolItem});
            this.tbtnUnit.Image = ((System.Drawing.Image)(resources.GetObject("tbtnUnit.Image")));
            this.tbtnUnit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtnUnit.Name = "tbtnUnit";
            this.tbtnUnit.Size = new System.Drawing.Size(32, 22);
            this.tbtnUnit.Text = "选择单位";
            this.tbtnUnit.ToolTipText = "选择单位";
            // 
            // DistanceToolItem
            // 
            this.DistanceToolItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.kiloToolStripMenuItem,
            this.metersToolStripMenuItem,
            this.decimetersToolStripMenuItem,
            this.centimetersToolStripMenuItem,
            this.millimetersToolStripMenuItem,
            this.milesToolStripMenuItem,
            this.nautToolStripMenuItem,
            this.yardsToolStripMenuItem,
            this.feetToolStripMenuItem,
            this.inchesToolStripMenuItem,
            this.decimalDegreeToolStripMenuItem});
            this.DistanceToolItem.Name = "DistanceToolItem";
            this.DistanceToolItem.Size = new System.Drawing.Size(152, 22);
            this.DistanceToolItem.Text = "距离";
            // 
            // kiloToolStripMenuItem
            // 
            this.kiloToolStripMenuItem.CheckOnClick = true;
            this.kiloToolStripMenuItem.Name = "kiloToolStripMenuItem";
            this.kiloToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.kiloToolStripMenuItem.Text = "Kilometers";
            this.kiloToolStripMenuItem.Click += new System.EventHandler(this.kiloToolStripMenuItem_Click);
            // 
            // metersToolStripMenuItem
            // 
            this.metersToolStripMenuItem.Name = "metersToolStripMenuItem";
            this.metersToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.metersToolStripMenuItem.Text = "Meters";
            this.metersToolStripMenuItem.Click += new System.EventHandler(this.metersToolStripMenuItem_Click);
            // 
            // decimetersToolStripMenuItem
            // 
            this.decimetersToolStripMenuItem.Name = "decimetersToolStripMenuItem";
            this.decimetersToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.decimetersToolStripMenuItem.Text = "Decimeters";
            this.decimetersToolStripMenuItem.Click += new System.EventHandler(this.decimetersToolStripMenuItem_Click);
            // 
            // centimetersToolStripMenuItem
            // 
            this.centimetersToolStripMenuItem.Name = "centimetersToolStripMenuItem";
            this.centimetersToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.centimetersToolStripMenuItem.Text = "Centimeters";
            this.centimetersToolStripMenuItem.Click += new System.EventHandler(this.centimetersToolStripMenuItem_Click);
            // 
            // millimetersToolStripMenuItem
            // 
            this.millimetersToolStripMenuItem.Name = "millimetersToolStripMenuItem";
            this.millimetersToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.millimetersToolStripMenuItem.Text = "Millimeters";
            this.millimetersToolStripMenuItem.Click += new System.EventHandler(this.millimetersToolStripMenuItem_Click);
            // 
            // milesToolStripMenuItem
            // 
            this.milesToolStripMenuItem.Name = "milesToolStripMenuItem";
            this.milesToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.milesToolStripMenuItem.Text = "Miles";
            this.milesToolStripMenuItem.Click += new System.EventHandler(this.milesToolStripMenuItem_Click);
            // 
            // nautToolStripMenuItem
            // 
            this.nautToolStripMenuItem.Name = "nautToolStripMenuItem";
            this.nautToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.nautToolStripMenuItem.Text = "NauticalMiles";
            this.nautToolStripMenuItem.Click += new System.EventHandler(this.nautToolStripMenuItem_Click);
            // 
            // yardsToolStripMenuItem
            // 
            this.yardsToolStripMenuItem.Name = "yardsToolStripMenuItem";
            this.yardsToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.yardsToolStripMenuItem.Text = "Yards";
            this.yardsToolStripMenuItem.Click += new System.EventHandler(this.yardsToolStripMenuItem_Click);
            // 
            // feetToolStripMenuItem
            // 
            this.feetToolStripMenuItem.Name = "feetToolStripMenuItem";
            this.feetToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.feetToolStripMenuItem.Text = "Feet";
            this.feetToolStripMenuItem.Click += new System.EventHandler(this.feetToolStripMenuItem_Click);
            // 
            // inchesToolStripMenuItem
            // 
            this.inchesToolStripMenuItem.Name = "inchesToolStripMenuItem";
            this.inchesToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.inchesToolStripMenuItem.Text = "Inches";
            this.inchesToolStripMenuItem.Click += new System.EventHandler(this.inchesToolStripMenuItem_Click);
            // 
            // decimalDegreeToolStripMenuItem
            // 
            this.decimalDegreeToolStripMenuItem.Name = "decimalDegreeToolStripMenuItem";
            this.decimalDegreeToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.decimalDegreeToolStripMenuItem.Text = "DecimalDegrees";
            this.decimalDegreeToolStripMenuItem.Click += new System.EventHandler(this.decimalDegreeToolStripMenuItem_Click);
            // 
            // AreaToolItem
            // 
            this.AreaToolItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.kiloAreaTSMenuItem,
            this.metersAreaTSMenuItem,
            this.milesAreaTSMenuItem,
            this.feetAreaTSMenuItem,
            this.yardsAreaTSMenuItem,
            this.hectaresTSMenuItem,
            this.acresTSMenuItem});
            this.AreaToolItem.Name = "AreaToolItem";
            this.AreaToolItem.Size = new System.Drawing.Size(152, 22);
            this.AreaToolItem.Text = "面积";
            // 
            // kiloAreaTSMenuItem
            // 
            this.kiloAreaTSMenuItem.Name = "kiloAreaTSMenuItem";
            this.kiloAreaTSMenuItem.Size = new System.Drawing.Size(130, 22);
            this.kiloAreaTSMenuItem.Text = "Kilometers";
            this.kiloAreaTSMenuItem.Click += new System.EventHandler(this.kiloAreaTSMenuItem_Click);
            // 
            // metersAreaTSMenuItem
            // 
            this.metersAreaTSMenuItem.Name = "metersAreaTSMenuItem";
            this.metersAreaTSMenuItem.Size = new System.Drawing.Size(130, 22);
            this.metersAreaTSMenuItem.Text = "Meters";
            this.metersAreaTSMenuItem.Click += new System.EventHandler(this.metersAreaTSMenuItem_Click);
            // 
            // milesAreaTSMenuItem
            // 
            this.milesAreaTSMenuItem.Name = "milesAreaTSMenuItem";
            this.milesAreaTSMenuItem.Size = new System.Drawing.Size(130, 22);
            this.milesAreaTSMenuItem.Text = "Miles";
            this.milesAreaTSMenuItem.Click += new System.EventHandler(this.milesAreaTSMenuItem_Click);
            // 
            // feetAreaTSMenuItem
            // 
            this.feetAreaTSMenuItem.Name = "feetAreaTSMenuItem";
            this.feetAreaTSMenuItem.Size = new System.Drawing.Size(130, 22);
            this.feetAreaTSMenuItem.Text = "Feet";
            this.feetAreaTSMenuItem.Click += new System.EventHandler(this.feetAreaTSMenuItem_Click);
            // 
            // yardsAreaTSMenuItem
            // 
            this.yardsAreaTSMenuItem.Name = "yardsAreaTSMenuItem";
            this.yardsAreaTSMenuItem.Size = new System.Drawing.Size(130, 22);
            this.yardsAreaTSMenuItem.Text = "Yards";
            this.yardsAreaTSMenuItem.Click += new System.EventHandler(this.yardsAreaTSMenuItem_Click);
            // 
            // hectaresTSMenuItem
            // 
            this.hectaresTSMenuItem.Name = "hectaresTSMenuItem";
            this.hectaresTSMenuItem.Size = new System.Drawing.Size(130, 22);
            this.hectaresTSMenuItem.Text = "Hectares";
            this.hectaresTSMenuItem.Click += new System.EventHandler(this.hectaresTSMenuItem_Click);
            // 
            // acresTSMenuItem
            // 
            this.acresTSMenuItem.Name = "acresTSMenuItem";
            this.acresTSMenuItem.Size = new System.Drawing.Size(130, 22);
            this.acresTSMenuItem.Text = "Acres";
            this.acresTSMenuItem.Click += new System.EventHandler(this.acresTSMenuItem_Click);
            // 
            // tbtnClear
            // 
            this.tbtnClear.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbtnClear.Image = ((System.Drawing.Image)(resources.GetObject("tbtnClear.Image")));
            this.tbtnClear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtnClear.Name = "tbtnClear";
            this.tbtnClear.Size = new System.Drawing.Size(23, 22);
            this.tbtnClear.Text = "清空";
            this.tbtnClear.Click += new System.EventHandler(this.tbtnClear_Click);
            // 
            // txtMessage
            // 
            this.txtMessage.Location = new System.Drawing.Point(0, 28);
            this.txtMessage.Multiline = true;
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(291, 124);
            this.txtMessage.TabIndex = 1;
            // 
            // tbtnSum
            // 
            this.tbtnSum.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbtnSum.Image = ((System.Drawing.Image)(resources.GetObject("tbtnSum.Image")));
            this.tbtnSum.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtnSum.Name = "tbtnSum";
            this.tbtnSum.Size = new System.Drawing.Size(23, 22);
            this.tbtnSum.Text = "总和";
            this.tbtnSum.Click += new System.EventHandler(this.tbtnSum_Click);
            // 
            // FrmMeasure
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 154);
            this.Controls.Add(this.txtMessage);
            this.Controls.Add(this.toolStrip1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmMeasure";
            this.Text = "Measure";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FrmMeasure_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tbtnLine;
        private System.Windows.Forms.ToolStripButton tbtnArea;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.ToolStripButton tbtnFeature;
        private System.Windows.Forms.ToolStripSplitButton tbtnUnit;
        private System.Windows.Forms.ToolStripMenuItem DistanceToolItem;
        private System.Windows.Forms.ToolStripMenuItem AreaToolItem;
        private System.Windows.Forms.ToolStripMenuItem kiloToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem metersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem decimetersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem centimetersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem millimetersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem milesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nautToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem yardsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem feetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem inchesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem decimalDegreeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem kiloAreaTSMenuItem;
        private System.Windows.Forms.ToolStripMenuItem metersAreaTSMenuItem;
        private System.Windows.Forms.ToolStripMenuItem milesAreaTSMenuItem;
        private System.Windows.Forms.ToolStripMenuItem feetAreaTSMenuItem;
        private System.Windows.Forms.ToolStripMenuItem yardsAreaTSMenuItem;
        private System.Windows.Forms.ToolStripButton tbtnClear;
        private System.Windows.Forms.ToolStripMenuItem hectaresTSMenuItem;
        private System.Windows.Forms.ToolStripMenuItem acresTSMenuItem;
        private System.Windows.Forms.ToolStripButton tbtnSum;
    }
}