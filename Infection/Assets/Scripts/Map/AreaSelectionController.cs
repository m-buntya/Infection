// �o�T�FChatGPT�Q�ƁE����

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

/// UI�{�^���ɂ��G���A�I���𐧌䂷��R���g���[���[
public class AreaSelectionController : MonoBehaviour
{
    [Header("�G���A�{�^���̈ꗗ")]
    [SerializeField] private Button[] areaButtonArray;


    /// �e�{�^���ɃN���b�N�C�x���g��o�^����
    private void Awake()
    {
        foreach (Button button in areaButtonArray)
        {
            button.onClick.AddListener(() => OnAreaButtonClicked(button));
        }
    }


    /// �{�^���������ꂽ�Ƃ��̏����B�{�^������Text�����ɃV�[�������[�h����B
    private void OnAreaButtonClicked(Button button)
    {
        TMP_Text areaText = button.GetComponentInChildren<TMP_Text>();

        if (areaText == null)
        {
            Debug.LogError("�{�^����TextMeshPro�̃e�L�X�g��������܂���B");
            return;
        }

        string areaName = areaText.text.Trim();

        if (string.IsNullOrEmpty(areaName))
        {
            Debug.LogError("�G���A������ł��B");
            return;
        }

        PlayerPrefs.SetString("SelectedArea", areaName);
        PlayerPrefs.Save();

        LoadSelectedAreaScene(areaName);
    }


    /// �V�[����Build�ɓo�^����Ă���Γǂݍ���
    private void LoadSelectedAreaScene(string areaName)
    {
        if (Application.CanStreamedLevelBeLoaded(areaName))
        {
            SceneManager.LoadScene(areaName);
        }
        else
        {
            Debug.LogError($"�V�[�� '{areaName}' �� Build Settings �ɓo�^����Ă��܂���B");
        }
    }
}
