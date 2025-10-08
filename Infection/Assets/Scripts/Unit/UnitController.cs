using System.Data;
using System.Net;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Rendering;

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

        public UnitStats unitStats { get; private set; }

        public bool isSynthesisReady { get; private set; } = false;

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

        // 合成準備完了の設定
        public void SetSynthesisReady(bool value)
        {
            isSynthesisReady = value;
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            if(unitGroup == UNIT_GROUP.PLAYER)
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

            Debug.Log($"group:{unitGroup}");
        }

        // Update is called once per frame
        void Update()
        {
            stateMachine.Update();
            Debug.Log($"感染ゲージ状態:{unitStats.enemyVirusPoint}");
            Debug.Log($"group:{unitGroup}");
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
        }

        // 感染ゲージ増加処理
        public void TakeVirusDamage(float addPoint, string group)
        {
            if (group == "Enemy")
            {
                unitStats.enemyVirusPoint += addPoint;
                Debug.Log($"Unit：敵ウイルスの感染ゲージが{addPoint}上昇した");
                TakeChange(unitStats.enemyVirusPoint);
            }
            else
            {
                unitStats.virusPoint += addPoint;
                Debug.Log($"Unit：自ウイルスの感染ゲージが{addPoint}上昇した");
                TakeChange(unitStats.virusPoint);
            }            
        }

        //寝返り処理
        public void TakeChange(float countPoint)
        {
            if (countPoint < unitStats.enemyVirusMaxPoint || countPoint < unitStats.virusMaxPoint) return;

            Debug.Log($"countPoint:{countPoint}");

            Debug.Log("寝返り処理開始");
            if (unitGroup == UNIT_GROUP.ENEMY && countPoint >= unitStats.enemyVirusMaxPoint)
            {
                Debug.Log("敵が寝返った");
                //値、グループ、見た目の変更
                unitStats.enemyVirusPoint = 0;
                unitStats.virusPoint = 0;
                unitGroup = UNIT_GROUP.PLAYER;
                Debug.Log($"group:{unitGroup}");
                this.transform.localScale = new Vector3(-this.transform.localScale.x, this.transform.localScale.y, this.transform.localScale.z);    // 見た目だけ反転
                
                //リスト処理
                unitManager.RemoveUnitList(this.gameObject, "Enemy");// 敵リストから削除
                unitManager.AddUnitList(this.gameObject, "Player");// 味方リストに追加

            }
            if (unitGroup == UNIT_GROUP.PLAYER && countPoint >= unitStats.virusMaxPoint)
            {
                Debug.Log("味方が寝返った");
                //値、グループ、見た目の変更
                unitStats.virusPoint = 0;
                unitStats.enemyVirusPoint = 0;
                unitGroup = UNIT_GROUP.ENEMY;
                Debug.Log($"group:{unitGroup}");
                this.transform.localScale = new Vector3(-this.transform.localScale.x, this.transform.localScale.y, this.transform.localScale.z);    // 見た目だけ反転

                //リスト処理
                unitManager.RemoveUnitList(this.gameObject, "Player");// リストから削除
                unitManager.AddUnitList(this.gameObject, "Enemy");// 敵リストに追加
            }
        }

        // 回復処理
        public void CareHp(float hp)
        {
            unitStats.hp += hp;

            if(unitStats.hp > unitStats.maxHp)
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

            if(unitGroup == UNIT_GROUP.PLAYER)
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
                    enemyVirusMaxPoint = 100,
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

    }


}