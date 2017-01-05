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

        Instantiate(scenePrefab, Vector3.zero, Quaternion.identity);
    }
}
