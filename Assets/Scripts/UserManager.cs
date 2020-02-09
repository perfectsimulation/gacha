using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class UserManager
{
    private UserData GetUserData()
    {
        // Search database for user
        
        // No user found, so create a new user
        UserData newUserData = UserData.CreateNewInstance();
        // POST the user data to database
        return newUserData;
    }

    public int GetCurrentLevel()
    {
        UserData userData = this.GetUserData();
        // Parse userData for the first false element in adventureCompletion
        return 0;
    }

    public int GetCurrentStage()
    {
        UserData userData = this.GetUserData();
        // Get current level
        int level = this.GetCurrentLevel();
        // Parse userData for the first false element in levelCompletion[level]
        return 0;
    }
}
