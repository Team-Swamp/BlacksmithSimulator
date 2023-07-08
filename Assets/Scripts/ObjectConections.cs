using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectConections : MonoBehaviour
{
    private ItemManager _itemManager;

    [SerializeField] private Vector3 size;
    private MeshRenderer renderer;

    [SerializeField] private List<GameObject> connectionPoints;
    [SerializeField] private GameObject AtachmentPoint;

    private bool _canConnect = true;

    private void Start()
    {
        _itemManager = FindObjectOfType<ItemManager>();

        var test = gameObject.GetComponentsInChildren<ConnectionPoint>();
        foreach (ConnectionPoint a in test)
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

        foreach (GameObject a in _items)
        {
            if (a.gameObject != this.gameObject)
            {
                var _currentDistance = Vector3.Distance(gameObject.transform.position, a.transform.position);
                if (_currentDistance < _closestDistance)
                {
                    _closestDistance = _currentDistance;
                    _closestObject = a;
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

        var test = Item.GetComponentsInChildren<ConnectionPoint>();
        foreach (ConnectionPoint a in test)
        {
            for (int i = 0; i < connectionPoints.Count; i++)
            {
                var _distanceJoints = Vector3.Distance(connectionPoints[i].gameObject.transform.position, a.transform.position);

                if(_distanceJoints < _closestDistance && connectionPoints[i].gameObject.transform.localPosition.y - a.transform.localPosition.y != 0)
                {
                    _closestDistance = _distanceJoints;

                    _closestConnection = a.gameObject;
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
        //Ik connect aan een object
        //Connection.transform.parent.SetParent(AtachmentPoint.transform, true);


        gameObject.transform.SetParent(Connection.transform.parent.FindChild("AtachmentPoint"));
    }

    public void RemoveConnectedItems(GameObject Connection)
    {
        gameObject.transform.SetParent(null, true);
        //AtachmentPoint.transform.DetachChildren();
        StartCoroutine(Timer(0.5f));
    }

    IEnumerator Timer(float time)
    {
        _canConnect = false;
        yield return new WaitForSeconds(time);
        _canConnect = true;
    }
}