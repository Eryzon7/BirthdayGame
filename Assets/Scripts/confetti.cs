using UnityEngine;
using UnityEngine.Audio;

public class Confetti : MonoBehaviour
{
    private ParticleSystem[] particles;
    public AudioSource audioSource;

    void Awake()
    {
        particles = GetComponentsInChildren<ParticleSystem>();
    }

    public void PlayWinEffects()
    {
        audioSource.Play();
        foreach (var ps in particles)
        {
            ps.Play();
        }
    }
}