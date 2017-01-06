using System;
using UnityEngine;
using Yarn.Unity;

public class Clickable : MonoBehaviour
{
    public string TextNode;
    public string RoomName;

    private string _fullNodeName;

    void Start()
    {
        RoomName = GetComponentInParent<Room>().GetName();

        if (RoomName == null)
        {
            Debug.LogError("Clickable object '" + name + "' could not find a parent with a room component on it!!!");
        }

        _fullNodeName = RoomName + "." + TextNode;
    }

    public void OnMouseUpAsButton()
    {
        var dialogRunner = FindObjectOfType<DialogueRunner>();

        // You can only click on something if you're not in the middle of a dialog!!!
        if (!dialogRunner.isDialogueRunning)
        {
            dialogRunner.StartDialogue(_fullNodeName);
        }   
    }
}
