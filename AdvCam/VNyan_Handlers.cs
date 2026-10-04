using System;
using UnityEngine;
using VNyanInterface;
using static AdvCam.VNyan_Functions;
using static AdvCam.Functions;

namespace AdvCam {
    public class VNyan_Handlers : IVNyanPluginManifest, IButtonClickedHandler, ITriggerHandler {
        internal const string VersionString = "0.1-alpha";
        public string PluginName { get; } = "Advanced Camera Control";
        public string Version { get; } = VersionString;
        public string Title => PluginName + " " + Version;
        public string Author { get; } = "LumKitty";
        public string Website { get; } = "https://lum.uk/";

        internal GUI objGUI = null;

        public void InitializePlugin() {
            VNyanInterface.VNyanInterface.VNyanUI.registerPluginButton("Advanced Camera Control", this);
            VNyanInterface.VNyanInterface.VNyanTrigger.registerTriggerListener(this);
        }

        public void pluginButtonClicked() {
            if (objGUI == null) {
                objGUI = new GUI();
                VNyan_Functions.CheckDefaultLayers();
            } else {
                objGUI.Show();
            }
        }

        public void triggerCalled(string name, int int1, int int2, int int3, string text1, string text2, string text3) {
            try {
                if (name.Length > 10) {
                    name = name.ToLower();
                    if (name.Substring(0, 9) == "_lum_cam_") {
                        Log("Detected trigger: " + name);
                        name = name.Substring(8);
                    } else {
                        return;
                    }
                    switch (name) {
                        case "_disablemain": DisableMainCam(); break;
                        case "_enablemain": EnableMainCam(); break;
                        case "_disable": DisableSpoutCam(text1); break;
                        case "_enable": EnableSpoutCam(text1); break;
                    }
                }
            } catch (Exception ex) {
                Log("ERR: " + ex.ToString());
            }
        }
    }
}
