using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VillageController : MonoBehaviour
{
    [SerializeField] private int maxHP = 100;
    private int currentHP;

    [SerializeField] private GameObject defenderPrefab; // 増援として出るユニット
    [SerializeField] private Transform spawnParent;
    [SerializeField] private RectTransform hpBar; // ✅ HPバーとして使うUI
    [SerializeField] private float hpBarShrinkSpeed = 0.5f; // ✅ HPバーの減少速度
    [SerializeField] private float spawnRadius = 3f; // 増援ユニットの出現範囲
    [SerializeField] private List<UnitData> growableUnits; // 増える対象のユニットリスト
    [SerializeField] private UnitData unitData;

    private bool isBeingAttacked = false;

    void Start()
    {
        currentHP = maxHP;
        UpdateHPBarInstant();
    }

    void OnMouseDown()
    {
        CommandAllUnitsToAttack(); // ✅ 毎回タップ時にすべてのユニットを攻撃状態にする
    }

    void CommandAllUnitsToAttack()
    {
        UnitMovement[] allUnits = FindObjectsOfType<UnitMovement>(); // ✅ すべてのユニットを取得

        foreach (var unit in allUnits)
        {
            unit.SetAttackTarget(transform); // ✅ すべてのユニットのターゲットを村に設定
        }
    }

    public void ReceiveDamage(int damage)
    {
        currentHP -= damage;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);

        StartCoroutine(SmoothShrinkHPBar(damage)); // ✅ ダメージ量に応じてHPバー縮小

        if (currentHP <= 0)
        {
            Destroy(gameObject);
            Debug.Log("村が陥落しました！");
        }
    }

    void UpdateHPBarInstant()
    {
        if (hpBar != null)
        {
            float ratio = (float)currentHP / maxHP;
            hpBar.localScale = new Vector3(ratio, 1f, 1f);
        }
    }

    IEnumerator SmoothShrinkHPBar(int damage) // ✅ 引数を受け取るように修正
    {
        float startRatio = hpBar.localScale.x;
        float targetRatio = (float)currentHP / maxHP; // ✅ HPの割合を計算
        float progress = 0f;

        while (progress < 1f)
        {
            progress += Time.deltaTime * hpBarShrinkSpeed;
            float newRatio = Mathf.Lerp(startRatio, targetRatio, progress);
            hpBar.localScale = new Vector3(newRatio, 1f, 1f);
            yield return null; // ✅ 毎フレーム更新してスムーズに縮む
        }
    }

    void SpawnRandomDefenders()
    {
        int spawnCount = Random.Range(1, 4); // 1〜3体ランダム増援
        for (int i = 0; i < spawnCount; i++)
        {
            Vector3 spawnPos = transform.position + (Vector3)Random.insideUnitCircle * spawnRadius;
            Instantiate(defenderPrefab, spawnPos, Quaternion.identity, spawnParent);
        }
    }

    void IncreaseDeployableUnits()
    {
        foreach (var unit in growableUnits)
        {
            if (unit.canGrowDeploycount)
            {
                unit.maxDeployCount += Random.Range(1, 3); // 1～2体ランダム増加
                Debug.Log($"{unit.unitName} の最大出撃数が {unit.maxDeployCount} に増えました！");
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        UnitMovement unit = other.GetComponent<UnitMovement>();
        if (unit != null)
        {
            unit.StopMovement(); // ✅ 移動を停止
            unit.StartCoroutine(unit.AttackVillage()); // ✅ 攻撃を開始
        }
    }
}