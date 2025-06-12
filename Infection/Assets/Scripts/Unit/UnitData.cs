using UnityEngine;

[CreateAssetMenu(fileName = "UnitData", menuName = "Game/UnitData")]
public class UnitData : ScriptableObject
{
    public string unitName;  // ���j�b�g��
    public int cost;         // �K�v�R�X�g
    public GameObject prefab; // ���j�b�g�̃v���n�u
}