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
    public partial class FormLocadora: Form
    {
        MySqlConnection conexao;

        public FormLocadora()
        {
            InitializeComponent();
        }
        // Conexão com o banco
        private void ConectarBanco()
        {
            string conexaoString = "server=localhost;database=biblioteca;uid=root;pwd=;";
            conexao = new MySqlConnection(conexaoString);
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            Close(); // Fecha o formulário atual
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            LimparCampos();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            DateTime dataEmprestimo = dtpEmprestimo.Value.Date;
            DateTime dataPrevistaEntrega = dtpPrevistaEntrega.Value.Date;
            DateTime? dataEntrega = dtpEntrega.Checked ? dtpEntrega.Value.Date : (DateTime?)null;

            string livroIdText = txtLivroId.Text.Trim();
            string usuarioIdText = txtUsuarioId.Text.Trim();

            if (string.IsNullOrEmpty(livroIdText) || string.IsNullOrEmpty(usuarioIdText))
            {
                MessageBox.Show("Por favor, preencha todos os campos obrigatórios.");
                return;
            }

            if (!int.TryParse(livroIdText, out int livroId) || !int.TryParse(usuarioIdText, out int usuarioId))
            {
                MessageBox.Show("IDs devem ser numéricos.");
                return;
            }

            try
            {
                ConectarBanco();
                conexao.Open();

                string sql = @"INSERT INTO locadora (dataEmprestimo, dataPrevistaEntrega, dataEntrega, livroId, usuarioId)
                       VALUES (@dataEmprestimo, @dataPrevistaEntrega, @dataEntrega, @livroId, @usuarioId)";

                MySqlCommand comando = new MySqlCommand(sql, conexao);
                comando.Parameters.AddWithValue("@dataEmprestimo", dataEmprestimo);
                comando.Parameters.AddWithValue("@dataPrevistaEntrega", dataPrevistaEntrega);
                comando.Parameters.AddWithValue("@dataEntrega", (object)dataEntrega ?? DBNull.Value);
                comando.Parameters.AddWithValue("@livroId", livroId);
                comando.Parameters.AddWithValue("@usuarioId", usuarioId);

                int linhasAfetadas = comando.ExecuteNonQuery();

                if (linhasAfetadas > 0)
                {
                    MessageBox.Show("Locação cadastrada com sucesso!");
                    LimparCampos();
                }
                else
                {
                    MessageBox.Show("Erro ao cadastrar locação.");
                }

                conexao.Close();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Erro no banco: " + ex.Message);
            }
        }

        private void LimparCampos()
        {
            dtpEmprestimo.Value = DateTime.Today;
            dtpPrevistaEntrega.Value = DateTime.Today.AddDays(7); // ou a lógica desejada
            dtpEntrega.Value = DateTime.Today; 
            dtpEntrega.Checked = false; // marca como não entregue ainda

            txtLivroId.Clear();
            txtUsuarioId.Clear();

            txtLivroId.Focus();
        }
    }
}
