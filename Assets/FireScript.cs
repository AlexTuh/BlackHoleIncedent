using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireScript : MonoBehaviour
{
    private float timeRem = 1f;
    private bool delete = false;
    private bool delete2 = false;
    [SerializeField] private GameObject Sign;
    Animator anim;
    private GameObject localSign;
    private void Awake()
    {
        gameObject.GetComponent<Collider2D>().enabled = false;
        localSign = Instantiate(Sign, transform.position, transform.rotation);
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        timeRem = timeRem - Time.deltaTime;
        if(timeRem <= 0)
        {
            if (!delete)
            {
                Destroy(localSign);
                timeRem = 0.2f;
                delete = true;
            }
            else if(!delete2)
            {
                anim.SetBool("Play", true);
                gameObject.GetComponent<Collider2D>().enabled = true;
                delete2 = true;
                timeRem = 1f;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
