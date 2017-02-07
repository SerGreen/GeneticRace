namespace MapEditor
{
    partial class EditorForm
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
            this.components = new System.ComponentModel.Container();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.screen = new System.Windows.Forms.PictureBox();
            this.bLoad = new System.Windows.Forms.Button();
            this.bSave = new System.Windows.Forms.Button();
            this.materialBox = new System.Windows.Forms.ComboBox();
            this.bGoal = new System.Windows.Forms.Button();
            this.bStart = new System.Windows.Forms.Button();
            this.bDeleteLast = new System.Windows.Forms.Button();
            this.bPolyAccept = new System.Windows.Forms.Button();
            this.bPolygon = new System.Windows.Forms.Button();
            this.bCircle = new System.Windows.Forms.Button();
            this.renderTimer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.screen)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.screen);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.bLoad);
            this.splitContainer1.Panel2.Controls.Add(this.bSave);
            this.splitContainer1.Panel2.Controls.Add(this.materialBox);
            this.splitContainer1.Panel2.Controls.Add(this.bGoal);
            this.splitContainer1.Panel2.Controls.Add(this.bStart);
            this.splitContainer1.Panel2.Controls.Add(this.bDeleteLast);
            this.splitContainer1.Panel2.Controls.Add(this.bPolyAccept);
            this.splitContainer1.Panel2.Controls.Add(this.bPolygon);
            this.splitContainer1.Panel2.Controls.Add(this.bCircle);
            this.splitContainer1.Size = new System.Drawing.Size(973, 535);
            this.splitContainer1.SplitterDistance = 501;
            this.splitContainer1.TabIndex = 0;
            // 
            // screen
            // 
            this.screen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.screen.Location = new System.Drawing.Point(0, 0);
            this.screen.Name = "screen";
            this.screen.Size = new System.Drawing.Size(973, 501);
            this.screen.TabIndex = 0;
            this.screen.TabStop = false;
            this.screen.SizeChanged += new System.EventHandler(this.screen_SizeChanged);
            this.screen.MouseDown += new System.Windows.Forms.MouseEventHandler(this.screen_MouseDown);
            this.screen.MouseMove += new System.Windows.Forms.MouseEventHandler(this.screen_MouseMove);
            this.screen.MouseUp += new System.Windows.Forms.MouseEventHandler(this.screen_MouseUp);
            // 
            // bLoad
            // 
            this.bLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bLoad.Location = new System.Drawing.Point(909, 3);
            this.bLoad.Name = "bLoad";
            this.bLoad.Size = new System.Drawing.Size(52, 23);
            this.bLoad.TabIndex = 4;
            this.bLoad.Text = "Load";
            this.bLoad.UseVisualStyleBackColor = true;
            this.bLoad.Click += new System.EventHandler(this.bLoad_Click);
            // 
            // bSave
            // 
            this.bSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bSave.Location = new System.Drawing.Point(851, 3);
            this.bSave.Name = "bSave";
            this.bSave.Size = new System.Drawing.Size(52, 23);
            this.bSave.TabIndex = 4;
            this.bSave.Text = "Save";
            this.bSave.UseVisualStyleBackColor = true;
            this.bSave.Click += new System.EventHandler(this.bSave_Click);
            // 
            // materialBox
            // 
            this.materialBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.materialBox.FormattingEnabled = true;
            this.materialBox.Items.AddRange(new object[] {
            "Grass",
            "Road"});
            this.materialBox.Location = new System.Drawing.Point(230, 5);
            this.materialBox.Name = "materialBox";
            this.materialBox.Size = new System.Drawing.Size(81, 21);
            this.materialBox.TabIndex = 3;
            this.materialBox.SelectedIndexChanged += new System.EventHandler(this.materialBox_SelectedIndexChanged);
            // 
            // bGoal
            // 
            this.bGoal.Location = new System.Drawing.Point(376, 3);
            this.bGoal.Name = "bGoal";
            this.bGoal.Size = new System.Drawing.Size(41, 23);
            this.bGoal.TabIndex = 2;
            this.bGoal.Text = "Goal";
            this.bGoal.UseVisualStyleBackColor = true;
            this.bGoal.Click += new System.EventHandler(this.bGoal_Click);
            // 
            // bStart
            // 
            this.bStart.Location = new System.Drawing.Point(329, 3);
            this.bStart.Name = "bStart";
            this.bStart.Size = new System.Drawing.Size(41, 23);
            this.bStart.TabIndex = 2;
            this.bStart.Text = "Start";
            this.bStart.UseVisualStyleBackColor = true;
            this.bStart.Click += new System.EventHandler(this.bStart_Click);
            // 
            // bDeleteLast
            // 
            this.bDeleteLast.Location = new System.Drawing.Point(198, 3);
            this.bDeleteLast.Name = "bDeleteLast";
            this.bDeleteLast.Size = new System.Drawing.Size(18, 23);
            this.bDeleteLast.TabIndex = 1;
            this.bDeleteLast.Text = "←";
            this.bDeleteLast.UseVisualStyleBackColor = true;
            this.bDeleteLast.Click += new System.EventHandler(this.bDeleteLast_Click);
            // 
            // bPolyAccept
            // 
            this.bPolyAccept.Location = new System.Drawing.Point(174, 3);
            this.bPolyAccept.Name = "bPolyAccept";
            this.bPolyAccept.Size = new System.Drawing.Size(18, 23);
            this.bPolyAccept.TabIndex = 1;
            this.bPolyAccept.Text = "✓";
            this.bPolyAccept.UseVisualStyleBackColor = true;
            this.bPolyAccept.Click += new System.EventHandler(this.bPolyAccept_Click);
            // 
            // bPolygon
            // 
            this.bPolygon.Location = new System.Drawing.Point(93, 3);
            this.bPolygon.Name = "bPolygon";
            this.bPolygon.Size = new System.Drawing.Size(75, 23);
            this.bPolygon.TabIndex = 0;
            this.bPolygon.Text = "Polygon";
            this.bPolygon.UseVisualStyleBackColor = true;
            this.bPolygon.Click += new System.EventHandler(this.bPolygon_Click);
            // 
            // bCircle
            // 
            this.bCircle.Location = new System.Drawing.Point(12, 3);
            this.bCircle.Name = "bCircle";
            this.bCircle.Size = new System.Drawing.Size(75, 23);
            this.bCircle.TabIndex = 0;
            this.bCircle.Text = "Circle";
            this.bCircle.UseVisualStyleBackColor = true;
            this.bCircle.Click += new System.EventHandler(this.bCircle_Click);
            // 
            // renderTimer
            // 
            this.renderTimer.Tick += new System.EventHandler(this.renderTimer_Tick);
            // 
            // EditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(973, 535);
            this.Controls.Add(this.splitContainer1);
            this.Name = "EditorForm";
            this.Text = "Map editor";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.screen)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.PictureBox screen;
        private System.Windows.Forms.ComboBox materialBox;
        private System.Windows.Forms.Button bGoal;
        private System.Windows.Forms.Button bStart;
        private System.Windows.Forms.Button bPolyAccept;
        private System.Windows.Forms.Button bPolygon;
        private System.Windows.Forms.Button bCircle;
        private System.Windows.Forms.Timer renderTimer;
        private System.Windows.Forms.Button bLoad;
        private System.Windows.Forms.Button bSave;
        private System.Windows.Forms.Button bDeleteLast;
    }
}

