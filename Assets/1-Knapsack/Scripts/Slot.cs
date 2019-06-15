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
        
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        
        
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
         
    }
}
