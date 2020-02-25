using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class Persistence
{
    private static UserData userData;
    private static List<CardData> cardLibrary;

    private static readonly string userDataPath = "UserData.txt";
    private static readonly string cardLibraryPath = "CardLibrary.txt";

    public static bool HasUserData()
    {
        if (userData != null)
            return true;
        else
            return DoesFileExistAtPath(userDataPath);

    }

    public static void SaveUserData(UserData data)
    {
        // Cache user data
        userData = data;
        Debug.Log("Saved user data");
        // Write to file
        File.WriteAllText(userDataPath, data.ToString());
    }

    public static UserData LoadUserData()
    {
        string serializedUserData = File.ReadAllText(userDataPath);
        UserData userData = UserData.Deserialize(serializedUserData);
        Debug.Log("Loaded user data");
        return userData;
    }

    public static void SaveCardLibrary(List<CardData> cards)
    {
        // Cache card library
        cardLibrary = cards;

        // Write to file
        File.WriteAllText(cardLibraryPath, cards.ToString());
    }

    public static List<CardData> LoadCardLibrary()
    {
        string serializedCardLibrary = File.ReadAllText(cardLibraryPath);

        Debug.Log(serializedCardLibrary);
        return new List<CardData>();
    }

    private static bool DoesFileExistAtPath(string path)
    {
        return File.Exists(path);
    }
}
