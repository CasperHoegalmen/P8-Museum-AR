using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ImageTargetController
{
    private static int numberOfImageTargets;

    private static int currentImageTarget = 0;

    public static int NumberOfImageTargets { get => numberOfImageTargets; set => numberOfImageTargets = value; }
    public static int CurrentImageTarget { get => currentImageTarget; set => currentImageTarget = value; }


    public static void SwitchToNextImageTarget()
    {
        CurrentImageTarget++;

    }
}
