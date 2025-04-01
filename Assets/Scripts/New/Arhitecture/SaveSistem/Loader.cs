using Assets.Scripts.Architecture.SaveSistem;
using Assets.Scripts.Architecture;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.New.Shop;
using Assets.Scripts.New.Shop.UI.NationSelect;

namespace Assets.Scripts.New.Arhitecture.SaveSistem
{
    public class Loader 
    {

        public void Load(ref ShopExitParams exitParams)
        {
            NationStorage[] nationStorages = Resources.LoadAll<NationStorage>("ScriptableObjects/Nations");
            TankPartSO[] parts = Resources.LoadAll<TankPartSO>("ScriptableObjects/Parts");
            if (exitParams == null)
            {
                exitParams = new ShopExitParams();
                Dictionary<NationName, NationStorageSave> datas = new();
                foreach (NationStorage storage in nationStorages)
                {
                    datas.Add(storage._name, new NationStorageSave(storage));
                }
                exitParams._nationDictonary = datas;
                foreach (TankPartSO part in parts)
                {
                    exitParams._partsStatus.Add(part._name, part._isAwailable);
                }
                exitParams._selectedCells = new()
                {
                    {NationName.USSR,1 },
                    {NationName.Germany,1},
                    {NationName.USA,1},
                };
            }
            else
            {
                foreach (var storage in nationStorages)
                {
                    var dat = exitParams._nationDictonary[storage._name];
                    storage._researchPoints = dat._researchPoints;
                } 
                foreach(var part in parts)
                {
                    var partStatus = exitParams._partsStatus[part._name];
                    part._isAwailable = partStatus;
                }
            }
        }
    }
}