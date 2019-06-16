using System;
using UnityEngine;
using System.Collections;
using Assets.Core.Code;
using Yarn;
using Yarn.Unity;

public class DeathTimer : MonoBehaviour
{
    private SuperTextMesh _deathDescription;
    private SuperTextMesh _timerValue;

    private DialogueRunner _dialogRunner;

    public int MaxSecondsRemaining = 180;

    private int _secondsRemaining;
    private IEnumerator _timerRoutine;

    private bool _isTutorial;

    void Start ()
    {
        _deathDescription = transform.Find("DeathDescription").GetComponent<SuperTextMesh>();
        _timerValue = transform.Find("TimerValue").GetComponent<SuperTextMesh>();

        _dialogRunner = FindObjectOfType<DialogueRunner>();

        _deathDescription.gameObject.SetActive(false);
        _timerValue.gameObject.SetActive(false);
    }

    [YarnCommand("start")]
    public void StartTimer()
    {
        _isTutorial = false;
        InitTimer();
    }

    [YarnCommand("start-tutorial")]
    public void StartTutorialTimer()
    {
        _isTutorial = true;
        InitTimer();
    }

    // Not a great name but w/e. Don't want to add args to startTimer
    private void InitTimer()
    {
        _deathDescription.gameObject.SetActive(true);
        _timerValue.gameObject.SetActive(true);

        _deathDescription.Text = "<c=fire><j>MINUTES BEFORE DEATH BY FLAME</c>";

        _secondsRemaining = MaxSecondsRemaining;

        _timerRoutine = DeathCountdown();

        StartCoroutine(_timerRoutine);
    }

    [AwaitableYarnCommand("add")]
    public IEnumerator AddTimer(string seconds)
    {
        var val = int.Parse(seconds);

        StopCoroutine(_timerRoutine);

        var goalValue = _secondsRemaining + val;
        
        yield return StartCoroutine(ModifyDeathCounter(goalValue));

        _timerRoutine = DeathCountdown();
        StartCoroutine(_timerRoutine);
    }

    [AwaitableYarnCommand("run-down")]
    public IEnumerator RunDownTimer()
    {
        StopCoroutine(_timerRoutine);
        yield return StartCoroutine(ModifyDeathCounter(1));
    }

    [YarnCommand("stop")]
    public void StopTimer()
    {
        if (_timerRoutine != null)
        {
            StopCoroutine(_timerRoutine);
            _timerRoutine = null;
        }

        _deathDescription.Text = "";
        _timerValue.Text = "";
    }


    private void SetTimerText(int seconds)
    {
        var timespan = TimeSpan.FromSeconds(_secondsRemaining);
        _timerValue.Text = string.Format("<c=fire>{0:D1}:{1:D2}</c>", timespan.Minutes, timespan.Seconds);

        var variableStorage = FindObjectOfType<SimpleVariableStorage>();
        variableStorage.SetValue("$time_remaining", new Value(string.Format("{0:D1}:{1:D2}", timespan.Minutes, timespan.Seconds)));
        variableStorage.SetValue("$seconds_remaining", new Value((float)_secondsRemaining));
    }

    private IEnumerator DeathCountdown()
    {
        SetTimerText(_secondsRemaining);

        while (_secondsRemaining > 0)
        {
            yield return new WaitForSeconds(1f);
            _secondsRemaining -= 1;

            if (_secondsRemaining == 0 && _isTutorial)
            {
                _secondsRemaining = 1;
            }

            SetTimerText(_secondsRemaining);
        }

        // Initiate time ran out ending
        _deathDescription.Text = "<c=fire>YOU ARE DEAD</c>";
        _dialogRunner.StartDialogue("Living.Time_Ran_Out");
    }

    private IEnumerator ModifyDeathCounter(int seconds)
    {
        var secondsToApply = Math.Abs(seconds - _secondsRemaining);
 
        while (secondsToApply > 0)
        {
            yield return new WaitForSeconds(.025f);
            
            _secondsRemaining += 1 * (Math.Sign(seconds - _secondsRemaining));

            secondsToApply -= 1;

            SetTimerText(_secondsRemaining);
        }
    }
}
