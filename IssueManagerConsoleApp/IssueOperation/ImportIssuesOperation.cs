using IssueManagerLibrary;

namespace IssueManagerConsoleApp.IssueOperation
{
    class ImportIssuesOperation : IOperation
    {
        public async Task Execute(GitService gitService)
        {
            Console.Write("Enter file path to import from: ");
            string importFilePath = Console.ReadLine();
            await gitService.ImportIssuesFromFile(importFilePath);
        }
    }
}
