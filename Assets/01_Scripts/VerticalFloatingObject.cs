﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class VerticalFloatingObject : MonoBehaviour
{
    public float moveTime;
    public float moveRange;
    public GameObject exPrefab;

    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        //transform.LookAt(player.transform);
        transform.DOMoveY(transform.position.y - moveRange, moveTime).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 接触したコライダーを持つゲームオブジェクトのTagがEnemyなら 
        if (collision.gameObject.tag == "PlayerAtk")
        {
            transform.DOMoveY(transform.position.y - 15, 2).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
            GameObject ex = Instantiate(exPrefab, transform.position, Quaternion.identity);
            Destroy(ex, 1.5f);
            Destroy(this.gameObject, 3f);
        }
    }



}
