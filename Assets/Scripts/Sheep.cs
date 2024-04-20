using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Sheep : MonoBehaviour
{
    public float maxRepelSpeed = 10f; //to control the min and max distance the sheep should run at
    public float minRepelSpeed = 2f;
    public float maxDistance = 5f; //what distance the sheep should move at the minimum speed.
    private GameObject[] sheeps;
    private List<Vector2> forces = new List<Vector2>();
    public SheepLogic avoidSheeps;
    public SheepLogic attractSheeps;

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
    }

    public void Update()
    {
        AttractSheeps();
        AvoidSheeps();
        var totalForce = Vector2.zero;
        foreach (var force in forces)
        {
            totalForce += force;
        }

        if (totalForce != Vector2.zero)
        {
            print("moving");
            transform.position += (Vector3)totalForce * Time.deltaTime;
            forces.Clear();
        }
    }

    private void AttractSheeps()
    {
        foreach(var sheep in GetClosestItems(sheeps, 3))
        {
            if (sheep == gameObject) continue; //skip the current sheep (this sheep)

            var force = attractSheeps.GetAttraction(sheep.transform.position);
            if (force != Vector2.zero)
            {
                forces.Add(force);
            }
        }
    }

    private void AvoidSheeps()
    {
        foreach (var sheep in GetClosestItems(sheeps, 3))
        {
            if (sheep == gameObject) continue; //skip the current sheep (this sheep)

            var force = avoidSheeps.GetAttraction(sheep.transform.position);
            if (force != Vector2.zero)
            {
                print(force);
                forces.Add(force);
            }
        }
    }

    private GameObject[] GetClosestItems(GameObject[] objs, int itemCount)
    {
        return sheeps.OrderBy(x => (x.transform.position - transform.position).magnitude)
            .Take(itemCount)
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
