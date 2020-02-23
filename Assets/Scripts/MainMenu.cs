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
            // TODO: remove placeholder starter cards
            string bigRedUrl = "https://i1.pngguru.com/preview/672/751/586/vulpix-sprite-orange-animal-pixel-screenshot-png-clipart.jpg";
            string jungleBoiUrl = "https://library.kissclipart.com/20181130/ztw/kissclipart-scyther-pokemon-clipart-pokmon-firered-and-leafg-951e6c8cd55db39f.jpg";
            LevelRequirement[] bigRedLevelReqs = new LevelRequirement[] { new LevelRequirement(), new LevelRequirement(3, 300f), new LevelRequirement(4, 400f), new LevelRequirement(5, 500f), new LevelRequirement(6, 600f), new LevelRequirement(7, 700f), new LevelRequirement(8, 800f), new LevelRequirement(9, 900f), new LevelRequirement(10, 1000f) };
            LevelRequirement[] jungleBoiLevelReqs = new LevelRequirement[] { new LevelRequirement(), new LevelRequirement(3, 300f), new LevelRequirement(4, 400f), new LevelRequirement(5, 500f), new LevelRequirement(6, 600f), new LevelRequirement(7, 700f), new LevelRequirement(8, 800f), new LevelRequirement(9, 900f), new LevelRequirement(10, 1000f), new LevelRequirement(11, 1100f), new LevelRequirement(12, 1200f), new LevelRequirement(13, 1300f), new LevelRequirement(14, 1400f), new LevelRequirement(15, 1500f), new LevelRequirement(16, 1600f), new LevelRequirement(17, 1700f), new LevelRequirement(18, 1800f), new LevelRequirement(19, 1900f), new LevelRequirement(20, 2000f) };
            int[] rankItemIds1 = new int[] { 3, 4, 5 };
            int[] rankItemQuantities1 = new int[] { 6, 3, 5 };
            int[] rankItemIds2 = new int[] { 6, 7, 8 };
            int[] rankItemQuantities2 = new int[] { 4, 4, 7 };
            RankRequirement[] bigRedRankReqs = new RankRequirement[] { new RankRequirement(), new RankRequirement(2, 10, rankItemIds1, rankItemQuantities1) };
            RankRequirement[] jungleBoiRankReqs = new RankRequirement[] { new RankRequirement(), new RankRequirement(2, 10, rankItemIds1, rankItemQuantities1), new RankRequirement(3, 20, rankItemIds2, rankItemQuantities2) };
            userManager.GetUserData().SetUsername(username);
            CardData[] card = new CardData[] {
                new CardData("big red", bigRedUrl, 1, 0, 0, 80, 10, 14, 0, 0, 1, 20, bigRedLevelReqs, bigRedRankReqs),
                new CardData("jungle boi", jungleBoiUrl, 1, 1, 0, 10, 20, 30, 40, 25, 4, 0, jungleBoiLevelReqs, jungleBoiRankReqs)
            };
            userManager.GetUserData().SetCardData(card);
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
    void ShowLoginButtons(bool shouldShow)
    {
        this.LoginButton.gameObject.SetActive(shouldShow);
        this.CreateUserButton.gameObject.SetActive(shouldShow);
    }

    // Show/Hide Username and Password input fields and Submit button
    void ShowLoginInputs(bool shouldShow)
    {
        this.UsernameInput.gameObject.SetActive(shouldShow);
        this.PasswordInput.gameObject.SetActive(shouldShow);
        this.SubmitButton.gameObject.SetActive(shouldShow);
        if (shouldShow && this.isNewUser)
        {
            this.SubmitButton.GetComponentInChildren<TextMeshProUGUI>().text = "Sign up";
        }
        else if (shouldShow)
        {
            this.SubmitButton.GetComponentInChildren<TextMeshProUGUI>().text = "Login";
        }
    }

}
