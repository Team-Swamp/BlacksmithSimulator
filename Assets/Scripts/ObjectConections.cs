using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public sealed class ObjectConections : MonoBehaviour
{
    [Header("Refrences")]
    [SerializeField] private ParticleSystem attachPartsParticle;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip attachSound;
    [SerializeField] private AudioClip deattachSound;
    
    [Space]
    [SerializeField] private List<GameObject> connectionPoints;

    [Header("Events")]
    [SerializeField] private UnityEvent onConnect;
    [SerializeField] private UnityEvent onDetach;

    private ItemManager _itemManager;
    private Vector3 _connectionPos;
    private bool _canConnect = true;
    private bool _isConnected;
    private float _closestDistance = 99f;
    private const float CanConnectAgainTimer = 0.5f;

    private void Start()
    {
        _itemManager = FindObjectOfType<ItemManager>();

        var points = gameObject.GetComponentsInChildren<ConnectionPoint>();
        foreach (var connectionPoint in points)
        {
            connectionPoints.Add(connectionPoint.gameObject);
        }
    }

    public void SetObjectConnection()
    {
        var closestItem = GetClosestObject();
        if (closestItem == null) return;
        
        var closestConnection = GetClosestJoint(closestItem);
        
        if (closestConnection != null
            && Vector3.Distance(closestConnection[0].transform.position, closestConnection[1].transform.position) < 1
            && _canConnect)
        {
            SetObjectPosition(closestConnection);
        }
    }
    
    public void RemoveConnectedItems()
    {
        if(gameObject.transform.childCount == 0) return;

        gameObject.transform.SetParent(null, true);
        StartCoroutine(Timer());
        
        if (!_isConnected) return;
        _isConnected = false;
        onDetach?.Invoke();
    }
    
    public void ActivateParticleSystem()
    {
        var particleSystemObject = Instantiate(attachPartsParticle);
        particleSystemObject.transform.position = _connectionPos;
    }

    public void MakeSound(bool isAttaching)
    {
        var currentSound = isAttaching ? attachSound : deattachSound;
        audioSource.clip = currentSound;
        audioSource.Play();
    }

    private GameObject GetClosestObject()
    {
        var items = _itemManager.GetItems();
        if (items.Count == 0) return null;
        
        GameObject closestObject = null;
        
        foreach (var obj in items)
        {
            if (obj == gameObject) continue;
            
            var currentDistance = Vector3.Distance(gameObject.transform.position, obj.transform.position);
            if (!(currentDistance < _closestDistance)) continue;
            
            _closestDistance = currentDistance;
            closestObject = obj;
        }

        return closestObject;
    }

    private List<GameObject> GetClosestJoint(GameObject Item)
    {
        var targetPoints = new List<GameObject>();
        GameObject closestConnection = null;
        GameObject closestConnectionSelf = null;

        var otherConnectionPoints = Item.GetComponentsInChildren<ConnectionPoint>();
        foreach (var otherPoint in otherConnectionPoints)
        {
            foreach (var point in connectionPoints)
            {
                var distanceJoints = Vector3.Distance(point.gameObject.transform.position, otherPoint.transform.position);

                if(distanceJoints >= _closestDistance 
                   && point.gameObject.transform.localPosition.y - otherPoint.transform.localPosition.y == 0) continue;
                
                _closestDistance = distanceJoints;

                closestConnection = otherPoint.gameObject;
                closestConnectionSelf = point.gameObject;
            }
        }
        
        targetPoints.Add(closestConnection);
        targetPoints.Add(closestConnectionSelf);
        return targetPoints;
    }

    private void SetObjectPosition(IReadOnlyList<GameObject> closestConnection)
    {
        var finalPos = closestConnection[0].transform.position;
        var offset = gameObject.transform.position - closestConnection[1].transform.position;
        _connectionPos = closestConnection[1].transform.position;

        gameObject.transform.position = finalPos + offset;
        
        AddConnectedItem(closestConnection[0]);
    }

    private void AddConnectedItem(GameObject targetConnection)
    {
        gameObject.transform.SetParent(targetConnection.transform.parent.FindChild("AtachmentPoint"));
        
        if (_isConnected) return;
        _isConnected = true;
        onConnect?.Invoke();
    }

    private IEnumerator Timer()
    {
        _canConnect = false;
        yield return new WaitForSeconds(CanConnectAgainTimer);
        _canConnect = true;
    }
}