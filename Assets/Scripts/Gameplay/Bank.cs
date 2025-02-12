using Assets.Scripts.Gameplay.UI;
using R3;
using System.Collections;
using TMPro;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Gameplay
{
    public class Bank : MonoBehaviour
    {
        public Subject<(int, int)> _pointsUpdate = new();

        [SerializeField] private BankUI _view;
        [SerializeField] private Base _base;
        [SerializeField] private EnemyRewardData[] enemyDatas;

        private int _moneyOnPlayerCount;
        private int _researchPOnPlayerCount;
        private int _moneyCount;
        private int _reserchPointsCount;

        private CompositeDisposable _disposables = new();

        [Inject]
        private void Costruct()
        {
            EventBus.Instance._enemyDeathAction += PointsUpdate;
            _base._playerOnBase.Subscribe(_  => CollectPoints()).AddTo(_disposables);
        }

        private void Start()
        {          
            _view.PointsUpdate(_moneyOnPlayerCount,_researchPOnPlayerCount);
        }

        private void PointsUpdate(EnemiesList _enemyType)
        {
            foreach (var enemyData in enemyDatas)
            {
                if (enemyData._enemyName == _enemyType)
                {
                    _moneyOnPlayerCount += enemyData._money;
                    _researchPOnPlayerCount += enemyData._reserchPoints;
                    break;
                }
            }
            _view.PointsUpdate(_moneyOnPlayerCount, _researchPOnPlayerCount);
        }

        private void CollectPoints()
        {
            _moneyCount += _moneyOnPlayerCount;
            _moneyOnPlayerCount = 0;
            _reserchPointsCount += _researchPOnPlayerCount;
            _researchPOnPlayerCount = 0;
            _pointsUpdate.OnNext((_moneyCount, _reserchPointsCount));
            _view.PointsUpdate(_moneyOnPlayerCount, _researchPOnPlayerCount);
        }

        private void OnDestroy()
        {
            EventBus.Instance._enemyDeathAction -= PointsUpdate;
        }
    }
}