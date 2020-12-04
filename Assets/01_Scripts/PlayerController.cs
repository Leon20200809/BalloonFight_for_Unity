using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    const string horizontal = "Horizontal";
    const string jump = "Jump";
    Rigidbody2D rb;
    Animator anim;
    public float moveSpeed;
    public float jumpPower;
    float scale;

    public bool isGrounded;
    [SerializeField, Header("地面判定レイヤー")]
    LayerMask groundLayer;

    //画面移動制限
    float limitPosX = 9.5f;
    float limitPosY = 4.45f;

    //風船
    public Ballon[] ballons;
    public int maxBallonCount;
    public Transform[] ballonTran;
    public Ballon ballonPrefab;
    public float generateTime;
    public bool isGenerating;

    //ゲームスタート時
    [SerializeField]
    StartChecker startChecker;
    public bool isFirstGenerateBallon;
    bool isGameOver = false;

    //のけぞり距離
    public float knockbackPower;

    //コイン得点
    public UIManager uIManager;
    public int coinPoint;

    // Start is called before the first frame update
    void Start()
    {
        InitPlayer();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isGameOver) return;
        Move();
    }

    private void Update()
    {
        LimitArea();
        Is_ground_decision_enabled();
        BallonRecaver();
        if (ballons[0] != null) Jump();

    }



    /// <summary>
    /// 制限設定
    /// </summary>
    void LimitArea()
    {
        //ジャンプパワー制限
        if (rb.velocity.y > 5.0f) rb.velocity = new Vector2(rb.velocity.x, 5.0f);

        //移動範囲制限
        float posX = Mathf.Clamp(transform.position.x, -limitPosX, limitPosX);
        float posY = Mathf.Clamp(transform.position.y, -limitPosY, limitPosY);

        //位置補正
        transform.position = new Vector2(posX, posY);
    }

    /// <summary>
    /// キャラクター初期設定
    /// </summary>
    void InitPlayer()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        scale = transform.localScale.x;
        ballons = new Ballon[maxBallonCount];
    }

    /// <summary>
    /// キャラクター移動
    /// </summary>
    void Move()
    {
        anim.SetBool("Landing", false);
        anim.SetBool("Fall", false);
        float x = Input.GetAxisRaw(horizontal);
        if (x != 0)
        {
            rb.velocity = new Vector2(x * moveSpeed, rb.velocity.y);


            //振り向き
            Vector3 temp = transform.localScale;
            temp.x = x;
            if (temp.x > 0)
            {
                temp.x = scale;
            }
            else
            {
                temp.x = -scale;
            }
            transform.localScale = temp;
            anim.SetBool("Idle", false);
            anim.SetFloat("Run", 0.5f);
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            anim.SetFloat("Run", 0.0f);
            anim.SetBool("Idle", true);
        }

    }

    /// <summary>
    /// キャラクタージャンプ
    /// </summary>
    void Jump()
    {
        if (Input.GetButtonDown(jump))
        {
            rb.AddForce(transform.up * jumpPower);
            anim.SetTrigger("Jump");

        }
    }

    //=============   接触判定   ==================//
    private void OnCollisionEnter2D(Collision2D col)
    {

        // 接触したコライダーを持つゲームオブジェクトのTagがEnemyなら 
        if (col.gameObject.tag == "Enemy")
        {

            // キャラと敵の位置から距離と方向を計算
            Vector3 direction = (transform.position - col.transform.position).normalized;

            // 敵の反対側にキャラを吹き飛ばす
            transform.position += direction * knockbackPower;

            DestroyBallon();
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {

        // 通過したコライダーを持つゲームオブジェクトの Tag が Coin の場合
        if (col.gameObject.tag == "Coin")
        {

            // 通過したコインのゲームオブジェクトの持つ Coin スクリプトを取得し、point 変数の値をキャラの持つ coinPoint 変数に加算
            coinPoint += col.gameObject.GetComponent<Coin>().point;

            // 通過したコインのゲームオブジェクトを破壊する
            Destroy(col.gameObject);

            uIManager.UpdateDisplayScore(coinPoint);
        }
    }




    /// <summary>
    /// 設置判定
    /// </summary>
    void Is_ground_decision_enabled()
    {
        // 接地判定。接地＝ true
        isGrounded = Physics2D.Linecast(transform.position + transform.up * 0.4f, transform.position - transform.up * 0.9f, groundLayer);

        // Linecast可視化
        Debug.DrawLine(transform.position + transform.up * 0.4f, transform.position - transform.up * 0.9f, Color.red, 1.0f);

        if (isGrounded == false && rb.velocity.y < 0.15f) anim.SetBool("Fall", true);
        if (isGrounded == true) anim.SetBool("Landing", true);
    }

    /// <summary>
    /// 手動バルーン生成　Qボタン入力
    /// </summary>
    void BallonRecaver()
    {
        if (isGrounded == true && isGenerating == false)
        {

            // Qボタンを押したら
            if (Input.GetKeyDown(KeyCode.Q))
            {

                // バルーンを１つ作成する
                StartCoroutine(GenerateBallon());
            }
        }
    }

    /// <summary>
    /// バルーン生成メソッド
    /// </summary>
    /// <returns></returns>
    IEnumerator GenerateBallon()
    {
        if (!isFirstGenerateBallon)
        {
            isFirstGenerateBallon = true;
            Debug.Log("ゲームスタート");
            startChecker.SetInitialSpeed();
        }
        //風船MAX時は処理しない
        if (ballons[1] != null)
        {
            yield break;
        }
        isGenerating = true;

        if (ballons[0] == null)
        {
            ballons[0] = Instantiate(ballonPrefab, ballonTran[0]);
            ballons[0].GetComponent<Ballon>().SetUpBallon(this);
        }
        else
        {
            ballons[1] = Instantiate(ballonPrefab, ballonTran[1]);
            ballons[1].GetComponent<Ballon>().SetUpBallon(this);
        }

        //風船生成ディレイ
        yield return new WaitForSeconds(generateTime);

        isGenerating = false;
    }


    /// <summary>
    /// バルーン破壊
    /// </summary>
    public void DestroyBallon()
    {

        // TODO 後程、バルーンが破壊される際に「割れた」ように見えるアニメ演出を追加する

        if (ballons[1] != null)
        {
            Destroy(ballons[1]);
        }
        else if (ballons[0] != null)
        {
            Destroy(ballons[0]);
        }
    }

    /// <summary>
    /// ゲームオーバー
    /// </summary>
    public void GameOver()
    {
        isGameOver = true;
        Debug.Log(isGameOver);
        // 画面にゲームオーバー表示を行う
        uIManager.DisplayGameOverInfo();
    }

}
