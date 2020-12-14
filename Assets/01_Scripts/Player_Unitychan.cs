using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Unitychan : PlayerController
{
    [SerializeField]
    Transform BulletTran;

    [SerializeField]
    Bullet bulletPrefab;

    [SerializeField]
    Sword swordPrefab;

    protected override void Update()
    {
        if (isGameOver) return;
        if (isGameStart) return;
        LimitArea();
        Is_ground_decision_enabled();
        BallonRecaver();
        if (ballons[0] != null) Jump();
        Attack();
    }

    protected void Attack()
    {
        if (Input.GetKey(KeyCode.P) || (Input.GetMouseButtonDown(0)))
        {
            if(in_proximity_attack_range == true)
            {
                //近接攻撃アニメーション
                Debug.Log("近接攻撃");
                anim.SetTrigger("Attack_B");
            }
            else
            {
                //遠距離攻撃アニメーション
                Debug.Log("遠距離攻撃");
                anim.SetTrigger("Attack_A");
            }
        }
    }

    public void GunshotAttack()
    {

    }
    public void SwordAttack()
    {

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "in_proximity_attack_range")
        {
            in_proximity_attack_range = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "in_proximity_attack_range")
        {
            in_proximity_attack_range = false;
        }
    }

}
