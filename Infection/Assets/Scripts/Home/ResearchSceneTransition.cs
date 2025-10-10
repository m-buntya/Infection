using UnityEngine;
using UnityEngine.SceneManagement;

public class ResearchSceneTransition : MonoBehaviour
{
    private SceneTransitionManager transitionManager;

    private void Start()
    {
        transitionManager = FindObjectOfType<SceneTransitionManager>();
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

    public void OnClickSelectionButton()
    {
        if (transitionManager != null)
        {
            transitionManager.RequestSceneChange("Formation");
        }
    }

}
