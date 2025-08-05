
using System.Text.RegularExpressions;

namespace ThreadTest
{
    public class VerifierXSS
    {

        public static string DeleteXSSText(string value)
        {
            if (string.IsNullOrEmpty(value))
                return value;

            string valueSanitized = value, pattern = @"(?<xss>((<{1}.*>{1})(.)(<{1}/{1}([a-z|A-Z])>{1})*))";

            try
            {
                Regex rgx = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Singleline);

                Match match = rgx.Match(valueSanitized);

                if (match.Success)
                {
                   string matchXss = match.Groups["xss"].Value;

                    if (!string.IsNullOrEmpty(matchXss))
                    {
                        valueSanitized = valueSanitized.Replace(matchXss, string.Empty);

                        DeleteXSSText(valueSanitized);
                    }

                }//End verify
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"Error on tried delete xss data, info: {ex.Message}");
            }

            return valueSanitized;

        }//End delete XSS text method


    }//End verifier XSS text
}//End namespace
