using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

public class MissionJudge : MonoBehaviour
{
    [SerializeField] private bool isMission_1 = false;
    [SerializeField] private bool isMission_2 = false;
    [SerializeField] private bool isMission_3 = false;
    [SerializeField] private Image missionMark_1;
    [SerializeField] private Image missionMark_2;
    [SerializeField] private Image missionMark_3;

    //呼び出しと判定
    void Start()
    {
        /*isMission_1 = true;
        isMission_2 = true;
        isMission_3 = true;*/

        ActiveClearMark();
    }

    //達成したミッションに合わせてマークを出す
    private void ActiveClearMark()
    {
        missionMark_1.gameObject.SetActive(true);
        missionMark_2.gameObject.SetActive(true);
        missionMark_3.gameObject.SetActive(true);
    }
}