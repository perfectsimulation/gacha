using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    // Open a Stage scene from the Adventure scene
    public void StartStage()
    {
        // TODO: add argument for stage details
        // called onClick StageButton in Adventure menu
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
