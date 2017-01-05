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

    private SuperTextMesh _superText;
    private SuperTextMesh SuperText
    {
        get
        {
            if (_superText == null)
            {
                var textWindow = transform.FindChild("MainText");
                return _superText = textWindow.GetComponent<SuperTextMesh>();
            }

            return _superText;
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

    // This is an IEnumerator so we can wait for animations
    public IEnumerator ShowDialogueWindow()
    {
        // Do a tween animation and wait for it to finish

        // For now just make it draw
        SuperText.gameObject.SetActive(true);

        yield return null;
    }


    public void SetName(string name)
    {
        var nameWindow = _activeDialogueWindow.transform.FindChild("Name");

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
        SuperText.Text = text;

        SuperText.RegularRead();

        while (SuperText.reading)
        {   
            yield return new WaitForEndOfFrame();
        }

        callback();
    }

    public void SkipTextCrawl()
    {
        if (SuperText.reading)
        {
            SuperText.SkipToEnd();
        }
    }

    public IEnumerator HideDialogWindow()
    {
        SuperText.UnRead();

        while (SuperText.unreading)
        {
            yield return new WaitForEndOfFrame();
        }

        SuperText.gameObject.SetActive(false);
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
