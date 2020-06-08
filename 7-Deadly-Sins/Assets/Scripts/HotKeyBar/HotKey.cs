using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HotKey : MonoBehaviour
{

    public GameObject coolDownImage;
    public GameObject removeButton;
    private IUsable _usable;
    private Image _image;
    private Text _quantityText;
    public bool isUsed = false;
    public bool isFilled = false;


    // Start is called before the first frame update
    void Start()
    {
        _image = transform.Find("Image").GetComponent<Image>();
        _quantityText = transform.Find("QuantityText").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isFilled)
        SetCooldown();
    }

    public void Refresh()
    {
        if (_usable is Item)
        {
            _quantityText.text = Inventory.instance.getValue((Item)_usable).ToString();
            if (Inventory.instance.getValue((Item)_usable) == -1)
            {
                _image.sprite = null;
                _quantityText.text = null;
                _usable = null;
                isFilled = false;
                removeButton.GetComponent<Button>().interactable = false;
                removeButton.SetActive(false);


            }
        }
    }

    public void RemoveFromHotKey()
    {
        _image.sprite = null;
        removeButton.GetComponent<Button>().interactable = false;
        removeButton.SetActive(false);
        _quantityText.text = null;
        _usable = null;
        isFilled = false;
    }

    public void SetUsable(IUsable usable)
    {
        
        _usable = usable;

        _image.sprite = usable.Image;
        isFilled = true;
        removeButton.GetComponent<Button>().interactable = true;
        removeButton.SetActive(true);
        Refresh();
    }

    public void UseUsable()
    {

        if (_usable != null)
        {
            _usable.Use();
            isUsed = true;
        }

        HotKeyBar.instance.RefreshHotkeys();
    }

    public void SetCooldown()
    {
        if (_usable is Skill && isUsed == false)
        {
            isUsed = true;
            coolDownImage.GetComponent<Image>().fillAmount = 1;
        }


        if (_usable is Skill)
        {
            if ((_usable as Skill).isCoolingDown)
            {
                coolDownImage.SetActive(true);
                coolDownImage.GetComponent<Image>().fillAmount -= 1 / (_usable as Skill).CooldownTime * Time.deltaTime;
                if (coolDownImage.GetComponent<Image>().fillAmount <= 0)
                {
                    coolDownImage.GetComponent<Image>().fillAmount = 0;
                    isUsed = false;
                    coolDownImage.SetActive(false);
                }
            }
        }
        
        
    }
}
