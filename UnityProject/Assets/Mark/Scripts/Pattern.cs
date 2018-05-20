using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Pattern {

    public enum Eye { Left, Right };

    [Tooltip("How many frames should this pattern be displayed for?")]
    public int frames;

    [Tooltip("Which eye should this pattern be shown on?")]
    public Eye eye;

    [Tooltip("What colour should be shown in the background for this pattern/eye?")]
    public Color background = Color.black;
}
