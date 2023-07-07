using UnityEngine;

public sealed class HeroWalkCyle : MonoBehaviour
{
    [Header("Postions")]
    [SerializeField] public Transform startingPos;
    [SerializeField] public Transform standInFrontBlacksmithPos;
    
    [Header("Hero stuff")]
    [SerializeField] private HeroWalking currentHero;
    [SerializeField] private GameObject[] heros;

    private bool _isFirstHero = true;

    private void Start() => SpawnNewHero();

    public void SpawnNewHero()
    {
        var randomNumber = Random.Range(0, heros.Length);
        currentHero = Instantiate(heros[randomNumber]).GetComponent<HeroWalking>();
        currentHero.StartingPos = startingPos;
        currentHero.StandInFrontBlacksmithPos = standInFrontBlacksmithPos;
        
        if (!_isFirstHero) return;
        _isFirstHero = false;
        currentHero.StartWalking();
    }

    public void StartWalkingNewHero() => currentHero.StartWalking();
}
