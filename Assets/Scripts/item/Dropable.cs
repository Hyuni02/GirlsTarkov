using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Dropable : MonoBehaviour, IDropHandler {

    public Transform owner;

    public virtual void OnDrop(PointerEventData eventData) {
        Transform image = eventData.pointerDrag.transform;
        Clickable item = image.GetComponent<Clickable>();
        item.beforeParent = transform;

        //item.real.transform.SetParent(owner);
    }

    public void RemoveItem(GameObject item) {
        Destroy(item.gameObject);
    }
}
