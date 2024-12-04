using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Assets.Scripts.Architecture.SaveSistem
{
    public class TestGameplaySaver : MonoBehaviour
    {
        public int _money;
        public int _researchPoints;
        public int _gold;

        [ContextMenu("Save")]
        public void Save()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            string path = Application.persistentDataPath + "/ShopEnterParams.dat";
            FileStream stream = new FileStream(path, FileMode.Create);
            ShopEnterParams menuEnterParams = new(_money, _researchPoints,_gold);
            formatter.Serialize(stream, menuEnterParams);
            stream.Close();
        }
    }
}