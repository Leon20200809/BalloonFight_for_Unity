using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    PlayerController player;
    Rigidbody2D rb;
    public float shotSpeed;
    public float knockbackPower;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        GunShot();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GunShot()
    {
        this.gameObject.transform.parent = null;
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2 (player.transform.localScale.x * shotSpeed, rb.velocity.y);
        Destroy(gameObject, 5f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 接触したコライダーを持つゲームオブジェクトのTagがEnemyなら 
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "BossEnemy")
        {
            int destroyPoint = 0;
            if (collision.gameObject.tag == "Enemy")
            {
                destroyPoint = collision.gameObject.GetComponent<VerticalFloatingObject>().point;

            }
            else if (collision.gameObject.tag == "BossEnemy")
            {
                destroyPoint = collision.gameObject.GetComponent<Toko_EnemyMovement>().point;

            }
            destroyPoint = destroyPoint - 200;
            Debug.Log(destroyPoint);

            // TODO 攻撃ポイント加算
            player.coinPoint += destroyPoint;
            player.uIManager.UpdateDisplayScore(player.coinPoint);
            Debug.Log(player.coinPoint);

        }

    }


}
