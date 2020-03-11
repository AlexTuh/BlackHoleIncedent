using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicScript : MonoBehaviour
{
    Rigidbody2D rb;
    PlayerController2D Controller;
    RespawnScript RespawnController;
    private float timeRem = 0;
    [SerializeField] private AudioSource Jump;
    [SerializeField] private GameObject Effect;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        Controller = gameObject.GetComponent<PlayerController2D>();
        RespawnController = GameObject.FindGameObjectWithTag("GameController").GetComponent<RespawnScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Controller.canWalk)
        {
            return;
        }
        if(timeRem > 0)
        {
            timeRem = timeRem - Time.deltaTime;
        }
        if(Input.GetAxis("Jump") > 0 && Controller.grounded && timeRem <= 0)
        {
            rb.gravityScale = rb.gravityScale * -1;
            Jump.Stop();
            Jump.Play();
            if(gameObject.transform.rotation.z == 0)
                gameObject.transform.rotation = new Quaternion(gameObject.transform.rotation.x, gameObject.transform.rotation.y, 180, 0);
            else
                gameObject.transform.rotation = new Quaternion(gameObject.transform.rotation.x, gameObject.transform.rotation.y, 0, 0);
            StartCoroutine(Delete(Instantiate(Effect, Controller.GroundCheck.position, transform.rotation)));
            Controller.FacingRight = !Controller.FacingRight;
            timeRem = 0.5f;
        }    
    }
    IEnumerator Delete(GameObject Effect)
    {
        yield return new WaitForSeconds(1);
        Destroy(Effect);
    }
}
