using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkManager
{
    public string registerUrl = "http://localhost/gacha-unity/register.php";
    public string loginUrl = "http://localhost/gacha-unity/login.php";

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
}
