using Assets.Scripts.Architecture;
using Assets.Scripts.New.Shop;
using R3;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.New.Arhitecture.SaveSistem
{
    public class ShopSaver : MonoBehaviour
    {
        [SerializeField] private DataManagerShop _dataManager;

        BinaryFormatter _formatter = new();
        private CompositeDisposable _disposable = new();

        [Inject]
        private void Construct()
        {
            _dataManager._save.Subscribe(data => Save(data)).AddTo(_disposable);
        }

        private void Save((ShopExitParams exitParams, GameplayEnterParams gameplayEnter) data)
        {
            NationStorage[] nationStorages = Resources.LoadAll<NationStorage>("ScriptableObjects/Nations");
            foreach (var nationStorage in nationStorages)
            {
                data.exitParams._nationDictonary[nationStorage._name] = new NationStorageSave(nationStorage);
            }
            string path = Application.persistentDataPath + "/ShopExitParams1.dat";//Override
            FileStream stream = new FileStream(path, FileMode.Create);
            _formatter.Serialize(stream, data.exitParams);
            stream.Close();

            GameplaySave(data.gameplayEnter);
        }

        private void GameplaySave(GameplayEnterParams gameplayEnter)
        {
            string path = Application.persistentDataPath + "/GameplayEnterParams1.dat";//Override
            FileStream stream = new FileStream(path, FileMode.Create);
            _formatter.Serialize(stream, gameplayEnter);
            stream.Close();
        }

        private void OnDestroy()
        {
            _disposable.Dispose();
        }
    }
}