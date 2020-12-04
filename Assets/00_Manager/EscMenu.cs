using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using DG.Tweening;

public class EscMenu : MonoBehaviour
{
    // ゲーム終了ボタン
    public Button btnQuitGame;

    // タイトルに戻るボタン
    public Button btnGoTitle;      

    void Start()
    {
        // 各ボタンに処理を登録
        btnQuitGame.onClick.AddListener(QuitGame);
        btnGoTitle.onClick.AddListener(GoTitleButton);

        // ゲーム内時間の流れを停止
        Time.timeScale = 0;
    }

    /// <summary>
    /// タイトル画面へ戻る
    /// </summary>
    public static void GoTitleButton()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("TitleScene");

        //StartCoroutine(Kankaku());

    }

    /// <summary>
    /// ゲームの終了処理
    /// </summary>
    void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
    }


    IEnumerator Kankaku() //コルーチンメソッド変数名Kankaku
    {
        //(1.5秒の間を設ける)
        yield return new WaitForSeconds(1f);
        Debug.Log("Wait");

        //タイトルシーンへ移行
        SceneManager.LoadScene("TitleScene");
    }

}
