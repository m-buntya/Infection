using UnityEngine;

public class UnitInfection : MonoBehaviour
{
    [Header("ゲージバーの設定")]
    public Transform gaugeTransform;
    public float maxScaleX = 0.95f;
    public float duration = 5f;
    public Color gaugeColor = Color.green;
    public GameObject gaugePrefub;

    private float elapsedTime = 0f;
    private Vector3 initialPosition;
    private SpriteRenderer spriteRenderer;

    //[SerializeField]
    private bool allowProgress = false;

    private bool infectionComplete = false;

    [Header("ゲージ位置補正")]
    public Vector3 positionOffset = Vector3.zero;

    [Header("感染完了時に表示する2Dオブジェクト")]
    public GameObject infectionCompleteObject;

    void Start()
    {
        gaugePrefub.SetActive(false);
        if (gaugeTransform == null)
        {
            Debug.LogError("GaugeTransform が設定されていません！");
            enabled = false;
            return;
        }

        gaugeTransform.localScale = new Vector3(0f, gaugeTransform.localScale.y, gaugeTransform.localScale.z);
        initialPosition = gaugeTransform.localPosition;

        spriteRenderer = gaugeTransform.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.color = gaugeColor;
        }
        Debug.Log($"Start() 実行 → allowProgress = {allowProgress}, enabled = {enabled}");

        infectionCompleteObject.SetActive(false);
    
}

public void StartProgress()
    {
        gaugePrefub.SetActive(true);
        allowProgress = true;
        enabled = true; 
        Debug.Log($"感染進行を開始しました（{gameObject.name}） → allowProgress = {allowProgress}, Scene = {gameObject.scene.name}");

        TickInfection(); // 即反映
    }
    public void StopProgress()
    {
        allowProgress = false;
        Debug.Log($"感染進行を停止しました（{gameObject.name}）");
    }

    void Update()
    {
        if (allowProgress && !infectionComplete)
        {
            TickInfection();
        }
    }

    public void AddInfection(float amount)
    {
        elapsedTime += amount;
        elapsedTime = Mathf.Clamp(elapsedTime, 0f, duration);
        UpdateGaugeVisual();
    }

    private void TickInfection()
    {
        if (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            UpdateGaugeVisual();

            if (!infectionComplete && elapsedTime >= duration)
            {
                infectionComplete = true;
                Debug.Log("感染完了！");
                OnInfectionComplete();
            }
        }
    }

    private void UpdateGaugeVisual()
    {
        gaugeTransform.localPosition = initialPosition + positionOffset;
        float t = Mathf.Clamp01(elapsedTime / duration);
        float newScaleX = Mathf.Lerp(0f, maxScaleX, t);

        gaugeTransform.localScale = new Vector3(newScaleX, gaugeTransform.localScale.y, gaugeTransform.localScale.z);
        gaugeTransform.localPosition = initialPosition + new Vector3((newScaleX - 1f) * 0.5f, 0f, 0f);

        if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.Lerp(gaugeColor, Color.red, t);
        }
    }

    private void OnInfectionComplete()
    {

        if (infectionCompleteObject != null)
        {
            infectionCompleteObject.SetActive(true);
        }
        else
        {
            Debug.LogWarning("感染完了オブジェクトが設定されていません");
        }
    }

}