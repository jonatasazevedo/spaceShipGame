using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nave_Inimiga : MonoBehaviour
{
    public GameObject explosion;
    public Animator ani;
    float speed = 6,r=1;
    bool shooting;
    public GameObject shoot;
    public Transform cannon;
    int score = 100;
    bool direita;
    float health = 1,px, py;
    Vector2 nave;
    SpriteRenderer renderer;

    void Start()
    {
        renderer = GetComponentInChildren<SpriteRenderer>();
    }
    void Update()
    {
        nave = transform.position;
        nave.y -= speed * Time.deltaTime;
        direita = true;
        if (nave.x >= 2.5 && direita)
        {
            r = -1;
            direita = false;
        }
        else if (nave.x <= -2.5 && !direita)
        {
            r = 1;
            direita = true;
        }
        nave.x += r * speed*Time.deltaTime;
        ani.SetFloat("Horizontal",r);
        transform.position = nave;
        Shot();
    }

    void Shot()
    {
        if (shooting) return;
        shooting = true;
        Instantiate(shoot, cannon.position, cannon.rotation);
        StartCoroutine(Cooldown());
    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(shoot.GetComponent<Shoot_Inimigo>().rate);
        shooting = false;
    }

    void OnTriggerEnter2D(Collider2D coli)
    {
        if (coli.GetComponent<Movimento>() && !coli.GetComponent<Movimento>().invincible)
        {
            GameObject xp = Instantiate(explosion, transform.position, transform.rotation);
            renderer.enabled = false;
            GetComponent<Collider2D>().enabled = false;
            Destroy(xp, 1);
        }
        else if (coli.GetComponent<Shoot>())
        {
            FindObjectOfType<Score>().ScoreUp(score);
            health -= coli.GetComponent<Shoot>().damage;
            if (health <= 0)
            {
                GameObject xp = Instantiate(explosion, transform.position, transform.rotation);
                coli.GetComponent<Renderer>().enabled = false;
                coli.GetComponent<Collider2D>().enabled = false;
                renderer.enabled = false;
                GetComponent<Collider2D>().enabled = false;
                Destroy(xp, 1);
                Destroy(gameObject);
            }

        }

    }
}
