using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemadeCadastroAluno
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public void SelecionarDados()
        {
            try
            {
                Conexao.Conectar();
                string sql = "SELECT * FROM tab_alunos";
                MySqlCommand cmd = new MySqlCommand(sql, Conexao.conn);
                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());

                dgvTabela.DataSource = dt;
                Conexao.Desconectar();


            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao selecionar os dados da tabela Finanças");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            try
            {
                // Abre conexão com o DB
                Conexao.Conectar();

                // Declara variáveis
                string nome = txtNome.Text;
                string endereco = txtEnd.Text;
                DateTime data = DateTime.Parse(dtpData.Text);
                string cpf = mtxtCPF.Text;
                string rg = mtxtRG.Text;

                // STRING de instrução SQL
                string sql = "INSERT INTO tab_alunos(nome,endereco,data,cpf,rg) VALUE(@nome, @endereco, @data, @cpf, @rg)";

                // Instanciando objeto de para tratemento nos comandos SQL
                MySqlCommand cmd = new MySqlCommand(sql, Conexao.conn);

                // Parâmetros de tratamento para adicionar os valores na tabela
                cmd.Parameters.AddWithValue("nome", nome);
                cmd.Parameters.AddWithValue("endereco", endereco);
                cmd.Parameters.AddWithValue("data", data);
                cmd.Parameters.AddWithValue("cpf", cpf);
                cmd.Parameters.AddWithValue("rg", rg);

                // Comando de execução
                cmd.ExecuteNonQuery();

                // Mensagem de cadastro bem sucedido
                MessageBox.Show("Aluno cadastrado com sucesso!");

                // Fechando conexão
                Conexao.Desconectar();

                // Limpando campos
                txtNome.Text = "";
                txtEnd.Text = "";
                dtpData.Text = "";
                mtxtCPF.Text = "";
                mtxtRG.Text = "";
            }
            catch(Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnExibir_Click(object sender, EventArgs e)
        {
            SelecionarDados();
        }
    }
}
