using System;
using System.Net.Http;

namespace IssueManagerLibrary
{
    public enum GitServiceProvider
    {
        GitHub,
        GitLab    
    }

    public interface IGitServiceFactory
    {
        GitService CreateGitService(GitServiceProvider provider, HttpClient httpClient, string usernameOrGroupName, string projectName, string accessToken);
    }

    public class GitServiceFactory : IGitServiceFactory
    {
        public GitService CreateGitService(GitServiceProvider provider, HttpClient httpClient, string usernameOrGroupName, string projectName, string accessToken)
        {
            switch (provider)
            {
                case GitServiceProvider.GitHub:
                    return new GitHubService(httpClient, GitHubService.GetRepositoryUrl(usernameOrGroupName, projectName), accessToken);
                case GitServiceProvider.GitLab:
                    return new GitLabService(httpClient, GitLabService.GetRepositoryUrl(usernameOrGroupName, projectName), accessToken);
                default:
                    throw new ArgumentException("Unsupported Git service provider.");
            }
        }
    }
}
