using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class ObjectConections : MonoBehaviour
{
    private ItemManager _itemManager;

    //private Vector3 size;
    private Vector3 _connectionPos;
    //private MeshRenderer renderer;
    private bool _isConnected;

    [SerializeField] private ParticleSystem attachPartsParticle;
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

        //renderer = GetComponent<MeshRenderer>();
        //size = renderer.bounds.size;
    }

    public void SetObjectConnection()
    {
        GameObject closestItem = GetClosestObject();
        if (closestItem == null) return;
        
        List<GameObject> closestConnection = GetClosestJoint(closestItem);

        if (closestConnection == null) return;
        if (Vector3.Distance(closestConnection[0].transform.position, closestConnection[1].transform.position) < (1) && _canConnect == true)
        {
            SetObjectPosition(closestConnection);
        }
    }

    private GameObject GetClosestObject()

    {
        var _items = _itemManager.GetItems();
        if (_items.Count == 0) return null;
        
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

    private List<GameObject> GetClosestJoint(GameObject Item)
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

    private void SetObjectPosition(List<GameObject> closestConnection)
    {
        var _finalPos = closestConnection[0].transform.position;
        var _offset = gameObject.transform.position - closestConnection[1].transform.position;
        _connectionPos = closestConnection[1].transform.position;

        gameObject.transform.position = _finalPos + _offset;


        AddConnectedItem(closestConnection[0]);
    }

    [System.Obsolete]
    private void AddConnectedItem(GameObject Connection)
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

    private IEnumerator Timer(float time)
    {
        _canConnect = false;
        yield return new WaitForSeconds(time);
        _canConnect = true;
    }
    
    public void ActivateParticleSystem()
    {
        var particleSystemObject = Instantiate(attachPartsParticle);
        particleSystemObject.transform.position = _connectionPos;
        //todo: Sounds can be placed here or make anhoter function and put in the same Unity Event
    }
}