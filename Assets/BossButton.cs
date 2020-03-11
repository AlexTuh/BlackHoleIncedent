using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossButton : MonoBehaviour
{
    //[SerializeField] private bool pressed = false;
    [SerializeField] private LayerMask ObjectWhichCanInteract;
    [SerializeField] private Vector2 Size;
    private BossScript Boss;
    private float timeOfExistance = 3f;
    void Awake()
    {
        Boss = GameObject.FindGameObjectWithTag("Boss").GetComponent<BossScript>();
    }

    // Update is called once per frame
    void Update()
    {
        timeOfExistance = timeOfExistance - Time.deltaTime;
        if (timeOfExistance < 0)
        {
            Destroy(gameObject);
        }
        Collider2D[] colliders = Physics2D.OverlapBoxAll(gameObject.transform.position, Size, ObjectWhichCanInteract);
        foreach (Collider2D col in colliders)
        {
            if (col.gameObject.tag == "Player")
            {
                Boss.TakeDamage();
                Destroy(gameObject);
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(gameObject.transform.position, Size);
    }
}
