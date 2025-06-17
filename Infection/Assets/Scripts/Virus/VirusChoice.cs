using StatePatteren.State;
using UnityEngine;
using UnityEngine.UIElements.Experimental;
using static UnityEditorInternal.ReorderableList;
using static VirusStats;

public class VirusChoice : MonoBehaviour
{
    [SerializeField] VirusData virusData;
    [SerializeField] VirusUIManager virusUIManager;

    //�E�C���X�̃X�e�[�^�X���Ǘ�
    private class VirusManager
    {
        public VirusStats virusStats { get; private set; }

        public void SetVirus(VirusStats selected)
        {
            virusStats = selected;
        }
    }

    private VirusManager virusManager = new VirusManager(); // �C���X�^���X����

    //�E�C���X���O������擾����̂ɕK�v�Ȋ֐�
    public VirusStats GetCurrentVirus()
    {
        return virusManager.virusStats;
        //TODO �O���֗�����悤�ɂ���A�m�F��Ƃ�����
        /* [SerializeField] VirusChoice virusChoice;
        VirusStats �Z�Z�Z;
        �Z�Z�Z =�@virusChoice.GetCurrentVirus();
        Debug.Log("�E�C���X��" + �Z�Z�Z.name)�@���̂悤�ɂ���Ǝ擾�ł���
        void Start�Ȃǃ^�C�~���O�ɂ���Ă̓o�O���Ď擾�ł��Ȃ����Ƃ����邩������Ȃ�*/

    }

    void Start()
    {
        //�P�ڂ̃E�C���X�������\��
        OnClickLeader(0);
    }

    //�{�^�����������ƂɃE�C���X��ς��Ēl���Z�b�g����
    public void OnClickLeader(int index)
    {
        //�}�X�^�[���Ȃ������Ƃ��̃f�o�b�O�p
        if (virusData == null || virusData.VirusParameter.Count <= index)
        {
            Debug.LogWarning("�E�C���X�̃}�X�^�[���s�����A�͈͊O�ł�");
            return;
        }

        VirusStats selected = Clone(virusData.VirusParameter[index]); // �I�����ꂽ�E�C���X�̃X�e�[�^�X�𕡐�

        // �Ǘ��N���X�ɃZ�b�g
        virusManager.SetVirus(selected);

        //UI�̍X�V
        virusUIManager.SetVirusInfo(selected);
    }

    //VirusStats�𕡐�����֐�
    public VirusStats Clone(VirusStats original)
    {
        return new VirusStats
        {
            virusCode = original.virusCode,
            transmission = original.transmission,
            evolutionConditions = original.evolutionConditions,
            name = original.name,
            explanation = original.explanation,
            hp = original.hp,
            atk = original.atk,
            virusPow = original.virusPow,
            atkSpd = original.atkSpd,
            spd = original.spd,
            range = original.range
        };
    }

}
