using TMPro;
using UnityEngine;

public class StageHud : MonoBehaviour
{
    public TextMeshProUGUI ScoreText;

    private StageManager stageManager;
    private float score;

    void Start()
    {
        this.stageManager = ModelLocator.GetModelInstance<StageManager>() as StageManager;
    }

    void Update()
    {
        float newScore = this.stageManager.GetScore();
        if (this.score != newScore)
        {
            this.score = newScore;
            this.ScoreText.text = string.Format("Score: {0}", newScore);
        }

    }
}
