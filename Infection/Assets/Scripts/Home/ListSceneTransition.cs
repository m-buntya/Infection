using UnityEngine;
using UnityEngine.SceneManagement;

public class ListSceneTransition : MonoBehaviour
{
    //Ú×ƒV[ƒ“‚ÖˆÚ“®
    public void OnClickListButton()
    {
        SceneManager.LoadScene("ListScene");
    }

}
