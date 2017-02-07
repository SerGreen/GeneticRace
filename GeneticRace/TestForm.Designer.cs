namespace GeneticRace
{
    partial class TestForm
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
            this.screen = new System.Windows.Forms.PictureBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.listRules = new System.Windows.Forms.ListView();
            this.ruleNumCol = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ruleCol = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listBots = new System.Windows.Forms.ListView();
            this.numCol = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.idCol = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.fitnessCol = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cpCol = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.idleCol = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.goalCol = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.kickedCol = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.mapsList = new System.Windows.Forms.ComboBox();
            this.saveList = new System.Windows.Forms.ComboBox();
            this.resetGeneration = new System.Windows.Forms.Button();
            this.valGen = new System.Windows.Forms.Label();
            this.valTime = new System.Windows.Forms.Label();
            this.valActive = new System.Windows.Forms.Label();
            this.labGen = new System.Windows.Forms.Label();
            this.labTime = new System.Windows.Forms.Label();
            this.labActive = new System.Windows.Forms.Label();
            this.saveGeneration = new System.Windows.Forms.Button();
            this.loadGeneration = new System.Windows.Forms.Button();
            this.uiUpdateTimer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.screen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // screen
            // 
            this.screen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(0)))));
            this.screen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.screen.Location = new System.Drawing.Point(0, 0);
            this.screen.Name = "screen";
            this.screen.Size = new System.Drawing.Size(1028, 415);
            this.screen.TabIndex = 0;
            this.screen.TabStop = false;
            this.screen.SizeChanged += new System.EventHandler(this.screen_SizeChanged);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
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
            this.splitContainer1.Panel2.Controls.Add(this.listRules);
            this.splitContainer1.Panel2.Controls.Add(this.listBots);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer1.Size = new System.Drawing.Size(1028, 582);
            this.splitContainer1.SplitterDistance = 415;
            this.splitContainer1.TabIndex = 1;
            // 
            // listRules
            // 
            this.listRules.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listRules.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ruleNumCol,
            this.ruleCol});
            this.listRules.FullRowSelect = true;
            this.listRules.GridLines = true;
            this.listRules.Location = new System.Drawing.Point(416, 3);
            this.listRules.MultiSelect = false;
            this.listRules.Name = "listRules";
            this.listRules.Size = new System.Drawing.Size(413, 158);
            this.listRules.TabIndex = 2;
            this.listRules.UseCompatibleStateImageBehavior = false;
            this.listRules.View = System.Windows.Forms.View.Details;
            // 
            // ruleNumCol
            // 
            this.ruleNumCol.Text = "#";
            this.ruleNumCol.Width = 26;
            // 
            // ruleCol
            // 
            this.ruleCol.Text = "Rule";
            this.ruleCol.Width = 430;
            // 
            // listBots
            // 
            this.listBots.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listBots.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.numCol,
            this.idCol,
            this.fitnessCol,
            this.cpCol,
            this.idleCol,
            this.goalCol,
            this.kickedCol});
            this.listBots.FullRowSelect = true;
            this.listBots.GridLines = true;
            this.listBots.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listBots.Location = new System.Drawing.Point(3, 3);
            this.listBots.MultiSelect = false;
            this.listBots.Name = "listBots";
            this.listBots.Size = new System.Drawing.Size(407, 157);
            this.listBots.TabIndex = 1;
            this.listBots.UseCompatibleStateImageBehavior = false;
            this.listBots.View = System.Windows.Forms.View.Details;
            this.listBots.SelectedIndexChanged += new System.EventHandler(this.listBots_SelectedIndexChanged);
            // 
            // numCol
            // 
            this.numCol.Text = "#";
            this.numCol.Width = 31;
            // 
            // idCol
            // 
            this.idCol.Text = "ID";
            // 
            // fitnessCol
            // 
            this.fitnessCol.Text = "Fitness";
            this.fitnessCol.Width = 62;
            // 
            // cpCol
            // 
            this.cpCol.Text = "CP";
            this.cpCol.Width = 49;
            // 
            // idleCol
            // 
            this.idleCol.Text = "Idle";
            // 
            // goalCol
            // 
            this.goalCol.Text = "Goal achieved";
            // 
            // kickedCol
            // 
            this.kickedCol.Text = "Kicked";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.mapsList);
            this.groupBox1.Controls.Add(this.saveList);
            this.groupBox1.Controls.Add(this.resetGeneration);
            this.groupBox1.Controls.Add(this.valGen);
            this.groupBox1.Controls.Add(this.valTime);
            this.groupBox1.Controls.Add(this.valActive);
            this.groupBox1.Controls.Add(this.labGen);
            this.groupBox1.Controls.Add(this.labTime);
            this.groupBox1.Controls.Add(this.labActive);
            this.groupBox1.Controls.Add(this.saveGeneration);
            this.groupBox1.Controls.Add(this.loadGeneration);
            this.groupBox1.Location = new System.Drawing.Point(835, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(190, 158);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Population";
            // 
            // mapsList
            // 
            this.mapsList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.mapsList.DisplayMember = "0";
            this.mapsList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.mapsList.FormattingEnabled = true;
            this.mapsList.Location = new System.Drawing.Point(6, 130);
            this.mapsList.Name = "mapsList";
            this.mapsList.Size = new System.Drawing.Size(85, 21);
            this.mapsList.TabIndex = 4;
            // 
            // saveList
            // 
            this.saveList.DisplayMember = "0";
            this.saveList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.saveList.FormattingEnabled = true;
            this.saveList.Items.AddRange(new object[] {
            "Slot #1",
            "Slot #2",
            "Slot #3",
            "Slot #4",
            "Slot #5"});
            this.saveList.Location = new System.Drawing.Point(6, 23);
            this.saveList.Name = "saveList";
            this.saveList.Size = new System.Drawing.Size(85, 21);
            this.saveList.TabIndex = 4;
            // 
            // resetGeneration
            // 
            this.resetGeneration.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.resetGeneration.Location = new System.Drawing.Point(97, 126);
            this.resetGeneration.Name = "resetGeneration";
            this.resetGeneration.Size = new System.Drawing.Size(83, 26);
            this.resetGeneration.TabIndex = 3;
            this.resetGeneration.Text = "Load/Reset";
            this.resetGeneration.UseVisualStyleBackColor = true;
            this.resetGeneration.Click += new System.EventHandler(this.resetGeneration_Click);
            // 
            // valGen
            // 
            this.valGen.AutoSize = true;
            this.valGen.Location = new System.Drawing.Point(94, 106);
            this.valGen.Name = "valGen";
            this.valGen.Size = new System.Drawing.Size(13, 13);
            this.valGen.TabIndex = 2;
            this.valGen.Text = "0";
            // 
            // valTime
            // 
            this.valTime.AutoSize = true;
            this.valTime.Location = new System.Drawing.Point(94, 93);
            this.valTime.Name = "valTime";
            this.valTime.Size = new System.Drawing.Size(13, 13);
            this.valTime.TabIndex = 2;
            this.valTime.Text = "0";
            // 
            // valActive
            // 
            this.valActive.AutoSize = true;
            this.valActive.Location = new System.Drawing.Point(94, 80);
            this.valActive.Name = "valActive";
            this.valActive.Size = new System.Drawing.Size(24, 13);
            this.valActive.TabIndex = 2;
            this.valActive.Text = "0/0";
            // 
            // labGen
            // 
            this.labGen.AutoSize = true;
            this.labGen.Location = new System.Drawing.Point(6, 106);
            this.labGen.Name = "labGen";
            this.labGen.Size = new System.Drawing.Size(85, 13);
            this.labGen.TabIndex = 1;
            this.labGen.Text = "Generation num.";
            // 
            // labTime
            // 
            this.labTime.AutoSize = true;
            this.labTime.Location = new System.Drawing.Point(6, 93);
            this.labTime.Name = "labTime";
            this.labTime.Size = new System.Drawing.Size(67, 13);
            this.labTime.TabIndex = 1;
            this.labTime.Text = "Time passed";
            // 
            // labActive
            // 
            this.labActive.AutoSize = true;
            this.labActive.Location = new System.Drawing.Point(6, 80);
            this.labActive.Name = "labActive";
            this.labActive.Size = new System.Drawing.Size(60, 13);
            this.labActive.TabIndex = 1;
            this.labActive.Text = "Active bots";
            // 
            // saveGeneration
            // 
            this.saveGeneration.Location = new System.Drawing.Point(97, 19);
            this.saveGeneration.Name = "saveGeneration";
            this.saveGeneration.Size = new System.Drawing.Size(87, 26);
            this.saveGeneration.TabIndex = 0;
            this.saveGeneration.Text = "Save";
            this.saveGeneration.UseVisualStyleBackColor = true;
            this.saveGeneration.Click += new System.EventHandler(this.saveGeneration_Click);
            // 
            // loadGeneration
            // 
            this.loadGeneration.Location = new System.Drawing.Point(97, 51);
            this.loadGeneration.Name = "loadGeneration";
            this.loadGeneration.Size = new System.Drawing.Size(87, 26);
            this.loadGeneration.TabIndex = 0;
            this.loadGeneration.Text = "Load";
            this.loadGeneration.UseVisualStyleBackColor = true;
            this.loadGeneration.Click += new System.EventHandler(this.loadGeneration_Click);
            // 
            // uiUpdateTimer
            // 
            this.uiUpdateTimer.Enabled = true;
            this.uiUpdateTimer.Tick += new System.EventHandler(this.uiUpdateTimer_Tick);
            // 
            // TestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1028, 582);
            this.Controls.Add(this.splitContainer1);
            this.Name = "TestForm";
            this.Text = "TestForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TestForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.screen)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox screen;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button loadGeneration;
        private System.Windows.Forms.Button saveGeneration;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListView listRules;
        private System.Windows.Forms.ListView listBots;
        private System.Windows.Forms.ColumnHeader numCol;
        private System.Windows.Forms.ColumnHeader idCol;
        private System.Windows.Forms.ColumnHeader fitnessCol;
        private System.Windows.Forms.ColumnHeader cpCol;
        private System.Windows.Forms.ColumnHeader goalCol;
        private System.Windows.Forms.ColumnHeader kickedCol;
        private System.Windows.Forms.ColumnHeader idleCol;
        private System.Windows.Forms.Timer uiUpdateTimer;
        private System.Windows.Forms.ColumnHeader ruleCol;
        private System.Windows.Forms.ColumnHeader ruleNumCol;
        private System.Windows.Forms.Label valGen;
        private System.Windows.Forms.Label valTime;
        private System.Windows.Forms.Label valActive;
        private System.Windows.Forms.Label labGen;
        private System.Windows.Forms.Label labTime;
        private System.Windows.Forms.Label labActive;
        private System.Windows.Forms.Button resetGeneration;
        private System.Windows.Forms.ComboBox saveList;
        private System.Windows.Forms.ComboBox mapsList;
    }
}