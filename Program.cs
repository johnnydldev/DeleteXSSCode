using System.Data.SqlTypes;
using System.Data.SqlClient;

namespace ThreadTest
{
    internal class Program
    {

        private static SessionInfo _CreateSession = new SessionInfo()
        {
            SessionId = Guid.NewGuid().ToString(),
            Browser = "Firefox",
            IPAddress = "192.168.20.45"
        };

        private static SessionInfo _SessionUpdated = new SessionInfo()
        {
            SessionId = Guid.NewGuid().ToString(),
            Browser = "Edge",
            IPAddress = "10.34.65.17"
        };

        private static SqlCommand _cmd;

        private static SqlConnection _connection;

        private static SqlDataReader _reader;

        static void Main(string[] args)
        {
            //CreateSession();
            //Thread _init = new Thread(()=> poolingSimulate());
            //_init.Start();

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

        public static void CreateSession()
        {
            try
            {
                using (_connection = new SqlConnection(StringConnection.StringConnectionDB))
                {
                    _cmd = new("sp_CreateSession", _connection)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure
                    };
                    _cmd.Parameters.AddWithValue("SessionId",_CreateSession.SessionId);
                    _cmd.Parameters.AddWithValue("Browser", _CreateSession.Browser);
                    _cmd.Parameters.AddWithValue("IPAddress", _CreateSession.IPAddress);

                    _connection.Open();
                    int IsCreated = _cmd.ExecuteNonQuery();

                    string messageConsole = (IsCreated > 0) ? "Session info created successfully" : "Session info create failed";

                    Console.WriteLine(messageConsole);
                   
                }//End using connection obj
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Has been occured the next error: {ex.Message}");
            }//End catch
        }//

        public static void UpdateSession()
        {
            try
            {
                using (_connection = new SqlConnection(StringConnection.StringConnectionDB))
                {
                    _cmd = new("sp_UpdateSession", _connection)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure
                    };
                    _cmd.Parameters.AddWithValue("SessionId", _CreateSession.SessionId);
                    _cmd.Parameters.AddWithValue("Browser", _CreateSession.Browser);
                    _cmd.Parameters.AddWithValue("IPAddress", _CreateSession.IPAddress);

                    _connection.Open();
                    int IsCreated = _cmd.ExecuteNonQuery();

                    string messageConsole = (IsCreated > 0) ? "Session info created successfully" : "Session info create failed";

                    Console.WriteLine(messageConsole);

                }//End using connection obj
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Has been occured the next error: {ex.Message}");
            }//End catch
        }//

        private static void poolingSimulate()
        {
            while (true)
            {
                GetSession();
                Thread.Sleep(5000);
            }//
        }//


        public static void GetSession()
        {
            SessionInfo sessionInfo = new SessionInfo();

            try
            {
                using (_connection = new SqlConnection(StringConnection.StringConnectionDB))
                {
                    _cmd = new("sp_Get_Session_By_Id", _connection)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure
                    };

                    _connection.Open();

                    using (_reader = _cmd.ExecuteReader())
                    {
                        while (_reader.Read())
                        {
                            sessionInfo = new SessionInfo()
                            {
                                SessionId = _reader["SessionId"].ToString(),
                                Browser = _reader["Browser"].ToString(),
                                IPAddress = _reader["IPAddress"].ToString()
                            };
                        }//
                    }//End reader use

                    if (!string.IsNullOrEmpty(sessionInfo.SessionId))
                    {
                        Console.WriteLine($"The session id: {sessionInfo.SessionId}, Browser: {sessionInfo.Browser}, Ip address: {sessionInfo.IPAddress}");
                    }

                }//End using connection obj
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Has been occured the next error: {ex.Message}");
            }//End catch

        }//


         

    }//End Program class
}//End namespace
