using System;
using UnityEngine;
using System.Collections;

public class SuperTextButton : MonoBehaviour
{
    private SuperTextMesh _textMesh;

    private BoxCollider2D _collider;

    private Action _clickAction;

    private void Start()
    {
        _textMesh = GetComponent<SuperTextMesh>();
        if (_collider != null)
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
        if (_collider == null)
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
    }

    private void OnMouseUpAsButton()
    {
        _textMesh.color = Color.white;
        if (_clickAction != null)
        {
           _clickAction();
        }
    }

    private void OnMouseUp()
    {
        _textMesh.color = Color.white;
    }

    private void OnMouseExit()
    {
        _textMesh.color = Color.white;
    }

    private void OnMouseEnter()
    {
        _textMesh.color = Color.grey;
    }
}
