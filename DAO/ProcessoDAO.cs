using AppWebArthurBraga.Configs;
using AppWebArthurBraga.Model;
using MySql.Data.MySqlClient;

namespace AppWebArthurBraga.DAO
{
    public class ProcessoDAO
    {
        private readonly Conexao _conexao;

        // A Conexao é injetada pelo container de dependências
        public ProcessoDAO(Conexao conexao)
        {
            _conexao = conexao;
        }

        // READ — lista todos os processos
        public List<Processo> Listar()
        {
            var lista = new List<Processo>();
            using var con = _conexao.GetConnection();
            using var comando = con.CreateCommand();
            comando.CommandText = "SELECT * FROM processos;";
            using var leitor = (MySqlDataReader)comando.ExecuteReader();

            while (leitor.Read())
            {
                lista.Add(MapearProcesso(leitor));
            }
            return lista;
        }

        // CREATE — insere um novo processo
        public void Inserir(Processo processo)
        {
            try
            {
                using var con = _conexao.GetConnection();

                string sql = @"INSERT INTO processos
                    (numero_pro, data_pro, interessado_pro,
                    assunto_pro, descricao_pro, situacao_pro)
                    VALUES
                    (@numero, @data, @interessado, @assunto, @descricao, @situacao)";

                using var comando = con.CreateCommand();
                comando.CommandText = sql;
                comando.Parameters.AddWithValue("@numero", processo.Numero);
                comando.Parameters.AddWithValue("@data", processo.Data!.Value.ToDateTime(TimeOnly.MinValue));
                comando.Parameters.AddWithValue("@interessado", processo.Interessado);
                comando.Parameters.AddWithValue("@assunto", processo.Assunto);
                comando.Parameters.AddWithValue("@descricao", processo.Descricao);
                comando.Parameters.AddWithValue("@situacao", processo.Situacao);

                comando.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
        }

        // READ — busca um processo pelo ID
        public Processo? BuscarPorId(int id)
        {
            using var con = _conexao.GetConnection();
            using var comando = con.CreateCommand();
            comando.CommandText = "SELECT * FROM processos WHERE id_pro = @id;";
            comando.Parameters.AddWithValue("@id", id);
            using var leitor = (MySqlDataReader)comando.ExecuteReader();

            if (leitor.Read())
            {
                return MapearProcesso(leitor);
            }
            return null;
        }

        // UPDATE — atualiza um processo existente
        public void Atualizar(Processo processo)
        {
            using var con = _conexao.GetConnection();
            using var comando = con.CreateCommand();
            comando.CommandText = @"
                UPDATE processos 
                SET numero_pro = @numero,
                    data_pro = @data,
                    interessado_pro = @interessado,
                    assunto_pro = @assunto,
                    descricao_pro = @descricao,
                    situacao_pro = @situacao
                WHERE id_pro = @id;";

            comando.Parameters.AddWithValue("@id", processo.Id);
            comando.Parameters.AddWithValue("@numero", processo.Numero);
            comando.Parameters.AddWithValue("@data", processo.Data.HasValue ? processo.Data.Value.ToDateTime(TimeOnly.MinValue) : DBNull.Value);
            comando.Parameters.AddWithValue("@interessado", processo.Interessado);
            comando.Parameters.AddWithValue("@assunto", processo.Assunto);
            comando.Parameters.AddWithValue("@descricao", (object?)processo.Descricao ?? DBNull.Value);
            comando.Parameters.AddWithValue("@situacao", processo.Situacao);

            comando.ExecuteNonQuery();
        }

        // DELETE — remove um processo pelo ID
        public void Excluir(int id)
        {
            using var con = _conexao.GetConnection();
            using var comando = con.CreateCommand();
            comando.CommandText = "DELETE FROM processos WHERE id_pro = @id;";
            comando.Parameters.AddWithValue("@id", id);
            comando.ExecuteNonQuery();
        }

        // Método auxiliar: converte a linha atual do leitor em um objeto Processo.
        // Usa o DAOHelper para ler com segurança as colunas que podem ser NULL.
        private static Processo MapearProcesso(MySqlDataReader leitor)
        {
            return new Processo
            {
                Id = leitor.GetInt32("id_pro"),
                Numero = DAOHelper.GetString(leitor, "numero_pro"),
                Data = DAOHelper.GetDateOnly(leitor, "data_pro"),
                Interessado = DAOHelper.GetString(leitor, "interessado_pro"),
                Assunto = DAOHelper.GetString(leitor, "assunto_pro"),
                Descricao = DAOHelper.GetString(leitor, "descricao_pro"),
                Situacao = DAOHelper.GetString(leitor, "situacao_pro")
            };
        }
    }
}
