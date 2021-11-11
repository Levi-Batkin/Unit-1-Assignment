 using UnityEngine;
 using System.Collections;
 
 public class Enemy : MonoBehaviour {
 
     public Transform target;
     private GameObject enemy;
     private GameObject player;
     private float range;
     public float speed;
 
 
     // Use this for initialization
     void Start () {
         //Searches for tags assigned to the enemy and player to allow the enemy to follow the player.
         enemy = GameObject.FindGameObjectWithTag ("enemy");
         player = GameObject.FindGameObjectWithTag ("player");
     }
     
     // Update is called once per frame
     void Update () {
        Vector2 velocity = new Vector2((transform.position.x - player.transform.position.x) * speed, (transform.position.y - player.transform.position.y) * speed);
        GetComponent<Rigidbody2D>().velocity = -velocity;

     }
 }