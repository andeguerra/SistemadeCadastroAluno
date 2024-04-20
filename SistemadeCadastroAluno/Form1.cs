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
        // Variável global para salvar o ID 
        int id = 0;

        public Form1()
        {
            InitializeComponent();
        }

        // Função para resgatar todos os dados da tabela no bd
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
            catch 
            {
                MessageBox.Show("Erro ao selecionar os dados da tabela Finanças");
            }
        }

        // Função para limpar os campos
        public void LimparCampos()
        {
            txtNome.Text = "";
            txtEnd.Text = "";
            dtpData.Text = "";
            mtxtCPF.Text = "";
            mtxtRG.Text = "";
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

                // Limpando campos
                LimparCampos();

                // Atualizando Tabela
                SelecionarDados();

                // Fechando conexão
                Conexao.Desconectar();

                
            }
            catch(Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnExibir_Click(object sender, EventArgs e)
        {
            // Mostra os dados da tabela da DGV
            SelecionarDados();
        }

        private void dgvTabela_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Salva na váriavel o id do item selecionado
            id = int.Parse(dgvTabela.CurrentRow.Cells[0].Value.ToString());

            // Passa os valores da tabela para os campos
            txtNome.Text = dgvTabela.CurrentRow.Cells[1].Value.ToString();
            txtEnd.Text = dgvTabela.CurrentRow.Cells[2].Value.ToString();
            dtpData.Text = dgvTabela.CurrentRow.Cells[3].Value.ToString();
            mtxtCPF.Text = dgvTabela.CurrentRow.Cells[4].Value.ToString();
            mtxtRG.Text = dgvTabela.CurrentRow.Cells[5].Value.ToString();

            // Ativa os botões
            btnAtualizar.Enabled = true;
            btnExcluir.Enabled = true;
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
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

                string sql = "UPDATE tab_alunos SET nome = @nome, endereco = @endereco, data = @data,  WHERE id = @id";
                MySqlCommand cmd = new MySqlCommand(sql, Conexao.conn);

                // Parâmetros de tratamento para atualizar os valores na tabela
                cmd.Parameters.AddWithValue("nome", nome);
                cmd.Parameters.AddWithValue("endereco", endereco);
                cmd.Parameters.AddWithValue("data", data);
                cmd.Parameters.AddWithValue("cpf", cpf);
                cmd.Parameters.AddWithValue("rg", rg);
                cmd.Parameters.AddWithValue("id", id);
                cmd.ExecuteNonQuery();

                // Mensagem de atualização bem sucedida
                MessageBox.Show("Atualização efetuada com sucesso");

                // Limpando campos
                LimparCampos();

                // Atualizando Tabela
                SelecionarDados();

                Conexao.Desconectar();
            }
            catch
            {
                MessageBox.Show("Errou ao tentar atualizar");
            }
        }
    }
}
