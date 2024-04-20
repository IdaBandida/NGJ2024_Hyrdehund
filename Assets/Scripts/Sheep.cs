using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheep : MonoBehaviour
{
    public float maxRepelSpeed = 10f; //to control the min and max distance the sheep should run at
    public float minRepelSpeed = 2f;
    public float maxDistance = 5f; //what distance the sheep should move at the minimum speed.

    public void ApplyRepulsion(Vector2 repulsionDirection, float distanceToDog) //called from player controller
    {
        float runSpeed = Mathf.Lerp(maxRepelSpeed, minRepelSpeed, distanceToDog / maxDistance);
        Vector3 newPosition = transform.position + (Vector3)repulsionDirection * runSpeed * Time.deltaTime; //convert vector 2 to vector 3. new position has speed applied to it
        transform.position = newPosition; //update its position accordingly.
    }
}
