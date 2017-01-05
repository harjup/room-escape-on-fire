using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class TextDisplayGui : MonoBehaviour
{

    private GameObject _normalDialogueWindow;
    private TextCrawler _textCrawler;
    private DialogueWindowText _dialogueWindowText;
    private Text _dialogueWindowName;
    private GameObject _dialogChoicePrefab;


    private GameObject _activeDialogueWindow;
    // Gonna be lazy about initialization with lazy initialization. Can't trust start to run first anyways.
//    private GameObject NormalNormalDialogueWindow 
//    { 
//        get
//        {
//            if (_normalDialogueWindow == null)
//            {
//                return _normalDialogueWindow = transform.FindChild("Normal").gameObject;
//            } 
//            return _normalDialogueWindow;
//        } 
//    }

    private TextCrawler TextCrawler
    {
        get
        {
            if (_textCrawler == null)
            {
                return _textCrawler = gameObject.GetComponent<TextCrawler>();
            } 
            return _textCrawler;
        }
    }

    private DialogueWindowText DialogueWindowText
    {
        get
        {
            //            if (_dialogueWindowText == null)
            //            {
            //                return _dialogueWindowText = gameObject.GetComponentInChildren<DialogueWindowText>();
            //            }
            //
            //            return _dialogueWindowText;

            if (_activeDialogueWindow == null)
            {
                return null;
            }

            return _activeDialogueWindow.GetComponentInChildren<DialogueWindowText>(true);
        }
    }

    private GameObject DialogChoicePrefab
    {
        get
        {
            if (_dialogChoicePrefab == null)
            {
                return _dialogChoicePrefab = Resources.Load<GameObject>("Menu/DialogChoiceButton");
            }

            return _dialogChoicePrefab;
        }
    }

    public void Awake()
    {
        StartCoroutine(CrawlText("THIS IS A TEST WOWWWWW!!!", () => { }));

        // NormalNormalDialogueWindow.SetActive(false);
    }
    
    // This is an IEnumerator so we can wait for animations
    public IEnumerator ShowDialogueWindow()
    {
        // Do a tween animation and wait for it to finish

        // For now just make it draw
       // _activeDialogueWindow.SetActive(true);

        yield return null;
    }


    public void SetName(string name)
    {
        var nameWindow = _activeDialogueWindow.transform.FindChild("NameText");

        if (nameWindow != null)
        {
            nameWindow.GetComponent<SuperTextMesh>().text = name;
        }
    }

    public void HideName()
    {
        //TODO: Implement this
    }

    public IEnumerator CrawlText(string text, Action callback)
    {
        var textWindow = transform.FindChild("MainTextBackground").FindChild("MainText");
        var tmpText = textWindow.GetComponent<SuperTextMesh>();

//        yield return new WaitForSeconds(3f);
        tmpText.Text = text;

        tmpText.RegularRead();

        while (tmpText.reading)
        {   
            yield return new WaitForEndOfFrame();
        }

        callback();
    }

    public void SkipTextCrawl()
    {
//        if (TextCrawler._inProcess)
//        {
//            TextCrawler.SkipToEnd();
//        }
    }

    public IEnumerator HideDialogWindow()
    {
        var text = DialogueWindowText;
        if (text != null)
        {
            text.SetText("");
        }
        
        TextCrawler.Clear();

        // We may not want to hide the window, just leave it up for now
        // _activeDialogueWindow.SetActive(false);

        yield return null;
    }


    public void ShowChoices(List<string> choices, Action<int> onChoice)
    {
        //TODO: Make this safer~
        var initialPosition = GameObject.Find("ChoiceInitialPosition").transform.position;

        // TODO: Refactor b/c this is copy pasted
        var buttons = new List<GameObject>();
        for (int i = 0; i < choices.Count; i++)
        {
            var choice = choices[i];
            var button = Instantiate(DialogChoicePrefab);
            button.transform.parent = transform.parent.Find("Buttons");
            button.transform.localScale = Vector3.one; // The scale is getting messed up for some reason??

            var screenHeightRefRes = 1080f;
            //button.transform.position = new Vector2(initialPosition.x, initialPosition.y - ((Screen.height / 6f) * i));
            button.transform.localPosition = new Vector2(initialPosition.x, initialPosition.y - ((screenHeightRefRes / 6f) * i));
            button.GetComponentInChildren<Text>().text = choice;

            // So, it turns out this is an issue with the old shitty version of mono unity uses
            // TODO: We can just directly plug i into the callback after Unity 5.5
            var choiceIndex = i; // Curse you C# mutability!

            buttons.Add(button);

            button.GetComponent<Button>()
                .onClick
                .AddListener(() =>
                {
                    CleanUpButtons(buttons); // This is gross, but should be called when all the buttons are in the list.
                    onChoice(choiceIndex);
                });
        }
    }

    public IEnumerator ShowChoicesAndWait(List<string> choices, Action<int> onChoice)
    {
        var waitingForChoice = true;
        ShowChoices(choices, i =>
        {
            onChoice(i);
            waitingForChoice = false;
        });

        while (waitingForChoice)
        {
            yield return null;
        }
    }



    private void CleanUpButtons(List<GameObject> buttons)
    {
        foreach (var button in buttons)
        {
            Destroy(button);
        }
    }

}
