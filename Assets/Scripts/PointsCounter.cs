using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointsCounter : MonoBehaviour
{
    public int currentPoints;
    private int enemyCost;
    private Text pointsCounterText;

    private void Awake()
    {
        pointsCounterText = GetComponent<Text>();    
    }

    public void PointsCount(int enemyCost)
    {
        currentPoints += enemyCost;
        pointsCounterText.text = currentPoints + "";
    }
}
