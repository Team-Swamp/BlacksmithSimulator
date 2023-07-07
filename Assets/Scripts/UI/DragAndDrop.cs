using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private Vector3 _mousePos;
    [SerializeField] private GameObject currentItemToDrag;

    private void Update()
    {
        if (Input.GetMouseButton(1)) // Check if the left mouse button is clicked
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Cast a ray from the mouse position

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                _mousePos = hit.point; // Get the intersection point of the ray with the object

                // Now you can use the mousePositionInWorld vector in your code
                Debug.Log("Mouse position in world space: " + _mousePos);
            }
            
            if (!currentItemToDrag) return;

            var itemPos = new Vector3(_mousePos.x, 1, _mousePos.z);
            currentItemToDrag.transform.position = itemPos;
        }
    }

    public void SetItemToDrag(GameObject targetItem)
    {
        currentItemToDrag = Instantiate(targetItem);
    }
}
