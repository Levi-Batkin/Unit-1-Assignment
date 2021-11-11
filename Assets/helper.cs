using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Globals;
using System.Threading;
public class Helper : MonoBehaviour
{
    public bool isGrounded;
    public GameObject projectile;
    public GameObject obj;
    public float speed = 16;
    public float gravityScale;
    public ParticleSystem explosion;
    
    public static void DoRayCollisionCheck(GameObject player)
    {
        float rayLength = 1.0f;
        // Creates a line to check if the player is touching certain objects.
        RaycastHit2D hit = Physics2D.Raycast(player.transform.position, Vector2.down, rayLength);

        Color hitColor = Color.white;


        if (hit.collider != null)
        {

            if (hit.collider.tag == "enemy")
            {
                print("Player has collided with Enemy");
                hitColor = Color.red;
            }

            if (hit.collider.tag == "ground")
            {
                print("Player has collided with ground");
                hitColor = Color.green;
            }
        }
        // Draws a raycast line.
        Debug.DrawRay(player.transform.position, Vector2.down * rayLength, hitColor);

    }

    public static void FlipObject(GameObject obj, bool faceLeft )
    {
        if (faceLeft == true)
        {
            // Rotates the object to face left.
            obj.transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            // Rotates the object to face right.
            obj.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }
    public static void Bullet(GameObject arrow, float xpos, float ypos, float xvel, float yvel)
    {
        Rigidbody2D rb;
        bool direction = false;
        if (xvel < 0)
        {
            direction = true;
        }
        GameObject instance = Instantiate(arrow, new Vector3(xpos, ypos, 0), Quaternion.Euler(0, direction == true ? 180 : 0, 0));
        rb = instance.GetComponent<Rigidbody2D>();
        rb.gravityScale = 0.25f;
        rb.velocity = new Vector3(xvel, yvel, 0);
        FlipObject(instance, direction);
        Destroy(instance, 2.5f);
    }
}