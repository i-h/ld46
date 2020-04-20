using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Entrance : MonoBehaviour
{
    public string Target = "";

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;


        if (Target == "BaseScene")
        {
            Miner[] miners = other.GetComponentsInChildren<Miner>();
            foreach (Miner m in miners)
            {
                PlayerData.Instance.GetInventory().Dump(m.GetStorage());
            }
        }

        SceneManager.LoadScene(Target);
    }

}
