using IssueManagerLibrary;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace IssueManagerLibrary
{
    public class GitLabService : GitService
    {
        public GitLabService(HttpClient httpClient, string repositoryUrl, string accessToken) : base(httpClient, repositoryUrl, accessToken) { }

        public static string GetRepositoryUrl(string repo, string project)
        {
            string projectPath = $"{repo}/{project}";
            string encodedProjectPath = Uri.EscapeDataString(projectPath);
            return $"https://gitlab.com/api/v4/projects/{encodedProjectPath}/issues";
        }

        public override async Task AddNewIssue(string title, string description)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, _repositoryUrl);
            request.Headers.Add("Authorization", $"Bearer {_accessToken}");

            var content = new MultipartFormDataContent();
            content.Add(new StringContent(title), "title");
            content.Add(new StringContent(description), "description");
            request.Content = content;

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Issue added successfully.");
            }
            else
            {
                Console.WriteLine($"Failed to add new issue. Status code: {response.StatusCode}");
            }
        }

        public override async Task ModifyIssue(string issueId, string newName, string newDescription)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, $"{_repositoryUrl}/{issueId}");
            request.Headers.Add("Authorization", $"Bearer {_accessToken}");

            var content = new MultipartFormDataContent();
            content.Add(new StringContent(newName), "title");
            content.Add(new StringContent(newDescription), "description");
            request.Content = content;

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Issue {issueId} modified successfully.");
            }
            else
            {
                Console.WriteLine($"Failed to modify issue {issueId}. Status code: {response.StatusCode}");
            }
        }

        public override async Task CloseIssue(string issueId)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, $"{_repositoryUrl}/{issueId}");
            request.Headers.Add("Authorization", $"Bearer {_accessToken}");

            var content = new MultipartFormDataContent();
            content.Add(new StringContent("close"), "state_event");
            request.Content = content;

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Issue {issueId} closed successfully.");
            }
            else
            {
                Console.WriteLine($"Failed to close issue {issueId}. Status code: {response.StatusCode}");
            }
        }

        public override async Task ExportIssuesToFile(string issueId, string filePath)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_repositoryUrl}/{issueId}");
            request.Headers.Add("Authorization", $"Bearer {_accessToken}");

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var issueJson = await response.Content.ReadAsStringAsync();

                await File.WriteAllTextAsync(filePath, issueJson);

                Console.WriteLine($"Issue {issueId} exported to file: {filePath}");
            }
            else if (response.StatusCode == HttpStatusCode.NotFound)
            {
                Console.WriteLine($"Issue {issueId} not found.");
            }
            else
            {
                Console.WriteLine($"Failed to export issue {issueId}. Status code: {response.StatusCode}");
            }
        }

        public override async Task ImportIssuesFromFile(string filePath)
        {
            var issueJson = await File.ReadAllTextAsync(filePath);

            var issueData = JsonConvert.DeserializeObject<IssueData>(issueJson);

            var jsonData = new
            {
                title = issueData.Title,
                description = issueData.Description
            };

            var content = new StringContent(JsonConvert.SerializeObject(jsonData), Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage(HttpMethod.Post, $"{_repositoryUrl}");
            request.Headers.Add("Authorization", $"Bearer {_accessToken}");

            request.Content = content;

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Issue imported successfully.");
            }
            else
            {
                Console.WriteLine($"Failed to import issue. Status code: {response.StatusCode}");
            }
        }
    }
}