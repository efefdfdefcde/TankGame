using Assets.Scripts.Shop.ResearchTree;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VenicleContainerView : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _name;
  

    public void Init(VehicleData data)
    {
        _icon.sprite = data._Icon;
        _name.text = data._name;

    }
}
