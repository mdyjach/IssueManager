using IssueManagerLibrary;

namespace IssueManagerConsoleApp.IssueOperation
{
    class ImportIssuesOperation(Action<string> onSuccess, Action<string> onFailure) : IssueOperation(onSuccess, onFailure)
    {
        public override async Task Execute(GitService gitService)
        {
            Console.Write("Enter file path to import from: ");
            string importFilePath = Console.ReadLine();
            await gitService.ImportIssuesFromFile(importFilePath, _onSuccess, _onFailure);
        }
    }
}
