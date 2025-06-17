using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class TestScript : MonoBehaviour
{
    [SerializeField] VirusChoice virusChoice;

    VirusStats a;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //a = virusChoice.GetCurrentVirus();

        //if (a != null)
        //{
        //    Debug.Log("�E�C���X��: " + a.name);
        //}
        //else
        //{
        //    Debug.LogWarning("�E�C���X���܂��ݒ肳��Ă��܂���I");
        //}

    }

    // Update is called once per frame
    void Update()
    {
        if (a == null)
        {
            a = virusChoice.GetCurrentVirus();
            if (a != null)
            {
                Debug.Log("�E�C���X��: " + a.name);
                Debug.Log("�E�C���X�ԍ�: " + a.virusCode);
                Debug.Log("�����o�H: " + a.transmission);
                Debug.Log("�i������: " + a.evolutionConditions);
                Debug.Log("������: " + a.explanation);
                Debug.Log("�̗�: " + a.hp);
                Debug.Log("�U����: " + a.atk);
                Debug.Log("������: " + a.virusPow);
                Debug.Log("�U�����x: " + a.atkSpd);
                Debug.Log("�ړ����x: " + a.spd);
                Debug.Log("�˒�: " + a.range);

            }
        }
    }
}
