using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class ResultPopUp : MonoBehaviour
{
    [SerializeField]
    Text txtScore;

    [SerializeField]
    CanvasGroup canvasGroup;

    [SerializeField]
    CanvasGroup canvasGroupRestart;

    [SerializeField]
    Button btnTitle;

    /// <summary>
    /// ResultPopUpの設定
    /// </summary>
    /// <param name="score"></param>
    public void SetUpResultPopUp(int score)
    {
        // 透明にする
        canvasGroup.alpha = 0;

        // 徐々にResultPopUpを表示
        canvasGroup.DOFade(1.0f, 1.0f);

        // スコアを表示
        txtScore.text = score.ToString();

        // リスタートのメッセージをゆっくりと点滅アニメさせる(学習済の命令です。復習しておきましょう)
        canvasGroupRestart.DOFade(0, 1.0f).SetEase(Ease.InOutQuad).SetLoops(-1, LoopType.Yoyo);

        // ボタンにメソッドを登録
        btnTitle.onClick.AddListener(OnClickRestart);
    }

    /// <summary>
    /// ボタンを押した際の制御
    /// </summary>
    private void OnClickRestart()
    {

        // リザルト表示を徐々に非表示にする
        canvasGroup.DOFade(0, 1.0f).SetEase(Ease.Linear);

        // 現在のシーンを再度読み込む
        StartCoroutine(Restart());
    }

    /// <summary>
    /// 現在のシーンを再度読み込む
    /// </summary>
    /// <returns></returns>
    private IEnumerator Restart()
    {
        yield return new WaitForSeconds(1.0f);

        // 現在のシーンの名前を取得
        string sceneName = SceneManager.GetActiveScene().name;

        // 再度読み込み、ゲームを再スタート
        SceneManager.LoadScene(sceneName);
    }
}
