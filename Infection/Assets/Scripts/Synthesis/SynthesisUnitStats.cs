using StatePatteren.State;
using System.Collections;
using UnityEngine;

[System.Serializable]
public class SynthesisUnitStats
{
    //public enum ROLE            // ロール
    //{
    //    Attacker,               // アタッカー
    //    Tank,                   // タンク
    //    Healer,                 // ヒーラー
    //    Baffer,                 // バッファー
    //    Debaffer,               // デバッファー
    //    Archer,                 // アーチャー
    //    Wizard                  // 魔法使い
    //}

    public int unitCode;// ユニット番号
    public string unitName; // ユニット名
    public UnitStats.ROLE combo1; //　合成元ユニット1のロール
    public UnitStats.ROLE combo2; //　合成元ユニット2のロール
    public UnitStats.ROLE role; // ロール
    public float hp; // 体力
    public float maxHp; // 最大体力
    public float atk; // 攻撃力
    public float virusPow; // 感染力
    public float atkSpd; // 攻撃速度
    public float spd; // 移動速度
    public bool isFly; // 飛行ユニットかどうか
    public float range; // 射程距離
}
