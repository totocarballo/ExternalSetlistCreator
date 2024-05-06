namespace ExternalSetlistCreator
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            TipMessage = new ToolTip(components);
            TxtSearch = new TextBox();
            ChkSortList = new CheckBox();
            BtnMoveDown = new Button();
            BtnMoveUp = new Button();
            LblSpeed = new Label();
            OfdAdd = new OpenFileDialog();
            SfdExport = new SaveFileDialog();
            FbdAdd = new FolderBrowserDialog();
            FbdExport = new FolderBrowserDialog();
            BtnExportYarg = new Button();
            NumSpeed = new NumericUpDown();
            BtnAddFolder = new Button();
            BtnClear = new Button();
            BtnRemove = new Button();
            BtnExportCh = new Button();
            BtnAddFile = new Button();
            LstFiles = new ListBox();
            BtnModify = new Button();
            ((System.ComponentModel.ISupportInitialize)NumSpeed).BeginInit();
            SuspendLayout();
            // 
            // TxtSearch
            // 
            TxtSearch.Location = new Point(399, 278);
            TxtSearch.Name = "TxtSearch";
            TxtSearch.Size = new Size(302, 23);
            TxtSearch.TabIndex = 24;
            TipMessage.SetToolTip(TxtSearch, "Enter text to search in the list.");
            TxtSearch.Click += TxtSearch_Click;
            TxtSearch.TextChanged += TxtSearch_TextChanged;
            // 
            // ChkSortList
            // 
            ChkSortList.AutoSize = true;
            ChkSortList.Location = new Point(399, 309);
            ChkSortList.Name = "ChkSortList";
            ChkSortList.Size = new Size(65, 19);
            ChkSortList.TabIndex = 23;
            ChkSortList.Text = "Sort list";
            TipMessage.SetToolTip(ChkSortList, "This will sort the current list automatically.");
            ChkSortList.UseVisualStyleBackColor = true;
            ChkSortList.CheckedChanged += ChkSortList_CheckedChanged;
            // 
            // BtnMoveDown
            // 
            BtnMoveDown.Location = new Point(318, 306);
            BtnMoveDown.Name = "BtnMoveDown";
            BtnMoveDown.Size = new Size(75, 23);
            BtnMoveDown.TabIndex = 22;
            BtnMoveDown.Text = "Item Down";
            TipMessage.SetToolTip(BtnMoveDown, "Hold 'Shift' before pressing to send the current item to the bottom of the list.");
            BtnMoveDown.UseVisualStyleBackColor = true;
            BtnMoveDown.Click += BtnMoveDown_Click;
            // 
            // BtnMoveUp
            // 
            BtnMoveUp.Location = new Point(318, 277);
            BtnMoveUp.Name = "BtnMoveUp";
            BtnMoveUp.Size = new Size(75, 23);
            BtnMoveUp.TabIndex = 21;
            BtnMoveUp.Text = "Item Up";
            TipMessage.SetToolTip(BtnMoveUp, "Hold 'Shift' before pressing to send the current item to the top of the list.");
            BtnMoveUp.UseVisualStyleBackColor = true;
            BtnMoveUp.Click += BtnMoveUp_Click;
            // 
            // LblSpeed
            // 
            LblSpeed.AutoSize = true;
            LblSpeed.Location = new Point(93, 281);
            LblSpeed.Name = "LblSpeed";
            LblSpeed.Size = new Size(85, 15);
            LblSpeed.TabIndex = 20;
            LblSpeed.Text = "Current Speed:";
            TipMessage.SetToolTip(LblSpeed, "When adding a file or a folder, the speed modifier sets the playback speed for those files.");
            // 
            // OfdAdd
            // 
            OfdAdd.Filter = "Supported files|notes.chart;notes.mid";
            OfdAdd.Title = "Select a .chart or .mid file from game song directory.";
            // 
            // SfdExport
            // 
            SfdExport.FileName = "Setlist.setlist";
            SfdExport.Filter = "CloneHero/ScoreSpy|*.setlist";
            SfdExport.Title = "Export current setlist to game format";
            // 
            // FbdExport
            // 
            FbdExport.Description = "Export current playlist to game format";
            FbdExport.RootFolder = Environment.SpecialFolder.MyComputer;
            FbdExport.ShowNewFolderButton = false;
            FbdExport.UseDescriptionForTitle = true;
            // 
            // BtnExportYarg
            // 
            BtnExportYarg.Location = new Point(707, 306);
            BtnExportYarg.Name = "BtnExportYarg";
            BtnExportYarg.Size = new Size(88, 23);
            BtnExportYarg.TabIndex = 25;
            BtnExportYarg.Text = "Export YARG";
            BtnExportYarg.UseVisualStyleBackColor = true;
            BtnExportYarg.Click += BtnExportYarg_Click;
            // 
            // NumSpeed
            // 
            NumSpeed.Increment = new decimal(new int[] { 5, 0, 0, 0 });
            NumSpeed.Location = new Point(184, 278);
            NumSpeed.Maximum = new decimal(new int[] { 5000, 0, 0, 0 });
            NumSpeed.Minimum = new decimal(new int[] { 5, 0, 0, 0 });
            NumSpeed.Name = "NumSpeed";
            NumSpeed.Size = new Size(47, 23);
            NumSpeed.TabIndex = 19;
            NumSpeed.Value = new decimal(new int[] { 100, 0, 0, 0 });
            NumSpeed.ValueChanged += NumSpeed_ValueChanged;
            NumSpeed.MouseClick += NumSpeed_MouseClick;
            // 
            // BtnAddFolder
            // 
            BtnAddFolder.Location = new Point(12, 306);
            BtnAddFolder.Name = "BtnAddFolder";
            BtnAddFolder.Size = new Size(75, 23);
            BtnAddFolder.TabIndex = 18;
            BtnAddFolder.Text = "Add Folder";
            BtnAddFolder.UseVisualStyleBackColor = true;
            BtnAddFolder.Click += BtnAddFolder_Click;
            // 
            // BtnClear
            // 
            BtnClear.Location = new Point(237, 306);
            BtnClear.Name = "BtnClear";
            BtnClear.Size = new Size(75, 23);
            BtnClear.TabIndex = 17;
            BtnClear.Text = "Clear";
            BtnClear.UseVisualStyleBackColor = true;
            BtnClear.Click += BtnClear_Click;
            // 
            // BtnRemove
            // 
            BtnRemove.Location = new Point(237, 277);
            BtnRemove.Name = "BtnRemove";
            BtnRemove.Size = new Size(75, 23);
            BtnRemove.TabIndex = 16;
            BtnRemove.Text = "Remove";
            BtnRemove.UseVisualStyleBackColor = true;
            BtnRemove.Click += BtnRemove_Click;
            // 
            // BtnExportCh
            // 
            BtnExportCh.Location = new Point(707, 277);
            BtnExportCh.Name = "BtnExportCh";
            BtnExportCh.Size = new Size(88, 23);
            BtnExportCh.TabIndex = 15;
            BtnExportCh.Text = "Export CH/SS";
            BtnExportCh.UseVisualStyleBackColor = true;
            BtnExportCh.Click += BtnExportCh_Click;
            // 
            // BtnAddFile
            // 
            BtnAddFile.Location = new Point(12, 277);
            BtnAddFile.Name = "BtnAddFile";
            BtnAddFile.Size = new Size(75, 23);
            BtnAddFile.TabIndex = 14;
            BtnAddFile.Text = "Add File";
            BtnAddFile.UseVisualStyleBackColor = true;
            BtnAddFile.Click += BtnAddFile_Click;
            // 
            // LstFiles
            // 
            LstFiles.FormattingEnabled = true;
            LstFiles.ItemHeight = 15;
            LstFiles.Location = new Point(12, 12);
            LstFiles.Name = "LstFiles";
            LstFiles.SelectionMode = SelectionMode.MultiExtended;
            LstFiles.Size = new Size(783, 259);
            LstFiles.TabIndex = 13;
            LstFiles.SelectedIndexChanged += LstFiles_SelectedIndexChanged;
            LstFiles.KeyDown += LstFiles_KeyDown;
            LstFiles.MouseDoubleClick += LstFiles_MouseDoubleClick;
            // 
            // BtnModify
            // 
            BtnModify.Location = new Point(156, 306);
            BtnModify.Name = "BtnModify";
            BtnModify.Size = new Size(75, 23);
            BtnModify.TabIndex = 26;
            BtnModify.Text = "Modify";
            BtnModify.UseVisualStyleBackColor = true;
            BtnModify.Click += BtnModify_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(807, 341);
            Controls.Add(BtnModify);
            Controls.Add(BtnExportYarg);
            Controls.Add(TxtSearch);
            Controls.Add(ChkSortList);
            Controls.Add(BtnMoveDown);
            Controls.Add(BtnMoveUp);
            Controls.Add(LblSpeed);
            Controls.Add(NumSpeed);
            Controls.Add(BtnAddFolder);
            Controls.Add(BtnClear);
            Controls.Add(BtnRemove);
            Controls.Add(BtnExportCh);
            Controls.Add(BtnAddFile);
            Controls.Add(LstFiles);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "MainForm";
            Text = "External Setlist Creator";
            ((System.ComponentModel.ISupportInitialize)NumSpeed).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ToolTip TipMessage;
        private OpenFileDialog OfdAdd;
        private SaveFileDialog SfdExport;
        private FolderBrowserDialog FbdAdd;
        private FolderBrowserDialog FbdExport;
        private Button BtnExportYarg;
        private TextBox TxtSearch;
        private CheckBox ChkSortList;
        private Button BtnMoveDown;
        private Button BtnMoveUp;
        private Label LblSpeed;
        private NumericUpDown NumSpeed;
        private Button BtnAddFolder;
        private Button BtnClear;
        private Button BtnRemove;
        private Button BtnExportCh;
        private Button BtnAddFile;
        private ListBox LstFiles;
        private Button BtnModify;
    }
}
