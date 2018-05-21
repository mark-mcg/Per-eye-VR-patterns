using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class EyeDetails
{
    public Camera camera;
    public Image image;
}

public class ImagePatternPlayer : PatternPlayer<ImagePattern>
{
    public ImagePatternSequence PatternSequence;
    public EyeDetails LeftEye, RightEye;
    public Color DefaultBackground = Color.blue;

    [UnityEngine.Tooltip("Should both eyes be reset on a new left/right eye pattern, or should patterns persist until they are changed for that eye?")]
    public bool ResetBothEyesOnNewPattern = true;

    public void Awake()
    {
        this.sequence = PatternSequence;
    }

    public override void IncrementFrameCounter()
    {
        if (currentPattern != null)
        {
            EyeDetails eyeToDisplay = GetEye(currentPattern.eye);

            if (eyeToDisplay.image.sprite == currentPattern.image)
                framesDisplayed++;

            // n.b. if you want to know how long the last frame was displayed for, see Time.deltaTime
        }
    }

    public override void DisplayNewPattern(ImagePattern pattern)
    {
        // Debug.Log("DisplayNewPattern called for pattern " + pattern);
        if (currentPattern != null && ResetBothEyesOnNewPattern)
        {
            SetEyeToDefaults(LeftEye);
            SetEyeToDefaults(RightEye);      
        }

        if (pattern != null)
        {
            // Set the new pattern on the specified eye
            EyeDetails newEyeToDisplay = GetEye(pattern.eye);
            newEyeToDisplay.image.sprite = pattern.image;
            newEyeToDisplay.image.color = new Color(255, 255, 255, 255); // make the image entirely opaque (assuming the PNG has an alpha channel)
            newEyeToDisplay.camera.backgroundColor = new Color(pattern.background.r, pattern.background.g, pattern.background.b, 255);
            currentPattern = pattern;
        }

        framesDisplayed = 0;
    }

    private EyeDetails GetEye(Pattern.Eye eye)
    {
        if (eye == Pattern.Eye.Right)
            return RightEye;
        else
            return LeftEye;
    }

    private void SetEyeToDefaults(EyeDetails eye)
    {
        eye.image.sprite = null;
        eye.image.color = new Color(0, 0, 0, 0); // make the image entirely transparent
        eye.camera.backgroundColor = new Color(DefaultBackground.r, DefaultBackground.g, DefaultBackground.b, 0);
    }
}