using System.Collections.Generic;
using System.Windows.Shapes;

namespace InvTrackerApp.Models
{
    public class PreMadeList
    {
        public string Name { get; set; } = "";
        public List<string> Items { get; set; } =  new List<string>();
    }
}