using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    private bool fightStart = false;
    [SerializeField] private Animator anim;
    [SerializeField] private Animator animPlatf;
    [SerializeField] private Animator animPlatfRight;
    [SerializeField] private GameObject Spike;
    [SerializeField] private GameObject Button;
    [SerializeField] private PlayerController2D Player;
    public List<SpawnPositions> SpawnPos;
    //[SerializeField] private int[] stages; //example 1, 4, 7, 9
    private int stage = 0;
    private float timeRem = 5;
    [SerializeField] private int life = 3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(life == 0)
        {
            anim.SetBool("Dead", true);
            StartCoroutine(LastMove());
        }
        if (fightStart && life > 0)
        {
            if(timeRem > 0)
            {
                timeRem = timeRem - Time.deltaTime;
            }
            if(timeRem <= 0)
            {
                SpawnRandomSpikes();
                timeRem = 5;
            }
        }
    }
    void SpawnRandomSpikes()
    {
        foreach (GameObject spwn in SpawnPos[stage].Positons)
        {
            if(stage == 3 || stage == 6 || stage == 9)
                Instantiate(Button, spwn.transform.position, spwn.transform.rotation);
            else
                Instantiate(Spike, spwn.transform.position, spwn.transform.rotation);
        }
        stage++;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (fightStart)
            {
                return;
            }
            fightStart = true;
            StartCoroutine(PrepareForBattle());
        }
    }
    IEnumerator PrepareForBattle()
    {
        Player.canWalk = false;
        animPlatf.SetBool("Close", true);
        yield return new WaitForSeconds(2);
        Player.canWalk = true;
        anim.SetBool("Start", true);
        yield return new WaitForSeconds(1);
        anim.SetBool("Start", false);
    }
    IEnumerator LastMove()
    {
        yield return new WaitForSeconds(1);
        animPlatfRight.SetBool("Close", true);
    }
    public void TakeDamage()
    {
        life--;
        StartCoroutine(DamageTakingAnimation());
    }
    IEnumerator DamageTakingAnimation()
    {
        anim.SetBool("Damage", true);
        yield return new WaitForSeconds(1);
        anim.SetBool("Damage", false);
    }
}
