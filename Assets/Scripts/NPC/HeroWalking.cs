using System;
using UnityEngine;
using UnityEngine.Events;

public sealed class HeroWalking : MonoBehaviour
{
    private enum HeroState
    {
        Spawned,
        WalkToBlackSmith,
        Standing,
        WalkingBack
    }
    private HeroState _currentHeroState;

    [Header("Settings")]
    [SerializeField] private float walkSpeed;
    [SerializeField] private float positionMargin = 0.1f;
    
    public Transform StartingPos { private get; set; }
    public Transform StandInFrontBlacksmithPos { private get; set; }

    private bool _isActive;
    private bool _wasWalkingBack;
    private bool _startTalking;
    private GradingSystem _gradingSystem;

    [Header("Events")]
    [SerializeField] private UnityEvent onStandingInFrontBlackSmith = new UnityEvent();
    [SerializeField] private UnityEvent onWalkingBack = new UnityEvent();

    [SerializeField] private Canvas canvas;
    [SerializeField] private DialogueUI dialogueUI;

    private void Start()
    {
        canvas = FindObjectOfType<Canvas>();
        dialogueUI = canvas.GetComponent<DialogueUI>();
        _gradingSystem = FindObjectOfType<GradingSystem>();
    }

    private void Update()
    {
        switch (_currentHeroState)
        {
            case HeroState.Spawned:
                if (_isActive) _currentHeroState = HeroState.WalkToBlackSmith;
                break;
            case HeroState.WalkToBlackSmith:
                WalkToTarget(StandInFrontBlacksmithPos.position);
                break;
            case HeroState.Standing:
                if (!_startTalking)
                {
                    _startTalking = true;
                    dialogueUI.ShowDialogue(null);
                }
                onStandingInFrontBlackSmith?.Invoke();
                break;
            case HeroState.WalkingBack:
                onWalkingBack?.Invoke();
                if (!_wasWalkingBack)
                {
                    _wasWalkingBack = true;
                    FindObjectOfType<HeroWalkCyle>().StartWalkingNewHero();
                }
                WalkToTarget(StartingPos.position);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void WalkToTarget(Vector3 targetPos)
    {
        transform.position = Vector3.Lerp(transform.position, targetPos, walkSpeed * Time.deltaTime);
        if (!(Vector3.Distance(transform.position, targetPos) <= positionMargin)) return;
        
        switch (_currentHeroState)
        {
            case HeroState.WalkToBlackSmith:
                _currentHeroState = HeroState.Standing;
                _gradingSystem.SetHero(gameObject.GetComponent<Diserars>());
                FindObjectOfType<HeroWalkCyle>().SpawnNewHero();
                break;
            case HeroState.WalkingBack:
                Destroy(gameObject);
                break;
        }
    }

    public void StartWalking() => _isActive = true;
    
    public void SetToWalkingBackState() => _currentHeroState = HeroState.WalkingBack;
}
