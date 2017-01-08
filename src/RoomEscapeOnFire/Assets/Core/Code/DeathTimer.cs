using System;
using UnityEngine;
using System.Collections;
using Yarn.Unity;

public class DeathTimer : MonoBehaviour
{
    private SuperTextMesh _deathDescription;
    private SuperTextMesh _timerValue;

    private DialogueRunner _dialogRunner;

    public int MaxSecondsRemaining = 180;

    private int _secondsRemaining;
    private IEnumerator _timerRoutine;

    void Start ()
    {
        _deathDescription = transform.FindChild("DeathDescription").GetComponent<SuperTextMesh>();
        _timerValue = transform.FindChild("TimerValue").GetComponent<SuperTextMesh>();

        _dialogRunner = FindObjectOfType<DialogueRunner>();

        _deathDescription.gameObject.SetActive(false);
        _timerValue.gameObject.SetActive(false);
    }

    [YarnCommand("start")]
    public void StartTimer()
    {
        _deathDescription.gameObject.SetActive(true);
        _timerValue.gameObject.SetActive(true);

        _deathDescription.Text = "<c=fire><j>MINUTES BEFORE DEATH BY FLAME</c>";
        
        _secondsRemaining = MaxSecondsRemaining;
        _timerRoutine = DeathCountdown();

        StartCoroutine(_timerRoutine);
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
    }

    private IEnumerator DeathCountdown()
    {
        SetTimerText(_secondsRemaining);

        while (_secondsRemaining > 0)
        {
            yield return new WaitForSeconds(1f);
            _secondsRemaining -= 1;

            SetTimerText(_secondsRemaining);
        }

        // Initiate time ran out ending
        _deathDescription.Text = "<c=fire>YOU ARE DEAD</c>";
        _dialogRunner.StartDialogue("Living.Time_Ran_Out");
    }
}
