using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject uiPanel;  // UI�̃I�u�W�F�N�g
    public GameObject showUIButton; // UI��\������{�^��

    public void ShowUI()
    {
        uiPanel.SetActive(true); // UI��\��
        showUIButton.SetActive(false); // �\���{�^�����\��
    }

    public void HideUI()
    {
        uiPanel.SetActive(false); // UI���\��
        showUIButton.SetActive(true); // �\���{�^�����ĕ\��
    }
}