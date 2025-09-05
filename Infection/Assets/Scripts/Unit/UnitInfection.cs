using UnityEngine;

public class UnitInfection : MonoBehaviour
{
    [Header("ゲージバーの設定")]
    public Transform gaugeTransform;         // ゲージバーの Transform（スプライトオブジェクト）
    public float maxScaleX = 2f;             // 最大スケール（X方向）
    public float duration = 5f;              // 最大スケールに達するまでの時間（秒）
    public Color gaugeColor = Color.green;   // ゲージの色（SpriteRenderer に適用）

    private float elapsedTime = 0f;
    private Vector3 initialPosition;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        if (gaugeTransform == null)
        {
            Debug.LogError("GaugeTransform が設定されていません！");
            enabled = false;
            return;
        }

        // 初期スケールをゼロにして「ゲージなし」状態から開始
        gaugeTransform.localScale = new Vector3(0f, gaugeTransform.localScale.y, gaugeTransform.localScale.z);
        initialPosition = gaugeTransform.localPosition;

        // 色を設定
        spriteRenderer = gaugeTransform.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.color = gaugeColor;
        }
    }

    void Update()
    {
        if (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);
            float newScaleX = Mathf.Lerp(0f, maxScaleX, t);

            // スケール変更
            gaugeTransform.localScale = new Vector3(newScaleX, gaugeTransform.localScale.y, gaugeTransform.localScale.z);

            // 左端固定（Pivotが中央の場合） → スケールの半分だけ右にずらす
            float offset = newScaleX * 0.5f;
            gaugeTransform.localPosition = initialPosition + new Vector3(offset, 0f, 0f);
        }
    }

    // 外部から感染を追加するメソッド（秒数ベース）
    public void AddInfection(float amount)
    {
        elapsedTime += amount;
        elapsedTime = Mathf.Clamp(elapsedTime, 0f, duration);
    }
}