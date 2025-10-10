using UnityEngine;

public class UnitAttackBace:MonoBehaviour
{
    public UnitStats unitStats; //ユニットのステータス
    public bool isEnemyUnit = false; //このユニットが敵かどうか
    public float attackInterval= 1f;
    private float attackTimer = 0f;


    private void Update()
    {
        attackTimer += Time.deltaTime;
       
    }

    void OnTriggerStay2D(Collider2D other)
    {
       
        if (attackTimer >= attackInterval)
        {
            
            GameClearController gameClearController = FindObjectOfType<GameClearController>();
            //if (gameClearController != null) return;
            int damage = Mathf.RoundToInt(unitStats.atk);
            if (isEnemyUnit && other.CompareTag("PlayerBace"))
            {
                
                gameClearController.ApplyDamageToBase(false, damage); //プレイヤー拠点にダメージ
                Debug.Log($"{unitStats.unitName}（敵）がプレイヤー拠点に{damage}ダメージを与えた");
                attackTimer = 0f;
            }
            else if (!isEnemyUnit && other.CompareTag("EnemyBace"))　//敵拠点にダメージ
            {
                gameClearController.ApplyDamageToBase(true, damage);
                Debug.Log($"{unitStats.unitName}（味方）が敵拠点に{damage}ダメージを与えた");
                attackTimer = 0f;
            }
            }
        
    }
}
