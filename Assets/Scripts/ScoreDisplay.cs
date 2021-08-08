using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    private int realScore;
    private int displayedScore;

    void Start()
    {

    }

    void FixedUpdate()
    {
        displayedScore += (int)((realScore - displayedScore) / 2f);
        if (Mathf.Abs(realScore - displayedScore) <= 1)
            displayedScore = realScore;

        GetComponent<Text>().text = "Score: " + displayedScore;
    }

    public void SetScore(int score)
    {
        this.realScore = score;
    }
}
