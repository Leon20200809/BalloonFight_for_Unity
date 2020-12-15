using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    GameObject player;
    Rigidbody2D rb;
    public float shotSpeed;
    public float knockbackPower;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
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
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log(collision);
            Destroy(gameObject);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log(collision);
            Destroy(gameObject);
        }

    }
}
