using UnityEngine;

public class BeatManager : MonoBehaviour
{
    public float bpm = 120f;
    private float beatInterval;
    public float timer;

    public System.Action OnBeat;

    void Start()
    {
        beatInterval = 80f / bpm;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= beatInterval)
        {
            timer -= beatInterval;
            OnBeat?.Invoke();
        }
    }
}
