using System.Collections.Generic;

namespace InvTrackerApp.Models
{
    public class PreMadeList
    {
        public string Name { get; set; } = "Sample Packing List";
        public List<string> Items { get; set; } = new() { "Toothbrush", "Socks", "Phone Charger" };
    }
    
    public class MyItems
    {
        public string Items { get; set; }
    }
}