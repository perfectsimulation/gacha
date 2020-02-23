using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageHud : MonoBehaviour
{
    public TextMeshProUGUI ScoreText;
    public GameObject CountdownOverlay;
    public GameObject ResultOverlay;

    private StageManager stageManager;
    private TextMeshProUGUI countdownText;

    private float score;
    private int countdown = 3;
    private bool isStageOver = false;

    void Start()
    {
        // Cache the stage manager
        this.stageManager = ModelLocator.GetModelInstance<StageManager>() as StageManager;
        // Show stage start countdown overlay
        this.ShowCountdownOverlay();
        // Hide stage results overlay
        this.ResultOverlay.SetActive(false);
    }

    void Update()
    {
        // Update score while stage is in session
        if (!this.isStageOver)
        {
            float newScore = this.stageManager.GetScore();
            // Change score in HUD if needed
            if (this.score != newScore)
            {
                this.score = newScore;
                this.ScoreText.text = string.Format("Score: {0}", newScore);
            }

            // Check if stage is over
            this.isStageOver = stageManager.IsStageOver();
        }
        else if (!this.ResultOverlay.activeInHierarchy)
        {
            // Stage is now over, show result overlay
            this.ResultOverlay.SetActive(true);
            this.ShowResultOverlay();
        }

    }

    public void EndStage()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    // Show countdown overlay before stage begins
    private void ShowCountdownOverlay()
    {
        this.CountdownOverlay.SetActive(true);
        this.countdownText = this.CountdownOverlay.GetComponentInChildren<TextMeshProUGUI>();
        StartCoroutine(BeginCountdown());
    }

    // Count down to begin stage
    private IEnumerator BeginCountdown()
    {
        // Set the countdown text
        this.countdownText.text = this.countdown.ToString();
        // Wait for one second
        yield return new WaitForSeconds(1f);
        // Decrement countdown
        this.countdown--;

        if (this.countdown > 0)
        {
            // Still more to count down
            StartCoroutine(BeginCountdown());
        }
        else
        {
            // Disable countdown overlay and set to true isCountdownComplete in stage manager
            this.CountdownOverlay.SetActive(false);
            this.stageManager.SetCountdownComplete();
        }
    }

    // Show result overlay after stage ends
    private void ShowResultOverlay()
    {
        this.ResultOverlay.SetActive(true);
    }
}
