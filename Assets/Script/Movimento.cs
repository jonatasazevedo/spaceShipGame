using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimento : MonoBehaviour
{
    private float health,maxHealth = 3;
    public GameObject explosion;
    public Animator ani;
    float speed=5, h=0, v=0;
    public bool invincible,damaging;
    SpriteRenderer renderer;
    bool shooting;
    public GameObject shoot;
    public Transform cannon;
    private void Start()
    {
        health = maxHealth;
        renderer = GetComponentInChildren<SpriteRenderer>();
        StartCoroutine(Invicibility());
    }

    IEnumerator Invicibility()
    {
        invincible = true;
        Color color = renderer.color;
        color.a = .5f;
        renderer.color = color;
        yield return new WaitForSeconds(2);
        color.a = 1;
        renderer.color = color;
        invincible = false;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Shot();
        }
        if (invincible)
        {
            Color color = renderer.color;
            color.a = Mathf.Repeat(Time.time, .5f);
            renderer.color = color;
        }
        if (damaging)
        {
            renderer.color = Color.Lerp(Color.white,Color.red,Mathf.Repeat(Time.time,.7f));
        }

        Vector2 pos = transform.position;
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        ani.SetFloat("Horizontal",h);
        ani.SetFloat("Vertical",v);
        pos.x += speed * h * Time.deltaTime;
        pos.y += speed * v * Time.deltaTime;
        transform.position = pos;
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
        yield return new WaitForSeconds(shoot.GetComponent<Shoot>().rate);
        shooting = false;
    }

    IEnumerator Damage(float damage)
    {
        health -= damage;
        damaging = true;
        yield return new WaitForSeconds(1);
        damaging = false;
        renderer.color = Color.white;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Shoot_Inimigo>() && !invincible)
        {
            float damage = collision.GetComponent<Shoot_Inimigo>().damage;
            collision.GetComponent<Shoot_Inimigo>().GetComponentInChildren<SpriteRenderer>().enabled = false;
            StartCoroutine(Damage(damage));
            if (health <= 0) Death();
            else GetComponent<AudioSource>().Play();
        }
        else if (collision.GetComponent<Nave_Inimiga>()) Death();
    }

    void Death()
    {
        FindObjectOfType<Spawn>().SpawnShip(1);
        GameObject exp = Instantiate(explosion,transform.position,transform.rotation);
        Destroy(exp,1);
        Destroy(gameObject,1);
        GetComponentInChildren<SpriteRenderer>().enabled = false;
        GetComponentInChildren<Collider2D>().enabled = false;
    }
}
