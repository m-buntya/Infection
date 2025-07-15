using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FormationChange : MonoBehaviour
{
    [Header("入れ替え対象のGameObject（同じ親の子）")]
    public Transform obj1;
    public Transform obj2;

    [Header("切り替えボタン")]
    public Button switchButton;

    [Header("アニメーション時間")]
    public float animDuration = 0.5f;

    // CanvasGroup を追加して判定制御する
    private CanvasGroup canvasGroup1;
    private CanvasGroup canvasGroup2;

    private bool isObj1Front = true;
    private bool isAnimating = false;

    private float obj1StartY;
    private float obj2StartY;

    private void Start()
    {
        switchButton.onClick.AddListener(() =>
        {
            if (!isAnimating)
            {
                StartCoroutine(SwapAnimation());
            }
        });

        obj1StartY = obj1.localPosition.y;
        obj2StartY = obj2.localPosition.y;

        obj1.SetAsLastSibling();
        obj2.SetAsFirstSibling();

        // CanvasGroup を取得（なければ追加）
        canvasGroup1 = obj1.GetComponent<CanvasGroup>();
        if (canvasGroup1 == null) { canvasGroup1 = obj1.gameObject.AddComponent<CanvasGroup>(); }

        canvasGroup2 = obj2.GetComponent<CanvasGroup>();
        if (canvasGroup2 == null) { canvasGroup2 = obj2.gameObject.AddComponent<CanvasGroup>(); }

        // 最初に前面にあるものだけクリック有効
        canvasGroup1.blocksRaycasts = true;
        canvasGroup2.blocksRaycasts = false;
    }

    private IEnumerator SwapAnimation()
    {
        isAnimating = true;

        Vector3 obj1Pos = obj1.localPosition;
        Vector3 obj2Pos = obj2.localPosition;

        float obj1TargetY = obj2StartY;
        float obj2TargetY = obj1StartY;

        float elapsed = 0f;

        while (elapsed < animDuration)
        {
            float t = elapsed / animDuration;
            float newY1 = Mathf.Lerp(obj1Pos.y, obj1TargetY, t);
            float newY2 = Mathf.Lerp(obj2Pos.y, obj2TargetY, t);

            obj1.localPosition = new Vector3(obj1Pos.x, newY1, obj1Pos.z);
            obj2.localPosition = new Vector3(obj2Pos.x, newY2, obj2Pos.z);

            elapsed += Time.deltaTime;
            yield return null;
        }

        obj1.localPosition = new Vector3(obj1Pos.x, obj1TargetY, obj1Pos.z);
        obj2.localPosition = new Vector3(obj2Pos.x, obj2TargetY, obj2Pos.z);

        // 並び順切り替え
        if (isObj1Front)
        {
            obj2.SetAsLastSibling();
            obj1.SetAsFirstSibling();

            canvasGroup1.blocksRaycasts = false;
            canvasGroup2.blocksRaycasts = true;
        }
        else
        {
            obj1.SetAsLastSibling();
            obj2.SetAsFirstSibling();

            canvasGroup1.blocksRaycasts = true;
            canvasGroup2.blocksRaycasts = false;
        }

        // Y座標の基準値入れ替え
        float temp = obj1StartY;
        obj1StartY = obj2StartY;
        obj2StartY = temp;

        isObj1Front = !isObj1Front;
        isAnimating = false;
    }
}
