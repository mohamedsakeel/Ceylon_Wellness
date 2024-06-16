using CeylonWellness.Web.Services;
using Newtonsoft.Json.Linq;
using Sanity.Linq;
using Sanity.Linq.CommonTypes;

namespace CeylonWellness.Web.Extensions
{
    public class TableSerializer
    {

        public static Task<string> Serialize(JToken block, object buildContext)
        {
            if (block == null)
            {
                throw new ArgumentNullException(nameof(block), "Block cannot be null.");
            }

            var rows = block["rows"];
            if (rows == null || !rows.Any())
            {
                return Task.FromResult("<table></table>"); // Return an empty table if there are no rows
            }

            var html = "<table>";

            foreach (var row in rows)
            {
                html += "<tr>";
                var cells = row["cells"];
                if (cells != null)
                {
                    foreach (var cell in cells)
                    {
                        var cellContent = cell?.ToString() ?? string.Empty; // Ensure cell is not null
                        html += $"<td>{cellContent}</td>";
                    }
                }
                html += "</tr>";
            }

            html += "</table>";
            return Task.FromResult(html);
        }
    }

}

