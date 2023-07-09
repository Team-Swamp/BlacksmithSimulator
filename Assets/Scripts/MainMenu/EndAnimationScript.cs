using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndAnimationScript : MonoBehaviour
{
    private Animation _animation;
    [SerializeField] private float duration;

    private void Update()
    {
        duration -= Time.deltaTime;
        if (duration <= 0)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
