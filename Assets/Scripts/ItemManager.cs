using System.Collections.Generic;
using UnityEngine;

public sealed class ItemManager : MonoBehaviour
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

    public void ClearList()
    {
        foreach (var part in ItemList)
        {
            Destroy(part.gameObject);
        }
        ItemList.Clear();
    }
}
