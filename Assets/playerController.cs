using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Globals;

public class playerController : MonoBehaviour
{
    private Rigidbody2D rb;
    public bool isGrounded;
    public GameObject player;
    public Animator anim;
    public GameObject arrow;
    public float speed = 16;
    public bool flipob;
    public float timer = 0;
    public int count;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        DoJump();
        DoMove();
        Animations(anim);
        Helper.DoRayCollisionCheck(player);
        if (Input.GetButton("Fire1") && timer <= 0) // Checks if the left mouse button is being pressed and the timer is set to 0.
        {
            // The code below limits the amount of prefabs allowed within the game.
            GameObject[] getCount = GameObject.FindGameObjectsWithTag("prefab"); // Searches for objects with the prefab tag on it.
            count = getCount.Length; // Counts the amount of prefabs.
            Vector2 velocity = rb.velocity;
            if (velocity.x > 0) // If the player is facing left.
            {   
                if (count < 10) // If there is less than 10 prefabs.
                {
                    Helper.Bullet(arrow, transform.position.x, transform.position.y, 6f, 0); // From the helper.cs script summon the arrow prefab.
                    print("Successfully spawned arrow");
                }
                else
                {
                    print("Too many arrows, please wait for arrows to despawn...");
                }
            }else if (velocity.x < 0) // If the player is facing right.
            {
                if (count < 10) // If there is less than 10 prefabs.
                {
                    Helper.Bullet(arrow, transform.position.x, transform.position.y, -6f, 0); // From the helper.cs script summon the arrow prefab.
                    print("Successfully spawned arrow");
                }
                else
                {
                    print("Too many arrows, please wait for arrows to despawn...");
                }
            }
            timer = 30; // Allows the arrow to start falling after 0.5 seconds.
        }
        timer--; // For every frame, remove 1 from the timer counter.
    }
    public void Animations(Animator anim)
    {
        if (Input.GetKey("d") || Input.GetKey("right"))  // Checks if the player is walking right.
        {
            anim.SetBool("WalkRight",true); // Set the walking right animation on.
            anim.SetBool("WalkLeft",false); // Set the walking left animation off.
        }
        if (Input.GetKey("a") || Input.GetKey("left")) // Checks if the player is walking left.
        {
            anim.SetBool("WalkLeft",true); // Set the walking left animation on.
            anim.SetBool("WalkRight",false); // Set the walking right animation off.
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        isGrounded = true; //If the player is touching the ground, allow jump.
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false; // If the player is not touching the ground, do not allow jump.
    }

    void DoJump()
    {
        Vector2 velocity = rb.velocity;

        // The code below only executes if W, Spacebar or Up arrow is being pressed whilst the player is touching the ground.
        if ((Input.GetKeyDown("w") || Input.GetKeyDown("space") || Input.GetKeyDown("up")) && (isGrounded == true))
        {
            if (velocity.y < 0.01f )
            {
                velocity.y = 7f; // give the player a velocity of 7 in the y axis
                
            }
        }

        rb.velocity = velocity;

    }

    void DoMove()
    {
        Vector2 velocity = rb.velocity;

        // Stop sliding movement when player moves left or right.
        velocity.x = 0;

        // Checks if the player is holding down the left movement keys.
        if (Input.GetKey("a") || Input.GetKey("left"))
        {
            velocity.x = -5f; // Moves the player by a velocity of -5 in the x axis.
        }

        // Checks if the player is holding down the right movement keys.
        if (Input.GetKey("d") || Input.GetKey("right"))
        {
            velocity.x = 5f; // Moves the player by a velocity of +5 in the x asis.
        }
        rb.velocity = velocity;
        
        
    }
}
