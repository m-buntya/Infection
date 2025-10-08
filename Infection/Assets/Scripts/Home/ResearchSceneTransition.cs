using UnityEngine;
using UnityEngine.SceneManagement;

public class ResearchSceneTransition : MonoBehaviour
{
    //研究シーンへ移動
    public void OnClickResearchButton()
    {
        SceneManager.LoadScene("ResearchScene");
    }

    //研究シーンから退出
    public void OnClickHomeButton()
    {
        SceneManager.LoadScene("HomeScene");
    }
    public void OnclickSelectionButton()
    {
        SceneManager.LoadScene("UnitFormation");
    }
}
