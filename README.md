# Detecting XSS code and deleting it

This project was created with Visual Studio 2022 and .Net Core 8 [Class VerifierXSS link](https://github.com/johnnydldev/DeleteXSSCode/blob/main/VerifierXSS.cs).

> [!IMPORTANT]
>
> ## DeleteXSSText Method Info

This I use and Regex pattern to capture the HTML tags and the Javascript scripting code injected:

```c#
    //Import the component with follow sentence above the main component with you've been work
    public static string DeleteXSSText(string value)
    {
        if (string.IsNullOrEmpty(value))
            return value;

        //Pattern to capture the XSS code injected
        string valueSanitized = value, pattern = @"(?<xss>((<{1}.*>{1})(.)(<{1}/{1}([a-z|A-Z])>{1})*))";

        try
        {
            Regex rgx = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Singleline);

            Match match = rgx.Match(valueSanitized);

            //Verifier the success match
            if (match.Success)
            {
            string matchXss = match.Groups["xss"].Value;

                if (!string.IsNullOrEmpty(matchXss))
                {
                    //Deleting the match code from original string
                    valueSanitized = valueSanitized.Replace(matchXss, string.Empty);

                    //Recursive call to verifier the info of all string still not contain the tags injected
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

```
