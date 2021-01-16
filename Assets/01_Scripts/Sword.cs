using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        Debug.Log("近接攻撃");
        Destroy(this.gameObject, 0.5f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 接触したコライダーを持つゲームオブジェクトのTagがEnemyなら 
        if (collision.gameObject.tag == "Enemy")
        {
            int destroyPoint = collision.gameObject.GetComponent<VerticalFloatingObject>().point;
            Debug.Log(destroyPoint);

            // TODO 攻撃ポイント加算
            int p = player.gameObject.GetComponent<PlayerController>().coinPoint += destroyPoint;
            player.gameObject.GetComponent<PlayerController>().uIManager.UpdateDisplayScore(p);
            Debug.Log(p);


            // TODO 風船回復
            player.gameObject.GetComponent<PlayerController>().GenerateBallonEnemyDestory();
        }

        // 接触したコライダーを持つゲームオブジェクトのTagがBossEnemyなら 
        if (collision.gameObject.tag == "BossEnemy")
        {
            int destroyPoint = collision.gameObject.GetComponent<Toko_EnemyMovement>().point;
            destroyPoint = destroyPoint - 200;
            Debug.Log(destroyPoint);

            // TODO 攻撃ポイント加算
            int p = player.gameObject.GetComponent<PlayerController>().coinPoint += destroyPoint;
            player.gameObject.GetComponent<PlayerController>().uIManager.UpdateDisplayScore(p);
            Debug.Log(p);

            // TODO 風船回復
            player.gameObject.GetComponent<PlayerController>().GenerateBallonEnemyDestory();

        }

    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
