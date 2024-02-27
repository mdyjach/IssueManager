using IssueManagerConsoleApp.IssueOperation;
using IssueManagerLibrary;

namespace IssueManagerConsoleApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            Console.WriteLine("Welcome to Git Service Console Interface!");

            IGitServiceFactory gitServiceFactory = new GitServiceFactory();

            GitService gitService = SetupGitService(gitServiceFactory);

            await PerformOperations(gitService);

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }

        public static GitService SetupGitService(IGitServiceFactory gitServiceFactory)
        {
            while (true)
            {
                Console.WriteLine("\nChoose Git Service:");
                Console.WriteLine("1. GitHub");
                Console.WriteLine("2. GitLab");
                Console.WriteLine("3. Exit");

                Console.Write("Enter your choice: ");
                string serviceChoice = Console.ReadLine();

                switch (serviceChoice)
                {
                    case "1":
                        return SetupService(gitServiceFactory, GitServiceProvider.GitHub);
                    case "2":
                        return SetupService(gitServiceFactory, GitServiceProvider.GitLab);
                    case "3":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        public static GitService SetupService(IGitServiceFactory gitServiceFactory, GitServiceProvider provider)
        {
            Dictionary<GitServiceProvider, Func<string, string, string>> urlConstructors = new Dictionary<GitServiceProvider, Func<string, string, string>>()
            {
                { GitServiceProvider.GitHub, (username, project) => GitHubService.GetRepositoryUrl(username, project) },
                { GitServiceProvider.GitLab, (username, project) => GitLabService.GetRepositoryUrl(username, project) }
            };

            string providerName = provider.ToString();

            Console.Write($"Enter {providerName} username or group name: ");
            string usernameOrGroupName = Console.ReadLine();
            Console.Write($"Enter {providerName} repository or project name: ");
            string projectName = Console.ReadLine();
            Console.Write($"Enter {providerName} access token: ");
            string accessToken = Console.ReadLine();

            HttpClient httpClient = new HttpClient();
            string repositoryUrl = urlConstructors[provider](usernameOrGroupName, projectName);
            httpClient.BaseAddress = new Uri(repositoryUrl);

            return gitServiceFactory.CreateGitService(provider, httpClient, usernameOrGroupName, projectName, accessToken);
        }


        public static async Task PerformOperations(GitService gitService)
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
                        await ExecuteOperation(gitService, new AddIssueOperation(OnSuccess, OnFailure));
                        break;
                    case "2":
                        await ExecuteOperation(gitService, new ModifyIssueOperation(OnSuccess, OnFailure));
                        break;
                    case "3":
                        await ExecuteOperation(gitService, new CloseIssueOperation(OnSuccess, OnFailure));
                        break;
                    case "4":
                        await ExecuteOperation(gitService, new ExportIssuesOperation(OnSuccess, OnFailure));
                        break;
                    case "5":
                        await ExecuteOperation(gitService, new ImportIssuesOperation(OnSuccess, OnFailure));
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

        static async Task ExecuteOperation(GitService gitService, IOperation operation)
        {
            await operation.Execute(gitService);
        }

        static void OnSuccess(string message)
        {
            Console.WriteLine($"Success: {message}");
        }

        static void OnFailure(string message)
        {
            Console.WriteLine($"Failure: {message}");
        }
    }
}
