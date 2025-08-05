# Detecting XSS code and deleting it

This project was created with Visual Studio 2022 and .Net Core 8 [Class VerifierXSS link](https://github.com/johnnydldev/DeleteXSSCode/blob/main/VerifierXSS.cs).

> [!IMPORTANT]
>
> ## DeleteXSSText Method Info

This I use and Regex pattern to capture the HTML tags and the Javascript scripting code injected:

```c#
    //The DeleteXSSText code used to delete malicious code injected
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

More information about implementation on **Program.cs** class on [Program.cs info](https://github.com/johnnydldev/DeleteXSSCode/blob/main/Program.cs)

> [!TIP]
> The implementation of **DeleteXSSText** you can do it the follow way:

```c#
    //Main method implementation to test
    static void Main(string[] args)
    {
    
        try
        {
            //String that simulate the malicious code injected on user input.
            string value = """
                Hola mi nombre es

                <div id='item-container'>
                    <section class="item-user-name">
                
                    </section>
                </div>
                
                <script type="module" src="../algo/path">
                    document.getElementById('');
                </script>

                Juan

                """;

            //Calling the method to sanitizing the info text of user input
            value = VerifierXSS.DeleteXSSText(value);

            //Printing the string sanitizated
            Console.WriteLine(value);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error {ex.Message}");

        }//End catching error

    }//End main method
    
```