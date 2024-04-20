using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    private int sheepCount;
    public TextMeshProUGUI scoreText;

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

        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        scoreText.text = sheepCount.ToString();
    }
}
