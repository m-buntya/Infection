using UnityEngine;
using TMPro;
using StatePatteren.State;

public class SynthesisUI : MonoBehaviour
{
    public Synthesissystem synthesissystem; // 合成システムの参照  

    UnitController unitController;

    public GameObject synthesisCanvas;
    public TextMeshProUGUI SynthesisTextBox;

    //合成可能なユニットの名前を入れる
    public string targetUnit1 = "アーチャー";
    public string targetUnit2 = "アタッカー";

    bool isDisplay = false; // 合成UIの表示フラグ

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        synthesisCanvas.SetActive(false);  // 合成UIを非表示にする

    }

    // Update is called once per frame
    void Update()
    {
        if (synthesissystem.isSynthes && !isDisplay)
        {
            //対象のユニット名を取得
            targetUnit1 = synthesissystem.taergetUnitName1;
            targetUnit2 = synthesissystem.targetUnitName2;

            //テキスト表示部分
            SynthesisTextBox.text = $"{targetUnit1}と{targetUnit2}が合成可能です\n合成しますか？";
            synthesisCanvas.SetActive(true);
            isDisplay = true;
            Time.timeScale = 0f;
        }
    }


    //合成するかのボタン処理
    public void SynthesisYesButton()
    {
        Debug.Log("合成します");
        synthesisCanvas.SetActive(false);
        synthesissystem.isSynthes = false;
        synthesissystem.hasDisplay = false;

        // 合成実行
        synthesissystem.GetSynthesisUnit();
        Time.timeScale = 1f;
    }

    public void SynthesisNoButton()
    {
        Debug.Log("合成しません");
        synthesisCanvas.SetActive(false);
        synthesissystem.isSynthes = false;
        synthesissystem.hasDisplay = false;

        Time.timeScale = 1f;
    }

}
