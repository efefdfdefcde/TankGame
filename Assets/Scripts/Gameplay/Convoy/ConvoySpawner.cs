using System.Collections.Generic;
using UnityEngine;
using Assets;
using Assets.Scripts.Convoy;
using System.Collections;


public class ConvoySpawner : MonoBehaviour
{
    [SerializeField] private List<Convoy> _convoyList ;
    [SerializeField] private float _distance;
    [SerializeField] private EnemyPool _enemyPool;

    private int _currentConvoyIndex;


    private void Start()
    {
        ConvoyListCheck();
        foreach(Convoy convoy in _convoyList)
        {
            convoy._convoyDestroyAction += ConvoyListCheck;//UnsubWrite
        }
    }

    [ContextMenu("Add Convoy Part")]
    private void AddConvoyPart()
    {
        if(_convoyList.Count > 0)
        {
            foreach(Convoy Convoy in _convoyList)
            {
                if (Convoy._isSelected)
                {
                    if(Convoy._convoyData.Count > 0)
                    {
                        int Count = Convoy._convoyData.Count;
                        Vector3 _previousPosition = Convoy._convoyData[Count - 1]._position;
                        ConvoyPartData previousPart = (ConvoyPartData)Convoy._convoyData[Count - 1].Clone();
                        previousPart._position = new Vector3(_previousPosition.x, _previousPosition.y, _previousPosition.z - _distance);
                        Convoy._convoyData.Add(previousPart);
                    }
                    else
                    {
                        Convoy._convoyData.Add(new ConvoyPartData(Convoy._spawnPoint.position));
                    }
                }
            }
        }
    }//Edit

    [ContextMenu("Create Convoy")]
    private void ConvoyListCheck()
    {
        StartCoroutine(SpawnWait());
        
    }

    private IEnumerator SpawnWait()
    {
        yield return new WaitForEndOfFrame();
        CreateConvoy(_currentConvoyIndex);
        _currentConvoyIndex++;
        if (_currentConvoyIndex == _convoyList.Count)
        {
            _currentConvoyIndex = 0;
        }
    }


    private void CreateConvoy(int convoiNumber)
    {
        var convoy = _convoyList[convoiNumber];
        foreach (var ConvoyPart in convoy._convoyData)
        {
            var ConstructPart = _enemyPool.GetFreeEnemy(ConvoyPart._typeOfEnemy);
            ConstructPart.Construct(ConvoyPart,convoy);
            ConstructPart.transform.position = ConvoyPart._position;
            ConstructPart.transform.rotation = Quaternion.Euler(0,0,0);
            ConstructPart.transform.RotateAround(convoy._spawnPoint.position, Vector3.up, convoy._spawnPoint.transform.rotation.eulerAngles.y);
            ConstructPart.gameObject.SetActive(true);
        }
    }

    private void OnDestroy()
    {
        foreach (Convoy convoy in _convoyList)
        {
            convoy._convoyDestroyAction -= ConvoyListCheck;//UnsubWrite
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        foreach (Convoy convoy in _convoyList)
        {
            if (convoy._isSelected)
            {
                Vector3 previousPosition = Vector3.zero;
                for (int i = 0; i < convoy._convoyData.Count; i++)
                {

                    Vector3 pointToRotate = convoy._convoyData[i]._position;
                    Vector3 pivotPoint = convoy._spawnPoint.position;
                    float angle = convoy._spawnPoint.transform.rotation.eulerAngles.y;
                    Vector3 direction = pointToRotate - pivotPoint;
                    Quaternion rotation = Quaternion.Euler(0, angle, 0);
                    Vector3 rotatedDirection = rotation * direction;
                    Vector3 newPosition = pivotPoint + rotatedDirection;
                    Gizmos.DrawSphere(newPosition, 0.5f);
                    if (i > 0)
                    {
                        Gizmos.DrawLine(previousPosition, newPosition);
                    }

                    previousPosition = newPosition;
                }
            }
        }
    }


}
