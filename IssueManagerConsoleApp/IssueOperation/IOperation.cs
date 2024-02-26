using IssueManagerLibrary;

namespace IssueManagerConsoleApp.IssueOperation
{
    interface IOperation
    {
        Task Execute(GitService gitService);
    }
}
