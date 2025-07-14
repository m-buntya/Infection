using UnityEngine;
using UnityEngine.UI;
using StatePatteren.State;
public class FormationUnitButton : MonoBehaviour
{
    public UnitController unitcontroller;
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
                    panelManager.HighlightUnit(unitcontroller);
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
