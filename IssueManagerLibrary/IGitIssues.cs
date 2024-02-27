using System.Threading.Tasks;

namespace IssueManagerLibrary
{
    public interface IGitIssues
    {
        Task AddNewIssue(string name, string description, Action<string> onSuccess, Action<string> onFailure);
        Task ModifyIssue(string issueId, string newName, string newDescription, Action<string> onSuccess, Action<string> onFailure);
        Task CloseIssue(string issueId, Action<string> onSuccess, Action<string> onFailure);
        Task ExportIssuesToFile(string issueId, string filePath, Action<string> onSuccess, Action<string> onFailure);
        Task ImportIssuesFromFile(string filePath, Action<string> onSuccess, Action<string> onFailure);
    }
}
