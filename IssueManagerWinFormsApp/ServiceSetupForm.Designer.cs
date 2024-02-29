namespace IssueManagerWinFormsApp
{
    partial class ServiceSetupForm
    {
        private System.ComponentModel.IContainer components = null;

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
            comboBoxService = new ComboBox();
            labelUsername = new Label();
            labelProjectName = new Label();
            labelAccessToken = new Label();
            textBoxUsername = new TextBox();
            textBoxProjectName = new TextBox();
            textBoxAccessToken = new TextBox();
            buttonOK = new Button();
            buttonCancel = new Button();
            LAbelService = new Label();
            SuspendLayout();
            // 
            // comboBoxService
            // 
            comboBoxService.FormattingEnabled = true;
            comboBoxService.Items.AddRange(new object[] { "GitHub", "GitLab" });
            comboBoxService.Location = new Point(105, 12);
            comboBoxService.Name = "comboBoxService";
            comboBoxService.Size = new Size(183, 23);
            comboBoxService.TabIndex = 0;
            // 
            // labelUsername
            // 
            labelUsername.AutoSize = true;
            labelUsername.Location = new Point(10, 46);
            labelUsername.Name = "labelUsername";
            labelUsername.Size = new Size(63, 15);
            labelUsername.TabIndex = 1;
            labelUsername.Text = "Username:";
            // 
            // labelProjectName
            // 
            labelProjectName.AutoSize = true;
            labelProjectName.Location = new Point(10, 72);
            labelProjectName.Name = "labelProjectName";
            labelProjectName.Size = new Size(82, 15);
            labelProjectName.TabIndex = 2;
            labelProjectName.Text = "Project Name:";
            // 
            // labelAccessToken
            // 
            labelAccessToken.AutoSize = true;
            labelAccessToken.Location = new Point(10, 98);
            labelAccessToken.Name = "labelAccessToken";
            labelAccessToken.Size = new Size(80, 15);
            labelAccessToken.TabIndex = 3;
            labelAccessToken.Text = "Access Token:";
            // 
            // textBoxUsername
            // 
            textBoxUsername.Location = new Point(105, 43);
            textBoxUsername.Name = "textBoxUsername";
            textBoxUsername.Size = new Size(337, 23);
            textBoxUsername.TabIndex = 4;
            // 
            // textBoxProjectName
            // 
            textBoxProjectName.Location = new Point(105, 69);
            textBoxProjectName.Name = "textBoxProjectName";
            textBoxProjectName.Size = new Size(337, 23);
            textBoxProjectName.TabIndex = 5;
            // 
            // textBoxAccessToken
            // 
            textBoxAccessToken.Location = new Point(105, 96);
            textBoxAccessToken.Name = "textBoxAccessToken";
            textBoxAccessToken.Size = new Size(337, 23);
            textBoxAccessToken.TabIndex = 6;
            // 
            // buttonOK
            // 
            buttonOK.Location = new Point(10, 122);
            buttonOK.Name = "buttonOK";
            buttonOK.Size = new Size(66, 22);
            buttonOK.TabIndex = 7;
            buttonOK.Text = "OK";
            buttonOK.UseVisualStyleBackColor = true;
            buttonOK.Click += buttonOK_Click;
            // 
            // buttonCancel
            // 
            buttonCancel.Location = new Point(81, 122);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new Size(66, 22);
            buttonCancel.TabIndex = 8;
            buttonCancel.Text = "Cancel";
            buttonCancel.UseVisualStyleBackColor = true;
            buttonCancel.Click += buttonCancel_Click;
            // 
            // LAbelService
            // 
            LAbelService.AutoSize = true;
            LAbelService.Location = new Point(12, 15);
            LAbelService.Name = "LAbelService";
            LAbelService.Size = new Size(47, 15);
            LAbelService.TabIndex = 9;
            LAbelService.Text = "Service:";
            // 
            // ServiceSetupForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(454, 158);
            Controls.Add(LAbelService);
            Controls.Add(buttonCancel);
            Controls.Add(buttonOK);
            Controls.Add(textBoxAccessToken);
            Controls.Add(textBoxProjectName);
            Controls.Add(textBoxUsername);
            Controls.Add(labelAccessToken);
            Controls.Add(labelProjectName);
            Controls.Add(labelUsername);
            Controls.Add(comboBoxService);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ServiceSetupForm";
            Text = "Service Setup";
            KeyPreview = true;
            KeyDown += new KeyEventHandler(ServiceSetupForm_KeyDown);
            ResumeLayout(false);
            PerformLayout();
        }

        private void ServiceSetupForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buttonOK.PerformClick();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                buttonCancel.PerformClick();
            }
        }

        private System.Windows.Forms.ComboBox comboBoxService;
        private System.Windows.Forms.Label labelUsername;
        private System.Windows.Forms.Label labelProjectName;
        private System.Windows.Forms.Label labelAccessToken;
        private System.Windows.Forms.TextBox textBoxUsername;
        private System.Windows.Forms.TextBox textBoxProjectName;
        private System.Windows.Forms.TextBox textBoxAccessToken;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private Label LAbelService;
    }
}
