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

            AdvancedTweakablesButton.AddDebugLog("Registering App Launcher Events");
            GameEvents.onGUIApplicationLauncherReady.Add(Add);
            GameEvents.onGUIApplicationLauncherDestroyed.Add(Destroy);
            GameEvents.onGUIApplicationLauncherUnreadifying.Add(Destroy);
        }

        private void Add()
        {
            if (launcherButton == null)
            {
                AdvancedTweakablesButton.AddDebugLog("Launcher Button Generated");
                launcherButton = ApplicationLauncher.Instance.AddModApplication(OnClick, OnClick, null, null, null, null,
                    ApplicationLauncher.AppScenes.SPH | ApplicationLauncher.AppScenes.VAB | ApplicationLauncher.AppScenes.FLIGHT, offTexture);

                SetTexture();
            }
        }

        private void Destroy(GameScenes scene)
        {
            AdvancedTweakablesButton.AddDebugLog("Game Scene Load Detected");
            if (scene != GameScenes.FLIGHT && scene != GameScenes.EDITOR)
                Destroy();
        }

        public void Destroy()
        {
            AdvancedTweakablesButton.AddDebugLog("Destroying Launcher Button");
            GameEvents.onGUIApplicationLauncherReady.Remove(Add);
            GameEvents.onGUIApplicationLauncherUnreadifying.Remove(Destroy);
            if (launcherButton != null)
            {
                AdvancedTweakablesButton.AddDebugLog("Launcher Button Destroyed");
                ApplicationLauncher.Instance.RemoveModApplication(launcherButton);
                launcherButton = null;
            }
            else
            {
                AdvancedTweakablesButton.AddDebugLog("Failed to Destroy Launcher Button");
            }
        }

        private void OnClick()
        {
            if (launcherButton != null)
            {
                AdvancedTweakablesButton.AddDebugLog("Launcher Button Left Click");
                GameSettings.ADVANCED_TWEAKABLES = !GameSettings.ADVANCED_TWEAKABLES;
                GameSettings.ApplySettings();
                SetTexture();
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
