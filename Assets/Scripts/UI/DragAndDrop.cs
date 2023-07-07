using UnityEngine;

public sealed class DragAndDrop : MonoBehaviour
{
    private Vector3 _mousePos;
    [SerializeField] private GameObject currentItemToDrag;
    [SerializeField, Tooltip("The height where the item is going to be dragged.")] private float dragHeight = 1;

    private void Update()
    {
        if (!Input.GetMouseButton(1)) return;
        
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out var hit))
        {
            if(!hit.collider.gameObject.CompareTag("DragArea")) return;
            _mousePos = hit.point;
            // Debug.Log("Mouse position in world space: " + _mousePos);
        }
            
        if (!currentItemToDrag) return;
        var itemPos = new Vector3(_mousePos.x, _mousePos.y, dragHeight);
        currentItemToDrag.transform.position = itemPos;
    }

    public void SetItemToDrag(GameObject targetItem)
    {
        currentItemToDrag = Instantiate(targetItem);
    }
}
