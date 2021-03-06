using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Story Part", menuName = "Story Part")]
public class StoryPart : ScriptableObject
{
    public string exhibitTag;
    //public new string name;
    public AudioClip audioClip;
    public int index;
    public int numberOfOptions;
    public bool hasFinished;
}
