using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public GameObject inimigos;
    public bool eraseScore;
    public Text scoreTxt, highScoreTxt;

    void Start()
    {
        inimigos.SetActive(false);
        if (eraseScore)
        {
            PlayerPrefs.DeleteKey("Score");
            eraseScore = false;
        }
        int score = FindObjectOfType<Spawn>().score;
        scoreTxt.text = "Score: " + score;
        int lastScore = 0;
        if (PlayerPrefs.HasKey("Score"))
        {
            lastScore = PlayerPrefs.GetInt("Score");
            if (score > lastScore) highScoreTxt.gameObject.SetActive(true);
        }
        else PlayerPrefs.SetInt("Score", score);
    }
}

