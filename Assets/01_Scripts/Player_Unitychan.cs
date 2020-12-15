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

    //クールタイム用
    public float timeleftA;


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
        timeleftA -= Time.deltaTime;

        if (Input.GetKey(KeyCode.P) || (Input.GetMouseButtonDown(0)))
        {
            if (timeleftA <= 0.0)
            {
                if (in_proximity_attack_range == true)
                {
                    //近接攻撃アニメーション
                    anim.SetTrigger("Attack_B");
                    timeleftA = 1.0f;
                }
                else
                {
                    //遠距離攻撃アニメーション
                    anim.SetTrigger("Attack_A");
                    timeleftA = 2.0f;
                }

            }
        }
    }

    public void GunshotAttack()
    {
        rb.AddForce(transform.up * jumpPower * 0.5f);
        Bullet b = Instantiate(bulletPrefab, BulletTran);
    }
    public void SwordAttack()
    {
        rb.AddForce(transform.up * jumpPower * 0.5f);
        Sword s = Instantiate(swordPrefab, BulletTran);
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
