using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomObjectGenerator : MonoBehaviour
{
    [SerializeField]
    GameObject[] objPrefab;

    [SerializeField]
    Transform generateTran;

    [Header("生成までの待機時間")]
    public Vector2 waitTimeRange;

    float waitTime;
    float timer;

    bool isActivate;
    GameDirector gameDirector;

    void Start()
    {
        SetGenerateTime();
    }

    /// <summary>
    /// 生成までの時間を設定
    /// </summary>
    private void SetGenerateTime()
    {
        // 生成までの待機時間を、最小値と最大値の間からランダムで設定
        waitTime = Random.Range(waitTimeRange.x, waitTimeRange.y);
    }

    void Update()
    {
        Debug.Log(isActivate);
        if (!isActivate) return;
        
        // 計測用タイマーを加算
        timer += Time.deltaTime;

        // 計測用タイマーが待機時間と同じか超えたら
        if (timer >= waitTime)
        {
            // タイマーをリセットして、再度計測できる状態にする
            timer = 0;

            // ランダムなオブジェクトを生成
            RandomGenerateObject();
        }
    }

    /// <summary>
    /// ランダムなオブジェクトを生成
    /// </summary>
    private void RandomGenerateObject()
    {

        // 生成するプレファブの番号をランダムに設定
        int randomIndex = Random.Range(0, objPrefab.Length);

        // プレファブを元にクローンのゲームオブジェクトを生成
        GameObject obj = Instantiate(objPrefab[randomIndex], generateTran);

        // ランダムな値を取得
        float randomPosY = Random.Range(-2.0f, 4.0f);

        // 生成されたゲームオブジェクトのY軸にランダムな値を加算して、生成されるたびに高さの位置を変更する
        obj.transform.position = new Vector2(obj.transform.position.x, obj.transform.position.y + randomPosY);

        // 次の生成までの時間をセットする
        SetGenerateTime();
    }


    /// <summary>
    /// 生成状態のオン/オフを切り替え
    /// </summary>
    /// <param name="isSwitch"></param>
    public void SwitchActivation(bool isSwitch)
    {
        isActivate = isSwitch;
    }

}
