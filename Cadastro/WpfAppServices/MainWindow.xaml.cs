using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Projeto.Data.Entities;
using Projeto.Data.Repositories;


namespace WpfAppServices
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : Window
    {
        ClienteRepository repository = new ClienteRepository();


        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click_Cadastrar(object sender, RoutedEventArgs e)
        {

            try
            {
                var cliente = new Cliente();

                cliente.Nome = txtNome.Text;
                cliente.Email = txtEmail.Text;
                //var teste = datacol.SelectedDate.Value.ToString("dd/MM/yyyy");
                cliente.DataCriacao = datacol.SelectedDate.Value;
                // Você tá tentando atribuir o valor do ToString para o DataCriacao..ele é um dateTime... e quando você faz o 
                // ToString(), ele vira uma string.. 
                // Isso já é o suficiente para você ter o valor na variável. ]
                // Executa aí pra debugarmos. 


                //var repository = new ClienteRepository();
                repository.Insert(cliente);



                MessageBox.Show("Cliente cadastrado com sucesso");
                LimparCampos();
                ListaCliente();

            }
            catch (Exception d)
            {
                MessageBox.Show("Dados inválidos", d.Message);
            }

        }

        private void ListaCliente()
        {


            var repository = new ClienteRepository();

            ObservableCollection<Cliente> lista = new ObservableCollection<Cliente>(repository.SelectAll());
            dataGrid.ItemsSource = lista;


        }

        private void LimparCampos()
        {
            txtId.Text = "";
            txtNome.Text = "";
            txtEmail.Text = "";
            datacol.SelectedDate = DateTime.Now;

        }

        private void BtnConsultar_Click(object sender, RoutedEventArgs e)
        {

            ListaCliente();

        }

        private void DataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {



            Cliente cliente = (Cliente)dataGrid.SelectedItem;
            if (cliente is null) return;  //return, para a execução igual ao break do case


            txtId.Text = cliente.IdCliente.ToString();
            txtNome.Text = cliente.Nome;
            txtEmail.Text = cliente.Email;
            datacol.SelectedDate = cliente.DataCriacao.Date;


        }
        private void BtnAtualizar_Click(object sender, RoutedEventArgs e)
        {
            // Validações 
            // Ações
            // Resultados
            try
            {

                var cliente = new Cliente();


                if (!string.IsNullOrWhiteSpace(txtId.Text))
                    cliente.IdCliente = Convert.ToInt32(txtId.Text);

                if (!string.IsNullOrWhiteSpace(txtNome.Text))
                    cliente.Nome = txtNome.Text;

                if (!string.IsNullOrWhiteSpace(txtEmail.Text))
                    cliente.Email = txtEmail.Text;

                if (!string.IsNullOrWhiteSpace(datacol.Text))
                    cliente.DataCriacao = datacol.SelectedDate.Value;



                repository.Update(cliente);

                MessageBox.Show("Cliente atualizado com sucesso");

                LimparCampos();
                ListaCliente();

            }
            catch (Exception c)
            {
                MessageBox.Show("Erro de validação ", c.Message);
            }

        }

        private void Cliente_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {

        }

        private void BtExcluir_Click(object sender, RoutedEventArgs e)
        {

            int id = Convert.ToInt32(txtId.Text);

            var repository = new ClienteRepository();
            repository.Delete(id);


            MessageBox.Show("Cliente excluído com sucesso");

            LimparCampos();
            ListaCliente();

        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }


    }
}
