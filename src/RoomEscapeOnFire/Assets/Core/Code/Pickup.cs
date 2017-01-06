using UnityEngine;
using System.Collections;
using Yarn.Unity;

public class Pickup : MonoBehaviour
{
    [YarnCommand("pickup")]
    public void GetPickedUp()
    {
        // Todo... other things???

        Destroy(gameObject);
    }
}
