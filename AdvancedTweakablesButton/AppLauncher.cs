using UnityEngine;
using KSP.UI.Screens;

namespace AdvancedTweakablesButton
{
    class AppLauncher : IButtonBar
    {
        ApplicationLauncherButton launcherButton = null;
        static Texture2D offTexture, onTexture;

        public AppLauncher(string appPath)
        {
            offTexture = GameDatabase.Instance.GetTexture(appPath + "LauncherOff", false);
            onTexture = GameDatabase.Instance.GetTexture(appPath + "LauncherOn", false);

            GameEvents.onGUIApplicationLauncherReady.Add(Add);
            GameEvents.onGUIApplicationLauncherUnreadifying.Add(Destroy);
        }

        private void Add()
        {
            if (launcherButton == null)
            {
                launcherButton = ApplicationLauncher.Instance.AddModApplication(OnClick, OnClick, null, null, null, null,
                    ApplicationLauncher.AppScenes.SPH | ApplicationLauncher.AppScenes.VAB | ApplicationLauncher.AppScenes.FLIGHT, offTexture);

                SetTexture();
            }
        }

        private void OnClick()
        {
            if (launcherButton != null)
            {
                GameSettings.ADVANCED_TWEAKABLES = !GameSettings.ADVANCED_TWEAKABLES;
                GameSettings.ApplySettings();
                SetTexture();
            }
        }

        private void Destroy(GameScenes scene)
        {
            Destroy();
        }

        public void Destroy()
        {
            if (launcherButton != null)
            {
                ApplicationLauncher.Instance.RemoveModApplication(launcherButton);
                launcherButton = null;
            }
        }

        public void SetTexture()
        {
            if (launcherButton != null)
            {
                if (GameSettings.ADVANCED_TWEAKABLES)
                {
                    launcherButton.SetTrue(false);
                    launcherButton.SetTexture(onTexture);
                }
                else
                {
                    launcherButton.SetFalse(false);
                    launcherButton.SetTexture(offTexture);
                }
            }
        }
    }
}
