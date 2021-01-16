using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

using DG.Tweening;

/// <summary>
/// 音源管理クラス
/// </summary>
public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    //BGM管理
    public enum BGM_Type
    {
        //BGM列挙子を登録
        Game0,
        Game1,
        Game2,
        Game3,
        Game4,
        Game5,
        Game6,
        Game7,
        Game8,

    }

    //SE管理
    public enum SE_Type
    {
       
        SE_00,
        SE_01,
        SE_02,
        SE_03,
        SE_04,

    }

    //VOICE管理
    public enum VOICE_Type
    {
        //VOICE列挙子を登録
        U0,
        U1,
        U2,
        U3,
        U4,
        U5,
        U6,
        U7,
        U8,
        U9,
        U10,
        U11,

    }

    //クロスフェード時間
    public const float CROSS_FADE_TIME = 1.0f;

    //ボリューム関連
    public float BGM_Volume = 0.1f;
    public float SE_Volume = 0.2f;
    public float VOICE_Volume = 0.2f;
    public bool Mute = false;

    //=== AudioClip ===→楽器
    public AudioClip[] BGM_Clips;
    public AudioClip[] SE_Clips;
    public AudioClip[] VOICE_Clips;

    //SE用オーディオミキサー→指揮者
    public AudioMixer audioMixer;

    //=== AudioSource ===→演奏者
    private AudioSource[] BGM_Sources = new AudioSource[2];
    private AudioSource[] SE_Sources = new AudioSource[16];
    private AudioSource[] VOICE_Sources = new AudioSource[20];

    private bool isCrossFading;
    private int currentBgmIndex = 999;

    //Escキーでオプションメニュー表示
    public EscMenu esc_Menu_Prefab;
    EscMenu escMenu;
    [SerializeField] Transform canvasTran;


    void Awake()
    {
        //シングルトンかつ、シーン遷移しても破棄されないようにする
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        //BGM用 AudioSource追加
        BGM_Sources[0] = gameObject.AddComponent<AudioSource>();
        BGM_Sources[1] = gameObject.AddComponent<AudioSource>();

        //SE用 AudioSource追加
        for (int i = 0; i < SE_Sources.Length; i++)
        {
            SE_Sources[i] = gameObject.AddComponent<AudioSource>();
        }

        //VOICE用 AudioSource追加
        for (int i = 0; i < VOICE_Sources.Length; i++)
        {
            VOICE_Sources[i] = gameObject.AddComponent<AudioSource>();
        }
    }

    void Start()
    {
        //SetUpCanvas();
    }

    // Update is called once per frame
    void Update()
    {
        // ボリューム設定
        if (!isCrossFading)
        {
            BGM_Sources[0].volume = BGM_Volume;
            BGM_Sources[1].volume = BGM_Volume;
        }

        foreach (AudioSource source in SE_Sources)
        {
            source.volume = SE_Volume;
        }

        foreach (AudioSource source in VOICE_Sources)
        {
            source.volume = VOICE_Volume;
        }

        //Escキーでメニュー表示
        //Display_menu_screen_in_escape_key();
    }

    /// <summary>
    /// BGM再生
    /// </summary>
    /// <param name="bgmType"></param>
    /// <param name="loopFlg"></param>
    public void PlayBGM(BGM_Type bgmType, bool loopFlg = true)
    {
        int index = (int)bgmType;
        currentBgmIndex = index;

        if (index < 0 || BGM_Clips.Length <= index)
        {
            return;
        }

        //同じBGMの場合は何もしない
        if (BGM_Sources[0].clip != null && BGM_Sources[0].clip == BGM_Clips[index])
        {
            return;
        }
        else if (BGM_Sources[1].clip != null && BGM_Sources[1].clip == BGM_Clips[index])
        {
            return;
        }

        //フェードでBGM開始
        if (BGM_Sources[0].clip == null && BGM_Sources[1].clip == null)
        {
            BGM_Sources[0].loop = loopFlg;
            BGM_Sources[0].clip = BGM_Clips[index];
            BGM_Sources[0].Play();
        }
        else
        {
            //クロスフェード処理
            StartCoroutine(CrossFadeChangeBMG(index, loopFlg));
        }
    }

    /// <summary>
    /// BGMのクロスフェード処理
    /// </summary>
    /// <param name="index"></param>
    /// <param name="loopFlg"></param>
    /// <returns></returns>
    private IEnumerator CrossFadeChangeBMG(int index, bool loopFlg)
    {
        isCrossFading = true;
        if (BGM_Sources[0].clip != null)
        {
            // [0]が再生されている場合、[0]の音量を徐々に下げて、[1]を新しい曲として再生
            BGM_Sources[1].volume = 0;
            BGM_Sources[1].clip = BGM_Clips[index];
            BGM_Sources[1].loop = loopFlg;
            BGM_Sources[1].Play();
            BGM_Sources[0].DOFade(0, CROSS_FADE_TIME).SetEase(Ease.Linear);

            yield return new WaitForSeconds(CROSS_FADE_TIME);
            BGM_Sources[0].Stop();
            BGM_Sources[0].clip = null;
        }
        else
        {
            // [1]が再生されている場合、[1]の音量を徐々に下げて、[0]を新しい曲として再生
            BGM_Sources[0].volume = 0;
            BGM_Sources[0].clip = BGM_Clips[index];
            BGM_Sources[0].loop = loopFlg;
            BGM_Sources[0].Play();
            BGM_Sources[1].DOFade(0, CROSS_FADE_TIME).SetEase(Ease.Linear);

            yield return new WaitForSeconds(CROSS_FADE_TIME);
            BGM_Sources[1].Stop();
            BGM_Sources[1].clip = null;
        }
        isCrossFading = false;
    }

    /// <summary>
    /// BGM完全停止
    /// </summary>
    public void StopBGM()
    {
        BGM_Sources[0].Stop();
        BGM_Sources[1].Stop();
        BGM_Sources[0].clip = null;
        BGM_Sources[1].clip = null;
    }

    /// <summary>
    /// SE再生
    /// </summary>
    /// <param name="sE_Type"></param>
    public void PlaySE(SE_Type sE_Type)
    {
        int index = (int)sE_Type;
        if (index < 0 || SE_Clips.Length <= index)
        {
            return;
        }

        //再生中ではないAudioSouceをつかってSEを鳴らす
        foreach (AudioSource source in SE_Sources)
        {
            if (false == source.isPlaying)
            {
                source.clip = SE_Clips[index];
                source.Play();
                return;
            }
        }
    }

    /// <summary>
    /// VOICE再生
    /// </summary>
    /// <param name="vO_Type"></param>
    public void PlayVOICE(VOICE_Type vO_Type)
    {
        int index = (int)vO_Type;
        if (index < 0 || VOICE_Clips.Length <= index)
        {
            return;
        }

        //再生中ではないAudioSouceをつかってSEを鳴らす
        foreach (AudioSource source in VOICE_Sources)
        {
            if (false == source.isPlaying)
            {
                source.clip = VOICE_Clips[index];
                source.Play();
                return;
            }
        }
    }

    /// <summary>
    /// SE停止
    /// </summary>
    public void StopSE()
    {
        //全てのSE用のAudioSouceを停止する
        foreach (AudioSource source in SE_Sources)
        {
            source.Stop();
            source.clip = null;
        }
    }

    /// <summary>
    /// BGM一時停止
    /// </summary>
    public void MuteBGM()
    {
        BGM_Sources[0].Stop();
        BGM_Sources[1].Stop();
    }

    /// <summary>
    /// 一時停止した同じBGMを再生(再開)
    /// </summary>
    public void ResumeBGM()
    {
        BGM_Sources[0].Play();
        BGM_Sources[1].Play();
    }

    ////* 未使用 *////

    /// <summary>
    /// AudioMixer設定
    /// </summary>
    /// <param name="vol"></param>
    public void SetAudioMixerVolume(float vol)
    {
        if (vol == 0)
        {
            audioMixer.SetFloat("volumeSE", -80);
        }
        else
        {
            audioMixer.SetFloat("volumeSE", 0);
        }
    }

    /// <summary>
    /// Escキーでメニュー表示
    /// </summary>
    public void Display_menu_screen_in_escape_key()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (escMenu == null)
            {
                escMenu = Instantiate(esc_Menu_Prefab, canvasTran, false);
            }
            else
            {
                Time.timeScale = 1;
                Destroy(escMenu.gameObject);
            }
        }
    }

    public void SetUpCanvas()
    {
        canvasTran = GameObject.FindGameObjectWithTag("UI_Canvas").transform;
    }
}
