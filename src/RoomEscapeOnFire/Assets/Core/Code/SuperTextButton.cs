﻿using System;
using UnityEngine;
using System.Collections;

public class SuperTextButton : MonoBehaviour
{
    public Color DefaultColor = Color.white;
    private SuperTextMesh _textMesh;

    private BoxCollider2D _collider;

    private Action _clickAction;

    private void Start()
    {
        _textMesh = GetComponent<SuperTextMesh>();
        if (_collider == null)
        {
            _collider = gameObject.AddComponent<BoxCollider2D>();
        }
    }

    public void SetText(string text)
    {
        if (_textMesh == null)
        {
            _textMesh = GetComponent<SuperTextMesh>();
        }

        _textMesh.Text = text;
        _textMesh.color = DefaultColor;
        _textMesh.Rebuild();
        if (_collider != null)
        {
            Destroy(_collider);
        }

        _collider = gameObject.AddComponent<BoxCollider2D>();
    }

    public void SetClickAction(Action clickAction)
    {
        _clickAction = clickAction;
    }

    private void OnMouseDown()
    {
        _textMesh.color = Color.black;
        _textMesh.Rebuild();
    }

    private void OnMouseUpAsButton()
    {
        _textMesh.color = DefaultColor;
        if (_clickAction != null)
        {
           _clickAction();
        }
    }

    private void OnMouseUp()
    {
        _textMesh.color = DefaultColor;
        _textMesh.Rebuild();
    }

    private void OnMouseExit()
    {
        _textMesh.color = DefaultColor;
        _textMesh.Rebuild();
    }

    private void OnMouseEnter()
    {
        _textMesh.color = Color.grey;
        _textMesh.Rebuild();
    }
}
