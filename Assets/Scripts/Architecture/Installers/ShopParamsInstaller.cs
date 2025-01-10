using Assets.Scripts.Architecture.SaveSistem;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Architecture.Installers
{
    public class ShopParamsInstaller : MonoInstaller
    {
        BinaryFormatter _formatter = new BinaryFormatter();

        public override void InstallBindings()
        {
            ShopExitParams exitParams = DeserializeExitParams();
            ShopEnterParams enterParams = DeserialazeEnterParams();
            Container.Bind<ShopExitParams>().FromInstance(exitParams).AsSingle();
            Container.Bind<ShopEnterParams>().FromInstance(enterParams).AsSingle();
        }

        private ShopExitParams DeserializeExitParams()
        {
            string path = Application.persistentDataPath + "/ShopExitParams.dat";
            if (File.Exists(path))
            {
                FileStream stream = new FileStream(path, FileMode.Open);
                ShopExitParams data = _formatter.Deserialize(stream) as ShopExitParams;
                stream.Close();
                return data;
            }
            else return null;
        }

        private ShopEnterParams DeserialazeEnterParams()
        {
            string path = Application.persistentDataPath + "/ShopEnterParams.dat";
            if (File.Exists(path))
            {
                FileStream stream = new FileStream(path, FileMode.Open);
                ShopEnterParams data = _formatter.Deserialize(stream) as ShopEnterParams;
                stream.Close();
                return data;
            }
            else return null;
        }

    }
}