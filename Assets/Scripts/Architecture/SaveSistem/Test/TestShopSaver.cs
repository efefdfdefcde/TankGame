using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Assets.Scripts.Architecture.SaveSistem.Test
{
    public class TestShopSaver : MonoBehaviour
    {
        public int _money;
        public int _gold;
        public int _researchPoints;

        [ContextMenu("Save")]
        public void Save()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            string path = Application.persistentDataPath + "/ShopExitParams.dat";
            FileStream stream = new FileStream(path, FileMode.Create);
            ShopExitParams menuExitParams = new(_money, _gold,_researchPoints);
            formatter.Serialize(stream, menuExitParams);
            stream.Close();
        }
    }
}