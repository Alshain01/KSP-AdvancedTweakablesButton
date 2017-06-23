using UnityEngine;
using KSP.Localization;

namespace AdvancedTweakablesButton
{
    class Toolbar : IButtonBar
    {
        string onTexture = "ToolbarOn", offTexture = "ToolbarOff";
        IButton toolbarButton;

        public Toolbar(string appPath)
        {
            onTexture = appPath + onTexture;
            offTexture = appPath + offTexture;
            toolbarButton = ToolbarManager.Instance.add("AdvancedTweakablesButton", "AdvancedTweakablesButton");
            toolbarButton.ToolTip = Localizer.GetStringByTag("#autoLOC_ATB_001");
            toolbarButton.Visibility = new GameScenesVisibility(GameScenes.FLIGHT, GameScenes.EDITOR);
            toolbarButton.TexturePath = GameSettings.ADVANCED_TWEAKABLES ? onTexture : offTexture;
            toolbarButton.OnClick +=
                (e) =>
                {
                    if (e.MouseButton == 0 && toolbarButton != null)
                    {
                        GameSettings.ADVANCED_TWEAKABLES = !GameSettings.ADVANCED_TWEAKABLES;
                        GameSettings.ApplySettings();
                        SetTexture();
                    }
                };

        }

        public void SetTexture()
        {
            if (toolbarButton != null)
                toolbarButton.TexturePath = GameSettings.ADVANCED_TWEAKABLES ? onTexture : offTexture;
        }

        public void Destroy()
        {
            if (toolbarButton != null)
            {
                toolbarButton.Destroy();
                toolbarButton = null;
            }
        }
    }
}
