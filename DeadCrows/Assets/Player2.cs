
// libraries

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player2 : MonoBehaviour
{
    // initialising variables
    public float moveSpeed = 5f;
    public float jumpheight = 5f;
    public Rigidbody2D playerrb;
    public bool GroundCheck;
    public Animator animator; 
    [SerializeField] private LayerMask whatisground;
    [SerializeField] private Transform Groundcheck; //set to object below person
    const float groundedradius = .25f;


    ////Unnecessary I suppose
    //[Header("Events")]
    //[Space]

    //public UnityEvent OnLandEvent;

    //[System.Serializable]
    //public class BoolEvent : UnityEvent<bool> { }

    //public BoolEvent OnLanding;
    //private bool isJumping = false;

    //public BoolEvent OnStop;
    //private bool isMoving = false;

    private void Awake()
    {
        playerrb = transform.GetComponent<Rigidbody2D>();
    }

    void Update()

    {
           
        RaycastHit2D colliders = Physics2D.Linecast(Groundcheck.position, Groundcheck.position + new Vector3(0,-groundedradius,0),whatisground); // check position from bottom for a distance. so much better then circle casting oh god.
         Debug.DrawLine(Groundcheck.position,Groundcheck.position + new Vector3(0,-groundedradius,0),Color.red); 
         if (colliders)
            {
                GroundCheck = true;

            //Setting state for jump animation
            animator.SetBool("isJumping", false);

            }
            else
            {
                GroundCheck = false;

            //Setting state for jump animation
            animator.SetBool("isJumping", true);

            }


        if (playerrb.velocity.x < 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        if (playerrb.velocity.x > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }


        if (Input.GetKeyDown(KeyCode.UpArrow) && GroundCheck == true)
        {
            Debug.Log("Jump!");
            playerrb.velocity = new Vector2(playerrb.velocity.x, jumpheight);

        }



        // input

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            playerrb.velocity = new Vector2(-moveSpeed, playerrb.velocity.y);
            //Setting state for walk animation
            animator.SetBool("isMoving", true);


        };
        if (Input.GetKey(KeyCode.RightArrow) ) // this false bit might be wrong
        {
            playerrb.velocity = new Vector2(moveSpeed, playerrb.velocity.y);
            //Setting state for walk animation
            animator.SetBool("isMoving", true);

        };

       if (Input.GetKey(KeyCode.RightArrow) == false && Input.GetKey(KeyCode.LeftArrow) == false)
       {
           playerrb.velocity = new Vector2(0, playerrb.velocity.y);
           animator.SetBool("isMoving", false);
        }

       
    }
}
