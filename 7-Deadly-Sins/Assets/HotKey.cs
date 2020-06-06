using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HotKey : MonoBehaviour
{
    private IUsable _usable;

    private Image _image;
    private Text _quantityText;


    // Start is called before the first frame update
    void Start()
    {
        _image = transform.Find("Image").GetComponent<Image>();
        _quantityText = transform.Find("QuantityText").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
            }
        }
    }

    public void SetUsable(IUsable usable)
    {
        
        _usable = usable;

        _image.sprite = usable.Image;

        Refresh();
    }

    public void UseUsable()
    {

        if (_usable != null)
        {
            _usable.Use();
        }

        HotKeyBar.instance.RefreshHotkeys();
    }
}
