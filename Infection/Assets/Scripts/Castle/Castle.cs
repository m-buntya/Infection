using UnityEngine;

public class Castle
{
    float hp;
    float maxHp;
    bool isDestroy;

    public Castle(int maxHp)
    {
        this.maxHp = maxHp;
        hp = this.maxHp;
        isDestroy = false;
    }

    public void TakeDamage(float damage)
    {
        hp -= damage;

        Debug.Log($"城がダメージを受けた！ Damege:{damage}");

        if (hp < 0)
        {
            isDestroy = true;
        }
    }

    public bool IsDestroy()
    {
        return isDestroy;
    }

    public float HpPercent()
    {
        return (hp / maxHp) * 100;
    }
}
