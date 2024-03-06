using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Events
{
    public static Action<Block> OnBlockDestroyed;
    public static Action<int> OnPointsAdded;
    public static Action OnLeveLUp;
}
