using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkManager
{
    public string registerUrl = "http://localhost/gacha-unity/register.php";

    // POST input field values for User Creation
    public IEnumerator RegisterNewUser(string username, string password)
    {
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>
        {
            new MultipartFormDataSection("username", username),
            new MultipartFormDataSection("password", password)
        };

        // TODO: implement environments
        UnityWebRequest www = UnityWebRequest.Post(this.registerUrl, formData);
        yield return www.SendWebRequest();

        // Handle errors
        if (www.downloadHandler.text == "0")
        {
            // No errors hitting the database
            Debug.Log("User created successfully");
        }
        else
        {
            // Error creating user
            Debug.Log("User creation failed with error: " + www.downloadHandler.text);
        }
    }
}
