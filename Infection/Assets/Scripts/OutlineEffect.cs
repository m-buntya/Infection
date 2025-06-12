using UnityEngine;

public class OutlineEffect : MonoBehaviour
{
    public GameObject originalObject;
    public Color outlineColor = Color.black; // �C���X�y�N�^�[�Őݒ�\
    public float outlineThickness = 1.1f; // �C���X�y�N�^�[�Őݒ�\

    private GameObject outlineObject;

    void Start()
    {
        // �I���W�i���I�u�W�F�N�g�𕡐�
        outlineObject = Instantiate(originalObject, originalObject.transform.position, originalObject.transform.rotation);
        outlineObject.transform.parent = originalObject.transform;

        // �A�E�g���C���p�̐ݒ�
        SpriteRenderer outlineRenderer = outlineObject.GetComponent<SpriteRenderer>();
        outlineRenderer.color = outlineColor; // �C���X�y�N�^�[�Őݒ肵���F��K�p
        outlineRenderer.sortingOrder = originalObject.GetComponent<SpriteRenderer>().sortingOrder - 1; // �w�i�ɔz�u

        // �C���X�y�N�^�[�Őݒ肵��������K�p
        outlineObject.transform.localScale *= outlineThickness;
    }
}