using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Inventory : MonoBehaviour {
    protected Slot[] SlotList;
    private float TargetAlpha = 1;

    private float Smoothing = 1;
    private CanvasGroup CanvasGroup;

    private void Start () {
        SlotList = GetComponentsInChildren<Slot>();
        CanvasGroup = GetComponent<CanvasGroup> ();
    }

    public bool StoreItem (int id) {
        Item item = InventoryManager.Instance.GetItemById (id);
        return StoreItem (item);
    }

    private bool StoreItem (Item item) {
        if (item == null) {
            Debug.LogError ("没有对应的item");
            return false;
        }
        if (item.Capacity == 1) {
            Slot empty = FindEmptySlot ();
            if (empty == null) {
                return false;
            }
            empty.StoreItem (item);

        } else {
            Slot sameSlot = FindSameIdSlot (item);
            if (sameSlot == null) {
                Slot empty = FindEmptySlot ();
                if (empty == null) {
                    return false;
                }
                empty.StoreItem (item);
            } else {
                sameSlot.StoreItem (item);
            }
        }
        return true;
    }

    private Slot FindSameIdSlot (Item item) {
        foreach (Slot slot in SlotList) {
            if (slot.GetItemId () == item.ID) {
                return slot;
            }
        }
        return null;
    }

    private Slot FindEmptySlot () {
        foreach (Slot slot in SlotList) {
            if (slot.transform.childCount == 0) {
                return slot;
            }
        }
        return null;
    }


}