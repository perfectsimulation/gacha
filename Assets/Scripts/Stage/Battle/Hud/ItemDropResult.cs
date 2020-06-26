using TMPro;
using UnityEngine;

public class ItemDropResult : MonoBehaviour
{
    public TextMeshProUGUI NameLabel;

    public void SetImageName(string name)
    {
        this.NameLabel.text = name;
    }
}
