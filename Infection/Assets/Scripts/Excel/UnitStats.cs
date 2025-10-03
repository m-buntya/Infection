using StatePatteren.State;
using System.Collections;
using UnityEngine;

[System.Serializable]
public class UnitStats
{
    public enum LEADER_SKILL    // リーダースキル
    {
        AtkBoost,               // 攻撃力上昇
        SpdBoost,               // 移動速度上昇
        Regeneration,           // 自然回復
    }

    public enum ROLE            // ロール
    {
        Attacker,               // アタッカー
        Tank,                   // タンク
        Healer,                 // ヒーラー
        Baffer,                 // バッファー
        Debaffer,               // デバッファー
        Archer,                 // アーチャー
        Wizard,                 // 魔法使い
    }

    public int unitCode;                // ユニット番号
    public string unitName;             // ユニット名
    public LEADER_SKILL leaderSkill;    // リーダースキル
    public ROLE role;                   // ロール
    public int lv;                      // レベル
    public int maxLv;                   // 最大レベル
    public float hp;                    // 体力
    public float maxHp;                 // 最大体力
    public float virusPoint;            // 感染ゲージ(味方)
    public float virusMaxPoint;         // 最大感染ゲージ(味方)
    public float enemyVirusPoint;       // 感染ゲージ(敵)
    public float enemyVirusMaxPoint;    // 最大感染ゲージ(敵)
    public float atk;                   // 攻撃力
    public float virusPow;              // 感染力
    public float atkSpd;                // 攻撃速度
    public float spd;                   // 移動速度
    public float range;                 // 射程距離
    public int cost;                    // コスト
    public int sortieCoolTime;          // 次の出撃にかかる時間
}