using IssueManagerLibrary;

namespace IssueManagerWinFormsApp
{
    public partial class ServiceSetupForm : Form
    {
        public GitService GitService { get; private set; }

        public ServiceSetupForm()
        {
            InitializeComponent();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            GitServiceProvider provider = GitServiceProvider.GitHub;
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
            GitService gitService = gitServiceFactory.CreateGitService((IssueManagerLibrary.GitServiceProvider)provider, httpClient, textBoxUsername.Text, textBoxProjectName.Text, textBoxAccessToken.Text);

            GitService = gitService;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}