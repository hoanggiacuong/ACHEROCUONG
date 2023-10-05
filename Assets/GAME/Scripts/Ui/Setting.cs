    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Setting :UICanvas
{

    public Slider themSlider, sfxSlider;

    public override void Open()
    {
        base.Open();
        Time.timeScale = 0;
    }
    public override void CloseDirectly()
    {
        base.CloseDirectly();
        Time.timeScale = 1;
    }

    public void ContinueButton()
    {
        Time.timeScale = 1;
        CloseDirectly();
    }

    public void RetryButton()
    {
        RoomManager.Ins.ReTry();
    }

    public void ThemeToggleBtn()
    {
        SoudManager.Ins.ToggleTheme();
    }

    public void SfxToggleBtn()
    {
        SoudManager.Ins.ToggleSfx();
    }

    public void ThemeVolume()
    {
        SoudManager.Ins.ThemeVolume(themSlider.value);
    }

    public void SfxVolume()
    {
        SoudManager.Ins.SfxVolume(sfxSlider.value);
    }
}
