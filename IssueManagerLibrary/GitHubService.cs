using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace IssueManagerLibrary
{
    public class GitHubService : GitService
    {
        public GitHubService(HttpClient httpClient, string repositoryUrl, string accessToken) : base(httpClient, repositoryUrl, accessToken) { }

        public static string GetRepositoryUrl(string owner, string repo)
        {
            return $"https://api.github.com/repos/{owner}/{repo}/issues";
        }

        public override async Task AddNewIssue(string name, string description)
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
                    Console.WriteLine("Issue added successfully.");
                }
                else
                {
                    Console.WriteLine($"Failed to add new issue. Status code: {response.StatusCode}");
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error adding new issue on GitHub: {ex.Message}");
            }
        }

        public override async Task ModifyIssue(string issueId, string newName, string newDescription)
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
                    Console.WriteLine($"Issue {issueId} modified successfully.");
                }
                else
                {
                    Console.WriteLine($"Failed to modify issue {issueId}. Status code: {response.StatusCode}");
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error occurred while modifying issue: {ex.Message}");
            }
        }


        public override async Task CloseIssue(string issueId)
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
                    Console.WriteLine($"Issue {issueId} closed successfully.");
                }
                else
                {
                    Console.WriteLine($"Failed to close issue {issueId}. Status code: {response.StatusCode}");
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error occurred while closing issue: {ex.Message}");
            }
        }


        public override async Task ExportIssuesToFile(string issueId, string filePath)
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

                    // Write issue JSON data to a file
                    await File.WriteAllTextAsync(filePath, issueJson);

                    Console.WriteLine($"Issue {issueId} exported to file: {filePath}");
                }
                else
                {
                    Console.WriteLine($"Failed to export issue {issueId}. Status code: {response.StatusCode}");
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error occurred while exporting issue: {ex.Message}");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Error occurred while writing to file: {ex.Message}");
            }
        }


        public override async Task ImportIssuesFromFile(string filePath)
        {
            try
            {
                // Read issue data from the file
                var issueJson = await File.ReadAllTextAsync(filePath);

                // Deserialize JSON data to an object
                var issueData = JsonConvert.DeserializeObject<IssueData>(issueJson);

                // Construct the request body
                var jsonData = new
                {
                    title = issueData.Title,
                    body = issueData.Body
                };

                var content = new StringContent(JsonConvert.SerializeObject(jsonData), Encoding.UTF8, "application/json");

                // Send a POST request to create the issue
                var request = new HttpRequestMessage(HttpMethod.Post, _repositoryUrl);
                request.Headers.Add("Authorization", $"token {_accessToken}");
                request.Headers.Add("User-Agent", "IssueManagerConsoleApp");

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
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error occurred while importing issue: {ex.Message}");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Error occurred while reading file: {ex.Message}");
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Error occurred while deserializing JSON: {ex.Message}");
            }
        }
    }
}
