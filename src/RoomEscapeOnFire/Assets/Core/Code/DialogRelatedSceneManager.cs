using System.Collections;
using Assets.Core.Code;
using UnityEngine;

public class DialogRelatedSceneManager : MonoBehaviour
{
    private GameObject _currentScene;
    private GameObject _livingRoomPrefab;


    void Start()
	{
	    var closetPrefab = Resources.Load<GameObject>("Prefabs/Rooms/Closet");
        _livingRoomPrefab = Resources.Load<GameObject>("Prefabs/Rooms/Living");
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

        SetupScene(_livingRoomPrefab);

        yield return StartCoroutine(SceneFadeInOut.Instance.FadeToClear());
    }
}
