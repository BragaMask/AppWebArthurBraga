using MySql.Data.MySqlClient;

namespace AppWebArthurBraga.Configs
{
    public class Conexao
    {
        private readonly string _connectionString;

        // A IConfiguration é injetada automaticamente e permite ler o appsettings.json
        public Conexao(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("MySqlConnection") ?? "";
        }

        // Cria e ABRE uma nova conexão com o banco
        public MySqlConnection GetConnection()
        {
            var conn = new MySqlConnection(_connectionString);
            conn.Open();
            return conn;
        }

        // Cria um comando SQL. Se nenhuma conexão for passada, abre uma nova.
        public MySqlCommand CreateCommand(string query, MySqlConnection? conn = null)
        {
            conn ??= GetConnection();
            return new MySqlCommand(query, conn);
        }
    }
}
