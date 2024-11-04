using Assets;
using TMPro;
using UnityEngine;

public class GameManger : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _money;
    [SerializeField] private TextMeshProUGUI _reserchPoints;
    [SerializeField] private EnemyRewardData[] enemyDatas;

     private int _moneyCount;
     private int _reserchPointsCount;

    private void Start()
    {
        EventBus.Instance._enemyDeathAction += PointsUpdate;
        _money.text = _moneyCount.ToString();
        _reserchPoints.text = _reserchPointsCount.ToString();
    }

    private void PointsUpdate(EnemiesList _enemyType)
    {
        foreach (var enemyData in enemyDatas)
        {
            if(enemyData._enemyName == _enemyType)
            {
                _moneyCount += enemyData._money;
                _reserchPointsCount += enemyData._reserchPoints;
                break;
            }
        }
        _money.text = _moneyCount.ToString();
        _reserchPoints.text = _reserchPointsCount.ToString();
    }

    private void OnDestroy()
    {
        EventBus.Instance._enemyDeathAction -= PointsUpdate;
    }
}
