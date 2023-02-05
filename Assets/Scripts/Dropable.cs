using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Dropable : MonoBehaviour, IDropHandler {
    public void OnDrop(PointerEventData eventData) {
        if(eventData.pointerDrag != null) {
            eventData.pointerDrag.transform.SetParent(transform);
            Transform from = eventData.pointerDrag.GetComponent<Clickable>().preParent;
            Transform to = RoomManager.instance.LocalPlayer.GetComponent<Inventory>().lastInteract;
            eventData.pointerDrag.GetComponent<Clickable>().info.MoveItem(from, to);
        }
    }
}
