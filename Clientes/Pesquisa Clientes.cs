using System;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.Data;
  using System.Drawing;
  using System.Linq;
  using System.Text;
  using System.Windows.Forms;
   
  namespace CSharpComSQLServer
  {
      public partial class frmPesquisaCliente : Form
      {
          DadosDataSet.ClientesDataTable DTClientes = new DadosDataSet.ClientesDataTable();
          DadosDataSetTableAdapters.ClientesTableAdapter TAClientes = new DadosDataSetTableAdapters.ClientesTableAdapter();
   
          frmCliente frmCli;
   
          public frmPesquisaCliente()
          {
              InitializeComponent();
          }
   
          public frmPesquisaCliente(frmCliente formularioClientes)
          {
              InitializeComponent();
              frmCli = formularioClientes;
          }
   
          private void txtPesquisa_TextChanged(object sender, EventArgs e)
          {
              try
              {
                  if (rdbCPF.Checked == true)
                  {
                      TAClientes.FillByBuscaCliCPF(DTClientes, txtPesquisa.Text);
                      dgvClientes.DataSource = DTClientes;
                  }
                  else if (rdbNome.Checked == true)
                  {
                      TAClientes.FillByBuscaCliNome(DTClientes, txtPesquisa.Text);
                      dgvClientes.DataSource = DTClientes;
                  }
              }
              catch (Exception ex)
              {
                  MessageBox.Show("Falha ao localizar dados do cliente\n" + ex.Message, "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
              }
          }
   
          private void btnOK_Click(object sender, EventArgs e)
          {
              try
              {
                  DataGridViewRow linha;
                  linha = dgvClientes.CurrentRow;
                  frmCli.mskCPF.Text = linha.Cells["CPF"].Value.ToString();
                  frmCli.txtNome.Text = linha.Cells["Nome"].Value.ToString();
                  frmCli.txtEndereco.Text = linha.Cells["Endereco"].Value.ToString();                
                  frmCli.mskTelefone.Text = linha.Cells["Telefone"].Value.ToString();
                  this.Close();
                  frmCli.btnAlterar.Enabled = true;
                  frmCli.btnExcluir.Enabled = true;
              }
              catch (Exception ex)
              {
                  MessageBox.Show("Selecione um cliente\n" + ex.Message, "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
              }
          }
   
          private void btnCancelar_Click(object sender, EventArgs e)
          {
              this.Close();
          }
   
          private void frmPesquisaCliente_Load(object sender, EventArgs e)
          {
              TAClientes.FillByDados(DTClientes);
              dgvClientes.DataSource = DTClientes;
          }
      }
  }  