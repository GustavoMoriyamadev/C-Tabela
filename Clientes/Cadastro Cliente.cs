using System;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.Data;
  using System.Drawing;
  using System.Linq;
  using System.Text;
  using System.Windows.Forms;
  using System.Text.RegularExpressions;
   
  namespace CSharpComSQLServer
  {
      public partial class frmCliente : Form
      {
          DadosDataSetTableAdapters.ClientesTableAdapter TAClientes = new DadosDataSetTableAdapters.ClientesTableAdapter();
   
          frmPrincipal Pri;
   
          public frmCliente()
          {
              InitializeComponent();
          }
   
          public frmCliente(frmPrincipal formularioPri)
          {
              InitializeComponent();
              Pri = formularioPri;
          } 
   
          private void frmCliente_Load(object sender, EventArgs e)
          {
              if (mskCPF.Text != "")
              {
                  btnAlterar.Enabled = false;
                  btnExcluir.Enabled = false;
              }
          }
   
          private void btnLLocalizar_Click(object sender, EventArgs e)
          {
              frmPesquisaCliente frmPesqCli = new frmPesquisaCliente(this);
              frmPesqCli.ShowDialog(this);
          }
   
          private void btnIncluir_Click(object sender, EventArgs e)
          {
              mskCPF.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
              if (mskCPF.Text == "")
                  MessageBox.Show("Informe um CPF!", "CPF", MessageBoxButtons.OK, MessageBoxIcon.Error);
              else
              {
                  if (Validacoes.validarCPF(mskCPF.Text))
                  {
                      mskCPF.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                      try
                      {
                          TAClientes.inserir_alterar_Cliente(mskCPF.Text, txtNome.Text, txtEndereco.Text, mskTelefone.Text, 1);
                          MessageBox.Show("Cliente cadastrado com sucesso!", "Clientes", MessageBoxButtons.OK, MessageBoxIcon.Information);
                      }
                      catch (Exception ex)
                      {
                          MessageBox.Show("Erro ao inserir Cliente\n" + ex.Message, "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                      }
                  }
                  else
                      MessageBox.Show("Informe um CPF válido!", "CPF", MessageBoxButtons.OK, MessageBoxIcon.Error);
              }
          }
   
          private void btnAlterar_Click(object sender, EventArgs e)
          {
              try
              {
                  TAClientes.inserir_alterar_Cliente(mskCPF.Text, txtNome.Text, txtEndereco.Text, mskTelefone.Text, 2);
                  MessageBox.Show("Dados atualizados!", "Clientes", MessageBoxButtons.OK, MessageBoxIcon.Information);
              }
              catch (Exception ex)
              {
                  MessageBox.Show("Erro ao atualizar Cliente\n" + ex.Message, "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
              }
          }
   
          private void btnExcluir_Click(object sender, EventArgs e)
          {
              if (MessageBox.Show("Deseja realmente excluir este cliente?", "Excluir cliente", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
              {
                  try
                  {
                      TAClientes.ExcluirCliente(mskCPF.Text);
                      mskCPF.Text = "";
                      mskTelefone.Text = "";
                      txtEndereco.Text = "";
                      txtNome.Text = "";
                      MessageBox.Show("Cliente excluído", "Confirmação de Exclusão", MessageBoxButtons.OK, MessageBoxIcon.Information);
                      btnExcluir.Enabled = false;
                      btnAlterar.Enabled = false;
                  }
                  catch (Exception ex)
                  {
                      MessageBox.Show("Falha ao excluir cliente!\n" + ex.Message, "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                  }
              }
          }
   
          private void btnFechar_Click(object sender, EventArgs e)
          {
              this.Close();
          }
      }
  }