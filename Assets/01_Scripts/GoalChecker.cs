using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalChecker : MonoBehaviour
{
    public float moveSpeed = 0.01f;     

    float stopPos = 6.5f; 

    public bool isGoal;

    void Update()
    {
        // 停止地点に到達するまで移動する
        if (transform.position.x > stopPos)
        {
            transform.position += new Vector3(-moveSpeed, 0, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {

        // 接触した(ゴールした)際に１回だけ判定する
        if (col.gameObject.tag == "Player" && isGoal == false)
        {
            isGoal = true;
            Debug.Log(col.name);

            // PlayerControllerの情報を取得
            PlayerController playerController = col.gameObject.GetComponent<PlayerController>();
            playerController.isGameStart = true;

            // PlayerControllerの持つ、UIManagerの変数を利用して、GenerateResultPopUpメソッドを呼び出す。引数にはPlayerControllerのcoinCountを渡す
            playerController.uIManager.GenerateResultPopUp(playerController.coinPoint);

        }
    }

}
