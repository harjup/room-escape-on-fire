using UnityEngine;
using System.Collections;
using Yarn.Unity;

public class Ending : MonoBehaviour
{
    public void SetSprite(string endingName)
    {
        GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Prefabs/EndingImages/" + endingName);
    }
    
    private bool spawnedContinueButton = false;
    public void Update()
    {
        if (!spawnedContinueButton &&!FindObjectOfType<DialogueRunner>().isDialogueRunning)
        {
            var buttonPrefab = Resources.Load<GameObject>("Prefabs/Text/SuperTextButton");
            var continuePos = transform.FindChild("ContinuePos");
            var button = Instantiate(buttonPrefab, continuePos) as GameObject;

            button.GetComponent<SuperTextMesh>().anchor = TextAnchor.MiddleCenter;
            button.GetComponent<SuperTextMesh>().alignment = SuperTextMesh.Alignment.Center;
            button.transform.position = continuePos.position;
            
            var textButton = button.GetComponent<SuperTextButton>();
            textButton.SetText("<w>Continue?</w>");
            textButton.SetClickAction(() => {Debug.Log("CONTINUE!!!!");});

            spawnedContinueButton = true;
        }
    }
}
