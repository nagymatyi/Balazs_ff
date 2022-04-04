using PKMXEN.Models;
using PKMXEN.WpfClient.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;

namespace PKMXEN.WpfClient
{
    /// <summary>
    /// Interaction logic for OrderEditorWindow.xaml
    /// </summary>
    public partial class OrderEditorWindow : Window
    {
        public OrderEditorWindow(Order order)
        {
            InitializeComponent();
            var vm = new OrderEditorWindowViewModel();
            vm.Setup(order);
            this.DataContext = vm;
        }

        private void Vm_EditedDone(object? sender, EventArgs e)
        {
            this.DialogResult = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in stack.Children)
            {
                if (item is TextBox t)
                {
                    t.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                }
            }
            this.DialogResult = true;
        }
    }
}
