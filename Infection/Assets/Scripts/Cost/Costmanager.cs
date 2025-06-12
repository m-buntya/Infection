using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CostManager : MonoBehaviour
{
    [SerializeField] private Image costBar; // UI�̉��o�[
    [SerializeField] private TextMeshProUGUI costText; // �R�X�g�\���p

    private int currentCost = 0; // �����R�X�g
    private const int MAX_COST = 100; // �ő�R�X�g
    private const float ACCUMULATE_INTERVAL = 3.0f; // �R�X�g����������Ԋu
    private float costRegenTimer = 0f; // �^�C�}�[�����p
    private const float MAX_WIDTH = 100f; // �o�[�̍ő剡��

    void Update()
    {
        costRegenTimer += Time.deltaTime;
        float progress = costRegenTimer / ACCUMULATE_INTERVAL; // �i�s����
        float newWidth = MAX_WIDTH * progress; // �������ő�l�ɍ��킹�Ē���

        costBar.rectTransform.sizeDelta = new Vector2(newWidth, costBar.rectTransform.sizeDelta.y);

        if (newWidth >= MAX_WIDTH)
        {
            AccumulateCost();
            costRegenTimer = 0f; // ���Z�b�g
         
        }
    }

    void AccumulateCost()
    {
        if (currentCost < MAX_COST)
        {
            currentCost++;
            UpdateCostUI(); // �R�X�g�������UI�X�V
            Debug.Log("�R�X�g�ǉ�: " + currentCost);

        }
    }

    public void SpendCost(int cost)
    {
        if (CanAfford(cost))
        {
            currentCost -= cost;
            Debug.Log("�R�X�g����: " + cost + " �c��: " + currentCost);
            UpdateCostUI(); // UI���X�V
        }
        else
        {
            Debug.LogWarning("�R�X�g���s�����Ă��܂��I");
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
            costText.text = currentCost.ToString(); // �R�X�g��UI�ɔ��f
            //Debug.Log("�R�X�gUI�X�V: " + currentCost);
        }
        else
        {
            Debug.LogWarning("costText���ݒ肳��Ă��܂���I");
        }
    }
}