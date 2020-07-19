using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;

public class SellButton : MonoBehaviour
{
    public GameObject sellQuantity;

    public void OpenQuanityText()
    {
        sellQuantity.SetActive(true);
    }
}
