using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct SubMenu
{
    public string name;
    public GameObject menuObjects;
}

public class MenuController : MonoBehaviour
{
    [SerializeField] private SubMenu[] subMenus;

    public void DeactivateMenu(string name)
    {
        foreach (var subMenu in subMenus)
        {
            if (subMenu.name == name)
            {
                subMenu.menuObjects.SetActive(false);
                return;
            }
        }
    }

    public void ActivateMenu(string name)
    {
        foreach (var subMenu in subMenus)
        {
            if (subMenu.name == name)
            {
                subMenu.menuObjects.SetActive(true);
                return;
            }
        }
    }
}
