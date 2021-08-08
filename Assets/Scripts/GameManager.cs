using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject appleRef;

    [SerializeField]
    private GameObject watermelonRef;

    [SerializeField]
    private GameObject coconutRef;

    [SerializeField]
    private Text textTimer;

    [SerializeField]
    private TextMesh comboText;


    private Vector3 lastCutFruitLocation;
    private int score;
    private int comboCount;
    private Vector3 comboPosition;


    private float gtimer;

    public Vector3 LastCutFruitLocation { set => lastCutFruitLocation = value; }

    // Start is called before the first frame update
    void Start()
    {
        gtimer = 30.5f;

        Cursor.visible = false;
        comboText.gameObject.SetActive(false);

        InvokeRepeating("MakeNewApple", 1f, 1f);
        InvokeRepeating("MakeNewWatermelon", 1.5f, 2f);
        InvokeRepeating("MakeNewCoconut", 2f, 2f);
        textTimer.text = "Time: " + gtimer;

        score = 0;
        comboCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        gtimer -= Time.deltaTime;
        textTimer.text = "Time: " + math.floor(gtimer).ToString();
        if (gtimer < 5)
            textTimer.color = Color.red;


        if (gtimer < 0)
        {
            Destroy(FindObjectOfType<KnifeScript>().gameObject);
            textTimer.text = "GAME OVER!";
            Destroy(gameObject);
        }
    }

    private void MakeNewApple()
    {

        float probability = UnityEngine.Random.value;
        int numberOfFruits = 1;
        if (probability < 0.1)
            numberOfFruits = 0;
        else if (probability < 0.2)
            numberOfFruits = 2;
        else if (probability < 0.24)
            numberOfFruits = 3;


        for (int i = 0; i < numberOfFruits; i++)
        {
            var clone = Instantiate<GameObject>(appleRef);
            clone.SetActive(true);
        }
    }

    private void MakeNewWatermelon()
    {
        float probability = UnityEngine.Random.value;
        int numberOfFruits = 1;
        if (probability < 0.25)
            numberOfFruits = 0;
        else if (probability < 0.27)
            numberOfFruits = 2;
        else if (probability < 0.28)
            numberOfFruits = 3;


        for (int i = 0; i < numberOfFruits; i++)
        {
            var clone = Instantiate<GameObject>(watermelonRef);
            clone.SetActive(true);
        }
    }

    private void MakeNewCoconut()
    {

        float probability = UnityEngine.Random.value;
        int numberOfFruits = 1;
        if (probability < 0.7)
            numberOfFruits = 0;
        else if (probability < 0.75)
            numberOfFruits = 2;
        else if (probability < 0.76)
            numberOfFruits = 3;


        for (int i = 0; i < numberOfFruits; i++)
        {
            var clone = Instantiate<GameObject>(coconutRef);
            clone.SetActive(true);
        }
    }

    public void IncrementScore(int points)
    {
        this.score += points;
        comboCount++;

        FindObjectOfType<ScoreDisplay>().SetScore(score);
    }

    public void DecrementScore(int points)
    {
        this.score -= points;
        if (score < 0)
            score = 0;
        FindObjectOfType<ScoreDisplay>().SetScore(score);
    }

    public void ResetCombo()
    {
        if (comboCount > 2)
        {
            StartCoroutine(AnimateComboText(comboCount));
            score += comboCount * 100;
            FindObjectOfType<ScoreDisplay>().SetScore(score);
            if (comboCount > 4)
            {
                score += comboCount * 50;
            }
        }
        comboCount = 0;
    }


    IEnumerator AnimateComboText(int comboNumber)
    {
        var cloned = Instantiate(comboText);

        cloned.text = "COMBO x" + comboNumber;
        cloned.fontSize = 70;
        cloned.gameObject.transform.position = new Vector3(lastCutFruitLocation.x, lastCutFruitLocation.y, -5);
        cloned.gameObject.SetActive(true);

        var localPoint = lastCutFruitLocation;

        const int step = 80;

        float opacity = 1.0f;

        for (int i = 0; i < step; i++)
        {
            yield return new WaitForSeconds(.005f);

            const float animationTimeProp = 2.5f;

            if (i >= step / animationTimeProp)
                opacity -= 1 / (1 - (1 / animationTimeProp)) / step;

            cloned.fontSize = cloned.fontSize + 1;

            cloned.gameObject.transform.position += new Vector3(0,
                Mathf.Sign(-localPoint.y), 0) * 0.03f;

            cloned.color = new Color(cloned.color.r, cloned.color.g, cloned.color.b, opacity);
        }
        cloned.gameObject.SetActive(false);

        Destroy(cloned);
    }
}
