using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class CastleManager : MonoBehaviour
{
    Dictionary<GameObject, Castle> castleDic = new Dictionary<GameObject, Castle>();
    [SerializeField] GameObject playerCastle;
    [SerializeField] GameObject enemyCastle;
    [SerializeField] CastleUI castleUI;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        castleDic[playerCastle] = new Castle(100);
        castleDic[enemyCastle] = new Castle(100);
    }

    // Update is called once per framete
    void Update()
    {
        if (castleDic[playerCastle].IsDestroy())
        {
            Debug.Log("敗北・・・");
        }
        else if (castleDic[enemyCastle].IsDestroy())
        {
            Debug.Log("勝利！！！");
        }
    }

    public Castle GetCastle(GameObject castle)
    {
        return castleDic[castle];
    }
    public void RequestPlayerCastleDamage(float damage)
    {
        if (castleUI != null)
        {
            castleUI.DamagePlayerCastle(damage);
        }
        else
        {
            Debug.LogWarning("CastleUI が未設定です");
        }
    }

    public void RequestEnemyCastleDamage(float damage)
    {
        if (castleUI != null)
        {
            castleUI.DamageEnemyCastle(damage);
        }
        else
        {
            Debug.LogWarning("CastleUI が未設定です");
        }
    }
}
