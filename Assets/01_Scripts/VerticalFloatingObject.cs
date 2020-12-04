using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class VerticalFloatingObject : MonoBehaviour
{
    public float moveTime;
    public float moveRange;

    // Start is called before the first frame update
    void Start()
    {
        transform.DOMoveY(transform.position.y - moveRange, moveTime).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
    }
}
