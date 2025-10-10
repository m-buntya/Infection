using UnityEngine;
using TMPro;
using System.Collections;

public class CastleUI : MonoBehaviour
{
    [SerializeField] CastleManager castleManager;
    [SerializeField] GameObject playerCastle;
    [SerializeField] GameObject enemyCastle;
    [SerializeField] TMP_Text playerCastleText;
    [SerializeField] TMP_Text enemyCastleText;

    public void DamagePlayerCastle(float damage)
    {
        Castle castle = castleManager.GetCastle(playerCastle);
        castle.TakeDamage(damage);
        StartCoroutine(ShowDamage(playerCastleText, damage));
    }

    public void DamageEnemyCastle(float damage)
    {
        Castle castle = castleManager.GetCastle(enemyCastle);
        castle.TakeDamage(damage);
        StartCoroutine(ShowDamage(enemyCastleText, damage));
    }

    IEnumerator ShowDamage(TMP_Text targetText, float damage)
    {
        Debug.Log($"ShowDamage 呼び出し成功！Damage: {damage}"); // ← 追加！


        targetText.gameObject.SetActive(true);     // 表示ON（初期は非アクティブ）
        targetText.text = damage.ToString();       // ダメージ数値のみ表示

        yield return new WaitForSeconds(2f);       // 表示時間（アニメーションと合わせる）

        targetText.gameObject.SetActive(false);    // 非表示（アニメーション終了後）
    }
     //✅ テスト用メソッド
    [ContextMenu("テキストテスト表示")]
    public void ShowTestText()
    {
        StartCoroutine(ShowDamage(playerCastleText, 999)); // 任意の数値でテスト
    }

}