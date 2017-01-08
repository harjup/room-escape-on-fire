using System;
using UnityEngine;
using System.Collections;

public class CursorTracker : MonoBehaviour
{
    public bool CursorInLeftScrollRange()
    {
        var leftRange = Screen.width* _scrollPercentage;
        return _cursorX <= leftRange;
    }

    public bool CursorInRightScrollRange()
    {
        var rightRange = Screen.width - (Screen.width * _scrollPercentage);
        return _cursorX >= rightRange;
    }

    private float _scrollPercentage = .1f;
    private float _cursorX;
    
    void Update()
    {
        _cursorX = Input.mousePosition.x;
    }
}
