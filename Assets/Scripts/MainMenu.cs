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

    private UserManager userManager;

    private void Start()
    {
        this.ShowButtons(true);
        this.ShowSignupInputs(false);
        this.userManager = ModelLocator.GetModelInstance<UserManager>() as UserManager;
    }

    public void Login()
    {
        this.ShowButtons(false);
        this.AttemptLogin();
    }

    public void CreateUser()
    {
        this.ShowButtons(false);
        this.ShowSignupInputs(true);
    }

    public void Submit()
    {
        // TODO Save user with provided credentials
        UserData newUser = DataInitializer.CreateUser();
        this.userManager.SetUserData(newUser);

        // After setting the user data for this session, begin Adventure
        this.StartAdventure();
    }

    // Open the Adventure scene from the Main scene
    public void StartAdventure()
    {
        // called onClick PlayButton in Main scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // Attempt to load User Data
    private void AttemptLogin()
    {
        // Load local data
        UserData data = Persistence.LoadUserData();
        this.userManager.SetUserData(data);

        // After setting the user data for this session, begin Adventure
        this.StartAdventure();
    }

    private void ShowButtons(bool shouldShow)
    {
        this.LoginButton.gameObject.SetActive(shouldShow);
        this.CreateUserButton.gameObject.SetActive(shouldShow);
    }

    // Show/Hide Username and Password input fields and Submit button
    private void ShowSignupInputs(bool shouldShow)
    {
        this.UsernameInput.gameObject.SetActive(shouldShow);
        this.PasswordInput.gameObject.SetActive(shouldShow);
        this.SubmitButton.gameObject.SetActive(shouldShow);
        this.SubmitButton.GetComponentInChildren<TextMeshProUGUI>().text = "Create me";
    }

}
