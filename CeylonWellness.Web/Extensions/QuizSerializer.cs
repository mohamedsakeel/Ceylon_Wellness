using Newtonsoft.Json.Linq;
using Sanity.Linq;
using System.Text;

namespace CeylonWellness.Web.Extensions
{
    public class QuizSerializer
    {
        public async Task<string> QuizSerializeAsync(JToken block, SanityOptions sanity)
        {
            var sb = new StringBuilder();

            var question = block["question"]?.ToString();
            var options = block["options"]?.ToObject<List<string>>();
            var correctAnswer = block["correctAnswer"]?.ToObject<int>();

            sb.Append("<div class=\"quiz\">");
            sb.AppendFormat("<h3>{0}</h3>", question);
            sb.Append("<ul>");
            for (int i = 0; i < options.Count; i++)
            {
                sb.AppendFormat("<li>{0}</li>", options[i]);
            }
            sb.Append("</ul>");
            sb.Append("</div>");

            return await Task.FromResult(sb.ToString());
        }
    }
}
