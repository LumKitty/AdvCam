using Klak.Spout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using VNyanInterface;

namespace AdvCam {
    internal static class VNyan_Functions {
        // private static Dictionary<string, Texture> RenderTextureBackup = new Dictionary<string, Texture>();

        private static int? _DefaultLayers = null;
        internal static int DefaultLayers {
            get {
                CheckDefaultLayers();
                return (int)_DefaultLayers;
            }
            set {
                CheckDefaultLayers();
                _DefaultLayers = value;
            }
        }

        internal static void CheckDefaultLayers() {
            if (_DefaultLayers == null) {
                _DefaultLayers = Camera.main.cullingMask;
            }
        }

        internal static void DisableMainCam() {
            CheckDefaultLayers();
            Camera.main.cullingMask = 0; 
        }

        internal static void EnableMainCam() { Camera.main.cullingMask = (int)DefaultLayers; }

        internal static void SetMainCamLayers(int LayerMask) {
            CheckDefaultLayers();
            Camera.main.cullingMask = LayerMask;
        }

        internal static ISpout2Camera GetSpoutCam(int SpoutIndex = 0) {
            ISpout2Camera[] AllCameras = VNyanInterface.VNyanInterface.VNyanRender.getSpout2Cameras();
            if (AllCameras.Count() > SpoutIndex + 1) { return null; }
            return AllCameras[SpoutIndex];
        }

        internal static bool GetSpoutCam(string SpoutName, ref ISpout2Camera SpoutCam) {
            SpoutName = SpoutName.ToLower();
            foreach (ISpout2Camera CurCamera in VNyanInterface.VNyanInterface.VNyanRender.getSpout2Cameras()) {
                if (CurCamera.getSourceName().ToLower() == SpoutName) {
                    SpoutCam = CurCamera;
                    return true;
                }
            }
            return false;
        }

        internal static bool GetGameObject(string SpoutName, ref GameObject SpoutCam) {
            ISpout2Camera CurCamera = null;
            if (GetSpoutCam(SpoutName, ref CurCamera)) {
                SpoutCam = (GameObject)CurCamera.getCameraGameObject();
                return true;
            } else {
                return false;
            }
        }

        internal static void DisableSpoutCam(string SpoutName) {
            UnityEngine.GameObject CurGameObject = null;
            if (GetGameObject(SpoutName, ref CurGameObject)) {
                SpoutSender CurSpout = CurGameObject.GetComponent<SpoutSender>();
                Camera CurCamera = CurGameObject.GetComponent<Camera>();
                RenderTexture rt = RenderTexture.GetTemporary(CurCamera.pixelWidth, CurCamera.pixelHeight, 24, RenderTextureFormat.ARGB32);
                //RenderTextureBackup[SpoutName] = CurSpout.sourceTexture;
                CurSpout.sourceTexture = rt;
                RenderTexture.ReleaseTemporary(rt);
                CurCamera.enabled = false;
            }
        }

        internal static void EnableSpoutCam(string SpoutName) {
            UnityEngine.GameObject CurGameObject = null;
            if (GetGameObject(SpoutName, ref CurGameObject)) {
                SpoutSender CurSpout = CurGameObject.GetComponent<SpoutSender>();
                Camera CurCamera = CurGameObject.GetComponent<Camera>();
                //if (RenderTextureBackup.ContainsKey(SpoutName)) {
                CurCamera.enabled = true;
                CurSpout.sourceTexture = CurCamera.activeTexture;
                //}
            }
        }
    }
}
