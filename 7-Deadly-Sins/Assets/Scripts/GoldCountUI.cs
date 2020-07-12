using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldCountUI : MonoBehaviour
{

    Text displayText;

    [SerializeField]
    GameObject goldUIChange;
    RectTransform goldUIChangeRectTransform;
    
    // Start is called before the first frame update
    void Start()
    {
        GoldCounter.instance.onGoldChange += OnGoldChange;
        displayText = GetComponentInChildren<Text>();
        displayText.text = GoldCounter.instance.gold.ToString();
        goldUIChangeRectTransform = goldUIChange.GetComponent<RectTransform>();
    }

    public void OnGoldChange(int previousGold, int currentGold)
    {
        GameObject newGoldUpdate = Instantiate(goldUIChange);
        newGoldUpdate.transform.SetParent(this.transform);
        RectTransform newGoldUpdateRectTransform = newGoldUpdate.GetComponent<RectTransform>();
        newGoldUpdateRectTransform.localPosition = goldUIChangeRectTransform.localPosition;
        newGoldUpdateRectTransform.localRotation = goldUIChangeRectTransform.localRotation;
        newGoldUpdateRectTransform.offsetMax = goldUIChangeRectTransform.offsetMax;
        newGoldUpdateRectTransform.offsetMin = goldUIChangeRectTransform.offsetMin;
        newGoldUpdate.SetActive(true);
        newGoldUpdate.GetComponent<GoldUIChange>().DisplayGoldChange(currentGold - previousGold);

        displayText.text = currentGold.ToString();
    }
}
