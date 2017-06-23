using UnityEngine;
using KSP.UI.Screens;

namespace AdvancedTweakablesButton
{
    [KSPAddon(KSPAddon.Startup.FlightAndEditor, true)]
    public class AdvancedTweakablesButton : MonoBehaviour
    {
        const string appPath = "AquilaEnterprises/AdvancedTweakablesButton/Resources/";
        IButtonBar button;

        public void Awake()
        {
            if (ToolbarManager.ToolbarAvailable)
                button = new Toolbar(appPath);
            else
                    button = new AppLauncher(appPath);

            // Ensure the icon stays in Sync when settings menu is used
            GameEvents.OnGameSettingsApplied.Add(button.SetTexture);
        }

        void OnDestroy()
        {
            if(button != null)
                button.Destroy();
        }
    }
}
