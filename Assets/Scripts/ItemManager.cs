using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> ItemList;
    public List<GameObject> GetItems()
    {
        return ItemList;
    }

    public void AddItems(GameObject item)
    {
        ItemList.Add(item);
    }

    public void RemoveItems(GameObject item)
    {
        ItemList.Remove(item);
    }

    public void GetFinalWeapon()
    {
        var finalWeapon = new GameObject();

        foreach (var weaponPart in ItemList)
        {
            weaponPart.transform.SetParent(finalWeapon.transform);
        }

        Instantiate(finalWeapon);
        // return finalWeapon;
    }
}
