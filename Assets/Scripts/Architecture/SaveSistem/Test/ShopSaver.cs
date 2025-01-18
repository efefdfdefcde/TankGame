using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Assets.Scripts.Architecture.SaveSistem.Test
{
    public class ShopSaver : MonoBehaviour
    {
        public int _money;
        public int _gold;



        private void Save()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            string path = Application.persistentDataPath + "/ShopExitParams.dat";
            FileStream stream = new FileStream(path, FileMode.Create);
            ShopExitParams menuExitParams = new(_money, _gold);
            formatter.Serialize(stream, menuExitParams);
            stream.Close();
        }
    }
}