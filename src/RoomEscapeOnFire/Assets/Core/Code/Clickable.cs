using UnityEngine;
using System.Collections;

public class Clickable : MonoBehaviour
{
    public string TextNode;

    public void OnMouseUpAsButton()
    {
        // TODO: Fire event through something that checks for whether we're in a "cutscene"
        FindObjectOfType<Yarn.Unity.DialogueRunner>().StartDialogue(TextNode);
    }
}
