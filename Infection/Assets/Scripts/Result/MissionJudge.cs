using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class MissionJudge : MonoBehaviour
{
    [SerializeField] private bool isMission_1 = false;
    [SerializeField] private bool isMission_2 = false;
    [SerializeField] private bool isMission_3 = false;

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

    [SerializeField] private GameObject hideOnTouchObject;
    [SerializeField] private TextMeshProUGUI touchPromtText;
    private bool isWaitingForTouch;

    //呼び出しと判定
    void Start()
    {
        /*isMission_1 = true;
        isMission_2 = true;
        isMission_3 = true;*/
        missionTextBack_1.gameObject.SetActive(false);
        missionTextBack_2.gameObject.SetActive(false);
        missionTextBack_3.gameObject.SetActive(false);

        hideOnTouchObject.gameObject.SetActive(true);
        touchPromtText.gameObject.SetActive(true);

        ActiveClearMark();
    }
    
    //達成したミッションに合わせてマーク・Image・テキストを出す
    private void ActiveClearMark()
    {
        if (isMission_1 == true)
        {
            missionMark_1.gameObject.SetActive(true);
            missionText_1.gameObject.SetActive(true);
            missionTextBack_1.gameObject.SetActive(true);
            missionText_1.text = message_1;
        }
        if (isMission_2 == true)
        {
            missionMark_2.gameObject.SetActive(true);
            missionText_2.gameObject.SetActive(true);
            missionTextBack_2.gameObject.SetActive(true);
            missionText_2.text = message_2;
        }
        if (isMission_3 == true)
        {
            missionMark_3.gameObject.SetActive(true);
            missionText_3.gameObject.SetActive(true);
            missionTextBack_3.gameObject.SetActive(true);
            missionText_3.text = message_3;
        }
        touchPromtText.gameObject.SetActive(true);
        touchPromtText.text = "Please Touch";
        isWaitingForTouch = true;
    }
    private void Update()
    {
        if(isWaitingForTouch && Input.GetMouseButtonDown(0))
        {
            if (hideOnTouchObject != null)
            {
                hideOnTouchObject.SetActive(false);
            }
            if (touchPromtText != null)
            {
                touchPromtText.gameObject.SetActive(false);
            }
            isWaitingForTouch = false;
        }
    }
}