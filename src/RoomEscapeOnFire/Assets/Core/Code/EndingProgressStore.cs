using System.Collections.Generic;
using UnityEngine;

public class EndingProgressStore : MonoBehaviour
{
    [SerializeField]
    private List<string> AchievedEndings = new List<string>();

    public void AddEnding(string endingName)
    {
        if (!AchievedEndings.Contains(endingName))
        {
            AchievedEndings.Add(endingName);
        }
    }

    public bool HasEnding(string endingName)
    {
        return AchievedEndings.Contains(endingName);
    }

    public bool HasAllEndings()
    {
        return AchievedEndings.Count >= 8;
    }
}
