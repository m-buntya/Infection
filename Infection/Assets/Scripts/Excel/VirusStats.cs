using JetBrains.Annotations;
using Mono.Cecil;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

[System.Serializable]
public class VirusStats
{
    //感染経路を定義
    public enum TRANSMISSION
    {
        //仮置き
        a,
        b,
        c,
    }

    //進化条件を定義
    public enum EVOLUTIONCONDITIONS
    { 
        //仮置き
        a,
        b,
        c,

    }

    public int virusCode; //ウイルス番号
    public TRANSMISSION transmission; //感染経路
    public EVOLUTIONCONDITIONS evolutionConditions; //進化条件
    public string name;
    public string explanation; //説明文
    public float hp;//体力
    public float atk;//攻撃力
    public float virusPow; //感染力
    public float atkSpd; //攻撃速度
    public float spd; //移動速度
    public float range; //射程
}
    