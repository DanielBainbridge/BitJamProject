using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class LevelComplete : MonoBehaviour
{
    SpriteRenderer spriteRenderer = null;
    public Sprite startSprite;
    public Sprite endSprite;
    UIFunctions uiMenu;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = startSprite;
        uiMenu = FindObjectOfType<UIFunctions>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController playerController = other.GetComponent<PlayerController>();
        if (playerController != null)
        {
            spriteRenderer.sprite = endSprite;
            uiMenu.OpenWinScreen();
        }
    }
}
