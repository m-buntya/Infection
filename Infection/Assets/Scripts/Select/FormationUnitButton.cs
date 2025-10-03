using UnityEngine;
using UnityEngine.UI;
using TMPro;
using StatePatteren.State;
using Unity.VisualScripting;
public class UnitParametor
{
    public UnitStats leaderUnit { get; private set; }   // リーダーユニットのパラメータ

    float defaultMaxHp = 0;
    float defaultAtk = 0;
    float defaultVirusPow = 0;
    float defaultSpd = 0;

    public int unitMemberCnt;
    int unitMemberMinCnt = 0;
    int unitMemberMaxCnt = 100;

    // 雑兵のメンバー数をセット
    public void SetSoldierCnt(int value)
    {
        unitMemberCnt = Mathf.Clamp(value, unitMemberMinCnt, unitMemberMaxCnt);
    }

    // リーダーのパラメータをセット
    public void SetLeaderStats(UnitStats leader)
    {
        leaderUnit = leader;

        defaultMaxHp = leader.maxHp;
        defaultAtk = leader.atk;
        defaultVirusPow = leader.virusPow;
        defaultSpd = leader.spd;
    }

    // 部隊のパラメータをセット
    public void SetUnitPara()
    {
        float correction = unitMemberCnt * 0.01f;      // 部隊の人数 * 1%の補正値

        leaderUnit.maxHp    = defaultMaxHp       + defaultMaxHp * correction;
        leaderUnit.hp       = leaderUnit.maxHp;
        leaderUnit.atk      = defaultAtk      + defaultAtk      * correction;
        leaderUnit.virusPow = defaultVirusPow + defaultVirusPow * correction;
        float slowRate = (float)unitMemberCnt / unitMemberMaxCnt;
        leaderUnit.spd = defaultSpd * (1 - slowRate);
    }
}

public class FormationUnitButton : MonoBehaviour
{
    [SerializeField] UnitStatsData unitStatsData;
    public UnitParametor unitPara { get; private set; }

    public GameObject unitPrefub;
    public GameObject redFrame;
    public Button button;
    public FormationPanelManager panelManager;

    public UnitController unitcontroller;

    public UnitAttackBace attackBace;

    private UnitStats currentlySelectedStats;

    [Header("表示用UI")]
    public TextMeshProUGUI roleText;
    public TextMeshProUGUI soldierCntText;
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI atkText;
    public TextMeshProUGUI virusPowText;
    public TextMeshProUGUI atkSpdText;
    public TextMeshProUGUI spdText;
    public TextMeshProUGUI rangeText;
    public TextMeshProUGUI costText;
    public TextMeshProUGUI unitNameText;
    public TextMeshProUGUI unitCodeText;

    [SerializeField] Slider soldierSlider;
    [Header("兵士数（外部から設定）")]
    public int soldierCnt = 0;

    private void Start()
    {
        unitPara = new UnitParametor();
        soldierSlider.onValueChanged.AddListener(OnSliderSoldier);      // スライダーの変更を検知できるようにする

        if (button != null)
        {
            button.onClick.AddListener(() =>
            {
                if (panelManager != null)
                {
                    var controller = unitPrefub.GetComponent<UnitController>();
                    panelManager.HighlightUnit(controller);
                }

                var attackBace = unitPrefub.GetComponent<UnitAttackBace>();
                if (attackBace != null && attackBace.unitStats != null)
                {
                    var stats = attackBace.unitStats;

                    roleText.text = stats.role.ToString();
                    soldierCntText.text = soldierCnt.ToString();
                    hpText.text = stats.maxHp.ToString("F1");
                    atkText.text = stats.atk.ToString("F1");
                    virusPowText.text = stats.virusPow.ToString("F1");
                    atkSpdText.text = stats.atkSpd.ToString("F1");
                    spdText.text = stats.spd.ToString("F1");
                    rangeText.text = stats.range.ToString();
                    costText.text = stats.cost.ToString();
                    unitNameText.text = stats.unitName;
                    unitCodeText.text = stats.unitCode.ToString();

                    var icon = unitPrefub.GetComponentInChildren<SpriteRenderer>()?.sprite;
                    var slotButton = panelManager.GetCurrentSlotButton();
                    if (slotButton != null)
                    {
                        slotButton.SetUnit(unitcontroller, icon, stats.unitCode.ToString(), stats.unitName);
                        Debug.Log($"📦 FormationUnitButton: unitCode = {stats.unitCode}, unitName = {stats.unitName}, iconName = {icon?.name}");
                        Debug.Log($"🔍 unitcontroller = {unitcontroller.name}, instanceID = {unitcontroller.GetInstanceID()}");
                    }
                }
                else
                {
                    Debug.LogWarning("UnitAttackBace または unitStats が null です");
                }
            });
        }
        attackBace = unitPrefub.GetComponent<UnitAttackBace>();

        SetRedFrameVisible(false);
    }

    public void SetRedFrameVisible(bool visible)
    {
        if (redFrame != null)
            redFrame.SetActive(visible);
    }

    // 雑兵数選択
    void OnSliderSoldier(float value)
    {
        unitPara.SetSoldierCnt((int)value);
        unitPara.SetUnitPara();
    }
}