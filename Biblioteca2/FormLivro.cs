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

namespace Biblioteca2
{
    public partial class FormLivro: Form
    {
        MySqlConnection conexao;

        public FormLivro()
        {
            InitializeComponent();
        }

        // Conexão com o banco
        private void ConectarBanco()
        {
            string conexaoString = "server=localhost;database=biblioteca;uid=root;pwd=;";
            conexao = new MySqlConnection(conexaoString);
        }


        private void txtTitulo_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtAutor_TextChanged(object sender, EventArgs e)
        {

        }
        // Fechar 
        private void btnfechar_Click(object sender, EventArgs e)
        {
            Close(); // Fecha o formulário atual
        }
        // Limpar Campos
        private void btnLimpar_Click(object sender, EventArgs e)
        {
            LimparCampos();
        }

        private void LimparCampos()
        {
            txtTitulo.Text = "";
            txtAutor.Text = "";
            txtTitulo.Focus();
        }
        //Salvar Dados
        private void btnSalvar_Click(object sender, EventArgs e)
        {
            string titulo = txtTitulo.Text.Trim();
            string autor = txtAutor.Text.Trim();

            if (string.IsNullOrEmpty(titulo) || string.IsNullOrEmpty(autor))
            {
                MessageBox.Show("Por favor, preencha todos os campos.");
                return;
            }

            try
            {
                ConectarBanco();
                conexao.Open();

                string sql = "INSERT INTO livro (titulo, autor) VALUES (@titulo, @autor)";
                MySqlCommand comando = new MySqlCommand(sql, conexao);
                comando.Parameters.AddWithValue("@titulo", titulo);
                comando.Parameters.AddWithValue("@autor", autor);

                int linhasAfetadas = comando.ExecuteNonQuery();

                if (linhasAfetadas > 0)
                {
                    MessageBox.Show("Livro cadastrado com sucesso!");
                    LimparCampos();
                }
                else
                {
                    MessageBox.Show("Erro ao cadastrar livro.");
                }

                conexao.Close();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Erro no banco: " + ex.Message);
            }
        }
    }
}
