using System.Collections;
using Assets.Core.Code;
using UnityEngine;
using Yarn.Unity;

public class DialogRelatedSceneManager : MonoBehaviour
{
    private GameObject _currentScene;
    private GameObject _livingRoomPrefab;
    private GameObject _endingPrefab;

    void Start()
	{
	    var closetPrefab = Resources.Load<GameObject>("Prefabs/Rooms/Closet");
        _livingRoomPrefab = Resources.Load<GameObject>("Prefabs/Rooms/Living");
        _endingPrefab = Resources.Load<GameObject>("Prefabs/Rooms/Ending");
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

    public void SetupEnding(string endingSprite)
    {
        if (_currentScene != null)
        {
            Destroy(_currentScene);
        }

        _currentScene = Instantiate(_endingPrefab, Vector3.zero, Quaternion.identity) as GameObject;

        // Undoes Unity appending (Clone) to the end of the instantiated object
        _currentScene.name = _endingPrefab.name;
        
        _currentScene.GetComponent<Ending>().SetSprite(endingSprite);
    }

    [AwaitableYarnCommand("transition")]
    public IEnumerator Transition()
    {
        yield return StartCoroutine(SceneFadeInOut.Instance.FadeToBlack());

        SetupScene(_livingRoomPrefab);

        yield return StartCoroutine(SceneFadeInOut.Instance.FadeToClear());
    }

    [AwaitableYarnCommand("ending")]
    public IEnumerator Ending(string endingName)
    {
        FindObjectOfType<DeathTimer>().StopTimer();

        yield return StartCoroutine(SceneFadeInOut.Instance.FadeToBlack());

        SetupEnding(endingName);

        yield return StartCoroutine(SceneFadeInOut.Instance.FadeToClear());
    }

    //<<ending SceneManager Out_Of_Time>>
}
