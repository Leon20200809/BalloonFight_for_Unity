using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using DG.Tweening.Core;

public class Toko_EnemyMovement : MonoBehaviour
{
    public Vector3[] path;
    public float duration;

    Tween doPath;

    Animator animator;
    public GameObject exPrefab;
    GameDirector gameDirector;
    public float timeleft;
    public float atkTime;
    public int atkType;

    public int hp;
    public int point;

    public float moveSpeed = 0.01f;
    float stopPos = 6.5f;


    // Start is called before the first frame update
    void Start()
    {
        gameDirector = GameObject.FindWithTag("GameManager").GetComponent<GameDirector>();
        animator = GetComponent<Animator>();
        doPath = transform.DOPath(path, duration, PathType.CatmullRom).SetOptions(true).SetLoops(-1);
    }

    private void AttackTime()
    {
        atkTime = Random.Range(2.5f, 4.0f);
    }

    private void AttackType()
    {
        atkType = Random.Range(0, 5);
    }

    public void Attack_A()
    {
        animator.SetTrigger("Attack_A");
    }

    public void Attack_B()
    {
        animator.SetTrigger("Attack_B");
    }
    public void Attack_A_Action()
    {
        //攻撃生成
        Debug.Log("攻撃A");
    }

    public void Attack_B_Action()
    {
        //攻撃生成
        Debug.Log("攻撃B");
    }

    void OnTriggerEnter2D(Collider2D col)
    {

        // プレイヤー攻撃HIT時
        if (col.gameObject.tag == "PlayerAtk")
        {
            GameObject ex = Instantiate(exPrefab, transform.position, Quaternion.identity);
            Destroy(ex, 1.5f);

            animator.SetTrigger("Hurt");
            hp -= 1;
            Destroy(col.gameObject);

            if (hp <= 0)
            {
                gameDirector.GenerateGoal();
                doPath.Kill(true);
                transform.DOMoveY(transform.position.y - 15, 2).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
                Destroy(this.gameObject, 3f);


            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        // 停止地点に到達するまで移動する
        if (transform.position.x > stopPos)
        {
            transform.position += new Vector3(-moveSpeed, 0, 0);
        }

        timeleft -= Time.deltaTime;
        if (timeleft <= 0.0)
        {
            AttackTime();
            AttackType();
            timeleft = atkTime;
            Debug.Log("敵の攻撃");
            Debug.Log(atkTime);
            Debug.Log(atkType);

            if (atkType == 1)
            {
                Attack_B();
            }
            else
            {
                Attack_A();
            }
        }

    }


}
