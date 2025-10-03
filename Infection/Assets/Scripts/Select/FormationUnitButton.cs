using UnityEngine;
using UnityEngine.UI;
using TMPro;
using StatePatteren.State;

public class FormationUnitButton : MonoBehaviour
{
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

    [Header("兵士数（外部から設定）")]
    public int soldierCnt = 0;

    private void Start()
    {
        var hiddenRoot = panelManager.HiddenUnitRoot; // ✅ FormationPanelManager から取得

        var instance = Instantiate(unitPrefub, hiddenRoot); // ✅ 非表示の親に入れる
        instance.SetActive(false); // ✅ 念のため個別にも非表示

        unitcontroller = instance.GetComponent<UnitController>();
        attackBace = instance.GetComponent<UnitAttackBace>();

        var statsFromPrefab = unitPrefub.GetComponent<UnitAttackBace>()?.unitStats;
        if (statsFromPrefab == null)
        {
            Debug.LogError("❌ プレファブに unitStats が設定されていません");
            return;
        }

        unitcontroller.SetUnitStats(statsFromPrefab);
        SetRedFrameVisible(false);

        button.onClick.AddListener(() =>
        {
            panelManager.HighlightUnit(unitcontroller);
            UpdateUnitUI(statsFromPrefab);

            var icon = unitcontroller.GetIconSprite();
            var slotButton = panelManager.GetCurrentSlotButton();
            if (slotButton != null)
            {
                slotButton.SetUnit(unitcontroller, icon, statsFromPrefab.unitCode.ToString(), statsFromPrefab.unitName);
            }
        });
    }
    private void UpdateUnitUI(UnitStats stats)
    {
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
    }
    public void SetRedFrameVisible(bool visible)
    {
        if (redFrame != null)
            redFrame.SetActive(visible);
    }
}