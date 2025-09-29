using UnityEngine;
using UnityEngine.UI;
using StatePatteren.State;
public class FormationUnitButton : MonoBehaviour
{
    public UnitController unitcontroller;
    public GameObject redFrame;
    public Button button;
    public FormationPanelManager panelManager;
    public GameObject unitPrefub;
    private UnitStats unitStats;
    private void Start()
    {
        if (unitPrefub != null)
        {

            unitcontroller = unitPrefub.GetComponent<UnitController>(); 
                }
        if (button != null)
        {
            button.onClick.AddListener(() =>
            {
                if (panelManager != null)
                    panelManager.HighlightUnit(unitcontroller);


                if (unitcontroller != null && unitcontroller.unitStats != null)
                {
                    var stats = unitcontroller.unitStats;
                    //Debug.Log($"選択されたユニット: {stats.unitName}, ロール: {stats.role}, レベル: {stats.lv}");
                }
                else
                {
                    Debug.LogWarning("unitcontroller または unitStats が null です");
                }
            });
        }

        SetRedFrameVisible(false); // 初期状態で赤枠非表示
    }

    public void SetRedFrameVisible(bool visible)
    {
        if (redFrame != null)
            redFrame.SetActive(visible);
    }
}