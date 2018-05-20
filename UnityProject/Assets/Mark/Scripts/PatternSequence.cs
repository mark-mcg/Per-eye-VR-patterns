using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PatternSequence<T> : MonoBehaviour
    where T : Pattern
{
    public List<T> patterns;
    public bool loop = true;
    private IEnumerator<T> patternEnumerator;

    public void Start()
    {
        patternEnumerator = patterns.GetEnumerator();
    }

    /// <summary>
    /// Get next pattern in list, looping if at end of list
    /// </summary>
    /// <returns></returns>
    public T GetNextPattern()
    {
        if (patterns.Count > 0)
        {
            if (patternEnumerator.MoveNext())
            {
                return patternEnumerator.Current;
            }
            else
            {
                if (loop)
                {
                    patternEnumerator.Reset();
                    patternEnumerator.MoveNext();
                    return patternEnumerator.Current;
                } else
                {
                    Debug.LogError("Finished list of patterns, returning null");
                    return null;
                }
            }
        } else
        {
            Debug.LogError("No Patterns assigned, returning null");
            return null;
        }
    }

}
