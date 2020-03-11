using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorScript : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private Animator effectAnim;
    [SerializeField] private SceneReloaderScript Reloader;
    [SerializeField] private LayerMask PlayerMask;
    [SerializeField] private Vector2 Size;
    [SerializeField] private int buttonsQuantity = 1;
    private int buttonPressed = 0;
    [SerializeField] private bool open = false;
    void Update()
    {
        if (open || buttonsQuantity == 0) 
        {
            Collider2D[] collided = Physics2D.OverlapBoxAll(gameObject.transform.position, Size, PlayerMask);
            foreach(Collider2D collider in collided)
            {
                if(collider.gameObject.tag == "Player")
                {
                    Reloader.ReStart(true);
                }
            }
        }
    }
    public void Open()
    {
        buttonPressed++;
        Debug.Log(buttonPressed);
        if (buttonPressed == buttonsQuantity)
        {
            anim.SetBool("Opening", true);
            effectAnim.SetBool("Pressing", true);
            open = true;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(gameObject.transform.position, Size);
    }
}
