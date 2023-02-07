using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Photon.Realtime;

public class Clickable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler {

    public GameObject real;

    Image image;
    CanvasGroup canvasGroup;
    RectTransform rectTransform;
    [HideInInspector] public Transform beforeParent;

    private void Awake() {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        image = GetComponent<Image>();
    }

    public void OnBeginDrag(PointerEventData eventData) {
        canvasGroup.blocksRaycasts = false;
        beforeParent = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData) {
        rectTransform.anchoredPosition += eventData.delta / Canvas_Inventory.Instance.canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData) {
        canvasGroup.blocksRaycasts = true;
        transform.SetParent(beforeParent);
        transform.SetAsFirstSibling();
        image.raycastTarget = true;
    }

    //우클릭
    public void OnPointerClick(PointerEventData eventData) {
        if(eventData.button == PointerEventData.InputButton.Left) {
            print("좌클릭 미구현");
        }
        if(eventData.button== PointerEventData.InputButton.Right) {
            transform.parent.GetComponent<Dropable>().RemoveItem(gameObject);
            real.GetComponent<Item>().Discard();
        }
    }
}
