using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabGroup : MonoBehaviour
{
    [SerializeField]
    ShopUI shopUI;

    [SerializeField]
    TabButton[] tabButtons;
    public Sprite tabIdle;
    public Sprite tabHover;
    public Sprite tabActive;

    public TabButton selectedTab;

    public void Start() {
        tabButtons = GetComponentsInChildren<TabButton>();
        OnTabSelected(tabButtons[0]);
    }

    public void OnTabEnter(TabButton tabButton){
        ResetTabs();
        if (selectedTab == null || tabButton != selectedTab)
            tabButton.backGround.sprite = tabHover;
    }

    public void OnTabExit(TabButton tabButton) {
        ResetTabs();
    }

    public void OnTabSelected(TabButton tabButton) {
        selectedTab = tabButton;
        ResetTabs();
        tabButton.backGround.sprite = tabActive;
        int index = GetTabIndex(tabButton);
        UpdateShopUI(index);
    }

    public void ResetTabs() {
        foreach(TabButton button in tabButtons) {
            if (button == selectedTab && selectedTab != null){
                continue;
            }

            button.backGround.sprite = tabIdle;
        }
    }

    int GetTabIndex(TabButton button) {
        int index = -1;
        for (int i = 0; i < tabButtons.Length ; i++) {
            if (tabButtons[i] == button) {
                index = i;
                break;
            }
        }
        return index;
    }

    void UpdateShopUI(int tabIndex) {
        if (tabIndex == 0) {
            shopUI.UpdateUI();
        } else if (tabIndex == 1) {
            shopUI.UpdateUIEquipment();
        } else if (tabIndex == 2) {
            shopUI.UpdateUIConsumable();
        } else if (tabIndex == 3) {
            shopUI.UpdateUIOthers();
        } else {
            Debug.LogError("Unknown tab index");
            UpdateShopUI(0);
        }
    }
}
