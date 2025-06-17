using StatePatteren.State;
using UnityEngine;
using UnityEngine.UIElements.Experimental;
using static UnityEditorInternal.ReorderableList;
using static VirusStats;

public class VirusChoice : MonoBehaviour
{
    [SerializeField] VirusData virusData;
    [SerializeField] VirusUIManager virusUIManager;

    //ウイルスのステータスを管理
    private class VirusManager
    {
        public VirusStats virusStats { get; private set; }

        public void SetVirus(VirusStats selected)
        {
            virusStats = selected;
        }
    }

    private VirusManager virusManager = new VirusManager(); // インスタンス生成

    //ウイルスを外部から取得するのに必要な関数
    public VirusStats GetCurrentVirus()
    {
        return virusManager.virusStats;
        //TODO 外部へ流せるようにする、確認作業をする
        /* [SerializeField] VirusChoice virusChoice;
        VirusStats 〇〇〇;
        〇〇〇 =　virusChoice.GetCurrentVirus();
        Debug.Log("ウイルス名" + 〇〇〇.name)　このようにすると取得できる
        void Startなどタイミングによってはバグって取得できないことがあるかもしれない*/

    }

    void Start()
    {
        //１つ目のウイルスを初期表示
        OnClickLeader(0);
    }

    //ボタンを押すごとにウイルスを変えて値をセットする
    public void OnClickLeader(int index)
    {
        //マスターがなかったときのデバッグ用
        if (virusData == null || virusData.VirusParameter.Count <= index)
        {
            Debug.LogWarning("ウイルスのマスターが不明か、範囲外です");
            return;
        }

        VirusStats selected = Clone(virusData.VirusParameter[index]); // 選択されたウイルスのステータスを複製

        // 管理クラスにセット
        virusManager.SetVirus(selected);

        //UIの更新
        virusUIManager.SetVirusInfo(selected);
    }

    //VirusStatsを複製する関数
    public VirusStats Clone(VirusStats original)
    {
        return new VirusStats
        {
            virusCode = original.virusCode,
            transmission = original.transmission,
            evolutionConditions = original.evolutionConditions,
            name = original.name,
            explanation = original.explanation,
            hp = original.hp,
            atk = original.atk,
            virusPow = original.virusPow,
            atkSpd = original.atkSpd,
            spd = original.spd,
            range = original.range
        };
    }

}
