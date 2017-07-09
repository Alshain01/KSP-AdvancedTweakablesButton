using UnityEngine;
using KSP.UI.Screens;

namespace AdvancedTweakablesButton
{
    [KSPAddon(KSPAddon.Startup.FlightAndEditor, false)]
    public class AdvancedTweakablesButton : MonoBehaviour
    {
        const string appPath = "AquilaEnterprises/AdvancedTweakablesButton/Resources/";
        IButtonBar button;

        public void Awake()
        {
            if (ToolbarManager.ToolbarAvailable)
            {
                AddDebugLog("Initializing Toolbar Button");
                button = new Toolbar(appPath);
            }
            else
            {
                AddDebugLog("Initializing Launcher Button");
                button = new AppLauncher(appPath);
            }

            // Ensure the icon stays in Sync when settings menu is used
            AddDebugLog("Registering Main Events");
            GameEvents.OnGameSettingsApplied.Add(button.SetTexture);
        }

        void OnDestroy()
        {
            GameEvents.OnGameSettingsApplied.Remove(button.SetTexture);
            if (button != null)
            {
                button.Destroy();
                AddDebugLog("Button Destroyed");
            }
        }

        static public void AddDebugLog(string message)
        {
#if DEBUG
            Debug.Log("<color=#800000ff>[Advanced Tweakables Button]</color> " + message);
#endif
        }
    }
}
