using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Spawn : MonoBehaviour {
    public GameObject nave,gameOver,imgnave,scoretxt;
    int lives,maxLives = 3;
    public int score;
    public Text LifeCounter;
    public AudioSource audio;
    void Start () {
        lives = maxLives;
        LifeCounter.text = maxLives.ToString();
        Instantiate(nave, transform.position, transform.rotation);
    }    

	public void SpawnShip(float time=0) {
        if (lives > 1)
        {
            lives--;
            LifeCounter.text = lives.ToString();
            StartCoroutine(Respawn(time));
        }
        else
        {
            lives = 0;
            gameOver.SetActive(true);
            imgnave.SetActive(false);
            score = FindObjectOfType<Score>().score;
            scoretxt.SetActive(false);
            audio.Stop();
        }
        
	}

    public void Restart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    IEnumerator Respawn(float time=0)
    {
        yield return new WaitForSeconds(time);
        Instantiate(nave, transform.position,transform.rotation);
    }
}
