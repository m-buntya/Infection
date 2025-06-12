using UnityEngine;

public class OutlineEffect : MonoBehaviour
{
    public GameObject originalObject;
    public Color outlineColor = Color.black; // インスペクターで設定可能
    public float outlineThickness = 1.1f; // インスペクターで設定可能

    private GameObject outlineObject;

    void Start()
    {
        // オリジナルオブジェクトを複製
        outlineObject = Instantiate(originalObject, originalObject.transform.position, originalObject.transform.rotation);
        outlineObject.transform.parent = originalObject.transform;

        // アウトライン用の設定
        SpriteRenderer outlineRenderer = outlineObject.GetComponent<SpriteRenderer>();
        outlineRenderer.color = outlineColor; // インスペクターで設定した色を適用
        outlineRenderer.sortingOrder = originalObject.GetComponent<SpriteRenderer>().sortingOrder - 1; // 背景に配置

        // インスペクターで設定した太さを適用
        outlineObject.transform.localScale *= outlineThickness;
    }
}