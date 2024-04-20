using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float runSpeed = 10;
    public float repelRadius = 5f; //radius around dog

    private Vector2 moveInput;
    public Rigidbody2D rb;
    public Transform dogTransform; // Reference to the dog's transform

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frames
    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal") * Time.deltaTime; //apply input to x axis
        moveInput.y = Input.GetAxisRaw("Vertical") *  Time.deltaTime; //apply input to y axis

        moveInput.Normalize();

        rb.velocity = moveInput * runSpeed; //apply speed to rigidbody

        CheckCollision();
    }

    void CheckCollision()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, repelRadius); //Get a list of all Colliders that fall within a circular area

        foreach (Collider2D collider in colliders)  //for each collider that fall within the radius
        {
            if (collider.CompareTag("Sheep")) //check if what we collide with is a sheep. If it is a sheep
            {
                {
                    Vector2 repelDirection = (collider.transform.position - transform.position).normalized; //calculate the direction and make it a unit vector by normalizing it
                    float distanceToDog = Vector2.Distance(collider.transform.position, dogTransform.position); //get distance from dog
                    //float distance = Vector2.Distance(transform.position, collider.transform.position); //get distance from dog. (dog position - sheep position)
                    //float repelStrength = Mathf.Lerp(repelForce, 0f, distance / radius); // Adjust repel strength based on distance
                    collider.GetComponent<Sheep>().ApplyRepulsion(repelDirection, distanceToDog); //call method on sheep script
                }
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Boundary"))
        {
            Debug.Log("collided with " + collision);
        }
    }
}
