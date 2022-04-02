using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using PKMXEN.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PKMXEN.WpfClient
{
    public class MainWindowViewModel : ObservableRecipient
    {
        public RestCollection<Carrier> Carriers { get; set; }

        private Carrier selectedCarrier;

        public Carrier SelectedCarrier
        {
            get { return selectedCarrier; }
            set
            {
                if (value != null)
                {
                    selectedCarrier = new Carrier()
                    {
                        Name = value.Name,
                        CarrierID = value.CarrierID
                        //Age = value.Age,
                        //Salary = value.Salary,
                        //TotalNumberOfParcels = value.TotalNumberOfParcels
                    };
                    OnPropertyChanged();
                    (DeleteCarrierCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }

        private string errorMessage;

        public string ErrorMessage
        {
            get { return errorMessage; }
            set { SetProperty(ref errorMessage, value); }
        }


        public ICommand CreateCarrierCommand { get; set; }

        public ICommand DeleteCarrierCommand { get; set; }

        public ICommand UpdateCarrierCommand { get; set; }

        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        public MainWindowViewModel()
        {
            if (!IsInDesignMode)
            {
                Carriers = new RestCollection<Carrier>("http://localhost:33503/", "carrier", "hub");

                CreateCarrierCommand = new RelayCommand(() =>
                {
                    Carriers.Add(new Carrier()
                    {
                        //todo
                        Name = SelectedCarrier.Name
                    });
                });

                //do nothing
                UpdateCarrierCommand = new RelayCommand(() =>
                {
                    try
                    {
                        Carriers.Update(SelectedCarrier);
                    }
                    catch (ArgumentException ex)
                    {
                        ErrorMessage = ex.Message;
                    }
                });

                //not working
                DeleteCarrierCommand = new RelayCommand(() =>
                {
                    Carriers.Delete(SelectedCarrier.CarrierID);
                },
                () =>
                {
                    return SelectedCarrier != null;
                });
                SelectedCarrier = new Carrier();
            }
        }
    }
}
