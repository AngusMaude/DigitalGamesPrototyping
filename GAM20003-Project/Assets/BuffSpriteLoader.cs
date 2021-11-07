using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffSpriteLoader : MonoBehaviour
{
    private GameManager gameManager;
    private SpriteRenderer spriteRenderer;
    void Start()
    {
        gameManager = FindObjectOfType(typeof(GameManager)) as GameManager;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        spriteRenderer.sprite = gameManager.GetBuffPlayer().GetComponent<SpriteRenderer>().sprite;
    }
}
