using UnityEngine;

public class PrefubLoadTester:MonoBehaviour
{
    private void Start()
    {
        string testCode = "Unit";
        var prefub=Resources.Load<GameObject>($"Units/{testCode}");
        if (prefub == null)
        {
            Debug.LogWarning($"[テスト]プレファブが見つかりません：Units/{testCode}");
        }
        else
        {
            Debug.Log($"[テスト]プレファブ読み込み成功:{prefub.name}");
        }
    }
}
