using System.Linq;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ListManager : MonoBehaviour
{
    [SerializeField] UnitStatsData unitStatsData;
    [SerializeField] VirusData VirusData;

    public DisplayArea displayArea;
    public Transform content; //ユニットなどを表示するための領域
    public GameObject displayAreaPrefab; //ユニットを表示するプレハブ

    //TODO キャラ詳細画面を作る
    //public GameObject listCanvas;
    //public GameObject detailsCanvas;

    public Button unitPageButton;
    public Button virusPageButton;

    private string pageName = "Unit";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //detailsCanvas.SetActive(false);
        UpdateDisplay();
        //displayArea = displayAreaPrefab.GetComponent<DisplayArea>();
    }

    void Update()
    {
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

    public void BackButton()
    {
       SceneManager.LoadScene("HomeScene");
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
