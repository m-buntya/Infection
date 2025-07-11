using StatePatteren.State;
using System.Collections;
using UnityEngine;

[System.Serializable]
public class SynthesisUnitStats
{
    public enum ROLE            // ロール
    {
        Attacker,               // アタッカー
        Tank,                   // タンク
        Healer,                 // ヒーラー
        Baffer,                 // バッファー
        Debaffer,               // デバッファー
        Archer,                 // アーチャー
        Wizard                  // 魔法使い
    }

    public int unitCode;// ユニット番号
    public string unitName; // ユニット名
    public ROLE role; // ロール
    public float hp; // 体力
    public float atk; // 攻撃力
    public float virusPow; // 感染力
    public float atkSpd; // 攻撃速度
    public float spd; // 移動速度
    public bool isFly; // 飛行ユニットかどうか
    public float range; // 射程距離
}
