using System.Collections;
using Assets.Core.Code;
using UnityEngine;
using Yarn;
using Yarn.Unity;

public class DialogRelatedSceneManager : MonoBehaviour
{
    private GameObject _currentScene;
    private GameObject _closetPrefab;
    private GameObject _trueClosetPrefab;
    private GameObject _livingRoomPrefab;
    private GameObject _endingPrefab;

    void Start()
	{
	    _closetPrefab = Resources.Load<GameObject>("Prefabs/Rooms/Closet");
        _trueClosetPrefab = Resources.Load<GameObject>("Prefabs/Rooms/True-Closet");
        _livingRoomPrefab = Resources.Load<GameObject>("Prefabs/Rooms/Living");
        _endingPrefab = Resources.Load<GameObject>("Prefabs/Rooms/Ending");
        SetupScene(_closetPrefab);
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

        // If we came from the ending screen clean it up
        var endingProgress = FindObjectOfType<EndingProgress>();
        if (endingProgress != null)
        {
            Destroy(endingProgress.gameObject);
        }
        
        FindObjectOfType<SimpleVariableStorage>().ResetToDefaults();

        // Kinda hackish, we want to set the is_epilogue after we clear everything else out so we can go down the epilogue route
        // TODO If we had a variable storage with multiple roots, or allowed "permanent" values, this wouldn't be a problem
        if (FindObjectOfType<EndingProgressStore>().HasAllEndings())
        {
            FindObjectOfType<SimpleVariableStorage>().SetValue("$is_epilogue", new Value(true)); 
        }

        SetupScene(_livingRoomPrefab);

        yield return StartCoroutine(SceneFadeInOut.Instance.FadeToClear());
    }

    [AwaitableYarnCommand("ending")]
    public IEnumerator Ending(string endingName)
    {
        FindObjectOfType<DeathTimer>().StopTimer();

        FindObjectOfType<EndingProgressStore>().AddEnding(endingName);

        yield return StartCoroutine(SceneFadeInOut.Instance.FadeToBlack());

        SetupEnding(endingName);

        yield return StartCoroutine(SceneFadeInOut.Instance.FadeToClear());
    }

    [AwaitableYarnCommand("closet")]
    public IEnumerator Closet()
    {
        yield return StartCoroutine(SceneFadeInOut.Instance.FadeToBlack());

        SetupScene(_trueClosetPrefab);

        yield return StartCoroutine(SceneFadeInOut.Instance.FadeToClear());
    }
}
