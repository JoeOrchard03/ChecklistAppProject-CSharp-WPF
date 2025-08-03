using System.Windows;

namespace InvTrackerApp;

//Class containing helper methods that can be accessed from any script
public class Helper
{
    //Method for opening the new window at the same location as the current window
    public static void OpenAtSamePosition(Window currentWindow, Window newWindow)
    {
        //Gives control over window starting location
        newWindow.WindowStartupLocation = WindowStartupLocation.Manual;
        newWindow.Left = currentWindow.Left;
        newWindow.Top = currentWindow.Top;
        //Opens new window
        newWindow.Show();
        //Closes old window
        currentWindow.Close();
    }
}