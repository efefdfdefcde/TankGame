using R3;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Architecture.SaveSistem.Test
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
            VehicleData[] vehicleDatas = Resources.LoadAll<VehicleData>("ScriptableObjects/VehicleDatas");
            foreach(var vehicledata in vehicleDatas)
            {
                var vehicleSave = data.exitParams._datas[vehicledata.name];
                vehicleSave.SetParams(vehicledata);
            }
            string path = Application.persistentDataPath + "/ShopExitParams.dat";
            FileStream stream = new FileStream(path, FileMode.Create);
            _formatter.Serialize(stream, data.exitParams);
            stream.Close();

            GameplaySave(data.gameplayEnter);
        }

        private void GameplaySave(GameplayEnterParams gameplayEnter)
        {
            string path = Application.persistentDataPath + "/GameplayEnterParams.dat";
            FileStream stream = new FileStream(path,FileMode.Create);
            _formatter.Serialize(stream, gameplayEnter);
            stream.Close();
        }

        private void OnDestroy()
        {
            _disposable.Dispose();
        }
    }
}