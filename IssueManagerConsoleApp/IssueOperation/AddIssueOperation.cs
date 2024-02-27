using IssueManagerLibrary;

namespace IssueManagerConsoleApp.IssueOperation
{
    class AddIssueOperation(Action<string> onSuccess, Action<string> onFailure) : IssueOperation(onSuccess, onFailure)
    {
        public override async Task Execute(GitService gitService)
        {
            Console.Write("Enter issue title: ");
            string title = Console.ReadLine();
            Console.Write("Enter issue description: ");
            string description = Console.ReadLine();

            await gitService.AddNewIssue(title, description, _onSuccess, _onFailure);
        }
    }
}
