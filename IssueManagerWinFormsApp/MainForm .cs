using IssueManagerLibrary;
using IssueManagerWinFormsApp.IssueOperation;


namespace IssueManagerWinFormsApp
{
    public partial class MainForm : Form
    {
        private GitService _gitService;
        private ServiceSetupForm _serviceSetupForm; 

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            _serviceSetupForm = new ServiceSetupForm();
            CreateService();
            EnableAddIssueButton(false);
            EnableModifyIssueButton(false);
            EnableCloseIssueButton(false);
            EnableExportIssueButton(false);
            EnableImportIssueButton(false);
        }

        private bool CreateService()
        {
            try
            {
                ServiceParameters parameters;
                //bool readed = ServiceParameters.ReadParametersFromFile(@"D:\confGitHub.json", out parameters);
                bool readed = ServiceParameters.ReadParametersFromFile(@"D:\confGitLab.json", out parameters);

                if (readed && parameters != null)
                {

                    HttpClient httpClient = new HttpClient();
                    string repositoryUrl = GitServiceProviderHelper.GetRepositoryUrl(parameters.GtService, parameters.User, parameters.Repo);
                    httpClient.BaseAddress = new Uri(repositoryUrl);

                    GitServiceFactory gitServiceFactory = new GitServiceFactory();
                    _gitService = gitServiceFactory.CreateGitService(parameters.GtService, httpClient, parameters.User, parameters.Repo, parameters.Token);

                    _serviceSetupForm.SetcomboBoxService(parameters.GtService);
                    _serviceSetupForm.SetTextBoxUsername(parameters.User);
                    _serviceSetupForm.SetTextBoxProjectName(parameters.Repo);
                    _serviceSetupForm.SetTextBoxAccessToken(parameters.Token);
            }
                else
                {
                    ShowServiceSetupForm();
                }

                return readed;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error reading parameters: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }


        private void ShowServiceSetupForm()
        {
            if (_serviceSetupForm.ShowDialog() == DialogResult.OK)
            {
                _gitService =  _serviceSetupForm.GitService;
                this.Visible = true;
            }
            else
            {
                Application.Exit();
            }
        }

        private void serviceSetupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _serviceSetupForm.ShowDialog();
            _gitService = _serviceSetupForm.GitService;
        }

