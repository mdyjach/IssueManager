using IssueManagerLibrary;

namespace IssueManagerConsoleApp.IssueOperation
{
    class ExportIssuesOperation(Action<string> onSuccess, Action<string> onFailure) : IssueOperation(onSuccess, onFailure)
    {
        public override async Task Execute(GitService gitService)
        {
            Console.Write("Enter issue ID to export: ");
            string issueIdToExport = Console.ReadLine();
            Console.Write("Enter file path to export to: ");
            string exportFilePath = Console.ReadLine();
            await gitService.ExportIssuesToFile(issueIdToExport, exportFilePath, _onSuccess, _onFailure);
        }
    }
}
