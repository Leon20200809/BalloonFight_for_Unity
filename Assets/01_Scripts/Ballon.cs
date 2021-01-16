using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ballon : MonoBehaviour
{
    PlayerController playerController;
    Tweener tweener;

    public void SetUpBallon(PlayerController playerController)
    {
        this.playerController = playerController;

        //元スケール
        Vector3 scale = transform.localScale;

        //スケール0
        transform.localScale = Vector3.zero;
        //徐々に膨らむ
        transform.DOScale(scale, 2.0f).SetEase(Ease.InBounce);
        //左右に動かす
        tweener = transform.DOLocalMoveX(0.22f, 0.2f).SetEase(Ease.Flash).SetLoops(-1, LoopType.Yoyo);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            tweener.Kill();

            playerController.DestroyBallon(this);
        }
    }
}
