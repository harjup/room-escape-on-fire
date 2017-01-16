using System.Collections;
using System.Collections.Generic;
using Assets.Core.Code;
using UnityEngine;

public class PizzaTracker : MonoBehaviour
{
    [AwaitableYarnCommand("pizza")]
    public IEnumerator PlayPizzaTracker()
    {
        var room = FindObjectOfType<Room>();
        var pizzaTrackerPrefab = Resources.Load<GameObject>("Prefabs/OneOffs/Pizza_Gauge");
        var res = Instantiate(pizzaTrackerPrefab);
        res.transform.parent = room.transform;
        yield return new WaitForSeconds(8f);
        Destroy(res);
    }

    [AwaitableYarnCommand("police")]
    public IEnumerator PlayPoliceTracker()
    {
        var room = FindObjectOfType<Room>();
        var pizzaTrackerPrefab = Resources.Load<GameObject>("Prefabs/OneOffs/Police_Gauge");
        var res = Instantiate(pizzaTrackerPrefab);
        res.transform.parent = room.transform;
        yield return new WaitForSeconds(10f);
        Destroy(res);
    }
}
