using Assets.Scripts.New.Shop.UI.BuildPopup;
using R3;
using System.Collections;
using TMPro;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.New.Shop.UI.Menu
{
    public class MenuBuildView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private TextMeshProUGUI _battleRating;
        [SerializeField] private TextMeshProUGUI _health;
        [SerializeField] private TextMeshProUGUI _armor;
        [SerializeField] private TextMeshProUGUI _weight;
        [SerializeField] private TextMeshProUGUI _maxWeight;
        [SerializeField] private TextMeshProUGUI _turretSpeed;
        [SerializeField] private TextMeshProUGUI _reload;
        [SerializeField] private TextMeshProUGUI _shelSize;
        [SerializeField] private TextMeshProUGUI _corpusSize;
        [SerializeField] private TextMeshProUGUI _shellCount;
        [SerializeField] private TextMeshProUGUI _turningSpeed;
        [SerializeField] private TextMeshProUGUI _enginePower;
        [SerializeField] private TextMeshProUGUI _maxSpeed;

        private CompositeDisposable _disposables = new();

        [Inject]
        private void Construct()
        {
            New.Arhitecture.EventBus.Instance._battleRatingUpdate.Subscribe(br => ShowBr(br)).AddTo(_disposables);
        }

        public void ShowTrack(Tracks track)
        {
            _maxSpeed.text = track._maxSpeed.ToString();
            _enginePower.text = track._enginePower.ToString();
            _maxWeight.text = track._maxWeight.ToString();
            _turningSpeed.text = track._turningSpeed.ToString();
        }
        public void ShowWeight(int weight)
        {
            _weight.text = weight.ToString();
        }

        public void ShowShellsCount(int count)
        {
            _shellCount.text = count.ToString();
        }

        public void ShowArmorAndHealth(int armor, int healt)
        {
            _armor.text = armor.ToString();
            _health.text = healt.ToString();
        }

        public void ShowCorpus(Corpus corpus)
        {
            _corpusSize.text = corpus._shellCapacity.ToString();
        }

        public void ShowTurret(Turret turret)
        {
            _turretSpeed.text = turret._turningSpeed.ToString();
        }

        public void ShowCannon(Cannon cannon)
        {
            _reload.text = cannon._reload.ToString();
            _shelSize.text = cannon._shellSize.ToString();
        }

        public void ClearString()
        {
            _health.text = "0";
            _armor.text = "0";
            _weight.text = "0";
            _maxWeight.text = "0";
            _turretSpeed.text = "0";
            _reload.text = "0";
            _shelSize.text = "0";
            _corpusSize.text = "0";
            _shellCount.text = "0";
            _turningSpeed.text = "0";
            _enginePower.text = "0";
            _maxSpeed.text = "0";
            _name.text = null;
        }

        private void ShowBr(float br)
        {
            _battleRating.text = br.ToString();
        }

        public void ShowName(string name)
        {
            _name.text = name;
        }

        private void OnDestroy()
        {
            _disposables.Dispose(); 
        }
    }
}