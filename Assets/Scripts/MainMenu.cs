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
    private StoryManager storyManager;

    private void Start()
    {
        this.ShowButtons(true);
        this.ShowSignupInputs(false);
        this.userManager = ModelLocator.GetModelInstance<UserManager>() as UserManager;
        this.storyManager = ModelLocator.GetModelInstance<StoryManager>() as StoryManager;
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
        UserData newUser = DataInitializer.CreateUser();
        Persistence.SaveUserData(newUser);
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
        SerializedUserData data = Persistence.LoadUserData();
        UserData userData = new UserData(data);
        this.userManager.SetUserData(userData);

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
