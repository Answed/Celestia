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
    protected string currentMenu;
    [SerializeField] protected GameObject backButton;

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

    public void SwitchMenu(string from, string to)
    {
        DeactivateMenu(from);
        ActivateMenu(to);
    }

    public void Back()
    {
        SwitchMenu(currentMenu, "Start");
        currentMenu = "Start";
        backButton.SetActive(true);
    }

    public void SwitchBackButton(bool state = false)
    {
        if(state)
            backButton.SetActive(true);
        else
            backButton.SetActive(false);
    }
}
