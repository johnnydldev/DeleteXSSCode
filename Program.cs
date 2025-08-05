
namespace ThreadTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
           
            try
            {
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

                value = VerifierXSS.DeleteXSSText(value);

                Console.WriteLine(value);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error {ex.Message}");

            }

        }//End main method

    }//End Program class
}//End namespace
