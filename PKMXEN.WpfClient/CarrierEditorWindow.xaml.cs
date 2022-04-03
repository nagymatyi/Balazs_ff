using PKMXEN.Models;
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
using System.Windows.Shapes;

namespace PKMXEN.WpfClient
{
    /// <summary>
    /// Interaction logic for CarrierEditorWindow.xaml
    /// </summary>
    public partial class CarrierEditorWindow : Window
    {
        public CarrierEditorWindow(Carrier carrier)
        {
            InitializeComponent();
            var vm =  new CarrierEditorWindowViewModel();
            vm.Setup(carrier);
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
