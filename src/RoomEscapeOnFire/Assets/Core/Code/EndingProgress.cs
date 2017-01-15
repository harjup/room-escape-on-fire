using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn;
using Yarn.Unity;

public class EndingProgress : MonoBehaviour
{
	void Start ()
	{
	    FindObjectOfType<MusicPlayer>().StopSong();

	    var hasAllEndings = FindObjectOfType<EndingProgressStore>().HasAllEndings();

	    var button = GameObject.Find("ContinueButton");
        var textButton = button.GetComponent<SuperTextButton>();

	    if (hasAllEndings)
	    {
            textButton.DefaultColor = new Color(135f / 255f, 255f / 255f, 66f / 255f);
            textButton.SetText("<size=1>9. EPILOGUE");
            textButton.SetClickAction(() =>
            {
                FindObjectOfType<DialogueRunner>().StartDialogue("Ending.Continue_Into_Epilogue");
                Destroy(textButton.gameObject);
            });
        }
        else
	    {
            textButton.SetText("<w>Continue?</w>");
            textButton.SetClickAction(() =>
            {
                FindObjectOfType<DialogueRunner>().StartDialogue("Ending.Continue");
                Destroy(textButton.gameObject);
            });
        }
    }
}