        private async Task ExecuteOperation(IOperation operation)
        {
            if (_gitService != null)
            {
                await operation.Execute(_gitService);
            }
            else
            {
                MessageBox.Show("Git service is not set up. Please set up the service first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ClearIssueDetails()
        {
            //add issue
            textBoxIssueTitle.Text = string.Empty;
            textBoxIssueDescription.Text = string.Empty;

            //modify issue
            textBoxModifyIssueId.Text = string.Empty;
            textBoxNewIssueTitle.Text = string.Empty;
            textBoxNewIssueDescription.Text = string.Empty;

            //close issue
            textBoxCloseIssueId.Text = string.Empty;

            //export issue
            textBoxExportIssueId.Text = string.Empty;
            textBoxExportFilePath.Text = string.Empty;

            //import issue
            textBoxImportFilePath.Text = string.Empty;
        }

        #region AddIssue

        public void EnableAddIssueButton(bool enable)
        {
            buttonAddIssue.Enabled = enable;
        }

        public void EnableAddIssueControls(bool enable)
        {
            buttonAddIssue.Enabled = enable;
            textBoxIssueTitle.Enabled = enable;
            textBoxIssueDescription.Enabled = enable;
        }

        private void textBoxAddIssueId_Changed(object sender, EventArgs e)
        {
            EnableAddIssueButton(!string.IsNullOrWhiteSpace(textBoxIssueTitle.Text));
        }

        private async void buttonAddIssue_Click(object sender, EventArgs e)
        {
            await ExecuteOperation(new AddIssueOperation(this));
        }

        public string GetIssueTitle()
        {
            return textBoxIssueTitle.Text;
        }

        public string GetIssueDescription()
        {
            return textBoxIssueDescription.Text;
        }

        #endregion AddIssue

        #region ModifyIssue

        public void EnableModifyIssueButton(bool enable)
        {
            buttonModifyIssue.Enabled = enable;
        }

        public void EnableModifyIssueControls(bool enable)
        {
            buttonModifyIssue.Enabled = enable;
            textBoxModifyIssueId.Enabled = enable;
            textBoxNewIssueTitle.Enabled = enable;
            textBoxNewIssueDescription.Enabled = enable;
        }

        private void textBoxModifyIssueId_Changed(object sender, EventArgs e)
        {
            EnableModifyIssueButton(!string.IsNullOrWhiteSpace(textBoxModifyIssueId.Text) && !string.IsNullOrWhiteSpace(textBoxNewIssueTitle.Text));
        }

        private async void buttonModifyIssue_Click(object sender, EventArgs e)
        {
            await ExecuteOperation(new ModifyIssueOperation(this));
        }

        public string GetModifyIssueId()
        {
            return textBoxModifyIssueId.Text;
        }

        public string GetNewIssueTitle()
        {
            return textBoxNewIssueTitle.Text;
        }

        public string GetNewIssueDescription()
        {
            return textBoxNewIssueDescription.Text;
        }

        #endregion ModifyIssue

        #region CloseIssue

        public void EnableCloseIssueButton(bool enable)
        {
            buttonCloseIssue.Enabled = enable;
        }

        public void EnableCloseIssueControls(bool enable)
        {
            buttonCloseIssue.Enabled = enable;
            textBoxCloseIssueId.Enabled = enable;
        }

        private void textBoxCloseIssueId_Changed(object sender, EventArgs e)
        {
            EnableCloseIssueButton(!string.IsNullOrWhiteSpace(textBoxCloseIssueId.Text));
        }

        private async void buttonCloseIssue_Click(object sender, EventArgs e)
        {
            await ExecuteOperation(new CloseIssueOperation(this));
        }

        public string GetCloseIssueId()
        {
            return textBoxCloseIssueId.Text;
        }

        #endregion CloseIssue

        #region ExportIssue

        public void EnableExportIssueButton(bool enable)
        {
            buttonExportIssue.Enabled = enable;
        }

        public void EnableExportIssueControls(bool enable)
        {
            buttonExportIssue.Enabled = enable;
            textBoxExportIssueId.Enabled = enable;
            textBoxExportFilePath.Enabled = enable;
            buttonBrowseExportFilePath.Enabled = enable;
        }

        private void textBoxExportControls_Changed(object sender, EventArgs e)
        {
            EnableExportIssueButton(!string.IsNullOrWhiteSpace(textBoxExportIssueId.Text) && !string.IsNullOrWhiteSpace(textBoxExportFilePath.Text));
        }

        private void buttonBrowseExportFilePath_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
                saveFileDialog.RestoreDirectory = true;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    textBoxExportFilePath.Text = saveFileDialog.FileName;
                }
            }
        }

        private async void buttonExportIssue_Click(object sender, EventArgs e)
        {
            string issueIdToExport = textBoxExportIssueId.Text;
            string exportFilePath = textBoxExportFilePath.Text;

            if (string.IsNullOrEmpty(issueIdToExport) || string.IsNullOrEmpty(exportFilePath))
            {
                MessageBox.Show("Please enter Issue ID and Export Path.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            await ExecuteOperation(new ExportIssuesOperation(this));
        }

        public string GetExportIssueId()
        {
            return textBoxExportIssueId.Text;
        }

        public string GetExportFilePath()
        {
            return textBoxExportFilePath.Text;
        }

        #endregion ExportIssue

        #region ImportIssue

        public void EnableImportIssueButton(bool enable)
        {
            buttonImportIssues.Enabled = enable;
        }

        public void EnableImportIssueControls(bool enable)
        {
            buttonImportIssues.Enabled = enable;
            textBoxImportFilePath.Enabled = enable;
            buttonBrowseImportFilePath.Enabled = enable;
        }

        private void textBoxImportFilePath_Changed(object sender, EventArgs e)
        {
            EnableImportIssueButton(!string.IsNullOrWhiteSpace(textBoxImportFilePath.Text));
        }

        private void buttonBrowseImportFilePath_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    textBoxImportFilePath.Text = openFileDialog.FileName;
                }
            }
        }

        private async void buttonImportIssues_Click(object sender, EventArgs e)
        {
            string importFilePath = textBoxImportFilePath.Text;

            if (string.IsNullOrEmpty(importFilePath))
            {
                MessageBox.Show("Please enter Issue ID and Export Path.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            await ExecuteOperation(new ImportIssuesOperation(this));
        }

        public string GetImportFilePath()
        {
            return textBoxImportFilePath.Text;
        }

        #endregion ImportIssue
    }
}
