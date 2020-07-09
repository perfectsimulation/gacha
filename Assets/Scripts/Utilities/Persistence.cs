using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class Persistence
{
    private static UserData userData;
    private static List<Card> cardLibrary;

    private static readonly string userDataPath = "UserData.json";
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
        File.WriteAllText(userDataPath, StringUtility.ToJson(new SerializedUserData(userData)));
    }

    public static SerializedUserData LoadUserData()
    {
        if (DoesFileExistAtPath(userDataPath))
        {
            string serializedUserData = File.ReadAllText(userDataPath);
            SerializedUserData userData = new SerializedUserData(serializedUserData);
            Debug.Log("Loaded user data");
            return userData;
        }

        return new SerializedUserData();
    }

    public static void SaveCardLibrary(List<Card> cards)
    {
        // Cache card library
        cardLibrary = cards;

        // Write to file
        File.WriteAllText(cardLibraryPath, cards.ToString());
    }

    public static List<Card> LoadCardLibrary()
    {
        string serializedCardLibrary = File.ReadAllText(cardLibraryPath);

        Debug.Log(serializedCardLibrary);
        return new List<Card>();
    }

    private static bool DoesFileExistAtPath(string path)
    {
        return File.Exists(path);
    }
}
