using UnityEngine;
using System.Collections;
using Yarn.Unity;

public class Ending : MonoBehaviour
{
    public void SetSprite(string endingName)
    {
        GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Prefabs/EndingImages/" + endingName);
    }
}
