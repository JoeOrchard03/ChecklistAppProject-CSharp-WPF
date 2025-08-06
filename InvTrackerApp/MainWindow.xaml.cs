using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;

//Placed in the inventory app namespace for organization
namespace InvTrackerApp
{
    //Define class, inherits from window to access the methods from that class
    public partial class MainWindow : Window
    {
        //Defines a list of items, ObservableCollection means that when you add or remove items the UI gets notified and updated
        private ObservableCollection<string> items = new ObservableCollection<string>();
        

        //This constructor runs when the window is created
        public MainWindow()
        {
            //InitializeComponent sets up the UI from the XAML file
            InitializeComponent();
            //Binds the ItemList listbox to the items list, now whenever items changes the listbox will update
            ItemList.ItemsSource = items;
        }

        //Runs when you click the Add button
        private void AddItem_Click(object sender, RoutedEventArgs e)
        {
            //input gets the input from the text box and .Trim removes the whitespace before the beginning and end of the string
            var input = ItemInput.Text.Trim();
            //Make sure input is not empty
            if (string.IsNullOrEmpty(input)) return;
            //Add item to the list, updating the listbox
            items.Add(input);
            //Clears the ItemInput ready for the next item
            ItemInput.Clear();
        }

        public void AddItemFromPreMadeList(string itemToAdd)
        {
            Debug.WriteLine("Current count of items are: " + items.Count.ToString());
            items.Add(itemToAdd);
            Debug.WriteLine("Adding " + itemToAdd);
            Debug.WriteLine("New count of items are: " + items.Count.ToString());
        }

        private void OpenPreMadeListWindow_Click(object sender, RoutedEventArgs e)
        {
            PreMadeListWindow window = new PreMadeListWindow(this);
            Helper.OpenAtSamePosition(this, window);
        }
    }
}