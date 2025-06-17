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
    public partial class Form1 : Form
    {
        MySqlConnection conexao;

        public Form1()
        {
            InitializeComponent();
        }

        private void ConectarBanco()
        {
            string conexaoString = "server=localhost;database=biblioteca;uid=root;pwd=;";
            conexao = new MySqlConnection(conexaoString);

            try
            {
                conexao.Open();
                MessageBox.Show("Conexão bem-sucedida com o banco de dados!");
                conexao.Close();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Erro ao conectar: " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormLivro telaLivros = new FormLivro();
            telaLivros.Show(); // ou ShowDialog() se quiser modal
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FormUsuario telaUsuarios = new FormUsuario();
            telaUsuarios.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FormLocadora telalocadora = new FormLocadora();
            telalocadora.Show();
        }

        private void btnsair_Click(object sender, EventArgs e)
        {
            Close(); // Fecha o programa
        }
    }
}
