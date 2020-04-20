using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StockStatusViewer : MonoBehaviour
{
    public LineRenderer Line;
    public Text StatusText;
    private void Awake()
    {
        Line = GetComponentInChildren<LineRenderer>();
        StatusText = GetComponentInChildren<Text>();
    }
}
