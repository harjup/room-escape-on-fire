using UnityEngine;
using System.Collections;
using DG.Tweening;
using Yarn.Unity;

public class ScrollableBackground : MonoBehaviour
{
    private CursorTracker _cursorTracker;
    private DialogueRunner _dialogueRunner;

    private float leftEdge = 7.25f;
    private float rightEdge = -7.16f;
    

    void Awake()
	{
	    _cursorTracker = FindObjectOfType<CursorTracker>();
        _dialogueRunner = FindObjectOfType<DialogueRunner>();

	}

	void Update()
    {
	    if (_dialogueRunner.isDialogueRunning)
	    {
	        return;
	    }

	    if (_cursorTracker.CursorInLeftScrollRange() && transform.localPosition.x < leftEdge)
	    {
	        transform.localPosition = transform.localPosition.AddX(Time.smoothDeltaTime * 3f);
	    }

        if (_cursorTracker.CursorInRightScrollRange() && transform.localPosition.x > rightEdge)
        {
            transform.localPosition = transform.localPosition.AddX(Time.smoothDeltaTime * -3f);
        }

    }
}
