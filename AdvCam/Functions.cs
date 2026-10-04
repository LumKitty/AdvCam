using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace AdvCam {
    internal static class Functions {
        internal static readonly Regex binary = new Regex("^[01]{1,32}$", RegexOptions.Compiled);

        internal static bool StrToBinary(string s, ref int result) {
            if (binary.IsMatch(s)) {
                result = Convert.ToInt32(s, 2);
                return true;
            } else {
                return false;
            }
        }

        internal static void Log(string Message, int LogLevel = 1) {
            //if (LogLevel <= _Settings.GlobalSettings.LogLevel) {
                UnityEngine.Debug.Log($"[AdvCam] {Message}");
            //}
        }
    }
}
