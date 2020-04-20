using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceCounter : MonoBehaviour
{
    public ResourceType Type = ResourceType.None;
    public Image ResourceIcon;
    public Text ResourceText;

    private void Start()
    {
        ResourceIcon.sprite = Resources.Load<Sprite>("UISprites/icon_" + Type.ToString().ToLower());
    }

}
