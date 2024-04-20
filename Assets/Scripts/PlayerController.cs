using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10;
    private Vector2 moveInput;
    public Rigidbody2D rb;

    public float radius = 5f; //radius around dog
    public float repelForce = 5f; //force applied to sheep

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

        rb.velocity = moveInput * speed; //apply speed to rigidbody

        CheckCollision();
    }

    void CheckCollision()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius); //Get a list of all Colliders that fall within a circular area

        foreach (Collider2D collider in colliders)  //for each collider that fall within the radius
        {
            if (collider.CompareTag("Sheep")) //check if what we collide with is a sheep. If it is a sheep
            {
                {
                    Vector2 repelDirection = (collider.transform.position - transform.position).normalized; //calculate the direction and make it a unit vector by normalizing it
                    float distance = Vector2.Distance(transform.position, collider.transform.position); //get distance from dog. (dog position - sheep position)
                    float repelStrength = Mathf.Lerp(repelForce, 0f, distance / radius); // Adjust repel strength based on distance
                    collider.GetComponent<Sheep>().ApplyRepulsion(repelDirection * repelStrength); //call method on sheep script
                }
            }
        }
    }
}
