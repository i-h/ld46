using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(GridLayoutGroup))]
public class FuelMeter : MonoBehaviour
{
    public float FuelPerBar = 25.0f;
    FuelTank playerTank;
    int maxBars = 0;
    int currentBars;
    List<Image> fuelBars;
    private void Start()
    {
        playerTank = PlayerData.Instance.GetCurrentVehicleFuelTank();
        Debug.Log(playerTank.GetFuelAmount());
        maxBars = Mathf.CeilToInt(playerTank.GetFuelAmount() / FuelPerBar);
        Image fuelBarIcon = Resources.Load<Image>("UIPrefabs/FuelBarIcon");

        fuelBars = new List<Image>(maxBars);

        for(int i = 0; i < maxBars; i++)
        {
            fuelBars.Add(Instantiate<Image>(fuelBarIcon, transform));
        }
        currentBars = maxBars;
    }

    void FixedUpdate()
    {
        int reqBars = Mathf.CeilToInt(playerTank.GetFuelAmount() / FuelPerBar);
        if(reqBars != currentBars)
        {
            for (int i = 0; i < fuelBars.Count; i++)
            {                
                fuelBars[i].gameObject.SetActive(i < reqBars);
            }
            currentBars = reqBars;
        }
    }
}
