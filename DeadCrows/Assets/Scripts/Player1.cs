// libraries
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public class Player1 : MonoBehaviour
{
    //Pause Menu
    public Canvas canvas;
    bool paused = false;


    // initialising variables
    public float moveSpeed = 5f;
    public float jumpheight = 5f;
    public Rigidbody2D playerObject;
    public bool GroundCheck;
    public Animator animator;
    public AudioManager SFXManager;

    [SerializeField] private LayerMask whatisground;
    [SerializeField] private Transform Groundcheck; //set to object below person

    const float groundedradius = .25f;

    // Original script start
    private void Awake()
    {
        playerObject = transform.GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
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
        if (Input.GetKeyDown(KeyCode.W) && GroundCheck == true)
        {
            if (playerObject.velocity.y <= 1) SFXManager.PlaySFX("Player1Jump");
            playerObject.velocity = new Vector2(playerObject.velocity.x, jumpheight);
        }

        // Player Moving Left
        if (Input.GetKey(KeyCode.A))
        {

            playerObject.velocity = new Vector2(-moveSpeed, playerObject.velocity.y);

            // Setting state for walk animation
            animator.SetBool("isMoving", true);
        };

        // Player Moving Right
        if (Input.GetKey(KeyCode.D))
        {
            playerObject.velocity = new Vector2(moveSpeed, playerObject.velocity.y);

            // Setting state for walk animation
            animator.SetBool("isMoving", true);
        };

        // Player has stopped moving
        if (Input.GetKey(KeyCode.D) == false && Input.GetKey(KeyCode.A) == false)
        {

            playerObject.velocity = new Vector2(0, playerObject.velocity.y);
            
            // Setting state for walk animation
            animator.SetBool("isMoving", false);
        }

        //MenuInputs
        if (Input.GetKey(KeyCode.P) && !paused)
        {
          //  PauseMenu.SetActive(true);
            canvas.GetComponent<Canvas>().enabled = true;

        }
        if (Input.GetKey(KeyCode.P) && paused == true)
        {
           // PauseMenu.SetActive(false);
            canvas.GetComponent<Canvas>().enabled = false;
        }
    }

    // Handle Object Triggers
    private void OnTriggerEnter2D(Collider2D collider)
    {
        bool CollectableTouched = true;

        if (collider.gameObject.CompareTag("Feather1")) ClearRubble("Rubble1");
        else if (collider.gameObject.CompareTag("Button1")) FlipObject("PlatformToFlip1");
        else if (collider.gameObject.CompareTag("CandleStick1")) FlipObject("PlatformToFlipA");
        else if (collider.gameObject.CompareTag("CandleStick2")) FlipObject("PlatformToFlipB");
        else if (collider.gameObject.CompareTag("CandleStick3")) FlipObject("PlatformToFlipC");
        else CollectableTouched = false;

        if (CollectableTouched) ClearCollectable(collider);
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
            targetObject.transform.position = new Vector3(targetObject.transform.position.x,
                                                            targetObject.transform.position.y - 1,
                                                            targetObject.transform.position.z);

            targetObject.transform.rotation = new Quaternion(targetObject.transform.rotation.x,
                                                                targetObject.transform.rotation.y, 0,
                                                                targetObject.transform.rotation.w);

            SFXManager.PlaySFX("ClearRubble");
        }
    }

    private void ClearCollectable(Collider2D collider)
    {
        GameObject.Destroy(collider.gameObject);
    }
}