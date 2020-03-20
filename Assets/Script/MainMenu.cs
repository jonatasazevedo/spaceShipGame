using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{

    public GameObject[] objectsToActivate;

    void Start()
    {
        bool started = GameController.Instance.started;
        ActiveObjects(started);
    }

    public void StartGame()
    {
        GameController.Instance.started = true;
        ActiveObjects(true);
    }
    void ActiveObjects(bool value)
    {
        foreach (GameObject obj in objectsToActivate)
        {
            obj.SetActive(value);
        }
        gameObject.SetActive(!value);
    }
}

