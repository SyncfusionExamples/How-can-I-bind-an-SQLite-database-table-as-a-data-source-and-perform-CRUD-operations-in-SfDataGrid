using Microsoft.Maui;
using Microsoft.Maui.Controls.Compatibility;
using Syncfusion.Maui.Data;
using Syncfusion.Maui.DataGrid;
using Syncfusion.Maui.DataGrid.Helper;
using System.Diagnostics;

namespace SfDataGridSample
{
    public partial class MainPage : ContentPage
    {
        public static SampleDemoDatabase sampleDemoDatabase;
        public static SampleDemoDatabase SampleDemoDatabase
        {
            get
            {
                if (sampleDemoDatabase == null)
                    sampleDemoDatabase = new SampleDemoDatabase();
                return sampleDemoDatabase;
            }
        }
        public MainPage()
        {
            InitializeComponent();
            dataGrid.ItemsSource = SampleDemoDatabase.OrderItemsDataSource;

        }

        // To add the row in the database and get the updated data in the data grid
        public void AddRowButtonClicked(object sender, EventArgs e)
        {
            SampleDemoDatabase.AddItem(new OrderItem { OrderID = 1004, Name = "Testing", TokenNo = 2023, BillStatus = "PAID" });

            // To get the updated data from the database
            SampleDemoDatabase.GetItems();

            // Set the updated data in the DataGrid
            dataGrid.ItemsSource = SampleDemoDatabase.OrderItemsDataSource;
        }

        // To remove the row from the database and get the updated data in the data grid
        public void RemoveButtonClicked(object sender, EventArgs e)
        {
            // To get the last row ID from the database
            int DeleteID = SampleDemoDatabase.OrderItemsDataSource.Last().ID;

            // To remove the last row from the database
            SampleDemoDatabase.DeleteItem(DeleteID);

            // To get the updated data from the database
            SampleDemoDatabase.GetItems();

            // Set the updated data in the DataGrid
            dataGrid.ItemsSource = SampleDemoDatabase.OrderItemsDataSource;
        }
    }
}
