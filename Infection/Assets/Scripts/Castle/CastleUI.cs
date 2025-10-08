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
    [SerializeField] TMP_Text testText;

    private void Start()
    {
        playerCastleText.enabled = false;
        enemyCastleText.enabled = false;
        testText.enabled = false;
        StartCoroutine(TestDisplay());
    }

    IEnumerator TestDisplay()
    {
        yield return new WaitForSeconds(2f);
        testText.text = "10";
        testText.enabled = true;

        yield return new WaitForSeconds(1.5f);

        testText.enabled = false;
    }
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
        targetText.text = damage.ToString();
        targetText.enabled = true;
        yield return new WaitForSeconds(1.5f);
        targetText.enabled = false;
    }
}