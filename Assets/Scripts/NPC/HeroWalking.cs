using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HeroWalking : MonoBehaviour
{
    private enum HeroState
    {
        Spawned,
        WalkToBlackSmith,
        Standing,
        WalkingBack
    }

    [SerializeField] private Transform startingPos;
    [SerializeField] private Transform standInFrontBlacksmithPos;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float positionMargin = 0.1f;

    private HeroState _currentState;
    [SerializeField] private bool _isActive;

    [SerializeField] private UnityEvent onStandingInFrontBlackSmith = new UnityEvent();
    [SerializeField] private UnityEvent onWalkingBack = new UnityEvent();

    private void Awake()
    {
        transform.position = startingPos.position;
    }

    private void Update()
    {
        switch (_currentState)
        {
            case HeroState.Spawned:
                if (_isActive) _currentState = HeroState.WalkToBlackSmith;
                break;
            case HeroState.WalkToBlackSmith:
                WalkToTarget(standInFrontBlacksmithPos.position);
                break;
            case HeroState.Standing:
                onStandingInFrontBlackSmith?.Invoke();
                break;
            case HeroState.WalkingBack:
                onWalkingBack?.Invoke();
                WalkToTarget(startingPos.position);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void WalkToTarget(Vector3 targetPos)
    {
        transform.position = Vector3.Lerp(transform.position, targetPos, walkSpeed * Time.deltaTime);
        if (!(Vector3.Distance(transform.position, targetPos) <= positionMargin)) return;
        
        switch (_currentState)
        {
            case HeroState.WalkToBlackSmith:
                _currentState = HeroState.Standing;
                break;
            case HeroState.WalkingBack:
                Destroy(gameObject);
                break;
        }
    }

    public void StartWalking()
    {
        _isActive = true;
    }
    
    public void SetToWalkingBackState()
    {
        _currentState = HeroState.WalkingBack;
    }
}
