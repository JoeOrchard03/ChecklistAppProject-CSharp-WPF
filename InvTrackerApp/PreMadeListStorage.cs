using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using InvTrackerApp.Models;

namespace InvTrackerApp;

public static class PreMadeListStorage
{
    //Gets the AppData\Roaming folder for the user, puts everything in it's own folder and references PreMadeLists.json
    public static readonly string PreMadeListFilePath =
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "InvTrackerApp",
            "PreMadeLists.json");

    //Constructor creates the file path if it does not exist
    static PreMadeListStorage()
    {
        Directory.CreateDirectory(Path.GetDirectoryName(PreMadeListFilePath)!);
        if (!File.Exists(PreMadeListFilePath))
        {
            File.WriteAllText(PreMadeListFilePath, "[]");
        }
    }

    //Loads from json
    public static ObservableCollection<PreMadeList> Load()
    {
        // Check if file exists and has content
        if (!File.Exists(PreMadeListFilePath) || string.IsNullOrWhiteSpace(File.ReadAllText(PreMadeListFilePath)))
        {
            Debug.WriteLine("Pre-made list file missing or empty, creating default.");

            // Create default: empty list of PreMadeList
            var defaultLists = new List<PreMadeList>();
            var defaultJson = JsonSerializer.Serialize(defaultLists);

            // Ensure directory exists
            Directory.CreateDirectory(Path.GetDirectoryName(PreMadeListFilePath)!);

            // Write default JSON to file
            File.WriteAllText(PreMadeListFilePath, defaultJson);
        }

        // Read the JSON after ensuring it exists
        var json = File.ReadAllText(PreMadeListFilePath);

        try
        {
            // Deserialize to a list
            var lists = JsonSerializer.Deserialize<List<PreMadeList>>(json);
            return lists != null
                ? new ObservableCollection<PreMadeList>(lists)
                : new ObservableCollection<PreMadeList>();
        }
        catch (JsonException ex)
        {
            Debug.WriteLine($"Error deserializing PreMadeList JSON: {ex.Message}");
            return new ObservableCollection<PreMadeList>();
        }
    }

    //Saves to the json, takes a list of PreMadeLists
    public static void Save(PreMadeList newList)
    {
        List<PreMadeList> existingLists;

        // Check if the file exists and load existing lists
        if (File.Exists(PreMadeListFilePath))
        {
            var json = File.ReadAllText(PreMadeListFilePath);
            existingLists = JsonSerializer.Deserialize<List<PreMadeList>>(json) ?? new List<PreMadeList>();
        }
        else
        {
            existingLists = new List<PreMadeList>();
        }

        // Add the new list to existing ones
        existingLists.Add(newList);

        // Serialize back to JSON and overwrite file
        var updatedJson = JsonSerializer.Serialize(existingLists, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(PreMadeListFilePath, updatedJson);
    }
    
    public static void Remove(string listName)
    {
        if (!File.Exists(PreMadeListFilePath))
            return;

        // Load current lists
        var json = File.ReadAllText(PreMadeListFilePath);
        var existingLists = JsonSerializer.Deserialize<List<PreMadeList>>(json) ?? new List<PreMadeList>();

        // Find the list with the given name and remove it
        var listToRemove = existingLists.FirstOrDefault(l => l.Name == listName);
        if (listToRemove != null)
        {
            existingLists.Remove(listToRemove);

            // Save updated collection back to JSON
            var updatedJson = JsonSerializer.Serialize(existingLists, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(PreMadeListFilePath, updatedJson);
        }
    }
}    