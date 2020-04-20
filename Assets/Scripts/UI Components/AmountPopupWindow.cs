using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmountPopupWindow : MonoBehaviour
{
    public static AmountPopupWindow CurrentWindow;
    public delegate void PopupCallback(int amount);

    public Text Header;
    public Text DescriptionText;
    public Slider Slider;
    public InputField Input;
    public Button Accept;
    public Button Cancel;

    PopupCallback acceptCallback;

    public static AmountPopupWindow Create()
    {
        if (CurrentWindow != null) return CurrentWindow;

        AmountPopupWindow prefab = Resources.Load<AmountPopupWindow>("UIPrefabs/AmountConfirmPopup");
        GameObject mainCanvas = GameObject.FindWithTag("MainCanvas");
        AmountPopupWindow window = Instantiate(prefab);
        window.transform.SetParent(mainCanvas.transform);
        window.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
        CurrentWindow = window;
        return window;
    }

    private void Start()
    {
        AddListeners();
    }
    void AddListeners()
    {
        Accept.onClick.AddListener(AcceptPressed);
        Cancel.onClick.AddListener(CancelPressed);
        Slider.onValueChanged.AddListener(SliderChanged);
        Input.onValueChanged.AddListener(InputChanged);
    }
    public void SetUpResourcePopup(string title, string description, ResourceType type, ResourceStorage storage, PopupCallback callback)
    {
        SetUpGenericPopup(title, description, 0, storage.CheckResource(type), callback);
    }
    public void SetUpGenericPopup(string title, string description, int minVal, int maxVal, PopupCallback callback)
    {
        Header.text = title;
        DescriptionText.text = description;
        Slider.minValue = minVal;
        Slider.maxValue = maxVal;
        Slider.value = maxVal / 2;
        Input.text = Mathf.RoundToInt(Slider.value).ToString();
        acceptCallback = callback;
    }

    void InputChanged(string val)
    {
        if(int.TryParse(val, out int intVal))
        {
            intVal = Mathf.Clamp(intVal, 0, (int)Slider.maxValue);
            Slider.value = intVal;
            Input.text = intVal.ToString();
        } else
        {
            Input.text = Mathf.RoundToInt(Slider.value).ToString();
        }
    }
    void SliderChanged(float val)
    {
        Input.text = Mathf.RoundToInt(Slider.value).ToString();
    }

    void AcceptPressed()
    {
        acceptCallback(Mathf.RoundToInt(Slider.value));
        Dispose();
    }
    void CancelPressed()
    {
        Dispose();
    }
    void Dispose()
    {
        Destroy(gameObject);
    }
}
