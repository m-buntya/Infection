using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CostManager : MonoBehaviour
{
    [SerializeField] private Image costBar; // UIの横バー
    [SerializeField] private TextMeshProUGUI costText; // コスト表示用

    private int currentCost = 0; // 初期コスト
    private const int MAX_COST = 100; // 最大コスト
    private const float ACCUMULATE_INTERVAL = 3.0f; // コストが増加する間隔
    private float costRegenTimer = 0f; // タイマー処理用
    private const float MAX_WIDTH = 100f; // バーの最大横幅

    void Update()
    {
        costRegenTimer += Time.deltaTime;
        float progress = costRegenTimer / ACCUMULATE_INTERVAL; // 進行割合
        float newWidth = MAX_WIDTH * progress; // 横幅を最大値に合わせて調整

        costBar.rectTransform.sizeDelta = new Vector2(newWidth, costBar.rectTransform.sizeDelta.y);

        if (newWidth >= MAX_WIDTH)
        {
            AccumulateCost();
            costRegenTimer = 0f; // リセット
         
        }
    }

    void AccumulateCost()
    {
        if (currentCost < MAX_COST)
        {
            currentCost++;
            UpdateCostUI(); // コスト増加後にUI更新
            Debug.Log("コスト追加: " + currentCost);

        }
    }

    public void SpendCost(int cost)
    {
        if (CanAfford(cost))
        {
            currentCost -= cost;
            Debug.Log("コスト消費: " + cost + " 残り: " + currentCost);
            UpdateCostUI(); // UIを更新
        }
        else
        {
            Debug.LogWarning("コストが不足しています！");
        }
    }

    public bool CanAfford(int cost)
    {
        return currentCost >= cost;
       
    }


    void UpdateCostUI()
    {
        if (costText != null)
        {
            costText.text = currentCost.ToString(); // コストをUIに反映
            //Debug.Log("コストUI更新: " + currentCost);
        }
        else
        {
            Debug.LogWarning("costTextが設定されていません！");
        }
    }
}