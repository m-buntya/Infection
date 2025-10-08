using UnityEngine;
using UnityEngine.SceneManagement;

public class ResearchSceneTransition : MonoBehaviour
{
    private SceneTransitionManager transitionManager;

    private void Start()
    {
        transitionManager = FindObjectOfType<SceneTransitionManager>();
        if (transitionManager == null)
        {
            Debug.LogError("[SceneTransition] SceneTransitionManager が見つかりません");
        }

    }

    //研究シーンへ移動
    public void OnClickResearchButton()
    {
        if (transitionManager != null)
        {
            transitionManager.RequestSceneChange("ResearchScene");
        }

    }

    //研究シーンから退出
    public void OnClickHomeButton()
    {
        if (transitionManager != null)
        {
            transitionManager.RequestSceneChange("HomeScene");
        }

    }
    public void OnclickSelectionButton()
    {
        if (transitionManager != null)
        {
            transitionManager.RequestSceneChange("UnitFormation");
        }

    }
}
