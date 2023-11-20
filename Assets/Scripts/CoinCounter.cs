using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinCounter : MonoBehaviour
{
    private Text coinCounterText;
    public int coinCount;

    private void Awake()
    {
        coinCounterText = GetComponent<Text>();
    }
    void Update()
    {
        UpdateText();
    }
    private void UpdateText()
    {
        coinCounterText.text = coinCount + "";
    }
}
