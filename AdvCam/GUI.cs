using System;
using System.Collections.Generic;
using System.Text;
using VNyanInterface;
using UnityEngine;
using static AdvCam.Functions;
using static AdvCam.VNyan_Functions;

namespace AdvCam {
    internal class GUI {
       
        private IVNyanUIWindow wndMain;
        IVNyanUIContentPanel pnlMain;
        IVNyanUIInputField txtMainLayers;
        IVNyanUIButton btnSetLayers;
        IVNyanUIButton btnRefreshLayers;
        IVNyanUIButton btnDefaultLayers;
        IVNyanUIButton btnDisableMainCam;
        IVNyanUIButton btnAdvancedMode;
        List<IVNyanUILabel> lblLayerNames = new List<IVNyanUILabel>();

        private string LayerMask;
        internal bool AdvModeEnabled = false;


        internal GUI() {
            wndMain = VNyanInterface.VNyanInterface.VNyanUI.createDraggableWindow(320, 70, true, false);
            pnlMain = wndMain.getContentPanel();
            IVNyanUILabel lblTitle = VNyanInterface.VNyanInterface.VNyanUI.createLabel(pnlMain, $"Adv. Camera Control - v{VNyan_Handlers.VersionString} by LumKitty", 10, -5, 400, 20);
            IVNyanUILabel lblMainCam = VNyanInterface.VNyanInterface.VNyanUI.createLabel(pnlMain, "Main Camera:", 10, -37, 100, 20);
            btnDefaultLayers = VNyanInterface.VNyanInterface.VNyanUI.createButton(pnlMain, "Enable", 100, -30, 50, 30);
            btnDisableMainCam = VNyanInterface.VNyanInterface.VNyanUI.createButton(pnlMain, "Disable", 160, -30, 50, 30);
            btnAdvancedMode = VNyanInterface.VNyanInterface.VNyanUI.createButton(pnlMain, "Advanced", 220, -30, 70, 30);

            btnDefaultLayers.onButtonClicked += btnDefaultLayers_OnClick;
            btnDisableMainCam.onButtonClicked += btnDisableMainCam_OnClick;
            btnAdvancedMode.onButtonClicked += btnAdvancedMode_OnClick;
        }

        void AdvancedMode() {
            if (AdvModeEnabled) { return; }
            wndMain.setSize(320, 615);
            LayerMask = Convert.ToString(Camera.main.cullingMask, 2).PadLeft(32, '0');
            txtMainLayers = VNyanInterface.VNyanInterface.VNyanUI.createInputField(pnlMain, 10, -80, 280, 30, LayerMask, "Mask");
            btnSetLayers = VNyanInterface.VNyanInterface.VNyanUI.createButton(pnlMain, "Set", 180, -120, 50, 30);
            btnRefreshLayers = VNyanInterface.VNyanInterface.VNyanUI.createButton(pnlMain, "Refresh", 240, -120, 50, 30);

            btnRefreshLayers.onButtonClicked += btnRefreshLayers_OnClick;
            btnSetLayers.onButtonClicked += btnSetLayers_OnClick;
            txtMainLayers.onInputFieldEndEdit += txtMainLayers_OnEdit;

            int YPos = -120;
            string TempLayerName = "";
            for (int n = 0; n <= 31; n++) {
                TempLayerName = UnityEngine.LayerMask.LayerToName(n);
                Log($"{n.ToString()}: {TempLayerName}");
                lblLayerNames.Add(VNyanInterface.VNyanInterface.VNyanUI.createLabel(pnlMain, $"{n.ToString()}: {TempLayerName}", 10, YPos, 160, 30));
                YPos -= 15;
            }
            AdvModeEnabled = true;
        }

        void RefreshLayers() {
            txtMainLayers.setText(Convert.ToString(Camera.main.cullingMask, 2).PadLeft(32, '0'));
        }

        void btnAdvancedMode_OnClick() {
            AdvancedMode();
        }

        void btnDisableMainCam_OnClick() {
            DisableMainCam();
            RefreshLayers();
        }

        void txtMainLayers_OnEdit() {
            string strNewLayerMask = txtMainLayers.getText();
            if (strNewLayerMask.Length == 32 && binary.IsMatch(strNewLayerMask)) {
                btnSetLayers.setText("Set");
            } else {
                btnSetLayers.setText("---");
            }
        }
        
        void btnDefaultLayers_OnClick() {
            EnableMainCam();
            RefreshLayers();
        }

        void btnRefreshLayers_OnClick() { RefreshLayers(); }

        void btnSetLayers_OnClick() {
            string strNewLayerMask = txtMainLayers.getText();
            if (strNewLayerMask.Length == 32) {
                int intNewLayerMask = 0; 
                if (StrToBinary(strNewLayerMask, ref intNewLayerMask)) {
                    SetMainCamLayers(intNewLayerMask);
                }
            }
        }
        
        private void PreFill() {
            //RefreshLayers();
        }

        internal void Show() {
            wndMain.setVisible(true);
            PreFill();
        }

        internal void Hide() {
            wndMain.setVisible(false);
        }
    }
}
