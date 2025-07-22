using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
public class RandomSpriteLineUI : MonoBehaviour
{
    [SerializeField] private List<Sprite> spriteOptions;
    [SerializeField] private GameObject imagePrefab;
    [SerializeField] private Transform parentTransform;

    public void StartSequentialDisplay(float delay = 0.3f)
    {
        StartCoroutine(SpawnSpritesSequentially(delay));
    }

    public IEnumerator SpawnSpritesSequentially(float delay)
    {
        int count = Random.Range(3, 6);

        for (int i = 0; i < count; i++)
        {
            Sprite s = spriteOptions[Random.Range(0, spriteOptions.Count)];
            GameObject go = Instantiate(imagePrefab, parentTransform);
            go.GetComponent<Image>().sprite = s;
            yield return new WaitForSeconds(delay);
        }
    }

}
