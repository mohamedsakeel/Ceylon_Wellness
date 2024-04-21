using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace CeylonWellness.Repositories.Repositories
{
    public class FAQRepository : IFAQRepository
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public FAQRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://api.openai.com");
            _httpClient.DefaultRequestHeaders.Add("Authorization", _configuration["OpenAI:ApiKey"]);
        }

        public async Task<string> QueryFAQ(string question)
        {
            try
            {
                if (ContainsRelevantKeywords(question))
                {
                    var prompt = "Q: " + question + "\nA:";
                    var request = new
                    {
                        model = "gpt-3.5-turbo-instruct", // Or your preferred model
                        prompt,
                        max_tokens = 50, // Adjust as needed
                        temperature = 0.7, // Adjust as needed
                                           // Add any other parameters or customization for your AI model
                    };

                    var response = await _httpClient.PostAsJsonAsync("/v1/completions", request);
                    response.EnsureSuccessStatusCode();

                    var responseBody = await response.Content.ReadAsStringAsync();
                    dynamic responseJson = JsonConvert.DeserializeObject(responseBody);

                    var answer = responseJson.choices[0].text.ToString();

                    return answer;
                }
                else
                {
                    return "I'm sorry, I can only respond to questions related to the app.";
                }

            }
            catch (Exception ex)
            {
                return "An error occurred while processing the request: " + ex.Message;
            }
        }

        public bool ContainsRelevantKeywords(string question)
        {
            #region Keywords
            // Define relevant keywords
            string[] keywords = {
                "diet",
                "meal plan",
                "fitness",
                "nutrition",
                "healthy eating",
                "Sri Lanka",
                "nutritious food",
                "health dieting",
                "local food options",
                "mobile application",
                "dietary planning",
                "user-friendly",
                "personalized parameters",
                "caloric calculations",
                "flexibility in dietary plans",
                "domain knowledge",
                "protein consumption",
                "health goals",
                "accessibility",
                "user acceptance",
                "practical solutions",
                "tailored application",
                "interviews",
                "stakeholders",
                "biochemistry"
            };
            #endregion

            // Check if the question contains any of the keywords
            foreach (var keyword in keywords)
            {
                if (question.ToLower().Contains(keyword))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
