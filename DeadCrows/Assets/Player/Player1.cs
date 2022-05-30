
// libraries

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player1 : MonoBehaviour
{
    // initialising variables
    public float moveSpeed = 5f;
    public float jumpheight = 5f;
    public Rigidbody2D rb;
    public bool GroundCheck;
    [SerializeField] private LayerMask whatisground;
    [SerializeField] private Transform Groundcheck; //set to object below person
    const float groundedradius = .25f;

   

   


    private void Awake()
    {
        rb = transform.GetComponent<Rigidbody2D>();
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
        




        if (Input.GetKeyDown(KeyCode.W) && GroundCheck == true)
        {
            Debug.Log("Jump!");
            rb.velocity = new Vector2(rb.velocity.x, jumpheight);

        }



        // input

        if (Input.GetKey(KeyCode.A))
        {
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);


        };
        if (Input.GetKey(KeyCode.D) ) // this false bit might be wrong
        {
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);

        };

        if (Input.GetKey(KeyCode.D) == false && Input.GetKey(KeyCode.A) == false)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

       
    }
}
