using PKMXEN.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKMXEN.WpfClient.ViewModels
{
    public class ParcelEditorWindowViewModel
    {
        public Parcel Actual { get; set; }

        public void Setup(Parcel parcel)
        {
            Actual = parcel;
        }

        public ParcelEditorWindowViewModel()
        {

        }
    }
}
