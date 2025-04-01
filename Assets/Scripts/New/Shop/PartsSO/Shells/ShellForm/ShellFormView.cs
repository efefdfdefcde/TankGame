using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.New.Shop.PartsSO.Shells.ShellForm
{
    public class ShellFormView : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private TextMeshProUGUI _count;
        [SerializeField] private TextMeshProUGUI _damage;
        [SerializeField] private TextMeshProUGUI _piercing;
        [SerializeField] private TextMeshProUGUI _fuseSensivity;

        public void Init(Shell shell,CannonShell cannon,int count)
        {
            _icon.sprite = shell._icon;
            _name.text = shell._name;
            _count.text = count.ToString();
            _damage.text = cannon._damage.ToString();
            _piercing.text = cannon._piersing.ToString();
            _fuseSensivity.text = cannon._fuseSensivity.ToString();
        }

        public void CountUpdate(int count)
        {
            _count.text = count.ToString();
        }
    }
}