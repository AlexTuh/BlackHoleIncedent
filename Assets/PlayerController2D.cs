using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2D : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] private Animator Anim;
    [SerializeField] private float speed;
    [Range(0, .3f)] [SerializeField] private float MovementSmoothing = .05f;
    private float HorizontalInput;
    public Transform GroundCheck;
    //[SerializeField] private float GroundedRadius;
    [SerializeField] private LayerMask WhatIsGround;
    [HideInInspector] public bool grounded;
    [HideInInspector] public bool onLians;
    [SerializeField] private float jumpForce = 2;
    [SerializeField] private bool AirControl = true;
    [SerializeField] private bool canJump = true;
    [SerializeField] AudioSource Death;
    public bool canWalk = true;
    private RespawnScript RespawnController;
    private List<Vector2> velocityBefore = new List<Vector2>();
    public enum characters { skeleton, magic }
    public characters choosenCharacter;
    private bool wasGrounded = false;
    private bool wasOnLianas = false;
    [HideInInspector]public bool FacingRight = false;

    // Start is called before the first frame update
    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        RespawnController = GameObject.FindGameObjectWithTag("GameController").GetComponent<RespawnScript>();
        if (Anim == null){
            Anim = gameObject.GetComponent<Animator>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        HorizontalInput = Input.GetAxis("Horizontal");
        if (HorizontalInput != 0)
        {
            Anim.SetBool("Walking", true);
            if (FacingRight && HorizontalInput < 0 && choosenCharacter.ToString() == "magic")
            {
                Flip();
            }
            else if (!FacingRight && HorizontalInput > 0 && choosenCharacter.ToString() == "magic")
            {
                Flip();
            }
        }
        else
        {
            Anim.SetBool("Walking", false);
        }
        if(choosenCharacter.ToString() == "skeleton")
        {
            SkeletonEventController();
        }
        else if(choosenCharacter.ToString() == "magic")
        {
            MagicEventController();
        }
    }
    void MagicEventController()
    {
        if (grounded == false)
        {
            Anim.SetBool("Falling", true);
        }
        else
        {
            Anim.SetBool("Falling", false); 
        }
        if (wasGrounded == false && grounded == true)
        {
            Anim.SetBool("Landing", true);
        }
        else
        {
            Anim.SetBool("Landing", false);
        }
        wasGrounded = grounded;
    }
    void SkeletonEventController()
    {
        bool isFallen = false;
        foreach (Vector2 tempVector in velocityBefore)
        {
            if (tempVector.y < -6.5)
            {
                isFallen = true;
                break;
            }
        }
        if (isFallen && grounded)
        {
            RespawnController.Respawn(gameObject, 1);
            rb.velocity = Vector2.zero;
            velocityBefore.Clear();
        }
        velocityBefore.Add(rb.velocity);
    }
    public void Flip()
    {
        FacingRight = !FacingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    private void FixedUpdate()
    {
        if (!canWalk)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            return;
        }
        Vector3 VectorZero = Vector3.zero;
        Vector2 targetVelocity = new Vector2(HorizontalInput * speed,rb.velocity.y);

        if(AirControl && grounded == false || grounded)
            rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref VectorZero, MovementSmoothing);

        wasOnLianas = onLians;
        grounded = false;
        onLians = false;
        Collider2D[] colliders = Physics2D.OverlapBoxAll(GroundCheck.position, new Vector2(.8f, .1f), WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject.tag == "Lians" && colliders[i].gameObject != gameObject && choosenCharacter.ToString() == "skeleton")
            {
                onLians = true;
            }
            else if (colliders[i].gameObject != gameObject)
            {
                grounded = true;
            }
            if (colliders[i].gameObject.tag == "Spikes")
            {
                RespawnController.Respawn(gameObject, 1);
                if(!Death.isPlaying)
                    Death.Play();
            }
        }
        if (choosenCharacter.ToString() == "skeleton")
        {
            rb.gravityScale = 2;
            AirControl = false;
        }
        if (grounded && Input.GetAxis("Jump") > 0 && canJump)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
        }
        else if (onLians)
        {
            rb.gravityScale = 0; //2 as allways
            AirControl = true;
            if (Input.GetAxis("Vertical") > 0)
            {
                rb.AddForce(Vector3.up * 4);
            }
            else if (Input.GetAxis("Vertical") < 0)
            {
                rb.AddForce(Vector3.down * 4);
            }
        }
        else if(onLians == false && wasOnLianas == true)
        {
            rb.velocity = new Vector2(rb.velocity.x,0);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(GroundCheck.position, new Vector3(.8f, .1f, 0));
    }
    public void Respawn()
    {
        StartCoroutine(RespawnIE());
    }
    IEnumerator RespawnIE()
    {
        Anim.SetBool("Resur", true);
        canWalk = false;
        yield return new WaitForSeconds(0.55f); //wait one second
        canWalk = true;
        Anim.SetBool("Resur", false);
    }
}
