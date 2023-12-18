# How can I bind an SQLite database table as a data source and perform CRUD operations in SfDataGrid
The [.NET MAUI DataGrid](https://www.syncfusion.com/maui-controls/maui-datagrid) Control is bound to an external data source to display the data in a tabular format. It supports data sources such as List, IEnumerable, and so on. The [SfDataGrid.ItemsSource](https://help.syncfusion.com/cr/maui/Syncfusion.Maui.DataGrid.SfDataGrid.html#Syncfusion_Maui_DataGrid_SfDataGrid_ItemsSourceProperty) property helps to bind this control with a collection of objects.

Refer to the following code example, which illustrates how to bind an SQLite database table as a data source and perform CRUD operations in SfDataGrid.
 
##### xaml:
 ```XML
 <StackLayout>
    <Button Text="Add Row in DataBase" Clicked="AddRowButtonClicked"/>
    <Button Text="Remove Row in DataBase" Clicked="RemoveButtonClicked"/>
    <dataGrid:SfDataGrid x:Name="dataGrid"
                      SelectionMode="Single" NavigationMode="Cell">
    </dataGrid:SfDataGrid>
</StackLayout>
 ```
 

##### xaml.cs:
 
 ```XML
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
 ```
 
##### ViewModel:
 
 ```XML
private ObservableCollection<OrderItem> orderItemsDataSource;
public ObservableCollection<OrderItem> OrderItemsDataSource
{
    get => this.orderItemsDataSource;
    set
    {
        this.orderItemsDataSource = value;
        
    }
}
SQLiteConnection database;

public const string DatabaseFilename = "TestDataBase.db3";
       
public SampleDemoDatabase()
{
    string dbPath = Path.Combine(FileSystem.AppDataDirectory, DatabaseFilename);

    // Get the SQLite database connection
    database = new SQLiteConnection(dbPath);

    // Create the table if it doesn't exist
    database.CreateTable<OrderItem>();

    // Insert sample data into the database
    InsertSampleData();

    // Get the sample data from the database
    GetItems();

}

public void InsertSampleData()
{
    // Sample data
    var sampleItems = new List<OrderItem>
    {
        new OrderItem { OrderID = 1001, Name = "Patient01", TokenNo = 1501, BillStatus = "PAID" },
        new OrderItem { OrderID = 1002, Name = "Patient02", TokenNo = 1502, BillStatus = "NOT PAID" },
        new OrderItem { OrderID = 1003, Name = "Patient03", TokenNo = 1503, BillStatus = "PAID" },
        // Add more sample items as needed
    };

    foreach (var item in sampleItems)
    {
        try
        {
            database.Insert(item);
        }
        catch (SQLiteException ex)
        {
            // Handle exceptions, e.g., duplicate insertions
            Console.WriteLine($"SQLiteException: {ex.Message}");
        }
    }
}

// To add an item to the databaseb 
public void AddItem(OrderItem item)
{
    database.Insert(item);
}

// To delete an item from the database
public void DeleteItem(int id)
{
    database.Delete(new OrderItem { ID =id});
}

public void GetItems()
{
        var table = database.Table<OrderItem>().ToList();
        OrderItemsDataSource= new ObservableCollection<OrderItem>(table);
}
 ```
 

[View sample in GitHub](https://github.com/SyncfusionExamples/How-can-I-bind-an-SQLite-database-table-as-a-data-source-and-perform-CRUD-operations-in-SfDataGrid/tree/master)

Take a moment to explore this [documentation](https://help.syncfusion.com/maui/datagrid/overview), where you can find more information about Syncfusion .NET MAUI DataGrid (SfDataGrid) with code examples. Please refer to this [link](https://www.syncfusion.com/maui-controls/maui-datagrid) to learn about the essential features of Syncfusion .NET MAUI DataGrid (SfDataGrid).

##### Conclusion

I hope you enjoyed learning about how can I bind an SQLite database table as a data source and perform CRUD operations in SfDataGrid?

You can refer to our [.NET MAUI DataGrid’s feature tour](https://www.syncfusion.com/maui-controls/maui-datagrid) page to learn about its other groundbreaking feature representations. You can also explore our [.NET MAUI DataGrid Documentation](https://help.syncfusion.com/maui/datagrid/getting-started) to understand how to present and manipulate data. 
For current customers, you can check out our .NET MAUI components on the [License and Downloads](https://www.syncfusion.com/sales/teamlicense) page. If you are new to Syncfusion, you can try our 30-day [free trial](https://www.syncfusion.com/downloads/maui) to explore our .NET MAUI DataGrid and other .NET MAUI components. 

If you have any queries or require clarifications, please let us know in the comments below. You can also contact us through our [support forums](https://www.syncfusion.com/forums), [Direct-Trac](https://support.syncfusion.com/create) or [feedback portal](https://www.syncfusion.com/feedback/maui?control=sfdatagrid), or the feedback portal. We are always happy to assist you!
