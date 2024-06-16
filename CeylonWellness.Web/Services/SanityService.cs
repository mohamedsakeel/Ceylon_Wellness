using CeylonWellness.Domain.Models;
using CloudinaryDotNet.Actions;
using Microsoft.CodeAnalysis.Scripting;
using Newtonsoft.Json.Linq;
using Sanity.Linq;
using Sanity.Linq.CommonTypes;
using System.Text;
using System.Text.Json;

namespace CeylonWellness.Web.Services
{
    public class SanityService
    {
        private SanityDataContext _context;

        public async Task SerializeToQuizFormat(JToken jToken)
        {
            // Deserialize the JToken into a Quiz object
            var quiz = jToken.ToObject<Quiz>();

            if (quiz == null)
            {
                throw new System.Exception("Failed to deserialize JToken to Quiz object");
            }

            // Convert the Quiz object to a custom serialized format
            var serializedQuiz = new
            {
                quiz.Question,
                Options = quiz.Options, // Directly assigning the list
                quiz.CorrectAnswer
            };

            // Serialize the custom format to JSON
            var json = JsonSerializer.Serialize(serializedQuiz);

            // Output the serialized quiz (for example purposes)
            System.Console.WriteLine(json);
        }

        public async Task<string> QuizSerializeAsync(JToken block, SanityOptions sanity)
        {
            var sb = new StringBuilder();

            var question = block["question"]?.ToString();
            var options = block["options"]?.ToObject<List<string>>();
            var correctAnswer = block["correctAnswer"]?.ToObject<int>();
            if (question != null && options != null)
            {
                sb.Append("<div class=\"quiz mb-4\">");
                sb.AppendFormat("<h3 class=\"mb-3\">{0}</h3>", question);
                sb.Append("<ul class=\"list-unstyled\">");

                for (int i = 0; i < options.Count; i++)
                {
                    sb.Append("<li class=\"mb-2\">");
                    sb.AppendFormat("<div class=\"form-check\">");
                    sb.AppendFormat("<input class=\"form-check-input\" type=\"radio\" name=\"quizOptions\" id=\"option{0}\" value=\"{1}\">", i, i);
                    sb.AppendFormat("<label class=\"form-check-label\" for=\"option{0}\">{1}</label>", i, options[i]);
                    sb.Append("</div>");
                    sb.Append("</li>");
                }
                sb.Append("</ul>");
                if (correctAnswer.HasValue)
                {
                    sb.AppendFormat("<input type=\"hidden\" id=\"correctAnswer\" value=\"{0}\" />", correctAnswer.Value);
                }
                sb.Append("</div>");
                sb.Append("<button type=\"button\" class=\"btn btn-primary\" onclick=\"checkAnswer()\">Submit</button>");
                sb.Append("<div id=\"result\" class=\"mt-3\"></div>");
                sb.Append("</div>");

            }
            return await Task.FromResult(sb.ToString());
        }

        //public SanityService()
        //{
        //    var options = new SanityOptions
        //    {
        //        ProjectId = "s3ly8pgl",
        //        Dataset = "production",
        //        Token = "skRfl9c8zxRWbCPqgX7fucHvoFqj44yaEWb5N7X4zeV12LN0p3Gm8VWJIKUyJgqRogH2XY3VPwfw0N4VRO5PYQZvDpXQ1VFxrOSvVYMspNgPCf4YtmIcWKTuy71HVPWC7SS632WS7g8ywUtBD2l84e2Z0CstZ1HYTfL98iT9rzG5XBosSotJ",
        //        UseCdn = false,

        //    };
        //    _context = new SanityDataContext(options);
        //    _context.AddHtmlSerializer("quiz", QuizSerializeAsync);

        //}

        public SanityDataContext GetContext()
        {
            var options = new SanityOptions
            {
                ProjectId = "s3ly8pgl",
                Dataset = "production",
                Token = "skRfl9c8zxRWbCPqgX7fucHvoFqj44yaEWb5N7X4zeV12LN0p3Gm8VWJIKUyJgqRogH2XY3VPwfw0N4VRO5PYQZvDpXQ1VFxrOSvVYMspNgPCf4YtmIcWKTuy71HVPWC7SS632WS7g8ywUtBD2l84e2Z0CstZ1HYTfL98iT9rzG5XBosSotJ",
                UseCdn = false,

            };
            var sanity = new SanityDataContext(options);
            sanity.AddHtmlSerializer("quiz", QuizSerializeAsync);
            return sanity;
        }
    }
}
