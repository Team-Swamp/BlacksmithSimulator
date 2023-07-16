using UnityEngine;

public sealed class ButtonSound : MonoBehaviour
{
    [SerializeField] private AudioSource source;

    public void PlayButtonSound() => source.Play();
}
