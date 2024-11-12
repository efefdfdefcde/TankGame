using Assets.Scripts.TankParts.Player;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Architecture.Installers
{
    public class PersuitManagerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<PersuitManager>().AsSingle();
        }
    }
}