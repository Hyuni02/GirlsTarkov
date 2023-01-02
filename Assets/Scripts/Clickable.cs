using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Clickable : MonoBehaviour, IPointerClickHandler {

    public ItemInfo info;

    public void OnPointerClick(PointerEventData eventData) {
        if(eventData.button == PointerEventData.InputButton.Right) {
            if (GetComponent<Image>().sprite != null) {
                UI_Inventory.instance.OpenRClickMenu(info);
            }
            else {
                Debug.Log("No Info in " + name);
            }
        }

        if(eventData.button == PointerEventData.InputButton.Left) {
            UI_Inventory.instance.CloseRClickMenu();
        }
    }
}
