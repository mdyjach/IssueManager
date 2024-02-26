using IssueManagerConsoleApp.IssueOperation;
using IssueManagerLibrary;

namespace IssueManagerConsoleApp.IssueOperation
{
    class CloseIssueOperation : IOperation
    {
        public async Task Execute(GitService gitService)
        {
            Console.Write("Enter issue ID to close: ");
            string issueIdToClose = Console.ReadLine();
            await gitService.CloseIssue(issueIdToClose);
        }
    }
}
