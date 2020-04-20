using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static PlayerData Instance;
    ResourceStorage _inventory = new ResourceStorage();
    FuelTank _fuelTank = new FuelTank();
    public ResourceStorage GetInventory() { return _inventory; }
    public FuelTank GetCurrentVehicleFuelTank() { return _fuelTank; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            GetCurrentVehicleFuelTank().AddFuel(200);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
    }
}
