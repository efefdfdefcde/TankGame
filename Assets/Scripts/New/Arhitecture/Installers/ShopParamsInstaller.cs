using Assets.Scripts.New.Arhitecture.SaveSistem;
using Assets.Scripts.New.Shop.Upgrades;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.New.Arhitecture.Installers
{
    public class ShopParamsInstaller : MonoInstaller
    {
        BinaryFormatter _formatter = new BinaryFormatter();

        public override void InstallBindings()
        {
            ShopExitParams exitParams = DeserializeExitParams();
            ShopEnterParams enterParams = DeserialazeEnterParams();
            if (exitParams != null)
            {
                Container.Bind<ShopExitParams>().FromInstance(exitParams).AsSingle();
            }
            if (enterParams != null)
            {
                Container.Bind<ShopEnterParams>().FromInstance(enterParams).AsSingle();
            }
        }

        private ShopExitParams DeserializeExitParams()
        {
            string path = Application.persistentDataPath + "/ShopExitParams1.dat";//Override
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
            string path = Application.persistentDataPath + "/ShopEnterParams1.dat";//Override
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