 using System.Collections.Generic;
 using System.Collections;
 using System;
 using UnityEngine;

 public class InventoryManager : MonoBehaviour {
     #region  单例模式
     static public InventoryManager Instance {
         get {
             if (_shareInstance == null) {
                 _shareInstance = GameObject.Find ("InventoryManager").GetComponent<InventoryManager> ();
             }
             return _shareInstance;
         }
     }
     static private InventoryManager _shareInstance;
     #endregion

     /// <summary>
     ///  物品信息的列表（集合）
     /// </summary>
     private List<Item> itemList;

     #region ToolTip
     private ToolTip toolTip;

     private bool isToolTipShow = false;

     private Vector2 toolTipPosionOffset = new Vector2 (10, -10);
     #endregion

     #region PickedItem
     private bool isPickedItem = false;

     public bool IsPickedItem {
         get {
             return isPickedItem;
         }
     }

     private ItemUI pickedItem; //鼠标选中的物体

     public ItemUI PickedItem {
         get {
             return pickedItem;
         }
     }

     #endregion

     private Canvas canvas;

     private void Awake () {
         ParseItemJson ();
     }

     void Start () {
         toolTip = GameObject.Find ("Canvas/ToolTip").GetComponent<ToolTip> ();
         canvas = GameObject.Find ("Canvas").GetComponent<Canvas> ();

         pickedItem = GameObject.Find ("PickedItem").GetComponent<ItemUI> ();
         pickedItem.Hide ();
     }

     private void Update () {
         if (isPickedItem) {
             //如果我们捡起了物体,我们就要让物品跟随鼠标
             Vector2 position;
             RectTransformUtility.ScreenPointToLocalPointInRectangle (canvas.transform as RectTransform, Input.mousePosition, null, out position);
             pickedItem.SetLocalPosition (position);
         } else if (isToolTipShow) {
             Vector2 position;
             RectTransformUtility.ScreenPointToLocalPointInRectangle (canvas.transform as RectTransform, Input.mousePosition, null, out position);
             toolTip.SetLocation (position + toolTipPosionOffset);
         }

         //物品丢弃的处理

         if (isPickedItem && Input.GetMouseButtonDown (0) && UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject () == false) {

             isPickedItem = false;
             PickedItem.Hide ();
         }

     }

     private void ParseItemJson () {
         itemList = new List<Item> ();
         //文本为在Unity里面是 TextAsset类型
         TextAsset itemText = Resources.Load<TextAsset> ("Items");
         string itemsJson = itemText.text; //物品信息的Json格式
         JSONObject j = new JSONObject (itemsJson);
         foreach (JSONObject temp in j.list) {
             string typeStr = temp["type"].str;
             Item.ItemType type = (Item.ItemType) System.Enum.Parse (typeof (Item.ItemType), typeStr);

             //下面的事解析这个对象里面的公有属性
             int id = (int) (temp["id"].n);
             string name = temp["name"].str;
             Item.ItemQuality quality = (Item.ItemQuality) System.Enum.Parse (typeof (Item.ItemQuality), temp["quality"].str);
             string description = temp["description"].str;
             int capacity = (int) (temp["capacity"].n);
             int buyPrice = (int) (temp["buyPrice"].n);
             int sellPrice = (int) (temp["sellPrice"].n);
             string sprite = temp["sprite"].str;

             Item item = null;
             switch (type) {
                 case Item.ItemType.Consumable:
                     int hp = (int) (temp["hp"].n);
                     int mp = (int) (temp["mp"].n);
                     item = new Consumable (id, name, type, quality, description, capacity, buyPrice, sellPrice, sprite, hp, mp);
                     break;
                 case Item.ItemType.Equipment:
                     //
                     int strength = (int) temp["strength"].n;
                     int intellect = (int) temp["intellect"].n;
                     int agility = (int) temp["agility"].n;
                     int stamina = (int) temp["stamina"].n;
                     Equipment.EquipmentType equipType = (Equipment.EquipmentType) System.Enum.Parse (typeof (Equipment.EquipmentType), temp["equipType"].str);
                     item = new Equipment (id, name, type, quality, description, capacity, buyPrice, sellPrice, sprite, strength, intellect, agility, stamina, equipType);
                     break;
                 case Item.ItemType.Weapon:
                     //
                     int damage = (int) temp["damage"].n;
                     Weapon.WeaponType wpType = (Weapon.WeaponType) System.Enum.Parse (typeof (Weapon.WeaponType), temp["weaponType"].str);
                     item = new Weapon (id, name, type, quality, description, capacity, buyPrice, sellPrice, sprite, damage, wpType);
                     break;
                 case Item.ItemType.Material:
                     //
                     item = new Material (id, name, type, quality, description, capacity, buyPrice, sellPrice, sprite);
                     break;
             }
             itemList.Add (item);
         }
     }

     //捡起物品槽指定数量的物品
     public void PickupItem (Item item, int amount) {
         pickedItem.SetItem (item, amount);
         isPickedItem = true;
         pickedItem.Show ();
         Vector2 localposition;
         RectTransformUtility.ScreenPointToLocalPointInRectangle (canvas.transform as RectTransform, Input.mousePosition, null, out localposition);
         pickedItem.SetLocalPosition (localposition);

     }

     public void HideToolTip () {
         toolTip.Hide ();
         isToolTipShow = false;
     }

     public Item GetItemById (int id) {
         foreach (Item item in itemList) {
             if (item.ID == id) {
                 return item;
             }
         }
         return null;
     }

     public void ShowToolTip (string content) {
         toolTip.Show (content);
         Vector2 localposition;
         RectTransformUtility.ScreenPointToLocalPointInRectangle (canvas.transform as RectTransform, Input.mousePosition, null, out localposition);
         toolTip.SetLocation (localposition);
         isToolTipShow = true;
     }

 }