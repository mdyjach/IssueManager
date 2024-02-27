using Newtonsoft.Json;
using System.Text;

namespace IssueManagerLibrary
{
    public class GitHubService : GitService
    {
        public GitHubService(HttpClient httpClient, string repositoryUrl, string accessToken) : base(httpClient, repositoryUrl, accessToken) { }

        public static string GetRepositoryUrl(string owner, string repo)
        {
            return $"https://api.github.com/repos/{owner}/{repo}/issues";
        }

        public override async Task AddNewIssue(string name, string description, Action<string> onSuccess, Action<string> onFailure)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Post, _repositoryUrl);
                request.Headers.Add("Authorization", $"token {_accessToken}");
                request.Headers.Add("User-Agent", "IssueManagerConsoleApp");

                var jsonContent = new
                {
                    title = name,
                    body = description
                };
                var content = new StringContent(JsonConvert.SerializeObject(jsonContent), Encoding.UTF8, "application/json");
                request.Content = content;

                var response = await _httpClient.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    onSuccess?.Invoke("Issue added successfully.");
                }
                else
                {
                    onFailure?.Invoke($"Failed to add new issue. Status code: {response.StatusCode}");
                }
            }
            catch (HttpRequestException ex)
            {
                onFailure?.Invoke($"Error adding new issue on GitHub: {ex.Message}");
            }
        }

        public override async Task ModifyIssue(string issueId, string newName, string newDescription, Action<string> onSuccess, Action<string> onFailure)
        {
            try
            {
                var jsonData = new
                {
                    title = newName,
                    body = newDescription
                };

                var content = new StringContent(JsonConvert.SerializeObject(jsonData), Encoding.UTF8, "application/json");

                var request = new HttpRequestMessage(new HttpMethod("PATCH"), $"{_repositoryUrl}/{issueId}");
                request.Headers.Add("Authorization", $"token {_accessToken}");
                request.Headers.Add("User-Agent", "IssueManagerConsoleApp");

                request.Content = content;

                var response = await _httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    onSuccess?.Invoke($"Issue {issueId} modified successfully.");
                }
                else
                {
                    onFailure?.Invoke($"Failed to modify issue {issueId}. Status code: {response.StatusCode}");
                }
            }
            catch (HttpRequestException ex)
            {
                onFailure?.Invoke($"Error occurred while modifying issue: {ex.Message}");
            }
        }


        public override async Task CloseIssue(string issueId, Action<string> onSuccess, Action<string> onFailure)
        {
            try
            {
                var jsonData = new
                {
                    state = "closed"
                };

                var content = new StringContent(JsonConvert.SerializeObject(jsonData), Encoding.UTF8, "application/json");

                var request = new HttpRequestMessage(new HttpMethod("PATCH"), $"{_repositoryUrl}/{issueId}");
                request.Headers.Add("Authorization", $"token {_accessToken}");
                request.Headers.Add("User-Agent", "IssueManagerConsoleApp");

                request.Content = content;

                var response = await _httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    onSuccess?.Invoke($"Issue {issueId} closed successfully.");
                }
                else
                {
                    onFailure?.Invoke($"Failed to close issue {issueId}. Status code: {response.StatusCode}");
                }
            }
            catch (HttpRequestException ex)
            {
                onFailure?.Invoke($"Error occurred while closing issue: {ex.Message}");
            }
        }


        public override async Task ExportIssuesToFile(string issueId, string filePath, Action<string> onSuccess, Action<string> onFailure)
        {
            try
            {
                var apiUrl = $"{_repositoryUrl}/{issueId}";

                var request = new HttpRequestMessage(HttpMethod.Get, apiUrl);
                request.Headers.Add("Authorization", $"token {_accessToken}");
                request.Headers.Add("User-Agent", "IssueManagerConsoleApp");

                var response = await _httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var issueJson = await response.Content.ReadAsStringAsync();

                    await File.WriteAllTextAsync(filePath, issueJson);

                    onSuccess?.Invoke($"Issue {issueId} exported to file: {filePath}");
                }
                else
                {
                    onFailure?.Invoke($"Failed to export issue {issueId}. Status code: {response.StatusCode}");
                }
            }
            catch (HttpRequestException ex)
            {
                onFailure?.Invoke($"Error occurred while exporting issue: {ex.Message}");
            }
            catch (IOException ex)
            {
                onFailure?.Invoke($"Error occurred while writing to file: {ex.Message}");
            }
        }

        public override async Task ImportIssuesFromFile(string filePath, Action<string> onSuccess, Action<string> onFailure)
        {
            try
            {
                var issueJson = await File.ReadAllTextAsync(filePath);

                var issueData = JsonConvert.DeserializeObject<IssueData>(issueJson);

                var jsonData = new
                {
                    title = issueData.Title,
                    body = issueData.Body
                };

                var content = new StringContent(JsonConvert.SerializeObject(jsonData), Encoding.UTF8, "application/json");

                var request = new HttpRequestMessage(HttpMethod.Post, _repositoryUrl);
                request.Headers.Add("Authorization", $"token {_accessToken}");
                request.Headers.Add("User-Agent", "IssueManagerConsoleApp");

                request.Content = content;

                var response = await _httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    onSuccess?.Invoke("Issue imported successfully.");
                }
                else
                {
                    onFailure?.Invoke($"Failed to import issue. Status code: {response.StatusCode}");
                }
            }
            catch (HttpRequestException ex)
            {
                onFailure?.Invoke($"Error occurred while importing issue: {ex.Message}");
            }
            catch (IOException ex)
            {
                onFailure?.Invoke($"Error occurred while reading file: {ex.Message}");
            }
            catch (JsonException ex)
            {
                onFailure?.Invoke($"Error occurred while deserializing JSON: {ex.Message}");
            }
        }

    }
}
