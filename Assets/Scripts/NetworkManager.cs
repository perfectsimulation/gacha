using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkManager
{
    public string registerUrl = "http://localhost/gacha-unity/register.php";
    public string loginUrl = "http://localhost/gacha-unity/login.php";
    public string stageUrl = "http://localhost/gacha-unity/stage.php";
    public string saveStageUrl = "http://localhost/gacha-unity/saveStage.php";

    // POST input field values for User Creation
    public IEnumerator RegisterNewUser(string username, string password, string progress)
    {
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>
        {
            new MultipartFormDataSection("username", username),
            new MultipartFormDataSection("password", password),
            new MultipartFormDataSection("progress", progress)
        };

        // TODO: implement environments
        UnityWebRequest www = UnityWebRequest.Post(this.registerUrl, formData);
        yield return www.SendWebRequest();

        // Handle errors
        if (www.downloadHandler.text[0].ToString() == "0")
        {
            // No errors hitting the database
            Debug.Log("User created successfully");

            // Set user data for this session
            UserManager userManager = ModelLocator.GetModelInstance<UserManager>() as UserManager;
            string progressJson = www.downloadHandler.text.Split('\t')[1];
            userManager.SetUserData(progressJson);
        }
        else
        {
            // Error creating user
            Debug.Log("User creation failed with error: " + www.downloadHandler.text);
        }
    }

    // Login user with provided credentials
    public IEnumerator LoginUser(string username, string password)
    {
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>
        {
            new MultipartFormDataSection("username", username),
            new MultipartFormDataSection("password", password)
        };

        UnityWebRequest www = UnityWebRequest.Post(this.loginUrl, formData);
        yield return www.SendWebRequest();

        // Handle errors
        if (www.downloadHandler.text[0].ToString() == "0")
        {
            // No errors hitting the database
            Debug.Log("User logged in successfully");

            // Set user data for this session
            UserManager userManager = ModelLocator.GetModelInstance<UserManager>() as UserManager;
            string progressJson = www.downloadHandler.text.Split('\t')[1];
            userManager.SetUserData(progressJson);
        }
        else
        {
            // Error logging in user
            Debug.Log("User login failed with error: " + www.downloadHandler.text);
        }
    }

    // Load stage from database
    public IEnumerator LoadStage(int level, int stage)
    {
        string id = string.Format("{0}-{1}", level, stage);
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>
        {
            new MultipartFormDataSection("id", id)
        };

        UnityWebRequest www = UnityWebRequest.Post(this.stageUrl, formData);
        yield return www.SendWebRequest();

        // Handle errors
        if (www.downloadHandler.text[0].ToString() == "0")
        {
            // No errors hitting the database
            Debug.Log("Stage data loaded successfully");

            // Set stage data
            StageManager stageManager = ModelLocator.GetModelInstance<StageManager>() as StageManager;
            string[] response = www.downloadHandler.text.Split('\t');
            stageManager.SetStageData(response);
        }
        else
        {
            // Error loading stage data
            Debug.Log("Stage load failed with error: " + www.downloadHandler.text);
        }
    }

    // Add new stage to the database
    public IEnumerator SaveStage(StageData stageData)
    {
        StageManager stageManager = ModelLocator.GetModelInstance<StageManager>() as StageManager;
        string id = string.Format("{0}-{1}", stageData.level, stageData.stage);
        string level = stageData.level.ToString();
        string stage = stageData.stage.ToString();
        string scoreTier = JsonHelper.ToJson<int>(stageData.scoreTier);
        string notesJson = stageManager.SerializeNoteData(stageData.notes);

        List<IMultipartFormSection> formData = new List<IMultipartFormSection>
        {
            new MultipartFormDataSection("id", id),
            new MultipartFormDataSection("level", level),
            new MultipartFormDataSection("stage", stage),
            new MultipartFormDataSection("scoreTier", scoreTier),
            new MultipartFormDataSection("notes", notesJson)
        };

        UnityWebRequest www = UnityWebRequest.Post(this.saveStageUrl, formData);
        yield return www.SendWebRequest();

        // Handle errors
        if (www.downloadHandler.text[0].ToString() == "0")
        {
            // No errors hitting the database
            Debug.Log("Stage data saved successfully");
            string stageJson = www.downloadHandler.text.Split('\t')[1];
            Debug.Log(stageJson);
        }
        else
        {
            // Error saving stage data
            Debug.Log("Stage save failed with error: " + www.downloadHandler.text);
        }
    }

}
