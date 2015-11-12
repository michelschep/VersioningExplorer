using Newtonsoft.Json.Linq;

namespace VersioningExplorer.Schema.Json
{
    /// <summary>
    /// Strategy towards rewriting a JSON document.
    /// </summary>
    interface IJsonRewriter
    {
        /// <summary>
        /// Rewrite a JSON document.
        /// </summary>
        /// <param name="json">to rewrite</param>
        /// <returns>rewritten JSON</returns>
        JObject Rewrite(JObject json);
    }
}
