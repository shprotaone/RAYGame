using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class PointViewer : MonoBehaviour
{
    [SerializeField] private float _punchMultiply;
    [SerializeField] private TMP_Text _levelText;
    [SerializeField] private TMP_Text _pointText;
    
    public void ChangeText(int value)
    {      
        _pointText.text = value.ToString();
        _pointText.transform.DOPunchPosition(Vector3.up * _punchMultiply, 0.2f);
    }

    public void SetLevelText(int value)
    {
        _levelText.text = value.ToString();
    }

}
