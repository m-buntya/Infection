using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;
public class MissionJudge : MonoBehaviour
{
    //ミッション状態
    [SerializeField] private bool isMission_1 = false;
    [SerializeField] private bool isMission_2 = false;
    [SerializeField] private bool isMission_3 = false;

    //マークとテキストUI
    [SerializeField] private Image missionMark_1;
    [SerializeField] private Image missionMark_2;
    [SerializeField] private Image missionMark_3;

    
    [SerializeField] private Image missionTextBack_1;
    [SerializeField] private Image missionTextBack_2;
    [SerializeField] private Image missionTextBack_3;


    [SerializeField] private TextMeshProUGUI missionText_1;
    [SerializeField] private TextMeshProUGUI missionText_2;
    [SerializeField] private TextMeshProUGUI missionText_3;

    [SerializeField] private string message_1 = "敵部隊全滅！";
    [SerializeField] private string message_2 = "120秒生存成功！";
    [SerializeField] private string message_3 = "敵拠点壊滅成功！";
    
    //報酬関連
    [SerializeField] private GameObject hideOnTouchObject;
    [SerializeField] private GameObject rewardObject;
    [SerializeField] private TextMeshProUGUI touchPromtText;

    [SerializeField] private RandomSpriteLineUI firstRewardDisplay;
    [SerializeField] private RandomSpriteLineUI secondRewardDisplay;

    //遷移するシーン名
    [SerializeField] private string nextSceneName = "ResultScene";

    [SerializeField] private TextMeshProUGUI nextStagePromtText;
    private bool isWaitingForTouch;

    private bool hasDisplayReward = false;

    //呼び出しと判定
    void Start()
    {
        /*isMission_1 = true;
        isMission_2 = true;
        isMission_3 = true;*/

        hideOnTouchObject.gameObject.SetActive(true);
        touchPromtText.gameObject.SetActive(false);
        ActiveClearMark();
        StartCoroutine(ShowMissionTextSepuentially());
        rewardObject.gameObject.SetActive(false);
        nextStagePromtText.gameObject.SetActive(false);

    }

    //達成したミッションに合わせてマーク・Image・テキストを出す
    private void ActiveClearMark()
    {

        if (missionText_1 != null) missionText_1.gameObject.SetActive(false);
        if (missionText_2 != null) missionText_2.gameObject.SetActive(false);
        if (missionText_3 != null) missionText_3.gameObject.SetActive(false);

        if (missionMark_1 != null) missionMark_1.gameObject.SetActive(isMission_1);
        if (missionMark_2 != null) missionMark_2.gameObject.SetActive(isMission_2);
        if (missionMark_3 != null) missionMark_3.gameObject.SetActive(isMission_3);

    }
    void Update()
    {
        if (isWaitingForTouch && Input.GetMouseButtonDown(0))
        {
            hideOnTouchObject.SetActive(false);
            touchPromtText.gameObject.SetActive(false);

            rewardObject.SetActive(true);

            if (!hasDisplayReward)
            {
                StartCoroutine(RunRewardSequence());
                hasDisplayReward = true;
            }
            else
            {
               
                StartCoroutine(WaitAndLoadNextScene(0.1f)); // 遷移までの余韻演出付き
                isWaitingForTouch = false;
            }
           
        }
    }



    IEnumerator ShowTextTypeEffect(TextMeshProUGUI targetText, string message, float delay = 0.05f)
    {
        targetText.gameObject.SetActive(true);
        targetText.text = "";

        foreach (char c in message)
        {
            targetText.text += c;
            yield return new WaitForSeconds(delay);
        }
    }
    IEnumerator ShowMissionTextSepuentially()
    {

        if (isMission_1)
            yield return StartCoroutine(ShowTextTypeEffect(missionText_1, message_1));

        if (isMission_2)
            yield return StartCoroutine(ShowTextTypeEffect(missionText_2, message_2));

        if (isMission_3)
            yield return StartCoroutine(ShowTextTypeEffect(missionText_3, message_3));

        touchPromtText.gameObject.SetActive(true);
        touchPromtText.text = "Please Touch";
        isWaitingForTouch = true;


    }
    //報酬表示の流れ
    IEnumerator RunRewardSequence()
    {
        if (firstRewardDisplay != null)
            yield return StartCoroutine(firstRewardDisplay.SpawnSpritesSequentially(0.3f));

        yield return new WaitForSeconds(0.5f);

        if (secondRewardDisplay != null)
            yield return StartCoroutine(secondRewardDisplay.SpawnSpritesSequentially(0.3f));
        if (nextStagePromtText != null)
        {
            nextStagePromtText.gameObject.SetActive(true);
            nextStagePromtText.text = "Next Stage Go!!";
        }

        // 🖱 タップ待ち開始（Update で処理）
        isWaitingForTouch = true;
    }

IEnumerator WaitAndLoadNextScene(float delay)
    {
        yield return new WaitForSeconds(delay);
        
       
        SceneManager.LoadScene(nextSceneName);
        isWaitingForTouch = true;

    }
}