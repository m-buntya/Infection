using System.Data;
using System.Net;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
namespace StatePatteren.State
{
    [RequireComponent(typeof(UnitInfection))]
    public class UnitController : MonoBehaviour
    {
        public enum UNIT_GROUP
        {
            PLAYER,
            ENEMY,
        }

        UnitGenerater unitGenerater;
        UnitManager unitManager;

        [SerializeField] GameObject damageTextPrefab;
        [SerializeField] Canvas uiCanvas;
        public UnitStats unitStats { get; private set; }

        private SquadStateMachine stateMachine;

        public SquadStateMachine StateMachine => stateMachine;

        UNIT_GROUP unitGroup;
        bool isDead => unitStats.hp <= 0;

        public void SetUnitStats(UnitStats stats)
        {
            unitStats = stats;
        }

        public void SetUnitGroup(UNIT_GROUP group)
        {
            unitGroup = group;
        }

        public UNIT_GROUP GetUnitGroup()
        {
            return unitGroup;
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            if (unitGroup == UNIT_GROUP.PLAYER)
            {
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);    // 見た目だけ反転
            }

            unitGenerater = GameObject.Find("UnitGenerater").GetComponent<UnitGenerater>();
            unitManager = GameObject.Find("UnitManager").GetComponent<UnitManager>();
            stateMachine = new SquadStateMachine(this);
            if (unitStats == null)
            {
                Debug.LogError("❌ unitStats が未設定です。SetUnitStats() が呼ばれているか確認してください。");
                return;
            }

            stateMachine = new SquadStateMachine(this);

            if (stateMachine.readyState == null)
            {
                Debug.LogError("❌ readyState が初期化されていません。SquadStateMachine のコンストラクタを確認してください。");
                return;
            }

            unitStats.hp = unitStats.maxHp;

            stateMachine.Initialize(stateMachine.readyState);
            //Invoke(nameof(TestDamageText), 3f);
        }

        // Update is called once per frame
        void Update()
        {
            stateMachine.Update();
        }

        // ダメージ処理
        public void TakeDamage(float damage)
        {
            unitStats.hp -= damage;
            Debug.Log($"Unit：{damage}のダメージを受けた");

            if (isDead)
            {
                Dead();
            }
            ShowDamageText(damage);
        }

        // 感染ゲージ増加処理
        public void TakeVirusDamage(float addPoint, string group)
        {
            if (group == "Enemy")
            {
                unitStats.enemyVirusPoint += addPoint;
                Debug.Log($"Unit：敵ウイルスの感染ゲージが{addPoint}上昇した");
            }
            else
            {
                unitStats.virusPoint += addPoint;
                Debug.Log($"Unit：自ウイルスの感染ゲージが{addPoint}上昇した");
            }
        }

        // 回復処理
        public void CareHp(float hp)
        {
            unitStats.hp += hp;

            if (unitStats.hp > unitStats.maxHp)
            {
                unitStats.hp = unitStats.maxHp;
            }

            Debug.Log($"Unit：体力が{hp}回復した");
        }

        // 感染回復処理
        public void CarevirusPoint(float carePoint, string group)
        {
            if (group == "Enemy")
            {
                unitStats.enemyVirusPoint -= carePoint;
                Debug.Log($"Unit：敵ウイルスの感染ゲージが{carePoint}減少した");
            }
            else
            {
                unitStats.virusPoint -= carePoint;
                Debug.Log($"Unit：自ウイルスの感染ゲージが{carePoint}減少した");
            }
        }

        // 壊滅処理
        void Dead()
        {
            Debug.Log("死亡処理開始");

            if (unitGroup == UNIT_GROUP.PLAYER)
            {
                unitManager.RemoveUnitList(gameObject, "Player");
            }
            else
            {
                unitManager.RemoveUnitList(gameObject, "Enemy");
            }

            Destroy(gameObject);
        }

        void Awake()
        {
            if (unitStats == null)
            {
                unitStats = new UnitStats
                {
                    unitName = "仮ユニット",
                    hp = 100,
                    maxHp = 100,
                    virusPoint = 0,
                    virusMaxPoint = 100,
                    // 他の初期値も必要に応じて設定
                };
                Debug.LogWarning("⚠ unitStats が未設定だったため、仮初期化されました。");
            }
        }

        public Sprite GetIconSprite()
        {
            var spriteRenderer = GetComponentInChildren<SpriteRenderer>(true); // ← trueで非アクティブも拾える
            return spriteRenderer?.sprite;
        }
        void ShowDamageText(float damage)
        {
            if (damageTextPrefab == null || uiCanvas == null) return;

            var textObj = Instantiate(damageTextPrefab, uiCanvas.transform);
            var controller = textObj.GetComponent<DamegeTextController>();
            controller?.Initialize(damage, transform); // ← ユニットの Transform を渡す
        }

        void TestDamageText()
        {
            float testDamage = -10f;
            Debug.Log($"🧪 テスト：{testDamage} のダメージ表示を実行します");
            ShowDamageText(testDamage);
        }



    }
}