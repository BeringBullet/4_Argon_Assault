using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{
    int score = 0;
    Text scoreText;
    // Start is called before the first frame update
    void Start()
    {
        scoreText = GetComponent<Text>();
        ScoreHit(0);
        InvokeRepeating("addScore", 10f, 10f);
    }

    // Update is called once per frame
    void Update() => scoreText.text = score.ToString();
    private void AddScore() => ScoreHit(1);
    public void ScoreHit(int value) => score += value;
}
