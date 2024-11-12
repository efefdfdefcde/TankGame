using System.Collections;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Architecture
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private PlayerHealthRoot _playerHealth;

        public override void InstallBindings()
        {
            Container.Bind<PlayerHealthRoot>().FromInstance(_playerHealth).AsSingle();
        }
    }
}