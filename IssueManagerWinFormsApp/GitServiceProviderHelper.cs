using IssueManagerLibrary;

namespace IssueManagerWinFormsApp
{
    public static class GitServiceProviderHelper
    {
        public static string GetRepositoryUrl(GitServiceProvider provider, string username, string project)
        {
            switch (provider)
            {
                case GitServiceProvider.GitHub:
                    return GitHubService.GetRepositoryUrl(username, project);
                case GitServiceProvider.GitLab:
                    return GitLabService.GetRepositoryUrl(username, project);
                default:
                    throw new ArgumentException("Invalid Git service provider.");
            }
        }
    }
}