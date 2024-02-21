using IssueManagerLibrary;

Console.WriteLine("Welcome to Git Service Console Interface!");

GitService gitService = null;

while (true)
{
    Console.WriteLine("\nChoose Git Service:");
    Console.WriteLine("1. GitHub");
    Console.WriteLine("2. GitLab");
    Console.WriteLine("3. Exit");

    Console.Write("Enter your choice: ");
    string serviceChoice = Console.ReadLine();

    if (serviceChoice == "1")
    {
        gitService = SetupGitHubService();
        break;
    }
    else if (serviceChoice == "2")
    {
        gitService = SetupGitLabService();
        break;
    }
    else if (serviceChoice == "3")
    {
        Environment.Exit(0);
    }
    else
    {
        Console.WriteLine("Invalid choice. Please try again.");
    }
}

await PerformOperations(gitService);

Console.WriteLine("\nPress any key to exit...");
Console.ReadKey();

static GitService SetupGitHubService()
{
    Console.Write("Enter GitHub username: ");
    string username = Console.ReadLine();
    Console.Write("Enter GitHub repository name: ");
    string repositoryName = Console.ReadLine();
    Console.Write("Enter GitHub access token: ");
    string accessToken = Console.ReadLine();

    string gitHubRepositoryUrl = GitHubService.GetRepositoryUrl(username, repositoryName);

    var gitHubHttpClient = new HttpClient();
    gitHubHttpClient.BaseAddress = new Uri(gitHubRepositoryUrl);

    return new GitHubService(gitHubHttpClient, gitHubRepositoryUrl, accessToken);
}

static GitService SetupGitLabService()
{
    Console.Write("Enter GitLab username or group name: ");
    string usernameOrGroupName = Console.ReadLine();
    Console.Write("Enter GitLab project name: ");
    string projectName = Console.ReadLine();
    Console.Write("Enter GitLab access token: ");
    string accessToken = Console.ReadLine();

    string gitLabRepositoryUrl = GitLabService.GetRepositoryUrl(usernameOrGroupName, projectName);

    var gitLabHttpClient = new HttpClient();
    gitLabHttpClient.BaseAddress = new Uri(gitLabRepositoryUrl);

    return new GitLabService(gitLabHttpClient, gitLabRepositoryUrl, accessToken);
}

static async Task PerformOperations(GitService gitService)
{
    while (true)
    {
        Console.WriteLine("\nChoose Operation:");
        Console.WriteLine("1. Add New Issue");
        Console.WriteLine("2. Modify Issue");
        Console.WriteLine("3. Close Issue");
        Console.WriteLine("4. Export Issues to File");
        Console.WriteLine("5. Import Issues from File");
        Console.WriteLine("6. Exit");

        Console.Write("Enter your choice: ");
        string operationChoice = Console.ReadLine();

        switch (operationChoice)
        {
            case "1":
                Console.Write("Enter issue title: ");
                string title = Console.ReadLine();
                Console.Write("Enter issue description: ");
                string description = Console.ReadLine();
                await gitService.AddNewIssue(title, description);
                break;
            case "2":
                Console.Write("Enter issue ID: ");
                string id = Console.ReadLine();
                Console.Write("Enter new title: ");
                string newTitle = Console.ReadLine();
                Console.Write("Enter new description: ");
                string newDescription = Console.ReadLine();
                await gitService.ModifyIssue(id, newTitle, newDescription);
                break;
            case "3":
                Console.Write("Enter issue ID to close: ");
                string issueIdToClose = Console.ReadLine();
                await gitService.CloseIssue(issueIdToClose);
                break;
            case "4":
                Console.Write("Enter issue ID to export: ");
                string issueIdToExport = Console.ReadLine();
                Console.Write("Enter file path to export to: ");
                string exportFilePath = Console.ReadLine();
                await gitService.ExportIssuesToFile(issueIdToExport, exportFilePath);
                break;
            case "5":
                Console.Write("Enter file path to import from: ");
                string importFilePath = Console.ReadLine();
                await gitService.ImportIssuesFromFile(importFilePath);
                break;
            case "6":
                Environment.Exit(0);
                break;
            default:
                Console.WriteLine("Invalid choice. Please try again.");
                break;
        }
    }
}