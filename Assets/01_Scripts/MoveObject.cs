using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveObject : MonoBehaviour
{
    [Header("移動速度")]
    public float moveSpeed;

    void Update()
    {
        transform.position += new Vector3(-moveSpeed, 0, 0);

        if (transform.position.x <= -18.0f)
        {
            Destroy(this.gameObject);
            Debug.Log("削除");
        }
    }
}
