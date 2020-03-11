using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    Animator anim;
    [SerializeField] private bool leaver = false;
    [SerializeField] private List<DoorScript> Doors = new List<DoorScript>();
    [SerializeField] private List<GameObject> Walls = new List<GameObject>();
    [SerializeField] private LayerMask ObjectWhichCanInteract;
    [SerializeField] private Vector2 Size;
    [SerializeField] AudioSource Audio;
    public bool pressed = false;
    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!leaver)
        {
            if (pressed == true)
            {
                return;
            }
            Collider2D[] colliders = Physics2D.OverlapBoxAll(gameObject.transform.position, Size, ObjectWhichCanInteract);
            foreach(Collider2D col in colliders)
            {
                if(col.gameObject.tag == "Player")
                {
                    anim.SetBool("Pressing", true);
                    foreach (DoorScript door in Doors)
                    {
                        door.Open();
                        pressed = true;
                        Audio.Stop();
                        Audio.Play();
                    }
                }
            }
        }
        else
        {
            Collider2D[] colliders = Physics2D.OverlapBoxAll(gameObject.transform.position, Size, ObjectWhichCanInteract);
            foreach (Collider2D col in colliders)
            {
                if (col.gameObject.tag == "Player")
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        Debug.Log("Pressed");
                        pressed = true;
                        Audio.Stop();
                        Audio.Play();
                    }
                }
            }
        }
        if (pressed && leaver)
        {
            foreach (GameObject wall in Walls)
            {
                if (wall.activeSelf == false)
                    wall.SetActive(true);
                else
                    wall.SetActive(false);
                Audio.Stop();
                Audio.Play();
            }
            pressed = false;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(gameObject.transform.position, Size);
    }
}
