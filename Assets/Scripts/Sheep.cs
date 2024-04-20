using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheep : MonoBehaviour
{
    public float repelSpeed = 5f;

    public void ApplyRepulsion(Vector2 repulsionDirection) //called from player controller
    {
        Vector3 newPosition = transform.position + (Vector3)repulsionDirection * repelSpeed * Time.deltaTime; //convert vector 2 to vector 3. new position has speed applied to it
        transform.position = newPosition;
    }
}
