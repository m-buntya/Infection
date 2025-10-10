using UnityEngine;
using UnityEngine.SceneManagement;

public class ListSceneTransition : MonoBehaviour
{
    private SceneTransitionManager transitionManager;
    private void Start()
    {
        transitionManager = FindObjectOfType<SceneTransitionManager>();
    }
    //Ú×ƒV[ƒ“‚ÖˆÚ“®
    public void OnClickListButton()
    {
        transitionManager.RequestSceneChange("ListScene");
    }

}
