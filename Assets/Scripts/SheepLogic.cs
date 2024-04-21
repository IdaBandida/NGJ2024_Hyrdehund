using UnityEngine;

public class SheepLogic
{
    public float minRange = 0f;
    public float maxRange = 10f;
    public float minForce = 0f;
    public float maxForce = 10f;

    public Vector2 GetAttraction(Vector2 pos, Vector2 other)
    {
        var delta = other - pos;
        if (delta.magnitude > maxRange || delta.magnitude < minRange)
        {
            return Vector2.zero;
        }

        var force = Mathf.Lerp(maxForce, minForce, (delta.magnitude - minRange) / maxRange);
        return delta.normalized * force;
    }
}
