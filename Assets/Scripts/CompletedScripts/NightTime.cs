using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightTime : MonoBehaviour
{
    public float levelTime = 100f;
    float currentTime;
    SpriteRenderer spriteRenderer;
    UIFunctions uiMenu;

    public List<Sprite> skySprites = new List<Sprite>();
    int spriteToCall
    {
        get { return (int)((currentTime / levelTime) * skySprites.Count); }
    }

    // Start is called before the first frame update
    void Start()
    {
        currentTime = 0;

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = skySprites[spriteToCall];
        uiMenu = FindObjectOfType<UIFunctions>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!uiMenu.uiON)
        {
            currentTime += Time.deltaTime;

            if (currentTime >= levelTime)
            {
                uiMenu.OpenFailScreen();
                return;
            }

            spriteRenderer.sprite = skySprites[spriteToCall];
        }
    }

    public void DecreaseCurrentTime(float decreaseAmount, float decreaseTime)
    {
        StartCoroutine(DecreaseTimeOverSeconds(decreaseAmount, decreaseTime));
    }

    IEnumerator DecreaseTimeOverSeconds(float decreaseAmount, float decreaseTime)
    {
        float timeStarted = Time.time;
        while (Time.time - timeStarted < decreaseTime)
        {
            currentTime -= (decreaseAmount / decreaseTime) * Time.deltaTime;
            if (currentTime < 0)
            {
                currentTime = 0;
            }
            yield return 0;
        }
    }
}
