using PKMXEN.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKMXEN.WpfClient.ViewModels
{
    public class CarrierEditorWindowViewModel
    {
        public Carrier Actual { get; set; }

        public void Setup(Carrier carrier)
        {
            this.Actual = carrier;
        }

        public CarrierEditorWindowViewModel()
        {

        }
    }
}
