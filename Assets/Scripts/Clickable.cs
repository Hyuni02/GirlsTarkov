using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Clickable : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IEndDragHandler, IDragHandler {

    public ItemInfo info;
    RectTransform rectTransform;
    CanvasGroup canvasGroup;
    [HideInInspector]
    public Transform preParent;

    private void Start() {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData) {
        canvasGroup.blocksRaycasts = false;

        preParent = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData) {
        rectTransform.anchoredPosition += eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData) {
        
        if (transform.parent == GameObject.Find("UI_Inventory")) 
        {
            transform.SetParent(preParent);
        }
        canvasGroup.blocksRaycasts = true;
    }

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
