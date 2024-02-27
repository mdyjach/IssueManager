using IssueManagerLibrary;

namespace IssueManagerConsoleApp.IssueOperation
{
    class ModifyIssueOperation(Action<string> onSuccess, Action<string> onFailure) : IssueOperation(onSuccess, onFailure)
    {
        public override async Task Execute(GitService gitService)
        {
            Console.Write("Enter issue ID: ");
            string id = Console.ReadLine();
            Console.Write("Enter new title: ");
            string newTitle = Console.ReadLine();
            Console.Write("Enter new description: ");
            string newDescription = Console.ReadLine();
            await gitService.ModifyIssue(id, newTitle, newDescription, _onSuccess, _onFailure);
        }
    }
}
