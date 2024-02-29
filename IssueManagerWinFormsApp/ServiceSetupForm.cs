using IssueManagerLibrary;

namespace IssueManagerWinFormsApp
{
    public partial class ServiceSetupForm : Form
    {
        private GitServiceProvider provider;
        internal GitService GitService { get; private set; }

        public ServiceSetupForm()
        {
            InitializeComponent();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            switch (comboBoxService.SelectedItem)
            {
                case "GitHub":
                    provider = GitServiceProvider.GitHub;
                    break;
                case "GitLab":
                    provider = GitServiceProvider.GitLab;
                    break;
            }

            HttpClient httpClient = new HttpClient();
            string repositoryUrl = GitServiceProviderHelper.GetRepositoryUrl(provider, textBoxUsername.Text, textBoxProjectName.Text);
            httpClient.BaseAddress = new Uri(repositoryUrl);

            GitServiceFactory gitServiceFactory = new GitServiceFactory();
            GitService gitService = gitServiceFactory.CreateGitService(provider, httpClient, textBoxUsername.Text, textBoxProjectName.Text, textBoxAccessToken.Text);

            GitService = gitService;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        public void SetcomboBoxService(GitServiceProvider provider)
        {
            this.provider = provider;
            comboBoxService.SelectedIndex = (int)provider;
        }

        public void SetTextBoxUsername(string username)
        {
            textBoxUsername.Text = username;
        }

        public void SetTextBoxProjectName(string projectName)
        {
            textBoxProjectName.Text = projectName;
        }

        public void SetTextBoxAccessToken(string accessToken)
        {
            textBoxAccessToken.Text = accessToken;
        }
    }
}