using TMPro;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public Boundary boundary;
    public TMP_Text scoreText;
    private int scoreValue = 0;
    // Start is called before the first frame update
    void Start()
    {
        // TODO
    }

    // Update is called once per frame
    void Update()
    {
        int newScore = this.boundary.GetScore();
        if (this.scoreValue != newScore)
        {
            this.scoreValue = newScore;
            this.scoreText.text = string.Format("Score: {0}", scoreValue);
        }

    }
}
