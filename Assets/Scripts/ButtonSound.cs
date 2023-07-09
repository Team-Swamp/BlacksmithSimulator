using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    [SerializeField] private AudioSource source;

    public void PlayButtonSound()
    {
        source.Play();
    }
}
