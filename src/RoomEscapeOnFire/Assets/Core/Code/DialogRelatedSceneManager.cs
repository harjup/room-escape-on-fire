using System.Collections;
using Assets.Core.Code;
using UnityEngine;

public class DialogRelatedSceneManager : MonoBehaviour
{
    private GameObject _currentScene;
    private GameObject _roomPrefab;


    void Start()
	{
	    var closetPrefab = Resources.Load<GameObject>("Prefabs/Rooms/Closet");
        _roomPrefab = Resources.Load<GameObject>("Prefabs/Rooms/Room");
        SetupScene(closetPrefab);
	}

    public void SetupScene(GameObject scenePrefab)
    {
        if (_currentScene != null)
        {
            Destroy(_currentScene);
        }

        _currentScene = Instantiate(scenePrefab, Vector3.zero, Quaternion.identity) as GameObject;

        // Undoes Unity appending (Clone) to the end of the instantiated object
        _currentScene.name = scenePrefab.name;
    }

    [AwaitableYarnCommand("transition")]
    public IEnumerator Transition()
    {
        yield return StartCoroutine(SceneFadeInOut.Instance.FadeToBlack());

        SetupScene(_roomPrefab);

        yield return StartCoroutine(SceneFadeInOut.Instance.FadeToClear());
    }
}
