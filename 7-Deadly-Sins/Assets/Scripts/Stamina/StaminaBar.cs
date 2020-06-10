using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey;
public class StaminaBar : MonoBehaviour
{
    private Stamina stamina;
    private float fillMaskWidth;
    //private Image barImage;
    private RawImage fillrawImage;
    private RectTransform fillMaskRectTransform;
    private RectTransform edgeRectTransform;
    public PlayerController player;

    private void Start()
    {
        player = PlayerManager.instance.player.GetComponent<PlayerController>();
        stamina = PlayerManager.instance.player.GetComponent<Stamina>();
    }

    private void Awake()
    {
        
        //barImage = transform.Find("Fill").GetComponent<Image>();
        fillMaskRectTransform = transform.Find("FillMask").GetComponent<RectTransform>();
        fillrawImage = transform.Find("FillMask").Find("Fill").GetComponent<RawImage>();
        fillMaskWidth = fillMaskRectTransform.sizeDelta.x;
        edgeRectTransform = transform.Find("Edge").GetComponent<RectTransform>();
        

        //CMDebug.ButtonUI(new Vector2(0, -50), "Spend Stamina", () => { stamina.UseStamina(30); });

    }

    private void Update()
    {
        
        stamina.Update();

        //barImage.fillAmount = stamina.GetStaminaNormalized();
        Rect uvRect = fillrawImage.uvRect;
        uvRect.x -= 0.5f * Time.deltaTime;
        fillrawImage.uvRect = uvRect;

        Vector2 fillMaskSizeDelta = fillMaskRectTransform.sizeDelta;
        fillMaskSizeDelta.x = stamina.GetStaminaNormalized() * fillMaskWidth;
        fillMaskRectTransform.sizeDelta = fillMaskSizeDelta;

        edgeRectTransform.anchoredPosition = new Vector2(stamina.GetStaminaNormalized() * fillMaskWidth, 0);
        edgeRectTransform.gameObject.SetActive(stamina.GetStaminaNormalized() < 1f);
    }  



}


