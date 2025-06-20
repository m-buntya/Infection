using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using static VirusStats;

public class VirusUIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI transmissionText;
    [SerializeField] TextMeshProUGUI evolutionDitionsText;
    [SerializeField] TextMeshProUGUI explanationText;
    [SerializeField] TextMeshProUGUI hpText;
    [SerializeField] TextMeshProUGUI atkText;
    [SerializeField] TextMeshProUGUI virusPowText;
    [SerializeField] TextMeshProUGUI atkSpdText;
    [SerializeField] TextMeshProUGUI spdText;
    [SerializeField] TextMeshProUGUI rangeText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // åƒÇ—èoÇ≥ÇÍÇΩÇ∆Ç´Ç…UIçXêV
    public void SetVirusInfo(VirusStats virus)
    {
        nameText.text = virus.name;
        transmissionText.text = virus.transmission.ToString();
        evolutionDitionsText.text = virus.evolutionConditions.ToString();
        explanationText.text = virus.explanation.ToString();
        hpText.text = virus.hp.ToString("F1");
        atkText.text = virus.atk.ToString("F1");
        virusPowText.text = virus.virusPow.ToString("F1");
        atkSpdText.text = virus.atkSpd.ToString("F1");
        spdText.text = virus.spd.ToString("F1");
        rangeText.text = virus.range.ToString("F1");
    }
}
