using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using static InvTrackerApp.Helper;

//Placed in the inventory app namespace for organization
namespace InvTrackerApp
{
    public partial class PreMadeListWindow : Window
    {
        public PreMadeListWindow()
        {
            InitializeComponent();
        }

        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            Window newWindow = new MainWindow();
            Helper.OpenAtSamePosition(this, newWindow);
        }
    }
}