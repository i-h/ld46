using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityHealth : MonoBehaviour
{
    public static CityHealth Instance;
    public float UpdateInterval = 2.0f;
    public int LastValuesCount = 60;
    public float TimeLimit = 1; //Minutes

    float _lastUpdate;
    float t = 0;
        
    float _cityHealth;

    List<float> _healthValues;

   
    StockStatusViewer _statusView;

    bool _initialized = false;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }

    float RandomVal()
    {
        return (Random.value * 2) - 1;
    }

    void Start()
    {
        if (!_initialized)
        {
            Debug.Log("Initializing City Health");
            _healthValues = new List<float>(LastValuesCount);
            float trueValue = 0;
            for (int i = 0; i < LastValuesCount; i++)
            {
                trueValue = 0.5f + (float)i / LastValuesCount * 0.5f;
                t = (float)i / LastValuesCount * Mathf.PI * 4;

                _healthValues.Add(i == LastValuesCount - 1 ? 1 : GetNewValue(trueValue));
            }
            _cityHealth = 1.0f;
            _initialized = true;
        }

        DisplayValues();

    }
    float GetNewValue(float trueValue)
    {
        if (trueValue <= 0) return 0;
        float val = trueValue + (RandomVal() * RandomVal() * RandomVal() * 0.5f + Mathf.Sin(t) * -0.3f) * trueValue;
        return val;
    }

    public void Boost(ResourceType type, int amount)
    {
        Debug.Log("City health before: " + _cityHealth);
        for(int i = 0; i < amount; i++)
        {
            float recovery = (1.0f - _cityHealth) * (1.0f / (5.0f + i * 2.0f));
            Debug.Log("Recovery: " + recovery);
            _cityHealth += recovery;
        }
        Debug.Log("City health after: " + _cityHealth);
    }

    void DisplayValues()
    {
        if (_statusView == null) _statusView = FindObjectOfType<StockStatusViewer>();        
        if (_statusView != null)
        {
            Vector3 posVector = new Vector3();
            _statusView.Line.positionCount = LastValuesCount;
            for (int i = 0; i < _healthValues.Count; i++)
            {
                posVector.x = (float)i / _healthValues.Count * 10;
                posVector.z = _healthValues[i] * 6;
                _statusView.Line.SetPosition(i, posVector);

                if(i == _healthValues.Count - 1)
                {
                    Vector3 statusPos = _statusView.StatusText.transform.position;

                    statusPos.y = _healthValues[i]*3+1;

                    _statusView.StatusText.transform.position = statusPos;
                    _statusView.StatusText.text = "City health: " + (int)(_healthValues[i] * 1000);
                }
            }


        }
    }

    private void FixedUpdate()
    {
        t += Time.fixedDeltaTime * (0.5f+Random.value*0.5f) * Mathf.PI / 15.0f;
        if (_lastUpdate + UpdateInterval < Time.time)
        {
            UpdateValue();
            _lastUpdate = Time.time;
        }
    }
    void UpdateValue()
    {
        float graphValue = 0;
        if (_cityHealth <= 0)
        {
            _cityHealth = 0;
        }
        else
        {
            float healthReduction = UpdateInterval / (TimeLimit * 60);
            _cityHealth -= healthReduction;
            graphValue = GetNewValue(_cityHealth);
            if (graphValue <= 0)
            {
                graphValue *= -1;
                graphValue += 0.01f;
            }
        }

        Debug.Log("City health: " + _cityHealth);
        _healthValues.RemoveAt(0);
        _healthValues.Add(graphValue);

        DisplayValues();
    }
}
