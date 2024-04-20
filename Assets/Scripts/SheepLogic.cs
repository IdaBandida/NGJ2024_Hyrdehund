using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepLogic : MonoBehaviour
{
    public float minRange = 0f;
    public float maxRange = 10f;
    public float minForce = 0f;
    public float maxForce = 10f;

    public Vector2 GetAttraction(Vector2 other)
    {
        var delta = other - (Vector2)transform.position;
        if (delta.magnitude > maxRange || delta.magnitude < minRange)
        {
            return Vector2.zero;
        }

        var force = Mathf.Lerp(maxForce, minForce, (delta.magnitude - minRange) / maxRange);
        //print(force);
        return delta.normalized * force;
    }
}
