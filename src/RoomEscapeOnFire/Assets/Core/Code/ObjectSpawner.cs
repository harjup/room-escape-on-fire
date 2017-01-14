using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

// There's a lot of :effort: going on in this class, that's for sure
public class ObjectSpawner : MonoBehaviour
{
    [YarnCommand("create-fire")]
    public void CreateFire()
    {
        var parent = GameObject.Find("Hintbot").transform;
        var firePrefab = Resources.Load<GameObject>("Prefabs/OneOffs/Fire");
        var res = Instantiate(firePrefab, parent);
        res.transform.position = parent.position.AddY(2f).AddZ(-1f);
        
        res.name = firePrefab.name;
    }

    [YarnCommand("remove-fire")]
    public void RemoveFire()
    {
        Destroy(GameObject.Find("Fire"));    
    }

    [YarnCommand("create-hole")]
    public void CreateHole()
    {
        var parent = GameObject.Find("Painting").transform;
        var holePrefab = Resources.Load<GameObject>("Prefabs/OneOffs/Hole");
        var res = Instantiate(holePrefab, parent);
        res.transform.position = parent.position;
        res.name = holePrefab.name;

        parent.GetComponent<SpriteRenderer>().enabled = false;
    }

    [YarnCommand("create-olaf")]
    public void CreateOlaf()
    {
        var parent = GameObject.Find("Fridge").transform;
        var targetPos = GameObject.Find("olaf-pos").transform;
        var olafPrefab = Resources.Load<GameObject>("Prefabs/OneOffs/Olaf");
        var res = Instantiate(olafPrefab, parent);
        res.transform.localPosition = targetPos.localPosition;
        res.name = olafPrefab.name;
    }
}
