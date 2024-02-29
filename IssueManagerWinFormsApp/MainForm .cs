using IssueManagerLibrary;
using IssueManagerWinFormsApp.IssueOperation;
using System.Globalization;


namespace IssueManagerWinFormsApp
{
    public partial class MainForm : Form
    {
        private GitService _gitService;
        private ServiceSetupForm _serviceSetupForm;
        private LanguageSelectionForm _languageSelectionForm;

        public MainForm()
        {
            InitializeComponent();
            EnableControls(false);
            EnableButtons(false);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            _serviceSetupForm = new ServiceSetupForm();
            _languageSelectionForm = new LanguageSelectionForm();
            CreateService();
        }

        private void SetCulture(string cultureName)
        {
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(cultureName);
        }

        private void CreateService()
        {
            CreateServiceFromFile();
            if (_gitService == null || !_gitService.IsValid)
            {
                ShowServiceSetupFormAtStart();
            }
        }

        private void CreateServiceFromFile()
        {
            try
            {
                ServiceParameters? parameters = null;
                Settings settings = new Settings();
                if (File.Exists(settings.GitHubConfigFilePath))
                {
                    ServiceParameters.ReadParametersFromFile(settings.GitHubConfigFilePath, out parameters);
                }
                else if (File.Exists(settings.GitLabConfigFilePath))
                {
                    ServiceParameters.ReadParametersFromFile(settings.GitLabConfigFilePath, out parameters);
                }

                if (parameters != null)
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

                    EnableControls(_gitService != null && _gitService.IsValid);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error reading parameters: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region controls actions

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (AboutBox aboutBox = new AboutBox())
            {
                aboutBox.ShowDialog();
            }
        }

        private DialogResult ShowServiceSetupForm()
        {
            DialogResult result = _serviceSetupForm.ShowDialog();

            if (result == DialogResult.OK)
            {
                _gitService = _serviceSetupForm.GitService;
                EnableControls(_gitService != null && _gitService.IsValid);
            }

            return result;
        }

        private void serviceSetupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowServiceSetupForm();
        }

        private void ShowServiceSetupFormAtStart()
        {
            if (ShowServiceSetupForm() == DialogResult.Cancel)
            {
                Application.Exit();
            }
        }

        private void languageToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (_languageSelectionForm.ShowDialog() == DialogResult.OK)
            {
                SetCulture(_languageSelectionForm.SelectedLanguage);
            }
        }

        #endregion controls actions

        #region set comonents enable and clear

        private void EnableControls(bool enable)
        {
            EnableAddIssueControls(enable);
            EnableModifyIssueControls(enable);
            EnableCloseIssueControls(enable);
            EnableExportIssueControls(enable);
            EnableImportIssueControls(enable);
        }

        private void EnableButtons(bool enable)
        {
            EnableAddIssueButton(enable);
            EnableModifyIssueButton(enable);
            EnableCloseIssueButton(enable);
            EnableExportIssueButton(enable);
            EnableImportIssueButton(enable);
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

        #endregion set comonents enable and clear

        #region service operationns

        private async Task ExecuteOperation(IOperation operation)
        {
            if (_gitService != null)
            {
                await operation.Execute(_gitService);
            }
            else
            {
                MessageBox.Show(Resources.GitServiceNotSetUp, Resources.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region AddIssue

        public void EnableAddIssueButton(bool enable)
        {
            buttonAddIssue.Enabled = enable;
        }

        public void EnableAddIssueControls(bool enable)
        {
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

        #endregion service operationns
    }
}
