using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

/// <summary>
/// 物品槽
/// </summary>
public class Slot : MonoBehaviour ,IPointerEnterHandler,IPointerExitHandler,IPointerDownHandler{

    public GameObject itemPrefab;
    private ItemUI ItemUI;
    /// <summary>
    /// 把item放在自身下面
    /// 如果自身下面已经有item了，amount++
    /// 如果没有 根据itemPrefab去实例化一个item，放在下面
    /// </summary>
    /// <param name="item"></param>
    public void StoreItem(Item item)
    {
        if ((int)transform.childCount  > 0 )
        {
           ItemUI itemUI = transform.GetChild(0).GetComponent<ItemUI>();
           itemUI.AddAmount(); 
        }else{
            GameObject go = Resources.Load<GameObject>("Prefabs/Item");
            GameObject itemGameObject =  Instantiate(go);
            itemGameObject.transform.SetParent(this.transform);
            itemGameObject.transform.localPosition = Vector3.one;
            itemGameObject.GetComponent<ItemUI>().SetItem(item);
            ItemUI =  itemGameObject.GetComponent<ItemUI>();
        }
    }


    /// <summary>
    /// 得到当前物品槽存储的物品类型
    /// </summary>
    /// <returns></returns>
    public Item.ItemType GetItemType()
    {
        if (ItemUI == null)
        {
            return Item.ItemType.None;
        }
        return ItemUI.Item.Type;
    }

    /// <summary>
    /// 得到物品的id
    /// </summary>
    /// <returns></returns>
    public int GetItemId()
    {
        if (ItemUI == null)
        {
            return -1;
        }
        return ItemUI.Item.ID;

    }

    public bool IsFilled()
    {
       return ItemUI.Item.Capacity == ItemUI.Amount;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
          if(transform.childCount>0)
            InventoryManager.Instance.HideToolTip();
            
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
         if (transform.childCount > 0)
        {
            string toolTipText = transform.GetChild(0).GetComponent<ItemUI>().Item.GetToolTipText();
            InventoryManager.Instance.ShowToolTip(toolTipText);
        }
        
        
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {

        Debug.Log(eventData.button + ":OnPointerDown" );
         if (eventData.button ==  PointerEventData.InputButton.Right)
         {
             if (InventoryManager.Instance.IsPickedItem == false && transform.childCount > 0)
             {
                 ItemUI currentItemUI = transform.GetChild(0).GetComponent<ItemUI>();
                 if (currentItemUI.Item is Equipment  || currentItemUI.Item is Weapon)
                 {
                     currentItemUI.ReduceAmount();
                     if (currentItemUI.Amount <= 0 )
                     {
                         DestroyImmediate(currentItemUI.gameObject);
                     }
                 }
                 
             }
         }

        // 自身是空 1,IsPickedItem ==true  pickedItem放在这个位置
                            // 按下ctrl      放置当前鼠标上的物品的一个
                            // 没有按下ctrl   放置当前鼠标上的物品的所有
                 //2,IsPickedItem==false  不做任何处理
        // 自身不是空 
                 //1,IsPickedItem==true
                        //自身的id==pickedItem.id  
                                    // 按下ctrl      放置当前鼠标上的物品的一个
                                    // 没有按下ctrl   放置当前鼠标上的物品的所有
                                                    //可以完全放下
                                                    //只能放下其中一部分
                        //自身的id!=pickedItem.id   pickedItem跟当前物品交换          
                 //2,IsPickedItem==false
                        //ctrl按下 取得当前物品槽中物品的一半
                        //ctrl没有按下 把当前物品槽里面的物品放到鼠标上

        if (eventData.button != PointerEventData.InputButton.Left) return;

         if (transform.childCount > 0) // 卡槽里不为空
         {
             Debug.Log(InventoryManager.Instance.IsPickedItem + "fwefw");
             if (InventoryManager.Instance.IsPickedItem == false )  // 没有选中物体
             {
                if (Input.GetKey(KeyCode.LeftControl)) //选中一半物体
                {
                    int half = Mathf.CeilToInt(ItemUI.Amount /2);
                    InventoryManager.Instance.PickupItem(ItemUI.Item,half);
                    ItemUI.ReduceAmount(half);
                }else { //全选物体
                    InventoryManager.Instance.PickupItem(ItemUI.Item, ItemUI.Amount);
                    Destroy(ItemUI.gameObject);
                    ItemUI =  null;
                }
             }else { // 当前有选中物体
                ItemUI pickedItem = InventoryManager.Instance.PickedItem;

                 //1,IsPickedItem==true
                    //自身的id==pickedItem.id  
                        // 按下ctrl      放置当前鼠标上的物品的一个
                        // 没有按下ctrl   放置当前鼠标上的物品的所有
                            //可以完全放下
                            //只能放下其中一部分
                    //自身的id!=pickedItem.id   pickedItem跟当前物品交换      

                if (pickedItem.Item.ID != ItemUI.Item.ID)
                {
                    pickedItem.Exchange(ItemUI);
                }

             }
         }else {  // 卡槽里为空
            Debug.Log("others ssss ");
         }
    }
}
