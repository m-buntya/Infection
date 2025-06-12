using UnityEngine;

public class HealthBar2D : MonoBehaviour
{
    [SerializeField] private SpriteRenderer healthBarSprite;
    private float maxHealth = 100f;
    private float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            Destroy(transform.parent.gameObject); // ユニットが死亡
        }
    }

    public void Heal(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
        UpdateHealthBar();
    }

    void UpdateHealthBar()
    {
        if (healthBarSprite != null)
        {
            float healthRatio = currentHealth / maxHealth;
            healthBarSprite.transform.localScale = new Vector3(healthRatio, 1f, 1f); // ✅ 体力に応じて横方向にスケール調整
        }
    }
}