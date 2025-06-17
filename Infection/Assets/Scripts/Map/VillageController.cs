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
        UpdateHPBarImmediate(); // 初期状態に反映
    }

    private void Update()
    {
        // 滑らかにX方向の比率のみ変更
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
        Debug.Log($"ダメージ: {damage}, 現在HP: {currentHP}, 割合: {targetRatio}");
        Debug.Log($"受けたダメージ: {damage}, 残りHP: {currentHP}, targetRatio: {targetRatio}");
        if (currentHP == 0)
        {
            
            // TODO: 陥落演出など追加
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