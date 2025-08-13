using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using System.Windows.Shapes;

namespace InvTrackerApp.Models
{
    public class PreMadeList
    {
        public string Name { get; set; } = "";
        public List<ChecklistItem> Items { get; set; } =  new List<ChecklistItem>();
        
        // This is not stored in JSON — calculated at runtime
        [JsonIgnore] 
        public bool CanAdd => Items.Any(item => !MainListItems.Contains(item));

        // Static reference to the main list's items (set when window opens)
        [JsonIgnore] 
        public static ObservableCollection<ChecklistItem> MainListItems { get; set; } = new();
    }
}