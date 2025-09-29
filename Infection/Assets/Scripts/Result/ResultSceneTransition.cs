using UnityEngine.SceneManagement;
using UnityEngine;

public class ResultSceneTransition : MonoBehaviour
{
    [SerializeField] CastleManager castleManager;
    [SerializeField] private GameObject playerCastle;
    [SerializeField] private GameObject enemyCastle;

    void Update()
    {
        if (castleManager.GetCastle(playerCastle).IsDestroy())
        {
            SceneManager.LoadScene("ResultScene");
        }
        else if (castleManager.GetCastle(enemyCastle).IsDestroy())
        {
            SceneManager.LoadScene("ResultScene");
        }
    }
}
