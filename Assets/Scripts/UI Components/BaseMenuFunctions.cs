using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseMenuFunctions : MonoBehaviour
{
    public Text CrystalText;
    public Text FuelText;

    public Transform StockView;
    public Transform GarageView;

    public float ViewSwitchSpeed = 1.0f;

    bool _stockView = false;
    Transform _viewTarget;
    float _viewSwitchProgress;

    private void Start()
    {
        UpdateValues();
    }

    void UpdateValues()
    {
        CrystalText.text = PlayerData.Instance.GetInventory().CheckResource(ResourceType.Crystal).ToString();
        FuelText.text = PlayerData.Instance.GetCurrentVehicleFuelTank().GetFuelAmount().ToString("F");
    }

    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("WorldScene");
    }
    public void Refuel()
    {
        AmountPopupWindow.Create().SetUpResourcePopup("Refuel", "Refuel with how many crystals?", ResourceType.Crystal, PlayerData.Instance.GetInventory(), RefuelCallback);
    }
    void RefuelCallback(int resourceAmount)
    {
        FuelTank tank = PlayerData.Instance.GetCurrentVehicleFuelTank();
        ResourceStorage inventory = PlayerData.Instance.GetInventory();
        ResourceType type = ResourceType.Crystal;

        float fuel = tank.Refuel(type, inventory.TakeResource(type, resourceAmount));
        tank.AddFuel(fuel);
        UpdateValues();
    }

    private void Update()
    {
        if(_stockView && _viewSwitchProgress < 1)
        {
            _viewSwitchProgress = Mathf.Clamp01(_viewSwitchProgress + Time.deltaTime * ViewSwitchSpeed);
            UpdateViewSwitchProgress();
        } else if (!_stockView && _viewSwitchProgress > 0)
        {
            _viewSwitchProgress = Mathf.Clamp01(_viewSwitchProgress - Time.deltaTime * ViewSwitchSpeed);
            UpdateViewSwitchProgress();
        }
    }
    void UpdateViewSwitchProgress()
    {
        Transform camTransf = Camera.main.transform;
        camTransf.position = Vector3.Slerp(GarageView.position, StockView.position,_viewSwitchProgress);
        camTransf.rotation = Quaternion.Slerp(GarageView.rotation, StockView.rotation, _viewSwitchProgress);
    }

    public void SwitchViews()
    {
        _stockView = !_stockView;
    }
    public void BoostPopupCallback(int amount)
    {
        CityHealth.Instance.Boost(ResourceType.Crystal, PlayerData.Instance.GetInventory().TakeResource(ResourceType.Crystal, amount));
        UpdateValues();
    }
    public void BoostHealth()
    {
        AmountPopupWindow.Create().SetUpResourcePopup("Boost the economy", "How many crystals to boost the economy and city health?", ResourceType.Crystal, PlayerData.Instance.GetInventory(), BoostPopupCallback);
    }
}
