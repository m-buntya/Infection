using UnityEngine;
using UnityEngine.UI;

public class FormationUnitButton : MonoBehaviour
{
    public UnitData unitData;
    public GameObject redFrame;
    public Button button;
    public FormationPanelManager panelManager;

    private void Start()
    {
        if (button != null)
        {
            button.onClick.AddListener(() =>
            {
                if (panelManager != null)
                    panelManager.HighlightUnit(unitData);
            });
        }

        SetRedFrameVisible(false); // ‰Šúó‘Ô‚ÅÔ˜g”ñ•\¦
    }

    public void SetRedFrameVisible(bool visible)
    {
        if (redFrame != null)
            redFrame.SetActive(visible);
    }
}
