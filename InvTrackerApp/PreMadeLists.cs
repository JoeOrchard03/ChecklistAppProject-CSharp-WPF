using System.Collections.Generic;

namespace InvTrackerApp.Models
{
    public class PreMadeList
    {
        public string Name { get; set; }
        public List<string> Items { get; set; } =  new List<string>();
    }
    
    public class MyItems
    {
        public string Items { get; set; }
    }
}