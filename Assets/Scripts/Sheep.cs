using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Sheep : MonoBehaviour
{
    public float maxRepelSpeed = 10f; //to control the min and max distance the sheep should run at
    public float minRepelSpeed = 2f;
    public float maxDistance = 5f; //what distance the sheep should move at the minimum speed.
    private Animator animator;
    private Rigidbody2D rb;
    private GameObject[] sheeps;
    private GameObject[] obstacles;
    private List<Vector2> forces = new List<Vector2>();
    private SheepLogic avoidSheeps = new SheepLogic
    {
        minRange = 0.5f,
        minForce = -15f,
        maxRange = 1f,
        maxForce = 0f
    };
    private SheepLogic attractSheeps = new SheepLogic
    {
        minRange = 1,
        minForce = 0.3f,
        maxRange = 6f,
        maxForce = 0.6f
    };
    private SheepLogic avoidObstacles = new SheepLogic
    {
        minRange = 0,
        minForce = -0.7f,
        maxRange = 4f,
        maxForce = 0f
    };

    public void ApplyRepulsion(Vector2 repulsionDirection, float distanceToDog) //called from player controller
    {
        float runSpeed = Mathf.Lerp(maxRepelSpeed, minRepelSpeed, distanceToDog / maxDistance);
        forces.Add(repulsionDirection * runSpeed);
        //Vector3 newPosition = transform.position + (Vector3)repulsionDirection * runSpeed * Time.deltaTime; //convert vector 2 to vector 3. new position has speed applied to it
        //transform.position = newPosition; //update its position accordingly.
    }

    public void Start()
    {
        sheeps = GameObject.FindGameObjectsWithTag("Sheep");
        obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
    }

    public void Update()
    {
        rb.velocity *= 0.9f;
        AttractSheeps();
        AvoidSheeps();
        AvoidObstacles();
        var totalForce = Vector2.zero;
        foreach (var force in forces)
        {
            totalForce += force;
        }

        if (totalForce.magnitude > 0.1f)
        {
            transform.position += (Vector3)totalForce * Time.deltaTime;
            var dir = totalForce.normalized;
            animator.SetBool("right", dir.x > 0.1f);
            animator.SetBool("left", dir.x < -0.1f);
            animator.SetBool("up", dir.y > 0.1f);
            animator.SetBool("down", dir.y < -0.1f);
            forces.Clear();
        }
        else
        {
            animator.SetBool("right", false);
            animator.SetBool("left", false);
            animator.SetBool("up", false);
            animator.SetBool("down", false);
        }

        sheeps = GameObject.FindGameObjectsWithTag("Sheep");

    }

    private void AttractSheeps()
    {
        foreach(var sheep in GetClosestItems(sheeps, 3, attractSheeps.maxRange))
        {
            if (sheep == gameObject) continue; //skip the current sheep (this sheep)

            var force = attractSheeps.GetAttraction(transform.position, sheep.transform.position);
            if (force != Vector2.zero)
            {
                forces.Add(force);
            }
        }
    }

    private void AvoidSheeps()
    {
        foreach (var sheep in GetClosestItems(sheeps, 5, avoidSheeps.maxRange))
        {
            if (sheep == gameObject) continue; //skip the current sheep (this sheep)

            var force = avoidSheeps.GetAttraction(transform.position, sheep.transform.position);
            if (force != Vector2.zero)
            {
                forces.Add(force);
            }
        }
    }

    private void AvoidObstacles()
    {
        foreach (var obstacle in GetClosestItems(obstacles, 5, avoidObstacles.maxRange))
        {
            var force = avoidObstacles.GetAttraction(transform.position, obstacle.transform.position);
            if (force != Vector2.zero)
            {
                forces.Add(force);
            }
        }
    }

    private GameObject[] GetClosestItems(GameObject[] objs, int maxCount, float maxRange)
    {
        return objs.Where(x => x != null)
            .Where(x => (x.transform.position - transform.position).magnitude <= maxRange)
            .OrderBy(x => (x.transform.position - transform.position).magnitude)
            .Take(maxCount)
            .ToArray();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Boundary"))
        {

            Debug.Log("sheep collided with " + collision);
        }
    }
}
