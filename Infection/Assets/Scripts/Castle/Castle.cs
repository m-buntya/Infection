using System;
using UnityEngine;

public class Castle
{
    float hp;
    float maxHp;
    bool isDestroy;
    string castleName;

    public event Action<float, string> OnDamaged;
    public Castle(int maxHp, string name)
    {
        this.maxHp = maxHp;
        hp = this.maxHp;
        isDestroy = false;
        castleName = name;
    }

    public void TakeDamage(float damage)
    {
        hp -= damage;

        Debug.Log($"{castleName}がダメージを受けた！ Damege:{damage}");

        OnDamaged?.Invoke(damage, castleName);

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
