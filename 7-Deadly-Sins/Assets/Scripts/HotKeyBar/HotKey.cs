using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HotKey : MonoBehaviour
{

    public GameObject coolDownImage;
    public GameObject removeButton;
    [HideInInspector]
    public IUsable _usable;
    private Image _image;
    private Text _quantityText;
    public bool isUsed = false;
    public bool isFilled = false;
    bool HotKeyDisabled;


    void Awake() {
        _image = transform.Find("Image").GetComponent<Image>();
        _quantityText = transform.Find("QuantityText").GetComponent<Text>();
        _image.enabled = false;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isFilled && !isUsed)
        {
            setCooldownImage();
        }

        if (isUsed)
        {
            SetCooldown();
        }
    }


    // For updating count in hotkey or removing from hotkey if used up in inventory
    public void Refresh()
    {
        if (_usable is Item)
        {
            _quantityText.text = Inventory.instance.getValue((Item)_usable).ToString();
            if (Inventory.instance.getValue((Item)_usable) == 0)
            {
                _image.sprite = null;
                _image.enabled = false;
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
        MonoBehaviour mb = _usable as MonoBehaviour;
        if (mb != null)
        {
            mb.transform.GetComponent<DragDropSkill>().enabled = true;
        }
        _image.sprite = null;
        _image.enabled = false;
        removeButton.GetComponent<Button>().interactable = false;
        removeButton.SetActive(false);
        _quantityText.text = null;
        _usable = null;
        isFilled = false;
        HotKeyBar.instance.ClearIUsableInMemory(this);
    }

    public void SetUsable(IUsable usable)
    {
        _usable = usable;
        _image.enabled = true;
        _image.sprite = usable.Image;
        isFilled = true;
        removeButton.GetComponent<Button>().interactable = true;
        removeButton.SetActive(true);
        HotKeyBar.instance.AddIUsableInMemory(this);
        Refresh();
    }

    public void UseUsable()
    {
        if (!HotKeyDisabled)
        {
            if (_usable != null)
            {
                Debug.Log("using usable: " + _usable);
                _usable.Use();
                isUsed = true;
            }

            //HotKeyBar.instance.RefreshHotkeys();
        }
    }

    public void SetCooldown()
    {
        


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

    private void setCooldownImage()
    {
        if (_usable is Skill)
        {
            coolDownImage.GetComponent<Image>().fillAmount = 1;
        }
    }

    public void DisableHotKey()
    {
        HotKeyDisabled = true;
    }

    public void EnableHotKey()
    {
        HotKeyDisabled = false;
    }

    public bool CheckWhetherHotKeyisCoolingDown()
    {
        if ((_usable as Skill).isCoolingDown)
        {
            return true;
        } else
        {
            return false;
        }
    }

    
}
