
// libraries

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player1 : MonoBehaviour
{
    // initialising variables
    public float moveSpeed = 5f;
    public float jumpheight = 5f;
    public Rigidbody2D playerrb;
    public bool GroundCheck;
    [SerializeField] private LayerMask whatisground;
    [SerializeField] private Transform Groundcheck; //set to object below person
    const float groundedradius = .25f;

   

   


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
                
            }
            else
            {
                GroundCheck = false;
            }


        if (playerrb.velocity.x < 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        if (playerrb.velocity.x > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }


        if (Input.GetKeyDown(KeyCode.W) && GroundCheck == true)
        {
            Debug.Log("Jump!");
            playerrb.velocity = new Vector2(playerrb.velocity.x, jumpheight);

        }



        // input

        if (Input.GetKey(KeyCode.A))
        {
            playerrb.velocity = new Vector2(-moveSpeed, playerrb.velocity.y);


        };
        if (Input.GetKey(KeyCode.D) ) // this false bit might be wrong
        {
            playerrb.velocity = new Vector2(moveSpeed, playerrb.velocity.y);

        };

        if (Input.GetKey(KeyCode.D) == false && Input.GetKey(KeyCode.A) == false)
        {
            playerrb.velocity = new Vector2(0, playerrb.velocity.y);
        }

       
    }
}
