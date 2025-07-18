using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using Unity.VisualScripting;
using TMPro;

public class Option : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;

    [SerializeField] public Slider bgmSlider; // BGMのスライダー
    [SerializeField] public Slider seSlider; // SEのスライダー

    private GameObject Obj; //このオブジェクトを参照

    //ボタンのオブジェクト
    public Button gameEndButton;
    public TMP_Text gameEndButtonText;

    private void Awake()
    {
        Positioning(SceneManager.GetActiveScene());
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDestroy()
    {
        // イベント解除（メモリリーク防止）
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void Start()
    {
        Obj = this.gameObject;
        bgmSlider.onValueChanged.AddListener(BGMVolumeChanged);
        seSlider.onValueChanged.AddListener(SEVolumeChanged);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Positioning(scene);
    }

    //シーンごとにボタンの表示を変える
    public void Positioning(Scene scene)
    {
        gameEndButton.onClick.RemoveAllListeners();

        switch (scene.name)
        {
            case "TitleScene":
                Debug.Log("TitleSceneで作動");
                gameEndButton.gameObject.SetActive(false);
                break;
            case "HomeScene":
                Debug.Log("HomeSceneで作動");
                gameEndButton.gameObject.SetActive(true);
                gameEndButtonText.text = "タイトルへ";
                gameEndButton.onClick.AddListener(GotoTitle);
                break;
            case "Main":
                Debug.Log("Mainで作動");
                gameEndButton.gameObject.SetActive(true);
                gameEndButtonText.text = "戦闘離脱";
                gameEndButton.onClick.AddListener(GameEnd);
                break;
            default:
                Debug.LogWarning("シーンを特定出来ません");
                break;
        }
    }

    //音量が変わるごとに数値を反映する関数
    private void BGMVolumeChanged(float value)
    {
        //10段階補正
        value /= 10;
        //-80dB～0dBの範囲に変換
        var volume = Mathf.Clamp(Mathf.Log10(value) * 20f, -80f, 0f);
        audioMixer.SetFloat("BGM", volume);
        Debug.Log($"BGM:{volume}");
    }

    private void SEVolumeChanged(float value)
    {
        //10段階補正
        value /= 10;
        //-80dB～0dBの範囲に変換
        var volume = Mathf.Clamp(Mathf.Log10(value) * 20f, -80f, 0f);
        audioMixer.SetFloat("SE", volume);
        Debug.Log($"SE:{volume}");
    }

    //オプション画面を閉じる
    public void OffOptionCanvas()
    {
        Obj.SetActive(false);
    }

    //タイトルへ戻るボタン
    private void GotoTitle()
    {
        Debug.Log("タイトルへ戻ります");
        SceneManager.LoadScene("TitleScene");
        OffOptionCanvas();
        //Destroy(Obj);
    }

    //戦闘終了ボタン
    private void GameEnd()
    {
        Debug.Log("戦闘を離脱しました");
        SceneManager.LoadScene("HomeScene");
        OffOptionCanvas();
    }

}
