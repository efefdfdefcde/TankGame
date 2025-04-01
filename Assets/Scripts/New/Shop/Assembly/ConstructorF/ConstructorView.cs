using Assets.Scripts.New.Shop.UI.Menu;
using R3;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Scripts.New.Shop.Assembly
{
    public class ConstructorView : MonoBehaviour
    {
        public Subject<Unit> _build = new();
        public Subject<Unit> _delete = new();
        public Subject<(string,Sprite)> _save = new();

        [SerializeField] private Button _buildButton;
        [SerializeField] private Button _deleteButton;
        [SerializeField] private Button _saveButton;

        [SerializeField] private Image _buildButtonImage;
        [SerializeField] private Image _deleteButtonImage;
        [SerializeField] private Image _saveButtonImage;

        [SerializeField] private TMP_InputField _name;

        [SerializeField] private Color _activeColor;

        [SerializeField] private RenderTexture _renderTexture;

        [SerializeField] private TextMeshProUGUI _overweightWarning;
        [SerializeField] private TextMeshProUGUI _shellsWarning;

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

        [SerializeField] private MenuBuildView _buildView;

        private Color _notActiveColor = Color.white;

        [Inject]
        private void Construct()
        {
            _buildButton.onClick.AddListener(Build);
            _saveButton.onClick.AddListener(Save);
            _deleteButton.onClick.AddListener(Delete);
        }

        public void ActivateBuildButton()
        {
            _buildButtonImage.color = _activeColor;
            _buildButton.gameObject.SetActive(true);
        }

        public void DeactivateBuildButton()
        {
            _buildButtonImage.color = _notActiveColor;
            _buildButton.gameObject.SetActive(false);
        }

        public void ActivateSaveButton()
        {
            _saveButtonImage.color = _activeColor;
            _saveButton.gameObject.SetActive(true);
        }

        public void DeactivateSaveButton()
        {
            _saveButtonImage.color = _notActiveColor;
            _saveButton.gameObject.SetActive(false);
        }

        public void ActivateDeleteButton()
        {
            _deleteButton.gameObject.SetActive(true);
            _deleteButtonImage.color = _activeColor;
        }

        public void DeactivateDeleteButton()
        {
            _deleteButton.gameObject.SetActive(false);
            _deleteButtonImage.color = _notActiveColor;
        }

        private void Build()
        {
            _build.OnNext(Unit.Default);
            DeactivateBuildButton();
        }

        private void Delete()
        {
            _delete.OnNext(Unit.Default);
            DeactivateDeleteButton();
        }

        private void Save()
        {
            _save.OnNext((_name.text,GenerateIcon()));
            DeactivateSaveButton();
        }

        private Sprite GenerateIcon()
        {
            Texture2D texture = new Texture2D(_renderTexture.width, _renderTexture.height, TextureFormat.ARGB32, false);

            RenderTexture.active = _renderTexture; 
            texture.ReadPixels(new Rect(0, 0, _renderTexture.width, _renderTexture.height), 0, 0);
            Color[] pixels = texture.GetPixels();
            for (int i = 0; i < pixels.Length; i++)
            {
                pixels[i] = pixels[i] * 2.5f;
            }
            texture.SetPixels(pixels);
            texture.Apply();
            RenderTexture.active = null; 

            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            return sprite;
        }

        public void OverweightWarning(bool status)
        {
            if (status) _overweightWarning.text = "Owerweight";
            else _overweightWarning.text = null;
        }

        public void ShellWarning(bool status, int shellsCount)
        {
            if (status) _shellsWarning.text = $"Minimal shells count {shellsCount}";
            else _shellsWarning.text = null;
        }

        public void ShowTrack(Tracks track)
        {
            _maxSpeed.text = track._maxSpeed.ToString();
            _enginePower.text = track._enginePower.ToString();
            _maxWeight.text = track._maxWeight.ToString();
            _turningSpeed.text = track._turningSpeed.ToString();
            _buildView.ShowTrack(track);
        }
        public void ShowWeight(int weight)
        {
            _weight.text = weight.ToString();
            _buildView.ShowWeight(weight);
        }

        public void ShowShellsCount(int count)
        {
            _shellCount.text = count.ToString();
            _buildView.ShowShellsCount(count);
        }

        public void ShowArmorAndHealth(int armor, int healt)
        {
            _armor.text = armor.ToString();
            _health.text = healt.ToString();
            _buildView.ShowArmorAndHealth(armor, healt);    
        }

        public void ShowCorpus(Corpus corpus)
        {
            _corpusSize.text = corpus._shellCapacity.ToString();
            _buildView.ShowCorpus(corpus);
        }

        public void ShowTurret(Turret turret)
        {
            _turretSpeed.text = turret._turningSpeed.ToString();
            _buildView.ShowTurret(turret);
        }

        public void ShowCannon(Cannon cannon)
        {
            _reload.text = cannon._reload.ToString();
            _shelSize.text = cannon._shellSize.ToString();
            _buildView.ShowCannon(cannon);
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
            _buildView.ClearString();
        }
        
        public void ShowName(string name)
        {
            _name.text = name;
            _buildView.ShowName(name);
        }

        private void OnDestroy()
        {
            _buildButton.onClick.RemoveAllListeners();
        }
    }
}