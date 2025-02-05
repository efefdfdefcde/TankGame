using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Architecture.SaveSistem
{
    public class VehicleLoader 
    {

        public void Load(ref ShopExitParams exitParams)
        {
            VehicleData[] vehicleDatas = Resources.LoadAll<VehicleData>("ScriptableObjects/VehicleDatas");
            if (exitParams == null)
            {
                exitParams = new ShopExitParams();
                Dictionary<string, VehicleSave> datas = new();
                foreach (VehicleData vehicleData in vehicleDatas)
                {
                    datas.Add(vehicleData.name, new VehicleSave(vehicleData));
                }
                exitParams._datas = datas;
            }
            else
            {
                foreach (VehicleData vehicleData in vehicleDatas)
                {
                    var dat = exitParams._datas[vehicleData.name];
                    vehicleData._isAwailable = dat._isAwailable;
                    vehicleData._name = dat._name;
                    vehicleData._health = dat._health;
                    vehicleData._armor = dat._armor;
                    vehicleData._speed = dat._speed;
                    vehicleData._enginePower = dat._enginePower;
                    vehicleData._turretRotationSpeed = dat._turretRotationSpeed;
                    vehicleData._reloadSpeed = dat._reloadSpeed;
                    vehicleData._researchPoints = dat._researchPoints;
                    vehicleData._shellStorageCapasity = dat._shellStorageCapasity;
                    foreach(var shellData in vehicleData._structs)
                    {
                        var shellSave = dat._shellInfo[shellData._shellType];
                        shellData._data._isAllowed = shellSave._isAllowed;
                        shellData._data._name = shellSave._name;
                        shellData._data._damage = shellSave._damage;
                        shellData._data._shellPenetration = shellSave._shellPenetration;
                        shellData._data._fuseSensivity = shellSave._fuseSensivity;
                        shellData._data._count = shellSave._count;
                    }
                }
            }
        }
    }
}