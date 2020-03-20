using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot_Inimigo : MonoBehaviour
{
    [Range(0.01f, 1)] public float rate = 0.7f;
    public float damage = 1;
    void Start()
    {
        Destroy(gameObject, 1);
    }
    void Update()
    {
        Vector3 p = transform.position;
        p.y -= 10 * Time.deltaTime;
        transform.position = p;
    }

}
