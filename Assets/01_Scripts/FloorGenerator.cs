using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorGenerator : MonoBehaviour
{
    [SerializeField]
    GameObject aerialFloorPrefab;

    [SerializeField]
    Transform generateTran;

    [Header("生成までの待機時間")]
    public float waitTime;
    float timer;

    GameDirector gameDirector;

    //生成判定
    bool isActivate;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(isActivate);
        if (!isActivate) return;

        timer += Time.deltaTime;

        if (timer >= waitTime)
        {
            timer = 0;

            //床生成
            GenerateFloor();
        }
    }

    /// <summary>
    /// 床生成
    /// </summary>
    void GenerateFloor()
    {

        // 空中床のプレファブを元にクローンのゲームオブジェクトを生成
        GameObject obj = Instantiate(aerialFloorPrefab, generateTran);

        // ランダムな値を取得
        float randomPosY = Random.Range(-4.0f, 4.0f);

        // 生成されたゲームオブジェクトのY軸にランダムな値を加算して、生成されるたびに高さの位置を変更する
        obj.transform.position = new Vector2(obj.transform.position.x, obj.transform.position.y + randomPosY);

        gameDirector.GenerateCount++;
    }

    /// <summary>
    /// FloorGeneratorの準備
    /// </summary>
    /// <param name="gameDirector"></param>
    public void SetUpGenerator(GameDirector gameDirector)
    {
        this.gameDirector = gameDirector;

        // TODO 他にも初期設定したい情報がある場合にはここに処理を追加する

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