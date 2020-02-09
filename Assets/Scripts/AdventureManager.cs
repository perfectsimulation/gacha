using UnityEngine;
using UnityEngine.SceneManagement;

public class AdventureManager : MonoBehaviour
{
    private UserManager userManager;
    private int currentLevel;
    // Populate the Adventure menu with Stage buttons using user data
    private void Start()
    {
        // Cache userManager
        this.userManager = ModelLocator.GetModelInstance<UserManager>() as UserManager;
    }
    // Open a Stage scene from the Adventure scene
    public void StartStage()
    {
        // TODO: add argument for stage details
        // called onClick StageButton in Adventure menu
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
