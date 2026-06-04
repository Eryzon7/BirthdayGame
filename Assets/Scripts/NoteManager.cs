using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class NoteManager : MonoBehaviour
{
    public GameObject notePrefab;
    public BeatManager beatManager;
    public BeatScore beatScore;

    public List<Note> activeNotes = new List<Note>();

    public Transform[] laneSpawns;
    public float rowSpacing = 5f;

    public float timing;

    public float perfectWindow = 0.27f;
    public float goodWindow = 0.3f;

    [SerializeField] private int perfectHitScore = 2;
    [SerializeField] private int goodHitScore = 1;

    [SerializeField] NoteChart chartNote;

    public int Score = 0;

    public System.Action songOver;

    private void Start()
    {
        foreach (ChartNote chartNote in chartNote.notes)
        {
            SpawnNote(chartNote);
        }
    }
    public void TryHit(int lane)
    {
        Note bestNote = null;
        foreach (Note note in activeNotes)
        {
            if (note.lane == lane && note.step == 0)
            {
                timing = beatManager.timer;
                Debug.Log(timing);
                bestNote = note;
            }
        }
        if (bestNote != null)
        {
            if (timing < perfectWindow)
            {
                beatScore.ComboCounter();
                beatScore.UpdateScore(perfectHitScore);
                Debug.Log("perfect hit");
                RemoveNote(bestNote);
            }
            else if (timing < goodWindow)
            {
                beatScore.ComboCounter();
                beatScore.UpdateScore(goodHitScore);
                RemoveNote(bestNote);
            }
        }
        else
        {
            beatScore.ComboReset();
        }
        
    }

    void RemoveNote(Note note)
    {
        activeNotes.Remove(note);
        Destroy(note.gameObject);
        if (activeNotes.Count <= 0)
        {
            songOver?.Invoke();
        }
    }
    void OnEnable()
    {
        beatManager.OnBeat += OnBeat;
    }

    void OnDisable()
    {
        beatManager.OnBeat -= OnBeat;
    }

    void OnBeat()
    {
        for (int i = activeNotes.Count - 1; i >= 0; i--)
        {
            Note note = activeNotes[i];

            note.step--;
            UpdateNotePosition(note);

            if (note.step < 0)
            {
                beatScore.ComboReset();
                RemoveNote(note);
            }
        }
    }

    private void UpdateNotePosition(Note note)
    {
        Vector3 lanePos = laneSpawns[note.lane].position;

        Vector3 newPos = new Vector3(
            lanePos.x,
            lanePos.y + (note.step * rowSpacing),
            lanePos.z
        );
        note.transform.position = newPos;
    }

    private void SpawnNote(ChartNote chartNote)
    {
        GameObject obj = Instantiate(notePrefab);
        Note note = obj.GetComponent<Note>();
        activeNotes.Add(note);

        note.lane = chartNote.lane;
        note.startStep = chartNote.beat;
        note.step = chartNote.beat;

        UpdateNotePosition(note);
    }
}
