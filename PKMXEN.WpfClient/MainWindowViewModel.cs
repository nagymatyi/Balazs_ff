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
        private string errorMessage;

        public string ErrorMessage
        {
            get { return errorMessage; }
            set { SetProperty(ref errorMessage, value); }
        }

        //Carrier
        public RestCollection<Carrier> Carriers { get; set; }

        public ICommand CreateCarrierCommand { get; set; }
        public ICommand UpdateCarrierCommand { get; set; }
        public ICommand DeleteCarrierCommand { get; set; }

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
                        CarrierID = value.CarrierID,
                        Age = value.Age,
                        Salary = value.Salary,
                        Orders = value.Orders,
                        TotalNumberOfParcels = value.TotalNumberOfParcels
                    };
                    OnPropertyChanged();
                    (DeleteCarrierCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }

        //Order
        public RestCollection<Order> Orders { get; set; }
        public ICommand CreateOrderCommand { get; set; }
        public ICommand UpdateOrderCommand { get; set; }
        public ICommand DeleteOrderCommand { get; set; }

        private Order selectedOrder;

        public Order SelectedOrder
        {
            get { return selectedOrder; }
            set
            {
                if (value != null)
                {
                    selectedOrder = new Order()
                    {
                        OrderDescription = value.OrderDescription,
                        OrderID = value.OrderID,
                        OrderDate = value.OrderDate,
                        OrderValue = value.OrderValue,
                        CarrierID = value.CarrierID,
                    };
                    OnPropertyChanged();
                    (DeleteOrderCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }

        //Parcel
        public RestCollection<Parcel> Parcels { get; set; }

        public ICommand CreateParcelCommand { get; set; }
        public ICommand UpdateParcelCommand { get; set; }
        public ICommand DeleteParcelCommand { get; set; }

        private Parcel selectedParcel;

        public Parcel SelectedParcel
        {
            get { return selectedParcel; }
            set
            {
                if (value != null)
                {
                    selectedParcel = new Parcel()
                    {
                        TrackingID = value.TrackingID,
                        Weight = value.Weight,
                        COD = value.COD,
                        ShippingDate = value.ShippingDate,
                        CustomerName = value.CustomerName,
                        Country = value.Country,
                        Address = value.Address,
                        OrderID = value.OrderID
                    };
                    OnPropertyChanged();
                    (DeleteParcelCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }


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
                Orders= new RestCollection<Order>("http://localhost:33503/", "order", "hub");
                Parcels = new RestCollection<Parcel>("http://localhost:33503/", "parcel", "hub");

                //Carrier CRUD
                CreateCarrierCommand = new RelayCommand(() =>
                {
                    new CarrierEditorWindow(SelectedCarrier).ShowDialog();
                    Carriers.Add(new Carrier()
                    {
                        Name = SelectedCarrier.Name,
                        Age = SelectedCarrier.Age,
                        TotalNumberOfParcels = SelectedCarrier.TotalNumberOfParcels,
                        Salary = SelectedCarrier.Salary
                    });
                });

                UpdateCarrierCommand = new RelayCommand(() =>
                {
                    new CarrierEditorWindow(SelectedCarrier).ShowDialog();
                    try
                    {
                        Carriers.Update(SelectedCarrier);
                    }
                    catch (ArgumentException ex)
                    {
                        ErrorMessage = ex.Message;
                    }
                });

                DeleteCarrierCommand = new RelayCommand(() =>
                {
                    Carriers.Delete(SelectedCarrier.CarrierID);
                },
                () =>
                {
                    return SelectedCarrier != null;
                });


                //Order CRUD
                CreateOrderCommand = new RelayCommand(() =>
                {
                    Orders.Add(new Order()
                    {
                        OrderDescription = SelectedOrder.OrderDescription,
                        OrderValue = SelectedOrder.OrderValue,
                        OrderDate = SelectedOrder.OrderDate,
                        CarrierID = SelectedCarrier.CarrierID
                    });
                });

                //not working
                UpdateOrderCommand = new RelayCommand(() =>
                {
                    try
                    {
                        Orders.Update(SelectedOrder);
                    }
                    catch (ArgumentException ex)
                    {
                        ErrorMessage = ex.Message;
                    }
                });

                DeleteOrderCommand = new RelayCommand(() =>
                {
                    Orders.Delete(SelectedOrder.OrderID);
                },
                () =>
                {
                    return SelectedOrder != null;
                });

                //Parcel CRUD
                CreateParcelCommand = new RelayCommand(() =>
                {
                    Parcels.Add(new Parcel()
                    {
                        Weight = SelectedParcel.Weight,
                        COD = SelectedParcel.COD,
                        ShippingDate = SelectedParcel.ShippingDate,
                        CustomerName = SelectedParcel.CustomerName,
                        Country = SelectedParcel.Country,
                        Address = SelectedParcel.Address,
                        OrderID = SelectedParcel.OrderID
                    });
                });

                //not working
                UpdateParcelCommand = new RelayCommand(() =>
                {
                    try
                    {
                        Parcels.Update(SelectedParcel);
                    }
                    catch (ArgumentException ex)
                    {
                        ErrorMessage = ex.Message;
                    }
                });

                DeleteParcelCommand = new RelayCommand(() =>
                {
                    Parcels.Delete(SelectedParcel.TrackingID);
                },
                () =>
                {
                    return SelectedParcel != null;
                });

                SelectedCarrier = new Carrier();
                SelectedOrder = new Order();
                SelectedParcel = new Parcel();
            }
        }
    }
}
