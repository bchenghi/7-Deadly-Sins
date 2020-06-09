using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
public class TabButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public TabGroup tabGroup;

    public Image backGround;

    void Awake() {
        backGround = GetComponent<Image>();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    public void OnPointerEnter(PointerEventData pointEventData) {
        tabGroup.OnTabEnter(this);
    }

    public void OnPointerExit(PointerEventData pointerEventData) {
        tabGroup.OnTabExit(this);
    }

    public void OnPointerClick(PointerEventData pointerEventData) {
        tabGroup.OnTabSelected(this);

    }
}
