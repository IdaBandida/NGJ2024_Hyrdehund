using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;

public class SheepAI : MonoBehaviour
{
    public float maxRotationSpeed = 180f;
    public float maxAcceleration = 2f;
    public SheepAI[] otherSheeps;
    private Vector3 desiredMovement;
    public float speed = 5;
    public LineDisplay rayLine;
    public LineDisplay normalLine;
    public LineDisplay forceLine;
    public ForceRule obstacleRule;

    // Start is called before the first frame update
    void Start()
    {
        otherSheeps = FindObjectsOfType<SheepAI>();
    }


    void FixedUpdate()
    {
        var forces = new Vector2[]
        {
            GetForceForObstacles()
        };
        UpdateMovement(forces);
    }

    private void UpdateMovement(Vector2[] forces)
    {
        var totalForce = forces.Aggregate((x, acc) => acc+x);
        var deltaDegrees = Vector3.SignedAngle(transform.up, totalForce, Vector3.forward) * Time.fixedDeltaTime * maxRotationSpeed;
        transform.RotateAround(transform.position, Vector3.forward, deltaDegrees);
        //var newVector = Vector3.RotateTowards(transform.right * speed, totalForce, Mathf.Deg2Rad * maxRotationSpeed * Time.fixedDeltaTime, Time.fixedDeltaTime * maxAcceleration);
        //transform.rotation = Quaternion.LookRotation(newVector);
        print("Speed = " + transform.up * Time.fixedDeltaTime * speed);
        transform.Translate(Vector3.up * Time.fixedDeltaTime * speed);
    }

    private Vector2 GetForceForObstacles()
    {
        var ray = Physics2D.Raycast(transform.position, transform.up, 10f);
        DisplayRay(ray);

        if (ray.collider != null)
        {
            var force = ray.normal * obstacleRule.GetForce(ray.distance);
            forceLine.DisplayRelative(force);
            return force;
        }
        else
        {
            forceLine.Hide();
            return Vector2.zero;
        }
    }

    private void DisplayRay(RaycastHit2D ray)
    {
        var endRayPoint = ray.collider != null ?
            (Vector3)ray.point :
            transform.position + transform.up * 10f;
        rayLine.Display(transform.position, endRayPoint);

        normalLine.Display(endRayPoint, endRayPoint + (Vector3)ray.normal * 3);
    }
}

[System.Serializable]
public class ForceRule
{
    public AnimationCurve curve;
    public float distanceFactor = 1f;
    public float forceFactor = 1f;

    public float GetForce(float distance)
    {
        return curve.Evaluate(distance / distanceFactor) * forceFactor;
    }
}