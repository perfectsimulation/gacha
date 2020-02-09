using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button LoginButton;
    public Button CreateUserButton;
    public TMP_InputField UsernameInput;
    public TMP_InputField PasswordInput;
    public Button SubmitButton;
    private bool isNewUser = true;

    private NetworkManager networkManager;

    private void Start()
    {
        this.ShowLoginInputs(false);
        this.networkManager = ModelLocator.GetModelInstance<NetworkManager>() as NetworkManager;
    }

    public void Login()
    {
        this.isNewUser = false;
        this.ShowLoginButtons(false);
        this.ShowLoginInputs(true);
    }

    public void CreateUser()
    {
        this.isNewUser = true;
        this.ShowLoginButtons(false);
        this.ShowLoginInputs(true);
    }

    public void Submit()
    {
        // New user
        if (this.isNewUser)
        {
            StartCoroutine(this.networkManager.RegisterNewUser(this.UsernameInput.text, this.PasswordInput.text));
        }
    }

    // Open the Adventure scene from the Main scene
    public void StartAdventure()
    {
        // called onClick PlayButton in Main scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // Show/Hide Login and Create User buttons
    void ShowLoginButtons(bool showOrHide)
    {
        this.LoginButton.gameObject.SetActive(showOrHide);
        this.CreateUserButton.gameObject.SetActive(showOrHide);
    }

    // Show/Hide Username and Password input fields and Submit button
    void ShowLoginInputs(bool showOrHide)
    {
        this.UsernameInput.gameObject.SetActive(showOrHide);
        this.PasswordInput.gameObject.SetActive(showOrHide);
        this.SubmitButton.gameObject.SetActive(showOrHide);
    }

}
