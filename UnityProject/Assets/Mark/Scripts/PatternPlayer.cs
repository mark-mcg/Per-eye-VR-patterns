using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class PatternPlayer<T> : MonoBehaviour 
    where T : Pattern {

    public PatternSequence<T> sequence;

    protected T currentPattern;
    protected float framesDisplayed = 0;

    public void Start()
    {
        Application.targetFrameRate = 90; // not strictly necessary, but noting this here
        QualitySettings.vSyncCount = 0; // again not strictly necessary, but important to remember vsync is ignored by the VR SDKs in favour of other ways of syncing frames
    }

    bool running = true;

    /// <summary>
    /// Called for every frame. We'll keep track of how many frames a pattern has been rendered for 
    /// </summary>
    void Update () {

        if (running)
        {
            // First check how many frames this pattern has now been displayed for
            IncrementFrameCounter();

            if (currentPattern == null || currentPattern.frames == framesDisplayed)
            {
                T nextPattern = sequence.GetNextPattern();

                if (nextPattern != null && nextPattern != currentPattern)
                {
                    DisplayNewPattern(nextPattern);
                }
                else
                {
                    Debug.LogError("PatternPlayer: Ran out of patterns, stopping...");
                    running = false;
                }
            }
        }
    }

    public abstract void DisplayNewPattern(T Pattern);
    public abstract void IncrementFrameCounter();


}
