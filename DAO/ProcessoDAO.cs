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
            var comando = _conexao.CreateCommand("SELECT * FROM processos;");
            var leitor = (MySqlDataReader)comando.ExecuteReader();

            while (leitor.Read())
            {
                lista.Add(MapearProcesso(leitor));
            }
            return lista;
        }

        // Método auxiliar: converte a linha atual do leitor em um objeto Processo.
        // Usa o DAOHelper para ler com segurança as colunas que podem ser NULL.
        private static Processo MapearProcesso(MySqlDataReader leitor)
        {
            return new Processo
            {
                Id = leitor.GetInt32("id_pro"),
                Numero = DAOHelper.GetString(leitor, "numero_pro"),
                Data = DAOHelper.GetDateTime(leitor, "data_pro"),
                Interessado = DAOHelper.GetString(leitor, "interessado_pro"),
                Assunto = DAOHelper.GetString(leitor, "assunto_pro"),
                Descricao = DAOHelper.GetString(leitor, "descricao_pro"),
                Situacao = DAOHelper.GetString(leitor, "situacao_pro")
            };
        }
    }
}
