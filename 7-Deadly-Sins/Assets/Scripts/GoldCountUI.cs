using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldCountUI : MonoBehaviour
{

    Text displayText;
    // Start is called before the first frame update
    void Start()
    {
        GoldCounter.instance.onGoldChange += OnGoldChange;
        displayText = GetComponentInChildren<Text>();
        displayText.text = GoldCounter.instance.gold.ToString();
    }

    public void OnGoldChange(int currentGold)
    {
        displayText.text = currentGold.ToString();
    }
}
