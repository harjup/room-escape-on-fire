using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class ObjectSpawner : MonoBehaviour
{
    [YarnCommand("create-fire")]
    public void CreateFire()
    {
        var parent = GameObject.Find("Hintbot").transform;
        var firePrefab = Resources.Load<GameObject>("Prefabs/OneOffs/Fire");
        var res = Instantiate(firePrefab, parent) as GameObject;
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
    }

    [YarnCommand("create-olaf")]
    public void CreateOlaf()
    {
        var parent = GameObject.Find("Fridge").transform;
        var olafPrefab = Resources.Load<GameObject>("Prefabs/OneOffs/Olaf");
        var res = Instantiate(olafPrefab, parent);
        res.transform.position = parent.position + new Vector3(-2.61f, -4.76f, -1f);
        res.name = olafPrefab.name;
    }
}
