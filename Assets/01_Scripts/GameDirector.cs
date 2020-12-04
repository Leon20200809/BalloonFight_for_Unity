using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDirector : MonoBehaviour
{

    [SerializeField]
    GoalChecker goalHousePrefab;

    [SerializeField]
    PlayerController playerController;

    [SerializeField]
    FloorGenerator[] floorGenerators;

    bool isSetUp;
    bool isGameUp;

    //生成回数
    int generateCount;
    public int clearCount;

    // generateCount 変数のプロパティ
    public int GenerateCount
    {
        set
        {
            generateCount = value;
            Debug.Log("生成数 / クリア目標数 : " + generateCount + " / " + clearCount);

            if (generateCount >= clearCount)
            {
                // ゴール地点を生成
                GenerateGoal();

                // ゲーム終了
                GameUp();
            }

        }
        get
        {
            return generateCount;
        }
    }




    // Start is called before the first frame update
    void Start()
    {
        // ゲーム開始状態にセット
        isGameUp = false;
        isSetUp = false;

        // FloorGeneratorの準備
        SetUpFloorGenerators();

        // TODO 各ジェネレータを停止
        Debug.Log("生成停止");
    }

    /// <summary>
    /// FloorGeneratorの準備
    /// </summary>
    private void SetUpFloorGenerators()
    {
        for (int i = 0; i < floorGenerators.Length; i++)
        {
            // FloorGeneratorの準備・初期設定を行う
            //floorGenerators[i].SetUpGenerator(this);           // <=　メソッドを追加する修正が済むまでコメントアウト
        }
    }
    // Update is called once per frame
    void Update()
    {
        // プレイヤーがはじめてバルーンを生成したら
        if (playerController.isFirstGenerateBallon && isSetUp == false)
        {

            // 準備完了
            isSetUp = true;

            // TODO 各ジェネレータを動かし始める
            Debug.Log("生成スタート");
        }
    }

    /// <summary>
    /// ゴール地点の生成
    /// </summary>
    private void GenerateGoal()
    {
        // ゴール地点を生成
        GoalChecker goalHouse = Instantiate(goalHousePrefab);

        // TODO ゴール地点の初期設定
        Debug.Log("ゴール地点 生成");
    }

    /// <summary>
    /// ゲーム終了
    /// </summary>
    public void GameUp()
    {

        // ゲーム終了
        isGameUp = true;

        // TODO 各ジェネレータを停止
        Debug.Log("生成停止");
    }
}
