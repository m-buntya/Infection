using UnityEngine;
using StatePatteren.State;

public class InfectionBarVisualizer : MonoBehaviour
{
    [Header("対象設定")]
    [SerializeField] private UnitController unit;
    [SerializeField] private SpriteRenderer barRenderer;
    [SerializeField] private string virusType = "Enemy";
    [SerializeField] private float maxWidth = 1.0f;
    [SerializeField] private float maxPoint = 100f;

    [Header("ログ表示")]
    [SerializeField] private bool showDebugLog = false; // ← Inspector から切り替え可能
    void Start()
    {
        if (unit == null)
            unit = GetComponentInParent<UnitController>();
    }

    void Update()
    {
        if (unit == null || barRenderer == null) return;

        float value = virusType == "Enemy"
            ? unit.unitStats.enemyVirusPoint
            : unit.unitStats.virusPoint;

        float ratio = Mathf.Clamp01(value / maxPoint);

        if (showDebugLog)
        {
            Debug.Log($"{unit.name}の{virusType}ウイルスゲージ：{value}/{maxPoint} ({ratio:P0})");
        }

        var scale = barRenderer.transform.localScale;
        scale.x = maxWidth * ratio;
        barRenderer.transform.localScale = scale;
    }
}