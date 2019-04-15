using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Limit : MonoBehaviour
{
    private int _number;
    [SerializeField] private int _maxLimit = 6;
    [SerializeField] private float _distance = 0.1f;

    private void Start()
    {
        _number = 0;
    }

    public void CheckAndAct()
    {
        _number += 1;
        if (_number>=_maxLimit)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - _distance, transform.position.z);
        }
    }

    public void EndGame()
    {
        transform.position = Vector3.zero;
    }
}
