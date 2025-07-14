using StatePatteren.State;
using UnityEngine;


/// 弾（Projectile）の処理を行うクラス。
/// 敵味方を判定し、命中したユニットにダメージを与える。
public enum UnitGroup
{
    Player, // プレイヤー側のユニット
    Enemy   // 敵側のユニット
}

public class Projectile : MonoBehaviour
{
    [Header("この弾が与えるダメージ量")]
    public int damage;

    [Header("この弾を撃ったユニットの所属グループ")]
    public UnitGroup shooterGroup;


    /// 2Dトリガーに他のColliderが入ったときに呼ばれる処理
    private void OnTriggerEnter2D(Collider2D other)
    {
        // ユニット管理クラスを取得（フィールド上のユニット一覧を持っている）
        UnitManager unitManager = GameObject.FindObjectOfType<UnitManager>();
        if (unitManager == null)
        {
            Debug.LogWarning("UnitManager がシーン上に存在しません");
            return;
        }

        GameObject otherObj = other.gameObject;
        bool isEnemy = false;

        // 衝突した相手が敵かどうかを判断
        if (shooterGroup == UnitGroup.Player)
        {
            // 自分がプレイヤーの場合、相手が敵リストに含まれていれば敵
            isEnemy = unitManager.GetEnemyUnits().Contains(otherObj);
        }
        else if (shooterGroup == UnitGroup.Enemy)
        {
            // 自分が敵の場合、相手がプレイヤーリストに含まれていれば敵
            isEnemy = unitManager.GetPlayerUnits().Contains(otherObj);
        }

        if (isEnemy)
        {
            // 相手に UnitController がついていればダメージを与える
            var unit = other.GetComponent<UnitController>();
            if (unit != null)
            {
                unit.TakeDamage(damage);
            }

            // 弾をオブジェクトプールに戻す（なければ破棄）
            if (ProjectilePool.Instance != null)
            {
                ProjectilePool.Instance.ReturnProjectile(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }


    /// 弾が再利用・有効化されたときに呼ばれる処理
    private void OnEnable()
    {
        // 速度をリセットして前回の動きを残さないようにする
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
        }
    }
}
