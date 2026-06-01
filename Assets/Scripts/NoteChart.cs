using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Rhythm/Note Chart")]
public class NoteChart : ScriptableObject
{
    public List<ChartNote> notes = new();
}
