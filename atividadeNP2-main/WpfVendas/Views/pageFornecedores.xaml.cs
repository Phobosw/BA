using DsiVendas.Models;
using System;
using System.Collections.Generic;
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
using WpfVendas.ViewModels;
using WpfVendas.Views;

namespace WpfVendas.Pages
{
    /// <summary>
    /// Interação lógica para pageClientes.xam
    /// </summary>
    public partial class pageFornecedores : Page
    {
        private FornecedorViewModel _viewModel;

        public pageFornecedores()
        {
            InitializeComponent();
            _viewModel = new FornecedorViewModel();
            DataContext = _viewModel;
        }

        private async void btnAtualizar_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.Fornecedores.Clear();
            await _viewModel.CarregarFornecedoresDaAPI();
        }

        private void btnAddFornecedor_Click(object sender, RoutedEventArgs e)
        {
            var novoFornecedor = new Fornecedor();

            var janelaCadastro = new cadFornecedor
            {
                Owner = Window.GetWindow(this)  
            };

            var viewModel = new FornecedorCadastroViewModel(janelaCadastro.Close, novoFornecedor);
            
            janelaCadastro.DataContext = viewModel;

            janelaCadastro.ShowDialog();
        }

        private void FornecedoresDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Verifica se algum cliente está selecionado
            if (FornecedoresDataGrid.SelectedItem is Fornecedor fornecedorSelecionado)
            {
                // Cria a janela de edição
                var janelaCadastro = new cadFornecedor
                {
                    Owner = Window.GetWindow(this)  // Define o dono como a janela principal (MainWindow)
                };

                // Cria o ViewModel para a janela de edição, passando o cliente selecionado e a ação de fechar a janela
                var viewModel = new FornecedorCadastroViewModel(janelaCadastro.Close, fornecedorSelecionado);
                // Define o DataContext da janela
                janelaCadastro.DataContext = viewModel;
                // Mostra a janela de edição modal (abre por cima da MainWindow)
                janelaCadastro.ShowDialog();
            }
        }
    }
}
