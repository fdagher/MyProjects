using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace JsonToHtml
{
    internal class JsonParser
    {
        internal static string Parse(string json)
        {
            bool isMultiLevel = true;

            if (json.StartsWith("{"))
            {
                isMultiLevel = true;
            }
            else if (json.StartsWith("["))
            {
                isMultiLevel = false;
            }

            if (isMultiLevel)
            {
                return ParseJsonObject(json);
            }
            else
            {
                return ParseJsonArray(json);
            }
        }

        private static string ParseJsonObject(string json)
        {
            StringBuilder builder = new StringBuilder();

            JObject obj = JObject.Parse(json);

            builder.Append("<table><tbody>");

            foreach(var property in obj.Properties())
            {
                builder.Append("<tr><td><b>" + Beautify(property.Name) + "</b></td>");
                builder.Append("<td>");

                foreach(var data in property)
                {
                    if (data.Type == JTokenType.Array)
                    {
                        builder.Append("<table><thread><tr>");

                        var firstProperty = data.First;
                    
                        if (firstProperty != null)
                        {
                            foreach (JProperty propertyData in firstProperty)
                            {
                                builder.Append("<th>" + Beautify(propertyData.Name) + "</th>");
                            }
                        }

                        builder.Append("</tr></thread>");
                        builder.Append("<tbody>");

                        foreach (var propertyData in (data as JArray))
                        {
                            builder.Append("<tr>");

                            foreach (JProperty jProperty in propertyData)
                            {
                                if (jProperty.HasValues)
                                {
                                    if (jProperty.Value.Type == JTokenType.Object)
                                    {
                                        var inlineClass = ((JObject)jProperty.Value).Properties();
                                        var result = inlineClass.Select(x => string.Format("<div><b>{0}:</b><span> {1}</span></div>", x.Name, x.Value)).ToList();
                                        string joinedResult = string.Join("", result);

                                        builder.Append("<td><div>" + joinedResult + "</div></td>");
                                    }
                                    else
                                    {
                                        builder.Append("<td>" + jProperty.Value + "</td>");
                                    }
                                }
                            }
                            builder.Append("</tr>");
                        }

                        builder.Append("</tbody></table>");
                    }
                    else if (data.Type == JTokenType.Object)
                    {
                        var uniqueClass = data as JObject;

                        builder.Append("<table><thread><tr>");

                        var classProperties = uniqueClass.Properties()
                                                .Select(x => x.Name)
                                                .ToList();
                        foreach (var classProperty in classProperties)
                        {
                            builder.Append("<th>" + Beautify(classProperty) + "</th>");
                        }

                        builder.Append("</tr></thread>");

                        builder.Append("<tbody><tr>");

                        foreach (var classProperty in uniqueClass.Properties())
                        {
                            if (classProperty.Value.Type == JTokenType.Object)
                            {
                                var inlineClass = ((JObject)classProperty.Value).Properties();
                                var result = inlineClass.Select(x => string.Format("<div><b>{0}</b> <span>: {1}</span></div>", x.Name, x.Value)).ToList();
                                string joinedResult = string.Join("", result);

                                builder.Append("<td>");
                                builder.Append("<div>" + joinedResult + "</div>");
                                builder.Append("</td>");
                            }
                            else if (classProperty.Value.Type == JTokenType.Array)
                            {
                                builder.Append(ParseJsonArray(classProperty.Value.ToString()));
                            }
                            else
                            {
                                builder.Append("<td>" + classProperty.Value + "</td>");
                            }
                        }

                        builder.Append("</tr></tbody></table>");
                    }
                    else
                    {
                        builder.Append(data);
                    }
                }

                builder.Append("</td></tr>");
            }

            builder.Append("</tbody></table>");

            return builder.ToString();
        }

        private static string ParseJsonArray(string json)
        {
            StringBuilder builder = new StringBuilder();
            JArray jsonAsArray = JArray.Parse(json);

            var classes = jsonAsArray
                            .OfType<JObject>()
                            .ToList();
            var nonClasses = jsonAsArray
                            .Where(x => x.Type != JTokenType.Object)
                            .ToList();

            builder.Append("<table><thread><tr>");

            var anyClass = classes.FirstOrDefault();
            if (anyClass != null)
            {
                var properties = anyClass.Properties().Select(x => x.Name).ToList();

                foreach (var property in properties)
                {
                    builder.Append("<th>" + Beautify(property) + "</th>");
                }
            }
            else
            {
                builder.Append("<th>Data</th>");
            }

            builder.Append("</tr></thread>");

            builder.Append("<tbody>");

            foreach (JObject item in classes)
            {
                builder.Append("<tr>");

                foreach (var property in item.Properties())
                {
                    if (property.Value.Type == JTokenType.Array)
                    {
                        var newjson = property.Value.ToString();

                        builder.Append("<td>");

                        builder.Append(ParseJsonObject(newjson));

                        builder.Append("</td>");
                    }
                    else if (property.Value.Type == JTokenType.Object)
                    {
                        var inlineClass = ((JObject)property.Value).Properties();
                        var resultado = inlineClass.Select(x => string.Format("<div><b>{0}</b> <span>: {1}</span></div>", x.Name, x.Value)).ToList();
                        string resultadoUnido = string.Join("", resultado);

                        builder.Append("<td><div>");

                        builder.Append(resultadoUnido);

                        builder.Append("</div></td>");
                    }
                    else
                    {
                        builder.Append("<td>" + property.Value + "</td>");
                    }
                }

                builder.Append("</tr>");
            }

            foreach (JValue item in nonClasses)
            {
                builder.Append("<tr>");

                builder.Append("<td>" + item.Value + "</td>");

                builder.Append("</tr>");
            }

            builder.Append("</tbody></table>");

            return builder.ToString();
        }

        private static string Beautify(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return "";
            }

            var r = new Regex(@"
                (?<=[A-Z])(?=[A-Z][a-z]) |
                 (?<=[^A-Z])(?=[A-Z]) |
                 (?<=[A-Za-z])(?=[^A-Za-z])", RegexOptions.IgnorePatternWhitespace);

            text = text.First().ToString().ToUpper() + String.Join("", text.Skip(1));

            return r.Replace(text, " ");
        }
    }
}