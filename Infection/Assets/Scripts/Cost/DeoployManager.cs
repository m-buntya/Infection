using UnityEngine;

public class DeployManager : MonoBehaviour
{
    [SerializeField] private CostManager costManager;
    [SerializeField] private Transform deployPosition;
    [SerializeField] private UnitData[] unitOptions; // �قȂ郆�j�b�g�̃��X�g

    private UnitData selectedUnit;

    public void SelectUnit(int index)
    {
        if (index >= 0 && index < unitOptions.Length)
        {
            selectedUnit = unitOptions[index]; // �{�^�����������Ƃ��ɈقȂ郆�j�b�g���Z�b�g
            Debug.Log("�I�����ꂽ���j�b�g: " + selectedUnit.unitName);
        }
    }

    public void TryDeployUnit()
    {
        Debug.Log("TryDeployUnit ���s: ���j�b�g�o���`�F�b�N");

        if (selectedUnit == null)
        {
            Debug.LogWarning("���j�b�g���I������Ă��܂���I");
            return;
        }

        if (costManager.CanAfford(selectedUnit.cost))
        {
            Debug.Log("�R�X�gOK�I�o�����܂��I");
            costManager.SpendCost(selectedUnit.cost);
            DeployUnit();
        }
        else
        {
            Debug.LogWarning("�R�X�g������܂���I");
        }
    }

    void DeployUnit()
    {
        if (selectedUnit == null || selectedUnit.prefab == null)
        {
            Debug.LogWarning("���j�b�g�̃v���n�u���ݒ肳��Ă��܂���I");
            return;
        }

        GameObject newUnit = Instantiate(selectedUnit.prefab, deployPosition.position, Quaternion.identity);
        Debug.Log(selectedUnit.unitName + " ���o���I => " + newUnit);
    }
}