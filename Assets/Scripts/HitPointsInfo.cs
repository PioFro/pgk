using ScriptableObjects;
using TMPro;
using UnityEngine;

public class HitPointsInfo : MonoBehaviour
{
    public Character Character { get; set; }

    public TextMeshPro Text;

    public void ChangeHitPointText(int hitPointsChange = 0)
    {
        SetText($"HP: {Character.Stats.CurrentHitPoints}/{Character.Stats.HitPoints}");
    }

    private void SetText(string text)
    {
        this.Text.text = text;
    }
}
