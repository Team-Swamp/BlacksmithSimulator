using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class ObjectConections : MonoBehaviour
{
    private ItemManager _itemManager;

    private Vector3 size;
    private MeshRenderer renderer;
    private bool _isConnected;

    [SerializeField] private List<GameObject> connectionPoints;

    [SerializeField] private UnityEvent onConnect;
    [SerializeField] private UnityEvent onDetach;

    private bool _canConnect = true;

    private void Start()
    {
        _itemManager = FindObjectOfType<ItemManager>();

        var _points = gameObject.GetComponentsInChildren<ConnectionPoint>();
        foreach (ConnectionPoint a in _points)
        {
            connectionPoints.Add(a.gameObject);
        }

        renderer = GetComponent<MeshRenderer>();
        size = renderer.bounds.size;
    }


    public void SetObjectConnection()
    {
        GameObject closestItem = GetClosestObject();
        List<GameObject> closestConnection = GetClosestJoint(closestItem);

        if (closestConnection == null) return;
        if (Vector3.Distance(closestConnection[0].transform.position, closestConnection[1].transform.position) < (size.y / 2) && _canConnect == true)
        {
            SetObjectPosition(closestConnection);
        }
    }


    public GameObject GetClosestObject()
    {
        var _items = _itemManager.GetItems();
        float _closestDistance = 99;
        GameObject _closestObject = null;

        foreach (GameObject obj in _items)
        {
            if (obj.gameObject != this.gameObject)
            {
                var _currentDistance = Vector3.Distance(gameObject.transform.position, obj.transform.position);
                if (_currentDistance < _closestDistance)
                {
                    _closestDistance = _currentDistance;
                    _closestObject = obj;
                }
            }
        }

        return _closestObject;
    }

    public List<GameObject> GetClosestJoint(GameObject Item)
    {
        float _closestDistance = 99;

        List<GameObject> _connectionPoints = new List<GameObject>();
        GameObject _closestConnection = null;
        GameObject _closestConnectionSelf = null;

        var _points = Item.GetComponentsInChildren<ConnectionPoint>();
        foreach (ConnectionPoint point in _points)
        {
            for (int i = 0; i < connectionPoints.Count; i++)
            {
                var _distanceJoints = Vector3.Distance(connectionPoints[i].gameObject.transform.position, point.transform.position);

                if(_distanceJoints < _closestDistance && connectionPoints[i].gameObject.transform.localPosition.y - point.transform.localPosition.y != 0)
                {
                    _closestDistance = _distanceJoints;

                    _closestConnection = point.gameObject;
                    _closestConnectionSelf = connectionPoints[i].gameObject;
                }
            }
        }
        _connectionPoints.Add(_closestConnection);
        _connectionPoints.Add(_closestConnectionSelf);
        return _connectionPoints;
    }

    public void SetObjectPosition(List<GameObject> closestConnection)
    {
        

        var _finalPos = closestConnection[0].transform.position;
        var _offset = gameObject.transform.position - closestConnection[1].transform.position;

        gameObject.transform.position = _finalPos + _offset;


        AddConnectedItem(closestConnection[0]);
    }

    [System.Obsolete]
    public void AddConnectedItem(GameObject Connection)
    {
        gameObject.transform.SetParent(Connection.transform.parent.FindChild("AtachmentPoint"));
        
        if (_isConnected) return;
        _isConnected = true;
        onConnect?.Invoke();
    }

    public void RemoveConnectedItems(GameObject Connection)
    {
        if (_isConnected)
        {
            _isConnected = false;
            onDetach?.Invoke();
        }

        gameObject.transform.SetParent(null, true);
        StartCoroutine(Timer(0.5f));
    }

    IEnumerator Timer(float time)
    {
        _canConnect = false;
        yield return new WaitForSeconds(time);
        _canConnect = true;
    }
}