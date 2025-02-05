using Assets.Scripts.Architecture.SaveSistem;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Architecture.Installers
{
    public class GameplayParamsInstaller : MonoInstaller
    {
        BinaryFormatter _formatter = new BinaryFormatter();

        public override void InstallBindings()
        {
            GameplayEnterParams _enterParams = DeserializeEnterParams();
            GameplayExitParams _exitParams = DeserializeExitParams();
            if (_enterParams != null)
            {
                Container.Bind<GameplayEnterParams>().FromInstance(_enterParams).AsSingle();
            }
            if (_exitParams != null)
            {
                Container.Bind<GameplayExitParams>().FromInstance(_exitParams).AsSingle();
            }
        }

        private GameplayEnterParams DeserializeEnterParams()
        {
            string path = Application.persistentDataPath + "/GameplayEnterParams.dat";
            if (File.Exists(path))
            {
                FileStream stream = new FileStream(path, FileMode.Open);
                GameplayEnterParams data = _formatter.Deserialize(stream) as GameplayEnterParams;
                stream.Close();
                return data;
            }
            else return null;
        }

        private GameplayExitParams DeserializeExitParams()
        {
            string path = Application.persistentDataPath + "/GameplayExitParams.dat";
            if (File.Exists(path))
            {
                FileStream stream = new FileStream(path, FileMode.Open);
                GameplayExitParams data = _formatter.Deserialize(stream) as GameplayExitParams;
                stream.Close();
                return data;
            }
            else return null;
        }
    }
}