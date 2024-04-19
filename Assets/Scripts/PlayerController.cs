using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10;
    private Vector2 moveInput;
    public Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal") * Time.deltaTime; //apply input to x axis
        moveInput.y = Input.GetAxisRaw("Vertical") *  Time.deltaTime; //apply input to y axis

        moveInput.Normalize();

        rb.velocity = moveInput * speed; //apply speed to rigidbody
     
    }
}
