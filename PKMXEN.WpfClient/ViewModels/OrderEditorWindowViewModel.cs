using PKMXEN.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKMXEN.WpfClient.ViewModels
{
    public class OrderEditorWindowViewModel
    {
        public Order Actual { get; set; }

        public void Setup(Order order)
        {
            this.Actual = order;
        }

        public OrderEditorWindowViewModel()
        {

        }
    }
}
