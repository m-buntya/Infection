using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class TestScript : MonoBehaviour
{
    [SerializeField] VirusChoice virusChoice;

    VirusStats a;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //a = virusChoice.GetCurrentVirus();

        //if (a != null)
        //{
        //    Debug.Log("ウイルス名: " + a.name);
        //}
        //else
        //{
        //    Debug.LogWarning("ウイルスがまだ設定されていません！");
        //}

    }

    // Update is called once per frame
    void Update()
    {
        if (a == null)
        {
            a = virusChoice.GetCurrentVirus();
            if (a != null)
            {
                Debug.Log("ウイルス名: " + a.name);
                Debug.Log("ウイルス番号: " + a.virusCode);
                Debug.Log("感染経路: " + a.transmission);
                Debug.Log("進化条件: " + a.evolutionConditions);
                Debug.Log("説明文: " + a.explanation);
                Debug.Log("体力: " + a.hp);
                Debug.Log("攻撃力: " + a.atk);
                Debug.Log("感染力: " + a.virusPow);
                Debug.Log("攻撃速度: " + a.atkSpd);
                Debug.Log("移動速度: " + a.spd);
                Debug.Log("射程: " + a.range);

            }
        }
    }
}
