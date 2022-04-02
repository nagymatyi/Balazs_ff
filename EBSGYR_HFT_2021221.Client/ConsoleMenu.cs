using PKMXEN.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKMXEN.Client
{

    public class ConsoleMenu
    {
        static RestService service1 = new RestService("http://localhost:33503");

        public ConsoleMenu()
        {
            MainMenu();
        }
        private static bool MainMenu()
        {
            Console.Clear();
            Console.WriteLine("HFT Féléves Feladat -- EBSGYR -- OE-NIK -- 2021221");
            Console.WriteLine();
            Console.WriteLine("-----------------------------------------------------------------------------");
            Console.WriteLine("Choose an option:");
            Console.WriteLine();
            Console.WriteLine("[1] DISPLAY FULL TABLE DATA -> (carrier, order, parcel..)");
            Console.WriteLine("[2] CREATE -> (carrier, order, parcel..)");
            Console.WriteLine("[3] READ -> (FROM carriers, orders, parcels...)");
            Console.WriteLine("[4] UPDATE -> (Update existing record in database)");
            Console.WriteLine("[5] DELETE -> (FROM existing record in database)");
            Console.WriteLine("[6] NON_CRUD -> (FROM existing record in database)");
            Console.WriteLine("[7] EXIT -> (EXIT from console app)");
            Console.WriteLine("-----------------------------------------------------------------------------");
            Console.WriteLine();
            Console.Write("\r\nSelect an option: ");

            switch (Console.ReadLine())
            {
                case "1":
                    DisplayTableData();
                    return true;
                case "2":
                    Create();
                    return true;
                case "3":
                    Read();
                    return true;
                case "4":
                    Update();
                    return true;
                case "5":
                    Delete();
                    return true;
                case "6":
                    NonCrud();
                    return true;
                case "7":
                    Environment.Exit(0);
                    return true;
                default:
                    return true;
            }
        }

        private static void DisplayTableData()
        {
            Console.Clear();
            Console.WriteLine("Which table do you want to display? - (Available tables: CARRIER / ORDER / PARCEL)");
            string tableName = Console.ReadLine().ToLower();
            switch (tableName)
            {
                case "carrier":
                    var carrierDisp = service1.Get<Carrier>("carrier");
                    foreach (var citem in carrierDisp)
                    {
                        Console.WriteLine(citem);
                    }
                    break;
                case "order":
                    var orderDisp = service1.Get<Carrier>("carrier");
                    foreach (var oitem in orderDisp)
                    {
                        Console.WriteLine(oitem);
                    }
                    break;
                case "parcel":
                    var parcelDisp = service1.Get<Carrier>("carrier");
                    foreach (var pitem in parcelDisp)
                    {
                        Console.WriteLine(pitem);
                    }
                    break;
            }     
        }
        private static void Read()
        {
            Console.Clear();
            Console.WriteLine("Which table do you want to display an item from?? - (Available tables: CARRIER / ORDER / PARCEL)");
            string tableName = Console.ReadLine().ToLower();
            Console.WriteLine("Which item ID do you want to display?");
            int selectedID = int.Parse(Console.ReadLine());

            switch (tableName)
            {
                case "carrier":
                    var c_select = service1.Get<Carrier>(selectedID, "carrier");
                    Console.WriteLine(c_select);
                    MainMenu();
                break;
                case "order":
                    var o_select = service1.Get<Order>(selectedID, "order");
                    Console.WriteLine(o_select);
                    MainMenu();
                break;
                case "parcel":
                    var p_select = service1.Get<Parcel>(selectedID, "parcel");
                    Console.WriteLine(p_select);
                    MainMenu();
                break;
            }
        }
        private static void Create()
        {
            Console.Clear();
            Console.WriteLine("Which table you want to add an item to? - (Available tables: CARRIER / ORDER / PARCEL)");
            string tableName = Console.ReadLine().ToLower();
            switch (tableName)
            {
                case "carrier":
                    Console.WriteLine("Enter the new name you want: ");
                    string createName = Console.ReadLine();
                    Console.WriteLine("Enter the new age you want: ");
                    int createAge = int.Parse(Console.ReadLine());
                    Console.WriteLine("Enter the new salary you want: ");
                    double createSalary = double.Parse(Console.ReadLine());
                    Console.WriteLine("Enter the total number of parcels: ");
                    int createParcelNo = int.Parse(Console.ReadLine());
                    service1.Post(new Carrier()
                    {
                        Name = createName,
                        Age = createAge,
                        Salary = createSalary,
                        TotalNumberOfParcels = createParcelNo,
                    }, "carrier");
                    Console.WriteLine("Item successfully created!");
                    Console.ReadKey();
                    MainMenu();
                break;

                case "order":
                    Console.WriteLine("Enter the new description you want: ");
                    string createDescription = Console.ReadLine();
                    Console.WriteLine("Enter the new value you want: ");
                    double createValue = double.Parse(Console.ReadLine());
                    Console.WriteLine("Enter the new date you want: ");
                    DateTime createDate = DateTime.Parse(Console.ReadLine());
                    Console.WriteLine("Enter the new carrierID: ");
                    int createCarrierID = int.Parse(Console.ReadLine());
                    service1.Post(new Order()
                    {
                        OrderDescription = createDescription,
                        OrderValue = createValue,
                        OrderDate = createDate,
                        CarrierID = createCarrierID,
                    }, "order");
                    Console.WriteLine("Item successfully updated!");
                    Console.ReadKey();
                    MainMenu();
                break;

                case "parcel":
                    Console.WriteLine("Enter the new weight you want: ");
                    double createWeight = double.Parse(Console.ReadLine());
                    Console.WriteLine("Enter the new COD value (true / false) you want: ");
                    bool createCOD = bool.Parse(Console.ReadLine());
                    Console.WriteLine("Enter the new shipping date you want: ");
                    DateTime createShippingDate = DateTime.Parse(Console.ReadLine());
                    Console.WriteLine("Enter the new customer name: ");
                    string createCustomerName = Console.ReadLine();
                    Console.WriteLine("Enter the new country name: ");
                    string createCountry = Console.ReadLine();
                    Console.WriteLine("Enter the new address: ");
                    string createAddress = Console.ReadLine();
                    Console.WriteLine("Enter the new orderID: ");
                    int createOrderID_FK = int.Parse(Console.ReadLine());
                    service1.Post(new Parcel()
                    {
                        Weight = createWeight,
                        COD = createCOD,
                        ShippingDate = createShippingDate,
                        CustomerName = createCustomerName,
                        Country = createCountry,
                        Address = createAddress,
                        OrderID = createOrderID_FK
                    }, "order");
                    Console.WriteLine("Item successfully updated!");
                    Console.ReadKey();
                    MainMenu();
                break;
            }
        }
        private static void Update()
        {
            Console.Clear();
            Console.WriteLine("Which table do you want to make changes to? - (Available tables: CARRIER / ORDER / PARCEL)");
            string tableName = Console.ReadLine().ToLower();
            switch (tableName)
            {
                case "carrier":
                        Console.WriteLine("Which item ID do you want to update?: ");
                        int putID = int.Parse(Console.ReadLine());
                        Console.WriteLine("Enter the new name you want: ");
                        string putName = Console.ReadLine();
                        Console.WriteLine("Enter the new age you want: ");
                        int putAge = int.Parse(Console.ReadLine());
                        Console.WriteLine("Enter the new salary you want: ");
                        double putSalary = double.Parse(Console.ReadLine());
                        Console.WriteLine("Enter the total number of parcels: ");
                        int putParcel = int.Parse(Console.ReadLine());
                        service1.Put(new Carrier()
                        {
                            CarrierID = putID,
                            Name = putName,
                            Age = putAge,
                            Salary = putSalary,
                            TotalNumberOfParcels = putParcel,
                        }, "carrier");
                        Console.WriteLine("Item successfully updated!");
                        Console.ReadKey();
                        MainMenu();
                break;

                case "order":
                        Console.WriteLine("Which item ID do you want to update?: ");
                        int putOrderID = int.Parse(Console.ReadLine());
                        Console.WriteLine("Enter the new description you want: ");
                        string putDescription = Console.ReadLine();
                        Console.WriteLine("Enter the new value you want: ");
                        double putValue = double.Parse(Console.ReadLine());
                        Console.WriteLine("Enter the new date you want: ");
                        DateTime putDate = DateTime.Parse(Console.ReadLine());
                        Console.WriteLine("Enter the new carrierID: ");
                        int putCarrierID = int.Parse(Console.ReadLine());
                        service1.Put(new Order()
                        {
                            OrderID = putOrderID,
                            OrderDescription = putDescription,
                            OrderValue = putValue,
                            OrderDate = putDate,
                            CarrierID = putCarrierID,
                        }, "order");
                        Console.WriteLine("Item successfully updated!");
                        Console.ReadKey();
                        MainMenu();
                break;

                case "parcel":
                        Console.WriteLine("Which item ID do you want to update?: ");
                        int putTrackingID = int.Parse(Console.ReadLine());
                        Console.WriteLine("Enter the new weight you want: ");
                        double putWeight = double.Parse(Console.ReadLine());
                        Console.WriteLine("Enter the new COD value (true / false) you want: ");
                        bool putCOD = bool.Parse(Console.ReadLine());
                        Console.WriteLine("Enter the new shipping date you want: ");
                        DateTime putShippingDate = DateTime.Parse(Console.ReadLine());
                        Console.WriteLine("Enter the new customer name: ");
                        string putCustomerName = Console.ReadLine();
                        Console.WriteLine("Enter the new country name: ");
                        string putCountry = Console.ReadLine();
                        Console.WriteLine("Enter the new address: ");
                        string putAddress = Console.ReadLine();
                        Console.WriteLine("Enter the new orderID: ");
                        int putOrderID_FK = int.Parse(Console.ReadLine());
                        service1.Put(new Parcel()
                        {
                            TrackingID = putTrackingID,
                            Weight = putWeight,
                            COD = putCOD,
                            ShippingDate = putShippingDate,
                            CustomerName = putCustomerName,
                            Country = putCountry,
                            Address = putAddress,
                            OrderID = putOrderID_FK
                        }, "order");
                        Console.WriteLine("Item successfully updated!");
                        Console.ReadKey();
                        MainMenu();
                 break;
            }
        }
        private static void NonCrud()
        {
            Console.WriteLine("Konzolról való elérés nem működik valamiért!");
        }
        private static void Delete()
        {
            Console.Clear();
            Console.WriteLine("Which table do you want to delete from? - (Available tables: CARRIER / ORDER / PARCEL)");
            string tableName = Console.ReadLine().ToLower();
            Console.WriteLine("Which value with which ID do you want to delete?");
            int deleteID = int.Parse(Console.ReadLine());
            service1.Delete(deleteID, tableName);
            Console.WriteLine("The record has been successfully deleted!");
            Console.WriteLine("Press a key to go back to the main menu!");
            Console.ReadKey();
            MainMenu();
        }
    }
}
