using UnityEngine;
using KSP.UI.Screens;

namespace AdvancedTweakablesButton
{
    [KSPAddon(KSPAddon.Startup.FlightAndEditor, true)]
    public class AdvancedTweakablesButton : MonoBehaviour
    {
        const string path = "AquilaEnterprises/AdvancedTweakablesButton/Resources";

        static Texture2D offTexture = GameDatabase.Instance.GetTexture(path + "offIcon", false);
        static Texture2D onTexture = GameDatabase.Instance.GetTexture(path + "onIcon", false);

        ApplicationLauncherButton launcherButton = null;

        public void Awake()
        {
            GameEvents.onGUIApplicationLauncherReady.Add(addButton);
            GameEvents.onGUIApplicationLauncherUnreadifying.Add(destroyButton);
            // Ensure the icon stays in Sync when settings menu is used
            GameEvents.OnGameSettingsApplied.Add(resetButtonVisual);
        }

        public void addButton()
        {
            if (launcherButton == null)
            {
                launcherButton = ApplicationLauncher.Instance.AddModApplication(onClick, onClick, null, null, null, null, 
                    ApplicationLauncher.AppScenes.SPH | ApplicationLauncher.AppScenes.VAB | ApplicationLauncher.AppScenes.FLIGHT, offTexture);

                resetButtonVisual();
            }
        }

        public void destroyButton(GameScenes scene)
        {
            if (launcherButton != null)
            {
                ApplicationLauncher.Instance.RemoveModApplication(launcherButton);
                launcherButton = null;
            }
        }

        void onClick()
        {
            if (launcherButton != null)
            {
                GameSettings.ADVANCED_TWEAKABLES = !GameSettings.ADVANCED_TWEAKABLES;
                GameSettings.ApplySettings();
                setIcon(launcherButton, GameSettings.ADVANCED_TWEAKABLES);
            }
        }

        void resetButtonVisual()
        {
            if (launcherButton != null)
            {
                if (GameSettings.ADVANCED_TWEAKABLES)
                {
                    launcherButton.SetTrue(false);
                    setIcon(launcherButton, true);
                }
                else
                {
                    launcherButton.SetFalse(false);
                    setIcon(launcherButton, false);
                }
            }
        }

        static void setIcon(ApplicationLauncherButton button, bool enabled)
        {
            if (enabled)
                button.SetTexture(onTexture);
            else
                button.SetTexture(offTexture);
        }


    }
}
