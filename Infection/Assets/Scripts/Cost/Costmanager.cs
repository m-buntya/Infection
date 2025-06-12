using TMPro;
using UnityEngine;

public class CostManager : MonoBehaviour
{
    [SerializeField] private TMP_Text costText;
    [SerializeField] private RectTransform costBar;
    [SerializeField] private float fullBarWidth = 100f; // �o�[�ő啝
    private int currentCost = 0; // �����R�X�g��0
    private const int MAX_COST = 100;
    private const float AccumulateInterval = 3.0f;
    private float regenTimer = 0f;
    private float valueTimer = 0f;
    private float barFillRatio = 0f;

    void Update()
    {
        // �R�X�g�񕜏���
        regenTimer += Time.deltaTime;
        if (regenTimer >= AccumulateInterval)
        {
            regenTimer = 0f;
            if (currentCost < MAX_COST)
            {
                currentCost++;
                UpdateCostText();
            }
        }

        // �o�[�L�k�����i3�b�ōő�܂ŐL�т�j
        valueTimer += Time.deltaTime;
        barFillRatio = Mathf.Clamp01(valueTimer / 3f);
        float barWidth = fullBarWidth * barFillRatio;
        costBar.sizeDelta = new Vector2(barWidth, costBar.sizeDelta.y);

        // 3�b��Ƀ��Z�b�g
        if (valueTimer >= 3f)
        {
            valueTimer = 0f;
            costBar.sizeDelta = new Vector2(0f, costBar.sizeDelta.y);
        }
    }

    public bool CanAfford(int cost)
    {
        return currentCost >= cost;
    }

    public void SpendCost(int cost)
    {
        if (!CanAfford(cost)) return;

        currentCost -= cost;
        UpdateCostText();
    }

    private void UpdateCostText()
    {
        if (costText != null)
            costText.text = currentCost.ToString();
    }

    public int GetCurrentCost()
    {
        return currentCost;
    }

    public void DisplayInsufficientCostFeedBack()
    {
        Debug.Log("�R�X�g������܂���I");
    }
}