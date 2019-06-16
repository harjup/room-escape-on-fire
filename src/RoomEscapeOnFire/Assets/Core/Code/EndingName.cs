using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EndingName : MonoBehaviour
{
    private string[] _endings = new[]
    {
        "Out_Of_Time",
        "Brute_Force",
        "Frozen",
        "Funny_Bug_Movie",
        "This_Is_Not_Cool",
        "Pizza",
        "Vore",
        "Rejection_Of_Vore",
    };

    void Start()
    {
        var superText = GetComponent<SuperTextMesh>();

        var progressStore = FindObjectOfType<EndingProgressStore>();

        var endingTexts = new List<string>();
        for (int i = 0; i < _endings.Length; i++)
        {
            var key = _endings[i];
            var num = i + 1;
            if (progressStore.HasEnding(key))
            {
                endingTexts.Add(num + ". " + key.Replace('_', ' '));
            }
            else
            {
                endingTexts.Add(num + ". ???");
            }
        }

        superText.Text = string.Join(Environment.NewLine, endingTexts.ToArray());
    }
}
