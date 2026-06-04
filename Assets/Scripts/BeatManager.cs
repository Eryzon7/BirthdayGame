using UnityEngine;
using UnityEngine.Audio;

public class BeatManager : MonoBehaviour
{
    public float bpm = 120f;
    private float beatInterval;
    public float timer;
    private bool songOver = false;

    [SerializeField] private NoteManager noteManager;

    [SerializeField] private AudioSource audioSource;

    public System.Action OnBeat;

    void Start()
    {
        beatInterval = 80f / bpm;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= beatInterval && songOver == false)
        {
            audioSource.Play();
            timer -= beatInterval;
            OnBeat?.Invoke();
        }
    }
    
    private void SongeOver()
    {
        songOver = true;
    }


    void OnEnable()
    {
        noteManager.songOver += SongeOver;
    }

    void OnDisable()
    {
        noteManager.songOver -= SongeOver;
    }
}
