using JetBrains.Annotations;
using Mono.Cecil;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

[System.Serializable]
public class VirusStats
{
    //�����o�H���`
    public enum TRANSMISSION
    {
        //���u��
        a,
        b,
        c,
    }

    //�i���������`
    public enum EVOLUTIONCONDITIONS
    { 
        //���u��
        a,
        b,
        c,

    }

    public int virusCode; //�E�C���X�ԍ�
    public TRANSMISSION transmission; //�����o�H
    public EVOLUTIONCONDITIONS evolutionConditions; //�i������
    public string name;
    public string explanation; //������
    public float hp;//�̗�
    public float atk;//�U����
    public float virusPow; //������
    public float atkSpd; //�U�����x
    public float spd; //�ړ����x
    public float range; //�˒�
}
    