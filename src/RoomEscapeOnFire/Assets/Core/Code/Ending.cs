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
            var altEndPrefab = Resources.Load<GameObject>("Prefabs/Rooms/Ending-Progress");
            var button = Instantiate(altEndPrefab);
            Destroy(gameObject);
        }
    }
}
