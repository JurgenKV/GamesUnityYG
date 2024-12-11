using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class RandomMusic : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private List<AudioResource> audioResources;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.resource = audioResources[Random.Range(0, audioResources.Count)];
        audioSource.Play();
    }
}
