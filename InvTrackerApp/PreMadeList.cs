using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using System.Windows.Shapes;

namespace InvTrackerApp.Models
{
    public class PreMadeList
    {
        //Name of premade list
        public string Name { get; set; } = "";
        //Items inside pre made list
        public List<ChecklistItem> Items { get; set; } =  new List<ChecklistItem>();
        
        // Block ignored by json
        [JsonIgnore] 
        //Marks whether the list can be added or not
        public bool CanAdd => Items.Any(item => !MainListItems.Contains(item));

        // Static reference to the main list's items (set when window opens)
        [JsonIgnore] 
        public static ObservableCollection<ChecklistItem> MainListItems { get; set; } = new();
    }
}