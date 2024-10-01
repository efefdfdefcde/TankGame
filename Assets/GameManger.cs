using TMPro;
using UnityEngine;

public class GameManger : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _pointsText;
    [SerializeField] private int _pointsCount;

    private int _points;

    private void Start()
    {
        HealthRoot._kill += PointsUpdate;
        _pointsText.text = _points.ToString();
    }

    private void PointsUpdate()
    {
        _points += _pointsCount;
        _pointsText.text = _points.ToString();
    }

    private void OnDestroy()
    {
        HealthRoot._kill -= PointsUpdate;
    }
}
