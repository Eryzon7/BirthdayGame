using UnityEngine;
using System.Collections.Generic;


public class HitInputs : MonoBehaviour
{
    public BeatManager beatManager;
    public NoteManager noteManager;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D)) HitLane(0);
        if (Input.GetKeyDown(KeyCode.F)) HitLane(1);
        if (Input.GetKeyDown(KeyCode.J)) HitLane(2);
        if (Input.GetKeyDown(KeyCode.K)) HitLane(3);
    }

    void HitLane(int lane)
    {
        Debug.Log("Hit");
        noteManager.TryHit(lane);
        
    }
}
