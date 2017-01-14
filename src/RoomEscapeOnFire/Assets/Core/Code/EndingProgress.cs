using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class EndingProgress : MonoBehaviour
{
	void Start ()
	{
	    var button = GameObject.Find("ContinueButton");
        var textButton = button.GetComponent<SuperTextButton>();
        textButton.SetText("<w>Continue?</w>");
        textButton.SetClickAction(() =>
        {
            FindObjectOfType<DialogueRunner>().StartDialogue("Ending.Continue");
            Destroy(textButton.gameObject);
        });


    }
}
