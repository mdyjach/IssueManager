using System.Threading.Tasks;

namespace IssueManagerLibrary
{
    public interface IGitIssues
    {
        Task AddNewIssue(string name, string description);
        Task ModifyIssue(string issueId, string newName, string newDescription);
        Task CloseIssue(string issueId);
        Task ExportIssuesToFile(string issueId, string filePath);
        Task ImportIssuesFromFile(string filePath);
    }
}
