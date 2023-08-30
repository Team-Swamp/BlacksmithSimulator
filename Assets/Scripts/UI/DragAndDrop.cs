using UnityEngine;
using UnityEngine.Events;

public sealed class DragAndDrop : MonoBehaviour
{
    [SerializeField] private GameObject currentItemToDrag;

    [Header("Settings")]
    [SerializeField, Tooltip("The offset where the item is going to be dragged.")] private float dragOffset = 1;
    [SerializeField] private Transform spawnPointForItems;

    [Header("Events")]
    [SerializeField] private UnityEvent onSpawnItem = new UnityEvent();
    [SerializeField] private UnityEvent onStopDragging = new UnityEvent();
    
    private ItemManager _itemManager;
    private Vector3 _mousePos;
    private Collider _lastDraggedItemCollider;

    private void Start() => _itemManager = FindObjectOfType<ItemManager>();

    private void Update()
    {
        SelectNewItemToDrag();
        RemoveItem();
        if (Input.GetMouseButtonUp(0)) onStopDragging?.Invoke();
        if (Input.GetMouseButton(0)) DragItem();
    }
    
    /// <summary>
    /// Creates new weapon part and sets it to the current item to drag.
    /// </summary>
    /// <param name="targetItem">New weapon part that spawns in.</param>
    public void SetItemToDrag(GameObject targetItem)
    {
        currentItemToDrag = Instantiate(targetItem, spawnPointForItems.position, targetItem.transform.rotation);
        _itemManager.AddItems(currentItemToDrag);
        onSpawnItem?.Invoke();
    }

    /// <summary>
    /// Dragges the current item to drag.
    /// </summary>
    private void DragItem()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out var hit))
        {
            if(!hit.collider.gameObject.CompareTag("DragArea")) return;
            _mousePos = hit.point;
        }

        if (!currentItemToDrag) return;

        if (_itemManager.GetItems().Count > 0) currentItemToDrag.GetComponent<ObjectConections>().SetObjectConnection();
        
        var itemPos = new Vector3(_mousePos.x, _mousePos.y, dragOffset);
        currentItemToDrag.transform.position = itemPos;
        if (_lastDraggedItemCollider) _lastDraggedItemCollider.enabled = false;
    }
    
    /// <summary>
    /// Remove the current item to drag.
    /// </summary>
    private void RemoveItem()
    {
        if (!Input.GetMouseButtonUp(1) // Rightmousebutton for remove item
            || currentItemToDrag == null) return;
        
        _itemManager.RemoveItems(currentItemToDrag);
        Destroy(currentItemToDrag);
        currentItemToDrag = null;
    }

    /// <summary>
    /// Select a diffrent weapon part to current item to drag.
    /// </summary>
    private void SelectNewItemToDrag()
    {
        if (!Input.GetMouseButtonDown(0)) return;
        
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (!Physics.Raycast(ray, out var hit)
            || !hit.collider.gameObject.CompareTag("DraggableItem")) 
            return;

        if (_lastDraggedItemCollider) _lastDraggedItemCollider.enabled = true;
            
        currentItemToDrag = hit.collider.gameObject;
        _lastDraggedItemCollider = currentItemToDrag.GetComponent<Collider>();
        
        currentItemToDrag.GetComponent<ObjectConections>().RemoveConnectedItems();
    }
}
 