using Newtonsoft.Json;

namespace IssueManagerLibrary
{
    public class ServiceParameters
    {
        public required GitServiceProvider GtService { get; set; }
        public required string User { get; set; }
        public required string Repo { get; set; }
        public required string Token { get; set; }

        public static void ReadParametersFromFile(string filePath, out ServiceParameters? parameters)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    string json = File.ReadAllText(filePath);
                    parameters = JsonConvert.DeserializeObject<ServiceParameters>(json);
                }
                else
                {
                    parameters = null;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}