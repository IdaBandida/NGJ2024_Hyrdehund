using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int sheepCount;

    private void Start()
    {
        CountSheep();
    }

    public void CountSheep()
    {
        GameObject[] sheep = GameObject.FindGameObjectsWithTag("Sheep"); // Find all game objects with the "Sheep" tag
        sheepCount = sheep.Length;// Count the number of sheep

        if (sheep.Length == 0)
        {
            Debug.Log("you won!");
        }

        else
        {
            Debug.Log(sheepCount);

        }

    }
}
