using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StageHud : MonoBehaviour
{
    public TextMeshProUGUI ScoreText;

    private StageManager stageManager;
    private int score;

    void Start()
    {
        this.stageManager = ModelLocator.GetModelInstance<StageManager>() as StageManager;
    }

    void Update()
    {
        int newScore = this.stageManager.GetScore();
        if (this.score != newScore)
        {
            this.score = newScore;
            this.ScoreText.text = string.Format("Score: {0}", newScore);
        }

    }
}
