using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Web;

namespace CeylonWellness.Web.Controllers
{
    public class DanswerAPIController : Controller
    {
        private readonly HttpClient httpClient;
        private readonly string _apiUrl = "http://13.71.110.49:3000";
        private readonly string _loginEndpoint = "/api/auth/login";
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DanswerAPIController(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
        {
            httpClient = httpClientFactory.CreateClient();
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            username = "mhmdsakeel123@gmail.com";
            password = "991291113Vvv";
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "username", username },
                { "password", password }
            });

                var loginResponse = await httpClient.PostAsync($"{_apiUrl}{_loginEndpoint}", content);

                if (!loginResponse.IsSuccessStatusCode)
                {
                    throw new Exception("Login failed"); // Handle login failure appropriately
                }

                // Extract access token or cookie information from the login response
                string accessToken = null;
                string cookieName = null;
                string cookieValue = null;

                var responseContent = await loginResponse.Content.ReadAsStringAsync();
                dynamic responseObject = JsonConvert.DeserializeObject(responseContent); // Assuming JSON response

                // Check for access token or cookie based on your API's response structure
                if (responseObject.hasOwnProperty("access_token"))
                {
                    accessToken = responseObject.access_token;
                }
                else if (responseObject.hasOwnProperty("cookie_name") && responseObject.hasOwnProperty("cookie_value"))
                {
                    cookieName = responseObject.cookie_name;
                    cookieValue = responseObject.cookie_value;
                }
                else
                {
                    throw new Exception("Invalid login response format"); // Handle missing token/cookie
                }

                if (accessToken != null)
                {
                    var cookie = new CookieOptions
                    {
                        HttpOnly = true, // Set appropriate security flags
                        Secure = true // Set to true if using HTTPS
                    };
                    _httpContextAccessor.HttpContext.Response.Cookies.Append("fastapiusersauth", accessToken, cookie);
                }
                //else if (sessionID != null) // Adapt for session cookie if used
                //{
                //    var cookie = new CookieOptions
                //    {
                //        HttpOnly = true, // Set appropriate security flags
                //        Secure = true // Set to true if using HTTPS
                //    };
                //    _httpContextAccessor.HttpContext.Response.Cookies.Append("session_id", sessionID, cookie);
                //}
                else
                {
                    throw new Exception("Invalid login response format"); // Handle missing token/cookie
                }

                return RedirectToAction("CallApiEndpoint"); // Redirect to protected action
            }
        }

        public async Task<IActionResult> CallApiEndpoint()
        {
            string apiUrl = "http://13.71.110.49:3000/api/persona"; // Build full API URL

            try
            {
                using (var httpClient = new HttpClient())
                {
                    // Check for cookie containing access token or session ID
                    //var cookieValue = _httpContextAccessor.HttpContext.Request.Cookies["fastapiusersauth"]; // Adjust cookie name if needed
                    string cookieValue = "fastapiusersauth=oZ1craceTRar3RGx4K3wBX5g6OPUvYCyt0F9SVcmsWA";
                    if (cookieValue != null)
                    {
                        //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("set-cookie", cookieValue);
                        HttpContext.Response.Headers.Add("Cookie", "fastapiusersauth=oZ1craceTRar3RGx4K3wBX5g6OPUvYCyt0F9SVcmsWA");
                    }
                    else
                    {
                        // Handle missing cookie scenario (redirect to login?)
                        return RedirectToAction("Index");
                    }

                    HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

                    response.EnsureSuccessStatusCode();

                    string responseBody = await response.Content.ReadAsStringAsync();

                    // ... (Process response data)

                    return Content(responseBody, "application/json"); // Return raw response
                }
            }
            catch (HttpRequestException e)
            {
                return BadRequest($"Request error: {e.Message}");
            }
        }

            public IActionResult Index()
        {
            return View();
        }


    }
}
