using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonScript : MonoBehaviour
{
    private RespawnScript RespawnController;
    private float delay = 0;
    private void Awake()
    {
        RespawnController = GameObject.FindGameObjectWithTag("GameController").GetComponent<RespawnScript>();
    }
    void Update()
    {
        if(delay > 0)
        {
            delay = delay - Time.deltaTime;
            return;
        }
        if(Input.GetAxis("Vertical") > 0)
        {
            RespawnController.Respawn(gameObject, 1);
            delay = .5f;
        }
    }
}
