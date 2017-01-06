using UnityEngine;

public class DialogRelatedSceneManager : MonoBehaviour
{
    private GameObject _currentScene;

	void Start()
	{
	    var closetPrefab = Resources.Load<GameObject>("Prefabs/Rooms/Closet");
        SetupScene(closetPrefab);
	}

    public void SetupScene(GameObject scenePrefab)
    {
        if (_currentScene != null)
        {
            Destroy(_currentScene);
        }

        var result = Instantiate(scenePrefab, Vector3.zero, Quaternion.identity);

        // Undoes Unity appending (Clone) to the end of the instantiated object
        result.name = scenePrefab.name;
    }
}
