using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MenuController
{
    public void General()
    {
        SwitchMenu(currentMenu, "General");
        currentMenu = "General";
    }

    public void Video()
    {
        SwitchMenu(currentMenu, "Video");
        currentMenu = "Video";
    }

    public void Audio()
    {
        SwitchMenu(currentMenu, "Audio");
        currentMenu = "Audio";
    }

    public void KeyBindings()
    {
        SwitchMenu(currentMenu, "KeyBindings");
        currentMenu = "KeyBindings";
    }
}
