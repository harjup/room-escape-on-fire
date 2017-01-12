using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class SwitchSprite : MonoBehaviour
{
    public Sprite NextSprite;

    [YarnCommand("switch-sprite")]
    public void DoTheSwitchSprite()
    {
        var spriteRenderer = GetComponent<SpriteRenderer>();
        if (NextSprite == null)
        {
            spriteRenderer.enabled = false;
        }

        spriteRenderer.sprite = NextSprite;
    }

    [YarnCommand("disable")]
    public void DisableCollisions()
    {
        var collider = GetComponent<BoxCollider2D>();
        collider.enabled = false;
    }
}
