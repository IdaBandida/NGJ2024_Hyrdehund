using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalArea : MonoBehaviour
{
    private ScoreManager scoreManagerScript;


    private void Start()
    {
        scoreManagerScript = FindObjectOfType<ScoreManager>();

        // Check if the ScoreManager script was found
        if (scoreManagerScript == null)
        {
            Debug.LogError("ScoreManager script not found in the scene!");
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Sheep"))
        {
            Debug.Log("sheep was collected");
            Destroy(collision.gameObject);

            if (scoreManagerScript != null)
            {
                Invoke("CountSheepAfterDelay", 0.1f);
            }
            else
            {
                Debug.LogWarning("ScoreManager script is null!");
            }

        }
    }

    private void CountSheepAfterDelay()
    {
        // Call CountSheep() after a slight delay to ensure the sheep is destroyed
        scoreManagerScript.CountSheep();
    }
}
