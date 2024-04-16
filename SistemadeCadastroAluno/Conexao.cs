using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemadeCadastroAluno
{
    internal class Conexao
    {
        public static MySqlConnection conn;
        public static string conexao = "server=localhost;" +
            "database=dbalunos;" +
            "uid=root;" +
            "pwb=";

        public static void Conectar()
        {
            conn = new MySqlConnection(conexao);
            conn.Open();
        }
        public static void Desconectar()
        {
            conn.Close();
        }
    }
}
