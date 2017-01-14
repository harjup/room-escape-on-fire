using System.Collections.Generic;
using UnityEngine;

public class EndingProgressStore : MonoBehaviour
{
    [SerializeField]
    private List<string> AchievedEndings = new List<string>();

    public void AddEnding(string endingName)
    {
        AchievedEndings.Add(endingName);
    }

    public bool HasEnding(string endingName)
    {
        return AchievedEndings.Contains(endingName);
    }
}
