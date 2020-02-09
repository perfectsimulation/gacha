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

    private UserManager userManager;
    private NetworkManager networkManager;

    private void Start()
    {
        this.ShowLoginInputs(false);
        this.userManager = ModelLocator.GetModelInstance<UserManager>() as UserManager;
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
        if (this.isNewUser)
        {
            // Create and add new user to database
            string username = this.UsernameInput.text;
            string password = this.PasswordInput.text;
            userManager.GetUserData().SetUsername(username);
            string progress = this.userManager.SerializeUserData(this.userManager.GetUserData());
            StartCoroutine(this.networkManager.RegisterNewUser(username, password, progress));
        }
        else
        {
            // Login using provided credentials
            string username = this.UsernameInput.text;
            string password = this.PasswordInput.text;
            StartCoroutine(this.networkManager.LoginUser(username, password));
        }

        // After setting the user data for this session, begin Adventure
        this.StartAdventure();
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
        if (showOrHide && this.isNewUser)
        {
            this.SubmitButton.GetComponentInChildren<TextMeshProUGUI>().text = "Sign up";
        }
        else if (showOrHide)
        {
            this.SubmitButton.GetComponentInChildren<TextMeshProUGUI>().text = "Login";
        }
    }

}
