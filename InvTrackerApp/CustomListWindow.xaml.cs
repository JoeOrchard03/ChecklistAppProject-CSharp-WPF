using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using InvTrackerApp.Models;

namespace InvTrackerApp;
using static InvTrackerApp.Helper;

public partial class CustomListWindow : Window
{
    public PreMadeListWindow preMadeListWindow;
    public string EnterCustomListNamePrompt { get; set; } = "Input Custom List Name";
    public string EnterCustomListItemPrompt { get; set; } = "Input List Item";
    
    private ObservableCollection<ChecklistItem> customListItems = new ObservableCollection<ChecklistItem>();
    
    public CustomListWindow(PreMadeListWindow preMadeListWindow)
    {
        InitializeComponent();
        this.preMadeListWindow = preMadeListWindow;
        CustomItemList.ItemsSource = customListItems;
        CustomListName.Text = EnterCustomListNamePrompt;
        CustomListItem.Text = EnterCustomListItemPrompt;
    }
    
    private void GoBack_Click(object sender, RoutedEventArgs e)
    {
        OpenAtSamePosition(this, preMadeListWindow);
    }
    
    private void CreateList_Click(object sender, RoutedEventArgs e)
    {
        Debug.WriteLine("Create custom list");
        if (string.IsNullOrEmpty(CustomListName.Text.ToString()) ||  CustomListName.Text == EnterCustomListNamePrompt)
        {
            MessageBox.Show("Please enter a name");
        }

        if (customListItems.Count == 0)
        {
            MessageBox.Show("Please enter at least one element to your custom list");
        }
        
        PreMadeList list = new PreMadeList();
        list.Name = CustomListName.Text;

        foreach (var i in customListItems)
        {
            list.Items.Add(new ChecklistItem { Text = i.Text, IsChecked = i.IsChecked });;
        }

        Debug.WriteLine($"Created custom list called: {list.Name}, it contains: {string.Join(", ", list.Items)}");

        PreMadeListStorage.Save(list);
        preMadeListWindow.preMadeLists.Add(list);

        
        CustomListName.Text = EnterCustomListNamePrompt;
        CustomListItem.Text = EnterCustomListItemPrompt;
        customListItems.Clear();
    }

    private void AddCustomListItem_Click(object sender, RoutedEventArgs e)
    {
        Debug.WriteLine("Add custom list item");
        var input = CustomListItem.Text.Trim();
        if(string.IsNullOrEmpty(input) || input == EnterCustomListItemPrompt) {return;}
        customListItems.Add(new ChecklistItem { Text = input });
        CustomListItem.Text = "";
    }
    
    #region Text box focus code

    private void CustomListName_GotFocus(object sender, RoutedEventArgs e)
    {
        if (CustomListName.Text == EnterCustomListNamePrompt)
        {
            CustomListName.Text = "";
        }
    }

    private void CustomListName_LostFocus(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(CustomListName.Text))
        {
            CustomListName.Text = EnterCustomListNamePrompt;
        }
    }
    
    private void CustomListItem_GotFocus(object sender, RoutedEventArgs e)
    {
        if (CustomListItem.Text == EnterCustomListItemPrompt)
        {
            CustomListItem.Text = "";
        }
    }

    private void CustomListItem_LostFocus(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(CustomListItem.Text))
        {
            CustomListItem.Text = EnterCustomListItemPrompt;
        }
    }

    #endregion
}