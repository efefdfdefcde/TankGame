using R3;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Architecture.SaveSistem
{
    public class GameplaySaver : MonoBehaviour
    {
        [SerializeField] private DataManagerGameplay _dataManager;

        private CompositeDisposable _disposables = new();

        
        private void Start()
        {
            _dataManager._saveParams.Subscribe(shopParams => Save(shopParams)).AddTo(_disposables);
        }

        public void Save(ShopEnterParams menuEnterParams)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            string path = Application.persistentDataPath + "/ShopEnterParams.dat";
            FileStream stream = new FileStream(path, FileMode.Create);          
            formatter.Serialize(stream, menuEnterParams);
            stream.Close();
        }
    }
}