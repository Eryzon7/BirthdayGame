using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class NoteManager : MonoBehaviour
{
    public GameObject notePrefab;
    public BeatManager beatManager;

    public List<Note> activeNotes = new List<Note>();
    private int maxSteps = 30;

    public Transform[] laneSpawns;
    public float rowSpacing = 5f;

    public float timing;

    public float perfectWindow = 0.1f;
    public float goodWindow = 0.3f;

    [SerializeField] NoteChart chartNote;

    public int Score = 0;

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
                bestNote = note;
            }
        }
        if (timing < perfectWindow)
        {
            Score = +2;
            RemoveNote(bestNote);
        }
        else if (timing < goodWindow)
        {
            Score = +1;
            RemoveNote(bestNote);
        }
        else
        {
            RemoveNote(bestNote);
        }
    }

    void RemoveNote(Note note)
    {
        activeNotes.Remove(note);
        Destroy(note.gameObject);
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

            if (note.step > maxSteps)
            {
                Destroy(note.gameObject);
                activeNotes.RemoveAt(i);
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
