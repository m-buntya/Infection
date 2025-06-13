using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class GameClearController : MonoBehaviour
{
    public int enemyBaseHP = 100;
    public int playerBaseHP = 100;
    public float remainingtime = 120f; //残り時間カウントダウン
   

    public Transform enemyHPBar; // 敵拠点のHPバー (2Dオブジェクト)
    public Transform playerHPBar; // 味方拠点のHPバー (2Dオブジェクト)
    public TMP_Text remainingTimeText; //残り時間テキスト

    private float originalBarScaleX; // 初期のバーの長さ

    void Start()
    {
        // 初期スケールを記録
        originalBarScaleX = enemyHPBar.localScale.x;
        
        float playerOriginalScaleX = playerHPBar.localScale.x;
        
        UpdateHPBars();


        //三秒後に味方拠点ダメージ適用（テスト用）
        //Invoke("TestDamageToPlayerBase", 3f);

        //三秒後に敵拠点ダメージ適用（テスト用）
        //Invoke("TestDamageToEnemyBase", 3f);
    }
    void Update()
    {
        remainingtime -= Time.deltaTime;
        //Debug.Log("残り時間：" + Mathf.CeilToInt(remainingtime) + "秒");

        UpdateRemainingTimeText();
        CheckGameClearConditions();
    }

    void UpdateRemainingTimeText()
    {
        remainingTimeText.text = "Time:" + Mathf.CeilToInt(remainingtime);
    }
    public void ApplyDamageToBase(bool isEnemy, int damage)
    {
        if (isEnemy)
            enemyBaseHP = Mathf.Max(enemyBaseHP - damage, 0);
        else
            playerBaseHP = Mathf.Max(playerBaseHP - damage, 0);

        UpdateHPBars();
        CheckGameClearConditions();
    }

    void UpdateHPBars()
    {
        float playerHPRatio = Mathf.Clamp((float)playerBaseHP / 100f, 0f, 1f); // 0～1の範囲に制限
        float enemyHPRatio = Mathf.Clamp((float)enemyBaseHP / 100f, 0f, 1f);

        playerHPBar.localScale = new Vector3(0.9699999f * playerHPRatio, playerHPBar.localScale.y, playerHPBar.localScale.z);
        enemyHPBar.localScale = new Vector3(originalBarScaleX * enemyHPRatio, enemyHPBar.localScale.y, enemyHPBar.localScale.z);

        //Debug.Log("📏 更新後の味方HPバースケール: " + playerHPBar.localScale.x);
    }

   public void CheckGameClearConditions()
    {
        if (enemyBaseHP <= 0)
        {
            TriggerGameClearEffects(GAME_CLEAR_TYPE.ObjectiveVictory);
            SceneManager.LoadScene("HomeScene");　//敵の拠点が０になったら勝利シーン（仮でHomeScene）に移動！
        }
        else if (playerBaseHP <= 0)
        {
            TriggerGameOverEffects(GAME_CLEAR_TYPE.SurvivalVictory);
            SceneManager.LoadScene("HomeScene"); //味方の拠点が０になったら敗北シーン（仮でHomeScene）に移動！
        }
        else if(remainingtime<=0) //カウントダウン終了
        {
            Debug.Log("120秒間生存達成！ゲームクリアシーンに移動！");
            TriggerGameClearEffects(GAME_CLEAR_TYPE.SurvivalVictory);
            SceneManager.LoadScene("HomeScene");
        }
       
     }

    void TriggerGameClearEffects(GAME_CLEAR_TYPE clearType)
    {
        Debug.Log("ゲームクリア演出: " + clearType.ToString());
        // ここにフェードアウトやエフェクト処理を追加
    }
    void TriggerGameOverEffects(GAME_CLEAR_TYPE overType)
    {
        Debug.Log("💀 ゲームオーバー演出: " + overType.ToString());
        // ここにフェードアウトやエフェクト処理を追加
    }
    //テスト用メソッド
    void TestDamageToPlayerBase()
    {
        Debug.Log("！！三秒経過！味方拠点にダメージ適用");
        ApplyDamageToBase(false, 100); 
    }
    void TestDamageToEnemyBase()
    {
        Debug.Log("s三秒経過！敵拠点にダメージ適用");
        ApplyDamageToBase(true, 100);
    }

    // 勝利タイプの定義
    public enum GAME_CLEAR_TYPE
    {
        InfectionVictory, //仮
        SurvivalVictory, 
        ObjectiveVictory 
            //ほかのノルマ
    }
}