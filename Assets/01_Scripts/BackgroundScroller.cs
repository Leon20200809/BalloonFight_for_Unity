using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [Header("スクロール速度")]
    public float scrollSpeed;

    [Header("スクロール終点")]
    public float stopPos;
    [Header("スクロール再始点")]
    public float restartPos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(-scrollSpeed, 0, 0);

        if (transform.position.x < stopPos)
        {
            transform.position = new Vector2(restartPos, 0);
        }
    }
}
