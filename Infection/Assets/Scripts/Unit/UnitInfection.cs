using UnityEngine;

public class UnitInfection : MonoBehaviour
{
    public float virusPoint = 0f;
    public UnitData unitData;

    [Header("感染ゲージ")]
    [SerializeField] private Transform gaugeBar; // ゲージ部分のTransform
    [SerializeField] private float maxGaugeWidth = 1f; // 最大時のxスケール
    [SerializeField] private float gaugeOffset = -0.05f; // 左に寄せる調整値（必要に応じて調整）

    [SerializeField] private bool testAutoInfect = false; // テストモードスイッチ
    [SerializeField] private float testInfectSpeed = 10f; // 1秒あたりの増加量

    private Vector3 initialScale;
    private Vector3 initialLocalPosition;


    private void Start()
    {
        virusPoint = unitData.initialvirusPoint;
        InfectionManager.Instance.RegisterUnit(this);

        if (gaugeBar != null)
        {
            initialScale = gaugeBar.localScale;
            initialLocalPosition = gaugeBar.localPosition;
        }
    }
    private void Update()
    {
        if (testAutoInfect && virusPoint < unitData.maxVirusPoint)
        {
            virusPoint += testInfectSpeed * Time.deltaTime;
            virusPoint = Mathf.Min(virusPoint, unitData.maxVirusPoint);
            UpdateGaugeBar();

            if (virusPoint >= unitData.maxVirusPoint)
            {
                Debug.Log($"{unitData.unitName} は感染限界に達し、テストモードでも消滅しました。");
                InfectionManager.Instance.UnregisterUnit(this);
                Destroy(gameObject);
            }
        }
    }


    public void AddInfection(float amount)
    {
        virusPoint = Mathf.Min(virusPoint + amount, unitData.maxVirusPoint);
        Debug.Log($"{unitData.unitName} の感染値: {virusPoint}");

        UpdateGaugeBar();

        if (virusPoint >= unitData.maxVirusPoint) //ゲージマックスになった時に消えます
        {
            Debug.Log($"{unitData.unitName} は感染限界に達し、消滅しました。");
            //InfectionManager.Instance.UnregisterUnit(this);
            //Destroy(gameObject);
        }
    }

    private void UpdateGaugeBar()
    {
        if (gaugeBar == null) return;

        float ratio = Mathf.Clamp01(virusPoint / unitData.maxVirusPoint);
        float newWidth = ratio * maxGaugeWidth;

        // スケール
        Vector3 newScale = gaugeBar.localScale;
        newScale.x = newWidth;
        gaugeBar.localScale = newScale;

        // 左から伸びるように位置調整（＋さらに寄せオフセット）
        Vector3 newPos = gaugeBar.localPosition;
        newPos.x = (newWidth - maxGaugeWidth) / 2f + gaugeOffset;
        gaugeBar.localPosition = newPos;
    }


    private void OnDestroy()
    {
        if (InfectionManager.Instance != null)
            InfectionManager.Instance.UnregisterUnit(this);
    }
}