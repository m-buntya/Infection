using System.Linq;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ListManager : MonoBehaviour
{
    [SerializeField] UnitStatsData unitStatsData;
    [SerializeField] VirusData VirusData;

    public GameObject listCanvas; //ユニット一覧画面
    public GameObject detailsCanvas; //キャラ詳細画面のキャンバス

    public DisplayArea displayArea;
    public Transform content; //ユニットなどを表示するための領域
    public GameObject displayAreaPrefab; //ユニットを表示するプレハブ

    // ページ切り替え用のボタン
    public Button unitPageButton;
    public Button virusPageButton;

    [SerializeField] public string pageName = "Unit";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        listCanvas.SetActive(true);
        detailsCanvas.SetActive(false);
        UpdateDisplay();
        //displayArea = displayAreaPrefab.GetComponent<DisplayArea>();
    }

    void Update()
    {
        //Debug.Log(displayArea.isClick);
        //Debug.Log($"{displayArea.isClick}");
        //if (displayArea.isClick == true)
        //{
        //    listCanvas.SetActive(false);
        //    detailsCanvas.SetActive(true);
        //}
    }

    public void UpdateDisplay()
    {
        foreach(Transform child in content)
        {
            Destroy(child.gameObject); // 既存の子オブジェクトを削除
        }

        if(pageName == "Unit")
        {
            // ユニットページのボタンを無効化
            unitPageButton.interactable = false;
            virusPageButton.interactable = true;

            //unitCode順にソート
            var sortedUnits = unitStatsData.UnitParameter.OrderBy(u => u.unitCode).ToList();

            //表示部分
            foreach (var unit in sortedUnits)
            {
                GameObject obj = Instantiate(displayAreaPrefab, content);
                DisplayArea displayArea = obj.GetComponent<DisplayArea>();
                displayArea.SetUnitData(unit);
            }
        }

        if(pageName == "Virus")
        {
            // ユニットページのボタンを無効化
            virusPageButton.interactable = false;
            unitPageButton.interactable = true;

            var sortedViruses = VirusData.VirusParameter.OrderBy(v => v.virusCode).ToList();

            foreach(var virus in sortedViruses)
            {
                GameObject obj = Instantiate(displayAreaPrefab, content);
                DisplayArea displayArea = obj.GetComponent<DisplayArea>();
                displayArea.SetVirusData(virus);
            }
        }

    }

    //リスト画面からホーム画面に戻る
    public void BackButton()
    {
       SceneManager.LoadScene("HomeScene");
    }

    //詳細画面からリスト画面に戻る
    public void BackDetailsButton()
    {
        detailsCanvas.SetActive(false);
        listCanvas.SetActive(true);
    }


    // ユニットページとウイルスページのボタンを押したときの処理
    public void UnitPageButton()
    {
        if (pageName == "Unit") return;

        pageName = "Unit"; 
        Debug.Log("ユニットページに切り替え");
        UpdateDisplay();
    }

    public void VirusPageButton()
    {
        if(pageName == "Virus") return;

        pageName = "Virus";
        Debug.Log("ウイルスページに切り替え");
        UpdateDisplay();
    }


}
