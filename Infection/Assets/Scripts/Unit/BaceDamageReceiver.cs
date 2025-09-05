using UnityEngine;

public class BaceDamageReceiver:MonoBehaviour
{
    public bool isEnemyBase = true; //この拠点が敵かどうか
    public int damagePerHit = 10;　//ユニット１体当たりのダメージ量

    private GameClearController gameClearController;

    private void Start()
    {
        gameClearController = GameObject.FindObjectOfType<GameClearController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //プレイヤーユニットのみ判定
    }
}
