using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gerador_de_inimigos : MonoBehaviour {
    public GameObject inimigo;

    void Start()
    {
        StartCoroutine(ShipGenerator());
    }

    IEnumerator ShipGenerator()
    {
        float time = Random.Range(1,3);
        yield return new WaitForSeconds(time);

        Vector3 pos = transform.position;
        pos.x = Random.Range(-3,3);
        GameObject ini = Instantiate(inimigo,pos,transform.rotation);
        Destroy(ini,2);
        StartCoroutine(ShipGenerator());
    }
}
