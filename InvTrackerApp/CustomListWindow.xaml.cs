using System.Windows;

namespace InvTrackerApp;
using static InvTrackerApp.Helper;

public partial class CustomListWindow : Window
{
    public PreMadeListWindow preMadeListWindow { get; }
    
    public CustomListWindow(PreMadeListWindow preMadeListWindow)
    {
        InitializeComponent();
    }
    
    // private void GoBack_Click(object sender, RoutedEventArgs e)
    // {
    //     OpenAtSamePosition(this, MainWindow);
    // }
}