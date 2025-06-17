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
    public partial class FormUsuario: Form
    {
        MySqlConnection conexao;
        public FormUsuario()
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
            // Captura os valores e remove espaços em branco
            string cpf_cnpj = txtcpf_cnpj.Text.Trim();
            string nome = txtNome.Text.Trim();
            string email = txtEmail.Text.Trim();
            string rua = txtRua.Text.Trim();
            string numero = txtNumero.Text.Trim();
            string complemento = txtComplemento.Text.Trim();
            string bairro = txtBairro.Text.Trim();
            string cidade = txtCidade.Text.Trim();
            string uf = txtUf.Text.Trim();
            string cep = txtCep.Text.Trim();
            string celular = txtCelular.Text.Trim();

            // Verificação dos campos obrigatórios
            if (string.IsNullOrEmpty(cpf_cnpj) || string.IsNullOrEmpty(nome) || string.IsNullOrEmpty(email)
                || string.IsNullOrEmpty(rua) || string.IsNullOrEmpty(numero) || string.IsNullOrEmpty(bairro)
                || string.IsNullOrEmpty(cidade) || string.IsNullOrEmpty(uf) || string.IsNullOrEmpty(cep)
                || string.IsNullOrEmpty(celular))
            {
                MessageBox.Show("Por favor, preencha todos os campos obrigatórios.");
                return;
            }

            try
            {
                ConectarBanco();
                conexao.Open();

                string sql = @"INSERT INTO usuarios 
                       (cpf_cnpj, nome, email, rua, numero, complemento, bairro, cidade, uf, cep, celular) 
                       VALUES 
                       (@cpf_cnpj, @nome, @email, @rua, @numero, @complemento, @bairro, @cidade, @uf, @cep, @celular)";

                MySqlCommand cmd = new MySqlCommand(sql, conexao);
                cmd.Parameters.AddWithValue("@cpf_cnpj", cpf_cnpj);
                cmd.Parameters.AddWithValue("@nome", nome);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@rua", rua);
                cmd.Parameters.AddWithValue("@numero", numero);
                cmd.Parameters.AddWithValue("@complemento", complemento);
                cmd.Parameters.AddWithValue("@bairro", bairro);
                cmd.Parameters.AddWithValue("@cidade", cidade);
                cmd.Parameters.AddWithValue("@uf", uf);
                cmd.Parameters.AddWithValue("@cep", cep);
                cmd.Parameters.AddWithValue("@celular", celular);

                int linhasAfetadas = cmd.ExecuteNonQuery();

                if (linhasAfetadas > 0)
                {
                    MessageBox.Show("Usuário cadastrado com sucesso!");
                    LimparCampos();
                }
                else
                {
                    MessageBox.Show("Erro ao cadastrar o usuário.");
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Erro no banco: " + ex.Message);
            }
            finally
            {
                conexao.Close();
            }
        }

        private void LimparCampos()
        {
            txtcpf_cnpj.Clear();
            txtNome.Clear();
            txtEmail.Clear();
            txtRua.Clear();
            txtNumero.Clear();
            txtComplemento.Clear();
            txtBairro.Clear();
            txtCidade.Clear();
            txtUf.Clear();
            txtCep.Clear();
            txtCelular.Clear();

            // Opcional: define o foco no primeiro campo
            txtcpf_cnpj.Focus();
        }

    }
}
