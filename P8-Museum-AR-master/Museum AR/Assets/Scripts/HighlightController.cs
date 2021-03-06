using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HighlightController
{
    private static List<int> highlights = new List<int>();
    public static List<int> Highlights { get => highlights; set => highlights = value; }

    public static void InitializeHighlightList()
    {
        Highlights.Insert(0, 0);
    }

    public static void ClearHighlights()
    {
        Highlights.Clear();
        Highlights.Insert(0, 0);
    }
}
