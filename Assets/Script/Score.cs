using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public int score;
    public Text sc;
    void Start()
    {
        score = 0;
        sc.text ="Score:"+score.ToString();
    }
    public void ScoreUp(int value)
    {
        score += value;
        sc.text ="Score:"+score.ToString();
    }
}
