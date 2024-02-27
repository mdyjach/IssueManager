using IssueManagerLibrary;

namespace IssueManagerConsoleApp.IssueOperation
{
    class CloseIssueOperation(Action<string> onSuccess, Action<string> onFailure) : IssueOperation(onSuccess, onFailure)
    {
        public override async Task Execute(GitService gitService)
        {
            Console.Write("Enter issue ID to close: ");
            string issueIdToClose = Console.ReadLine();
            await gitService.CloseIssue(issueIdToClose, _onSuccess, _onFailure);
        }
    }
}
