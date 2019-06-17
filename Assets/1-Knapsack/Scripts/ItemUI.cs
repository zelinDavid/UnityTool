using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour {
    #region  Data
    public Item Item { get; private set; }
    public int Amount { get; private set; }
    #endregion

    private Image ItemImage {
        get {
            return GetComponent<Image> ();
        }
    }

    private Text AmoutText {
        get {
            return GetComponentInChildren<Text> ();
        }
    }

    private float targetScale = 1f;

    private Vector3 animationScale = new Vector3 (1.4f, 1.4f, 1.4f);

    private float smoothing = 4;

    private void Update () {
        if (transform.localScale.x != targetScale) {
            float scale = Mathf.Lerp (transform.localScale.x, targetScale, smoothing * Time.deltaTime);
            transform.localScale = new Vector3 (scale, scale, scale);
            if (Mathf.Abs (transform.localScale.x - targetScale) < 0.2f) {
                transform.localScale = new Vector3 (targetScale, targetScale, targetScale);
            }
        }
    }

    public void SetItem (Item item, int account = 1) {
        transform.localScale = animationScale;
        this.Item = item;
        this.Amount = account;

        ItemImage.sprite = Resources.Load<Sprite>(item.Sprite);
        if (Item.Capacity > 1) {
            AmoutText.text = account.ToString ();
        } else {
            AmoutText.text = "";
        }
    }

    public void AddAmount (int amount = 1) {
        if (Item.Capacity > 1) {
            Amount += amount;
            AmoutText.text = Amount.ToString ();
        } else {
            AmoutText.text = "";
        }
    }

    public void ReduceAmount (int amount = 1) {
        if (Item.Capacity > 1) {
            Amount -= amount;
            AmoutText.text = Amount > 1 ? Amount.ToString () : "";
        } else {
            Amount = 0;
            AmoutText.text = "";
        }
    }
    public void SetAmount (int amount) {
        transform.localScale = animationScale;
        this.Amount = amount;
        //update ui 
        if (Item.Capacity > 1)
            AmoutText.text = Amount.ToString ();
        else
            AmoutText.text = "";
    }

    //当前物品 跟 另一个物品 交换显示
    public void Exchange (ItemUI itemUI) {
        Item item = this.Item;
        int amout = this.Amount;

        this.SetItem (itemUI.Item, itemUI.Amount);
        itemUI.SetItem (item, amout);
    }

    public void Show () {
        gameObject.SetActive (true);
    }
    public void Hide () {
        
        gameObject.SetActive (false);
    }

    public void SetLocalPosition (Vector3 position) {
        transform.localPosition = position;
    }

}