using UnityEngine;
using UnityEngine.UI;

public class VillageController : MonoBehaviour
{
    [SerializeField] private int maxHP = 100;
    private int currentHP;

    [SerializeField] private Image hpFillImage;

    [SerializeField] private float hpBarLerpSpeed = 10f;

    private float targetRatio = 1f;
    private float currentRatio = 1f;

    private void Start()
    {
        currentHP = maxHP;
        targetRatio = 0.95f;
        currentRatio = 0.95f;
        UpdateHPBarImmediate(); // ������Ԃɔ��f
    }

    private void Update()
    {
        // ���炩��X�����̔䗦�̂ݕύX
        currentRatio = Mathf.Lerp(currentRatio, targetRatio, Time.deltaTime * hpBarLerpSpeed);

        if (hpFillImage != null)
        {
            Vector3 originalScale = hpFillImage.rectTransform.localScale;
            hpFillImage.rectTransform.localScale = new Vector3(currentRatio, originalScale.y, originalScale.z);
        }
    }

    public void ReceiveDamage(int damage)
    {
        currentHP = Mathf.Max(currentHP - damage, 0);
        targetRatio = (float)currentHP / maxHP;
        Debug.Log($"�_���[�W: {damage}, ����HP: {currentHP}, ����: {targetRatio}");
        Debug.Log($"�󂯂��_���[�W: {damage}, �c��HP: {currentHP}, targetRatio: {targetRatio}");
        if (currentHP == 0)
        {
            
            // TODO: �ח����o�Ȃǒǉ�
        }
    }

    private void OnMouseDown()
    {
        UnitMovement[] allUnits = FindObjectsOfType<UnitMovement>();
        foreach (var unit in allUnits)
        {
            unit.SetAttackTarget(transform);
        }
    }

    private void UpdateHPBarImmediate()
    {
        if (hpFillImage != null)
        {
            Vector3 originalScale = hpFillImage.rectTransform.localScale;
            float ratio = (float)currentHP / maxHP;
            hpFillImage.rectTransform.localScale = new Vector3(ratio, originalScale.y, originalScale.z);
        }
    }
}