using TMPro;
using UnityEngine;

public class DamegeTextController:MonoBehaviour
{
    [SerializeField] TMP_Text damageText;
    Transform target;
    Vector3 offset = new Vector3(0, 0.01f, 0); //頭上に表示
    [SerializeField] Animator animator;

    private void Awake()
    {
        if (damageText != null)
        {
            damageText.enabled = false;
        }
    }

    public void Initialize(float damage, Transform followTarget)
    {
        target = followTarget;

        if (damageText != null)
        {
            damageText.text = $"-{Mathf.Abs(damage)}";
            damageText.enabled = true;
        }

        animator?.Play("FadeUp"); // ← アニメーション再生

        Invoke(nameof(DestroySelf), 1.5f);
    }


    void LateUpdate()
    {
        if (target == null) return;

        Vector3 screenPos = Camera.main.WorldToScreenPoint(target.position + offset);
        transform.position = screenPos;
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }

}
