using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class PatternPlayer<T> : MonoBehaviour 
    where T : Pattern {

    public Pattern.Eye renderEye;

    public PatternSequence<T> sequence;
    public int FPSTarget = 60;

    protected T currentPattern;
    protected float framesDisplayed = 0;

    public void Start()
    {
        Application.targetFrameRate = FPSTarget; // not strictly necessary, but noting this here
        QualitySettings.vSyncCount = 0; // again not strictly necessary, but important to remember vsync is ignored by the VR SDKs in favour of other ways of syncing frames
    }

    bool running = true;


    float timeSinceTarget = 0;
    float count = 0;

    void CountPatterns()
    {
        if (timeSinceTarget == 0)
        {
            // first frame being rendered
            timeSinceTarget = Time.time;
        }

        if (count == FPSTarget)
        {
            float timeElapsed = Time.time - timeSinceTarget;
            Debug.LogError("Rendered " +FPSTarget+ "FPS in " + timeElapsed + " seconds, aiming for ~1 second if it's rendering correctly...");
            timeSinceTarget = Time.time;
            count = 0;
        }
        
        count++;
    }

    
    /// <summary>
    /// Called for every frame. We'll keep track of how many frames a pattern has been rendered for 
    /// </summary>
    void Update () {

        if (running)
        {
            // First check how many frames this pattern has now been displayed for
            IncrementFrameCounter();
            CountPatterns();

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
