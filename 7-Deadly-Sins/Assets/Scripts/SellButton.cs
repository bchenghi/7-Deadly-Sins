
using UnityEngine;

public class SellButton : MonoBehaviour
{
    public GameObject sellQuantity;

    public void OpenQuanityText()
    {
        HotKeyBar.instance.DisableAllMaster();
        sellQuantity.SetActive(true);
    }
}
