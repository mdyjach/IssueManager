using System;
using System.Drawing;
using System.Windows.Forms;

namespace IssueManagerWinFormsApp
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem serviceSetupToolStripMenuItem;
        private TabControl tabControlMain;
        private TabPage tabPageAddIssue;
        private Label labelDescription;
        private Button buttonAddIssue;
        private TextBox textBoxIssueDescription;
        private TextBox textBoxIssueTitle;
        private Label labelTitle;
        private TabPage tabPageModifyIssue;
        private TextBox textBoxModifyIssueId;
        private Label labelModifyIssueId;
        private Label labelNewIssueDescription;
        private Label labelNewIssueTitle;
        private Button buttonModifyIssue;
        private TextBox textBoxNewIssueDescription;
        private TextBox textBoxNewIssueTitle;
        private TabPage tabPageCloseIssue;
        private Button buttonCloseIssue;
        private TextBox textBoxCloseIssueId;
        private Label labelCloseIssueId;
        private TabPage tabPageExportIssues;
        private Label labelExportIssueId;
        private Label labelExportFilePath;
        private TextBox textBoxExportIssueId;
        private TextBox textBoxExportFilePath;
        private Button buttonBrowseExportFilePath;
        private Button buttonExportIssue;
        private TabPage tabPageImportIssues;
        private Label labelImportFilePath;
        private TextBox textBoxImportFilePath;
        private Button buttonBrowseImportFilePath;
        private Button buttonImportIssues;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem  = new ToolStripMenuItem();
            serviceSetupToolStripMenuItem = new ToolStripMenuItem();
            tabControlMain = new TabControl();
            tabPageAddIssue = new TabPage();
            labelDescription = new Label();
            buttonAddIssue = new Button();
            textBoxIssueDescription = new TextBox();
            textBoxIssueTitle = new TextBox();
            labelTitle = new Label();
            tabPageModifyIssue = new TabPage();
            textBoxModifyIssueId = new TextBox();
            labelModifyIssueId = new Label();
            labelNewIssueDescription = new Label();
            labelNewIssueTitle = new Label();
            buttonModifyIssue = new Button();
            textBoxNewIssueDescription = new TextBox();
            textBoxNewIssueTitle = new TextBox();
            tabPageCloseIssue = new TabPage();
            buttonCloseIssue = new Button();
            textBoxCloseIssueId = new TextBox();
            labelCloseIssueId = new Label();
            tabPageExportIssues = new TabPage();
            labelExportIssueId = new Label();
            labelExportFilePath = new Label();
            textBoxExportIssueId = new TextBox();
            textBoxExportFilePath = new TextBox();
            buttonBrowseExportFilePath = new Button();
            buttonExportIssue = new Button();
            tabPageImportIssues = new TabPage();
            labelImportFilePath = new Label();
            textBoxImportFilePath = new TextBox();
            buttonBrowseImportFilePath = new Button();
            buttonImportIssues = new Button();
            tabControlMain.SuspendLayout();
            tabPageAddIssue.SuspendLayout();
            tabPageModifyIssue.SuspendLayout();
            tabPageCloseIssue.SuspendLayout();
            tabPageExportIssues.SuspendLayout();
            tabPageImportIssues.SuspendLayout();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            fileToolStripMenuItem}); // Dodano nowe ToolStripMenuItem do MenuStrip
            menuStrip1.Location = new System.Drawing.Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new System.Drawing.Size(800, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {serviceSetupToolStripMenuItem}); 
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            fileToolStripMenuItem.Text = "File";
            // 
            // serviceSetupToolStripMenuItem
            // 
            serviceSetupToolStripMenuItem.Name = "serviceSetupToolStripMenuItem";
            serviceSetupToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            serviceSetupToolStripMenuItem.Text = "Service Setup";
            serviceSetupToolStripMenuItem.Click += new System.EventHandler(serviceSetupToolStripMenuItem_Click);
            // 
            // ServiceSetupForm
            // 
            ClientSize = new System.Drawing.Size(800, 450);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "ServiceSetupForm";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();

            // 
            // tabControlMain
            // 
            tabControlMain.Controls.Add(tabPageAddIssue);
            tabControlMain.Controls.Add(tabPageModifyIssue);
            tabControlMain.Controls.Add(tabPageCloseIssue);
            tabControlMain.Controls.Add(tabPageExportIssues);
            tabControlMain.Controls.Add(tabPageImportIssues);
            tabControlMain.Dock = DockStyle.Fill;
            tabControlMain.Location = new Point(0, 0);
            tabControlMain.Name = "tabControlMain";
            tabControlMain.SelectedIndex = 0;
            tabControlMain.Size = new Size(488, 350);
            tabControlMain.TabIndex = 0;
            // 
            // tabPageAddIssue
            // 
            tabPageAddIssue.Controls.Add(labelDescription);
            tabPageAddIssue.Controls.Add(buttonAddIssue);
            tabPageAddIssue.Controls.Add(textBoxIssueDescription);
            tabPageAddIssue.Controls.Add(textBoxIssueTitle);
            tabPageAddIssue.Controls.Add(labelTitle);
            tabPageAddIssue.Location = new Point(4, 24);
            tabPageAddIssue.Name = "tabPageAddIssue";
            tabPageAddIssue.Padding = new Padding(3);
            tabPageAddIssue.Size = new Size(452, 322);
            tabPageAddIssue.TabIndex = 0;
            tabPageAddIssue.Text = "Add Issue";
            tabPageAddIssue.UseVisualStyleBackColor = true;
            // 
            // labelDescription
            // 
            labelDescription.AutoSize = true;
            labelDescription.Location = new Point(6, 59);
            labelDescription.Name = "labelDescription";
            labelDescription.Size = new Size(70, 15);
            labelDescription.TabIndex = 5;
            labelDescription.Text = "Description:";
            // 
            // buttonAddIssue
            // 
            buttonAddIssue.Location = new Point(82, 203);
            buttonAddIssue.Name = "buttonAddIssue";
            buttonAddIssue.Size = new Size(75, 23);
            buttonAddIssue.TabIndex = 3;
            buttonAddIssue.Text = "Add Issue";
            buttonAddIssue.UseVisualStyleBackColor = true;
            buttonAddIssue.Click += buttonAddIssue_Click;
            // 
            // textBoxIssueDescription
            // 
            textBoxIssueDescription.Location = new Point(82, 56);
            textBoxIssueDescription.Multiline = true;
            textBoxIssueDescription.Name = "textBoxIssueDescription";
            textBoxIssueDescription.Size = new Size(355, 141);
            textBoxIssueDescription.TabIndex = 2;
            // 
            // textBoxIssueTitle
            // 
            textBoxIssueTitle.Location = new Point(82, 19);
            textBoxIssueTitle.Name = "textBoxIssueTitle";
            textBoxIssueTitle.Size = new Size(355, 23);
            textBoxIssueTitle.TabIndex = 1;
            textBoxIssueTitle.TextChanged += textBoxAddIssueId_Changed;
            // 
            // labelTitle
            // 
            labelTitle.AutoSize = true;
            labelTitle.Location = new Point(8, 22);
            labelTitle.Name = "labelTitle";
            labelTitle.Size = new Size(32, 15);
            labelTitle.TabIndex = 0;
            labelTitle.Text = "Title:";
            // 
            // tabPageModifyIssue
            // 
            tabPageModifyIssue.Controls.Add(textBoxModifyIssueId);
            tabPageModifyIssue.Controls.Add(labelModifyIssueId);
            tabPageModifyIssue.Controls.Add(labelNewIssueDescription);
            tabPageModifyIssue.Controls.Add(labelNewIssueTitle);
            tabPageModifyIssue.Controls.Add(buttonModifyIssue);
            tabPageModifyIssue.Controls.Add(textBoxNewIssueDescription);
            tabPageModifyIssue.Controls.Add(textBoxNewIssueTitle);
            tabPageModifyIssue.Location = new Point(4, 24);
            tabPageModifyIssue.Name = "tabPageModifyIssue";
            tabPageModifyIssue.Padding = new Padding(3);
            tabPageModifyIssue.Size = new Size(452, 322);
            tabPageModifyIssue.TabIndex = 1;
            tabPageModifyIssue.Text = "Modify Issue";
            tabPageModifyIssue.UseVisualStyleBackColor = true;
            // 
            // textBoxModifyIssueId
            // 
            textBoxModifyIssueId.Location = new Point(150, 10);
            textBoxModifyIssueId.Name = "textBoxModifyIssueId";
            textBoxModifyIssueId.Size = new Size(287, 23);
            textBoxModifyIssueId.TabIndex = 4;
            textBoxModifyIssueId.TextChanged += textBoxModifyIssueId_Changed;
            // 
            // labelModifyIssueId
            // 
            labelModifyIssueId.AutoSize = true;
            labelModifyIssueId.Location = new Point(8, 13);
            labelModifyIssueId.Name = "labelModifyIssueId";
            labelModifyIssueId.Size = new Size(50, 15);
            labelModifyIssueId.TabIndex = 5;
            labelModifyIssueId.Text = "Issue ID:";
            // 
            // labelNewIssueDescription
            // 
            labelNewIssueDescription.AutoSize = true;
            labelNewIssueDescription.Location = new Point(8, 83);
            labelNewIssueDescription.Name = "labelNewIssueDescription";
            labelNewIssueDescription.Size = new Size(70, 15);
            labelNewIssueDescription.TabIndex = 4;
            labelNewIssueDescription.Text = "Description:";
            // 
            // labelNewIssueTitle
            // 
            labelNewIssueTitle.AutoSize = true;
            labelNewIssueTitle.Location = new Point(8, 46);
            labelNewIssueTitle.Name = "labelNewIssueTitle";
            labelNewIssueTitle.Size = new Size(32, 15);
            labelNewIssueTitle.TabIndex = 0;
            labelNewIssueTitle.Text = "Title:";
            // 
            // buttonModifyIssue
            // 
            buttonModifyIssue.Location = new Point(150, 278);
            buttonModifyIssue.Name = "buttonModifyIssue";
            buttonModifyIssue.Size = new Size(75, 23);
            buttonModifyIssue.TabIndex = 3;
            buttonModifyIssue.Text = "Modify Issue";
            buttonModifyIssue.UseVisualStyleBackColor = true;
            buttonModifyIssue.Click += buttonModifyIssue_Click;
            // 
            // textBoxNewIssueDescription
            // 
            textBoxNewIssueDescription.Location = new Point(150, 83);
            textBoxNewIssueDescription.Multiline = true;
            textBoxNewIssueDescription.Name = "textBoxNewIssueDescription";
            textBoxNewIssueDescription.Size = new Size(287, 189);
            textBoxNewIssueDescription.TabIndex = 2;
            // 
            // textBoxNewIssueTitle
            // 
            textBoxNewIssueTitle.Location = new Point(150, 46);
            textBoxNewIssueTitle.Name = "textBoxNewIssueTitle";
            textBoxNewIssueTitle.Size = new Size(287, 23);
            textBoxNewIssueTitle.TabIndex = 1;
            textBoxNewIssueTitle.TextChanged += textBoxModifyIssueId_Changed;
            // 
            // tabPageCloseIssue
            // 
            tabPageCloseIssue.Controls.Add(buttonCloseIssue);
            tabPageCloseIssue.Controls.Add(textBoxCloseIssueId);
            tabPageCloseIssue.Controls.Add(labelCloseIssueId);
            tabPageCloseIssue.Location = new Point(4, 24);
            tabPageCloseIssue.Name = "tabPageCloseIssue";
            tabPageCloseIssue.Padding = new Padding(3);
            tabPageCloseIssue.Size = new Size(452, 322);
            tabPageCloseIssue.TabIndex = 2;
            tabPageCloseIssue.Text = "Close Issue";
            tabPageCloseIssue.UseVisualStyleBackColor = true;
            // 
            // buttonCloseIssue
            // 
            buttonCloseIssue.Location = new Point(135, 45);
            buttonCloseIssue.Name = "buttonCloseIssue";
            buttonCloseIssue.Size = new Size(90, 25);
            buttonCloseIssue.TabIndex = 2;
            buttonCloseIssue.Text = "Close Issue";
            buttonCloseIssue.UseVisualStyleBackColor = true;
            buttonCloseIssue.Click += buttonCloseIssue_Click;
            // 
            // textBoxCloseIssueId
            // 
            textBoxCloseIssueId.Location = new Point(135, 16);
            textBoxCloseIssueId.Name = "textBoxCloseIssueId";
            textBoxCloseIssueId.Size = new Size(287, 23);
            textBoxCloseIssueId.TabIndex = 1;
            textBoxCloseIssueId.TextChanged += textBoxCloseIssueId_Changed;
            // 
            // labelCloseIssueId
            // 
            labelCloseIssueId.AutoSize = true;
            labelCloseIssueId.Location = new Point(10, 19);
            labelCloseIssueId.Name = "labelCloseIssueId";
            labelCloseIssueId.Size = new Size(50, 15);
            labelCloseIssueId.TabIndex = 0;
            labelCloseIssueId.Text = "Issue ID:";
            // 
            // tabPageExportIssues
            // 
            tabPageExportIssues.Controls.Add(labelExportIssueId);
            tabPageExportIssues.Controls.Add(labelExportFilePath);
            tabPageExportIssues.Controls.Add(textBoxExportIssueId);
            tabPageExportIssues.Controls.Add(textBoxExportFilePath);
            tabPageExportIssues.Controls.Add(buttonBrowseExportFilePath);
            tabPageExportIssues.Controls.Add(buttonExportIssue);
            tabPageExportIssues.Location = new Point(4, 24);
            tabPageExportIssues.Name = "tabPageExportIssues";
            tabPageExportIssues.Padding = new Padding(3);
            tabPageExportIssues.Size = new Size(452, 322);
            tabPageExportIssues.TabIndex = 3;
            tabPageExportIssues.Text = "Export Issue";
            tabPageExportIssues.UseVisualStyleBackColor = true;
            // 
            // labelExportIssueId
            // 
            labelExportIssueId.AutoSize = true;
            labelExportIssueId.Location = new Point(10, 20);
            labelExportIssueId.Name = "labelExportIssueId";
            labelExportIssueId.Size = new Size(50, 15);
            labelExportIssueId.TabIndex = 0;
            labelExportIssueId.Text = "Issue ID:";
            // 
            // labelExportFilePath
            // 
            labelExportFilePath.AutoSize = true;
            labelExportFilePath.Location = new Point(10, 50);
            labelExportFilePath.Name = "labelExportFilePath";
            labelExportFilePath.Size = new Size(71, 15);
            labelExportFilePath.TabIndex = 1;
            labelExportFilePath.Text = "Export Path:";
            // 
            // textBoxExportIssueId
            // 
            textBoxExportIssueId.Location = new Point(150, 20);
            textBoxExportIssueId.Name = "textBoxExportIssueId";
            textBoxExportIssueId.Size = new Size(287, 23);
            textBoxExportIssueId.TabIndex = 2;
            textBoxExportIssueId.TextChanged += textBoxExportControls_Changed;
            // 
            // textBoxExportFilePath
            // 
            textBoxExportFilePath.Location = new Point(150, 50);
            textBoxExportFilePath.Name = "textBoxExportFilePath";
            textBoxExportFilePath.Size = new Size(282, 23);
            textBoxExportFilePath.TabIndex = 3;
            textBoxExportFilePath.TextChanged += textBoxExportControls_Changed;
            // 
            // buttonBrowseExportFilePath
            // 
            buttonBrowseExportFilePath.Location = new Point(438, 50);
            buttonBrowseExportFilePath.Name = "buttonBrowseExportFilePath";
            buttonBrowseExportFilePath.Size = new Size(30, 23);
            buttonBrowseExportFilePath.TabIndex = 5;
            buttonBrowseExportFilePath.Text = "...";
            buttonBrowseExportFilePath.UseVisualStyleBackColor = true;
            buttonBrowseExportFilePath.Click += buttonBrowseExportFilePath_Click;
            // 
            // buttonExportIssue
            // 
            buttonExportIssue.Location = new Point(150, 79);
            buttonExportIssue.Name = "buttonExportIssue";
            buttonExportIssue.Size = new Size(75, 23);
            buttonExportIssue.TabIndex = 4;
            buttonExportIssue.Text = "Export";
            buttonExportIssue.UseVisualStyleBackColor = true;
            buttonExportIssue.Click += buttonExportIssue_Click;
            // 
            // tabPageImportIssues
            // 
            tabPageImportIssues.Controls.Add(labelImportFilePath);
            tabPageImportIssues.Controls.Add(textBoxImportFilePath);
            tabPageImportIssues.Controls.Add(buttonBrowseImportFilePath);
            tabPageImportIssues.Controls.Add(buttonImportIssues);
            tabPageImportIssues.Location = new Point(4, 24);
            tabPageImportIssues.Name = "tabPageImportIssues";
            tabPageImportIssues.Padding = new Padding(3);
            tabPageImportIssues.Size = new Size(480, 322);
            tabPageImportIssues.TabIndex = 4;
            tabPageImportIssues.Text = "Import Issues";
            tabPageImportIssues.UseVisualStyleBackColor = true;
            // 
            // labelImportFilePath
            // 
            labelImportFilePath.AutoSize = true;
            labelImportFilePath.Location = new Point(10, 15);
            labelImportFilePath.Name = "labelImportFilePath";
            labelImportFilePath.Size = new Size(94, 15);
            labelImportFilePath.TabIndex = 0;
            labelImportFilePath.Text = "Import File Path:";
            // 
            // textBoxImportFilePath
            // 
            textBoxImportFilePath.Location = new Point(110, 12);
            textBoxImportFilePath.Name = "textBoxImportFilePath";
            textBoxImportFilePath.Size = new Size(322, 23);
            textBoxImportFilePath.TabIndex = 1;
            textBoxImportFilePath.TextChanged += textBoxImportFilePath_Changed;
            // 
            // buttonBrowseImportFilePath
            // 
            buttonBrowseImportFilePath.Location = new Point(438, 12);
            buttonBrowseImportFilePath.Name = "buttonBrowseImportFilePath";
            buttonBrowseImportFilePath.Size = new Size(30, 23);
            buttonBrowseImportFilePath.TabIndex = 2;
            buttonBrowseImportFilePath.Text = "...";
            buttonBrowseImportFilePath.UseVisualStyleBackColor = true;
            buttonBrowseImportFilePath.Click += buttonBrowseImportFilePath_Click;
            // 
            // buttonImportIssues
            // 
            buttonImportIssues.Location = new Point(110, 41);
            buttonImportIssues.Name = "buttonImportIssues";
            buttonImportIssues.Size = new Size(100, 23);
            buttonImportIssues.TabIndex = 3;
            buttonImportIssues.Text = "Import Issues";
            buttonImportIssues.UseVisualStyleBackColor = true;
            buttonImportIssues.Click += buttonImportIssues_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(488, 350);
            Controls.Add(tabControlMain);
            MaximizeBox = false;
            Name = "MainForm";
            Text = "MainForm";
            Load += MainForm_Load;
            tabControlMain.ResumeLayout(false);
            tabPageAddIssue.ResumeLayout(false);
            tabPageAddIssue.PerformLayout();
            tabPageModifyIssue.ResumeLayout(false);
            tabPageModifyIssue.PerformLayout();
            tabPageCloseIssue.ResumeLayout(false);
            tabPageCloseIssue.PerformLayout();
            tabPageExportIssues.ResumeLayout(false);
            tabPageExportIssues.PerformLayout();
            tabPageImportIssues.ResumeLayout(false);
            tabPageImportIssues.PerformLayout();
            ResumeLayout(false);
        }
    }
}
