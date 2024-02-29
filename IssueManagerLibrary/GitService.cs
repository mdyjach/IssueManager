using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IssueManagerLibrary
{
    public abstract class GitService : IGitIssues
    {
        protected readonly HttpClient _httpClient;
        protected readonly string _repositoryUrl;
        protected readonly string _accessToken;

        public bool IsValid
        {
            get
            {
                bool urlIsValid = Uri.TryCreate(_repositoryUrl, UriKind.Absolute, out Uri result)
                      && (result.Scheme == Uri.UriSchemeHttp || result.Scheme == Uri.UriSchemeHttps);
                return urlIsValid && !string.IsNullOrEmpty(_accessToken);
            }
        }

        public GitService(HttpClient httpClient, string repositoryUrl, string accessToken)
        {
            _httpClient = httpClient;
            _repositoryUrl = repositoryUrl;
            _accessToken = accessToken;
        }

        public abstract Task AddNewIssue(string name, string description, Action<string> onSuccess, Action<string> onFailure);

        public abstract Task ModifyIssue(string issueId, string newName, string newDescription, Action<string> onSuccess, Action<string> onFailure);

        public abstract Task CloseIssue(string issueId, Action<string> onSuccess, Action<string> onFailure);
            
        public abstract Task ExportIssuesToFile(string issueId, string filePath, Action<string> onSuccess, Action<string> onFailure);

        public abstract Task ImportIssuesFromFile(string filePath, Action<string> onSuccess, Action<string> onFailure);
      
    }
}
