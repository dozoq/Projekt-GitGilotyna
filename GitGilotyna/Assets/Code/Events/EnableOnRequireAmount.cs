using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableOnRequireAmount : MonoBehaviour
{
    [SerializeField] private GameObject _interactive;

    [SerializeField] private int count;
    private int _actualCount = 0;
    
    private int ActualCount
    {
        get { return _actualCount;}
        set { _actualCount+=value; CheckCondition();}
    }

    public void IncreaseCountByOne() => ActualCount++;

    private void CheckCondition()
    {
        if (_actualCount >= count)
        {
            _interactive.SetActive(true);
        }
    }
    
}
