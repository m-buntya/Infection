using UnityEngine;
using TMPro;
    public class ResultSceneController:MonoBehaviour
{
    public TMP_Text resultText;

    private void Start()
    {
        if (GameResultManager.result_Type == GameResultManager.RESULT_TYPE.GameClear)
        {
            resultText.text = "GAME CLEAR";
        }
        else if(GameResultManager.result_Type==GameResultManager.RESULT_TYPE.GameOver)
        {
            resultText.text = "GAME OVER";
        }
    }
}
