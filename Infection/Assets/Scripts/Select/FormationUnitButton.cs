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

    [Header("兵士数（外部から設定）")]
    public int soldierCnt = 0;

    private void Start()
    {
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
                }
                else
                {
                    Debug.LogWarning("UnitAttackBace または unitStats が null です");
                }
            });
        }

        SetRedFrameVisible(false);
    }

    public void SetRedFrameVisible(bool visible)
    {
        if (redFrame != null)
            redFrame.SetActive(visible);
    }
}