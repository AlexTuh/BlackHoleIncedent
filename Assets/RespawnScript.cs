using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RespawnScript : MonoBehaviour
{
    private GameObject Player;
    [SerializeField] private List<Transform> RespawnPointList;
    [SerializeField] private SceneReloaderScript SceneReloader;
    [SerializeField] private bool spawnObjectAfterDeadth = false;
    [SerializeField] private GameObject DeadthObject;
    // Start is called before the first frame update
    void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    public void Respawn(GameObject objectToRespawn, int lvl)
    {
        if (spawnObjectAfterDeadth)
        {
            //spawn dead
            if(Vector2.Distance(Player.transform.position, RespawnPointList[lvl-1].position) < 3) { return; }

            Vector2 spawnPos = new Vector2(0,objectToRespawn.transform.position.y);
            spawnPos.x = (float)Math.Round(objectToRespawn.transform.position.x * 2, MidpointRounding.AwayFromZero) / 2;
            spawnPos.y = (float)Math.Round(objectToRespawn.transform.position.y * 2, MidpointRounding.AwayFromZero) / 2;
            Instantiate(DeadthObject, spawnPos, objectToRespawn.transform.rotation);

            objectToRespawn.transform.position = RespawnPointList[lvl - 1].position;

            objectToRespawn.GetComponent<PlayerController2D>().Respawn();
        }
        else
        {
            SceneReloader.ReStart();
        }
    }
}
