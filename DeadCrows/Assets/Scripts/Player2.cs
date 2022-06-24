// libraries
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public class Player2 : MonoBehaviour
{
    // initialising variables
    public float moveSpeed = 5f;
    public float jumpheight = 5f;
    public Rigidbody2D playerObject;
    public bool GroundCheck;
    public Animator animator;
    public AudioManager SFXManager;

    private int DoubleP2TriggersPressed = 0;

    [SerializeField] private LayerMask whatisground;
    [SerializeField] private Transform Groundcheck; //set to object below person

    const float groundedradius = .25f;


    // Original script start
    private void Awake()
    {
        playerObject = transform.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        RaycastHit2D colliders = Physics2D.Linecast(Groundcheck.position, Groundcheck.position + new Vector3(0, -groundedradius, 0), whatisground); // check position from bottom for a distance. so much better then circle casting oh god.
        Debug.DrawLine(Groundcheck.position, Groundcheck.position + new Vector3(0, -groundedradius, 0), Color.red);

        if (colliders)
        {
            GroundCheck = true;

            //Setting state for jump animation
            animator.SetBool("isJumping", false);
        }

        else
        {
            GroundCheck = false;

            // Setting state for jump animation
            animator.SetBool("isJumping", true);
        }

        // Player Inertia
        if (playerObject.velocity.x < 0) transform.localScale = new Vector3(1, 1, 1);
        if (playerObject.velocity.x > 0) transform.localScale = new Vector3(-1, 1, 1);

        // Player Jumping
        if (Input.GetKeyDown(KeyCode.UpArrow) && GroundCheck == true)
        {
            if (playerObject.velocity.y <= 1) SFXManager.PlaySFX("Player2Jump");
            playerObject.velocity = new Vector2(playerObject.velocity.x, jumpheight);
        }

        // Player Moving Left
        if (Input.GetKey(KeyCode.LeftArrow))
        {

            playerObject.velocity = new Vector2(-moveSpeed, playerObject.velocity.y);

            // Setting state for walk animation
            animator.SetBool("isMoving", true);
        };

        // Player Moving Right
        if (Input.GetKey(KeyCode.RightArrow))
        {
            playerObject.velocity = new Vector2(moveSpeed, playerObject.velocity.y);

            // Setting state for walk animation
            animator.SetBool("isMoving", true);
        };

        // Player has stopped moving
        if (Input.GetKey(KeyCode.RightArrow) == false && Input.GetKey(KeyCode.LeftArrow) == false)
        {

            playerObject.velocity = new Vector2(0, playerObject.velocity.y);

            // Setting state for walk animation
            animator.SetBool("isMoving", false);
        }
    }

    // Handle Object Triggers
    private void OnTriggerEnter2D(Collider2D collider)
    {
        bool CollectableTouched = true;

        if (collider.gameObject.CompareTag("LightSwitch")) LightCandles();
        else if (collider.gameObject.CompareTag("StackOfDishes1")) FlipObject("ChairToFlip1");
        else if (collider.gameObject.CompareTag("BlackFeather2")) ClearRubble("Rubble2");

        // Update the environment counter as both players must activate these
        else if (collider.gameObject.CompareTag("Egg")) GameObject.Find("Enviroment").GetComponent<Environment>().TotalEggsActivated++;

        // Player 2 must touch both triggers to lower the platform
        else if (collider.gameObject.CompareTag("DoubleP2Trigger"))
        {
            DoubleP2TriggersPressed++;

            if (DoubleP2TriggersPressed == 2)
            {
                GameObject[] ObjectNameTag = GameObject.FindGameObjectsWithTag("DoubleP2Platform");

                // Lowers all Objects with Matching ObjectNameTag
                foreach (GameObject targetObject in ObjectNameTag)
                {
                    targetObject.transform.position = new Vector3(targetObject.transform.position.x,
                                                                  targetObject.transform.position.y - 1,
                                                                  targetObject.transform.position.z);
                }

                SFXManager.PlaySFX("ClearRubble");
            }
        }

        else if (collider.gameObject.CompareTag("LastFeathers")) ClearRubble("BarrierToDestroy");

        // Update the environment counter as both players must activate these
        else if (collider.gameObject.CompareTag("LatActivator")) GameObject.Find("Enviroment").GetComponent<Environment>().LastItemsActivated++;

        // Only clear collectibles
        else CollectableTouched = false;

        if (CollectableTouched) ClearCollectable(collider);
    }

    // Light the Candles for Player 1 and Clear their Initial Darkness
    private void LightCandles()
    {

        print("Turning on the Lights!");
    }

    // Clears the Rubble with the Matching RubbleTag
    private void ClearRubble(string RubbleTag)
    {
        // Find GameObjects with RubbleTag
        GameObject[] RubbleToClear = GameObject.FindGameObjectsWithTag(RubbleTag);

        // Destroy all Rubble with RubbleTag
        foreach (GameObject targetRubble in RubbleToClear)
        {
            GameObject.Destroy(targetRubble);

            SFXManager.PlaySFX("ClearRubble");
        }
    }

    // Flip Object(s) with the Matching ObjectNameTag
    private void FlipObject(string ObjectNameTag)
    {
        // Find GameObjects with ObjectNameTag
        GameObject[] ObjectToFlip = GameObject.FindGameObjectsWithTag(ObjectNameTag);

        // Flip all Objects with Matching ObjectNameTag
        foreach (GameObject targetObject in ObjectToFlip)
        {
            if (ObjectNameTag == "ChairToFlip1")
            {
                targetObject.transform.position = new Vector3(targetObject.transform.position.x,
                                                              targetObject.transform.position.y + 1.25f,
                                                              targetObject.transform.position.z);

                targetObject.transform.rotation = new Quaternion(targetObject.transform.rotation.x,
                                                                 targetObject.transform.rotation.y, 0,
                                                                 targetObject.transform.rotation.w);
            }

            SFXManager.PlaySFX("ClearRubble");
        }
    }

    private void ClearCollectable(Collider2D collider)
    {
        GameObject.Destroy(collider.gameObject);
    }
}