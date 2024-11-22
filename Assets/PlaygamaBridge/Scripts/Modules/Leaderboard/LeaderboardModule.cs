﻿/*
 * This file is part of Playgama Bridge.
 *
 * Playgama Bridge is free software: you can redistribute it and/or modify
 * it under the terms of the GNU Lesser General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * any later version.
 *
 * Playgama Bridge is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
 * GNU Lesser General Public License for more details.
 *
 * You should have received a copy of the GNU Lesser General Public License
 * along with Playgama Bridge. If not, see <https://www.gnu.org/licenses/>.
*/

#if UNITY_WEBGL
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
#if !UNITY_EDITOR
using Playgama.Common;
using System.Runtime.InteropServices;
#endif

namespace Playgama.Modules.Leaderboard
{
    public class LeaderboardModule : MonoBehaviour
    {
        public bool isSupported
        {
            get
            {
#if !UNITY_EDITOR
                return PlaygamaBridgeIsLeaderboardSupported() == "true";
#else
                return false;
#endif
            }
        }

        public bool isNativePopupSupported
        {
            get
            {
#if !UNITY_EDITOR
                return PlaygamaBridgeIsLeaderboardNativePopupSupported() == "true";
#else
                return false;
#endif
            }
        }

        public bool isSetScoreSupported
        {
            get
            {
#if !UNITY_EDITOR
                return PlaygamaBridgeIsLeaderboardSetScoreSupported() == "true";
#else
                return false;
#endif
            }
        }

        public bool isGetScoreSupported
        {
            get
            {
#if !UNITY_EDITOR
                return PlaygamaBridgeIsLeaderboardGetScoreSupported() == "true";
#else
                return false;
#endif
            }
        }

        public bool isGetEntriesSupported
        {
            get
            {
#if !UNITY_EDITOR
                return PlaygamaBridgeIsLeaderboardGetEntriesSupported() == "true";
#else
                return false;
#endif
            }
        }

#if !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern string PlaygamaBridgeIsLeaderboardSupported();

        [DllImport("__Internal")]
        private static extern string PlaygamaBridgeIsLeaderboardNativePopupSupported();

        [DllImport("__Internal")]
        private static extern string PlaygamaBridgeIsLeaderboardSetScoreSupported();

        [DllImport("__Internal")]
        private static extern string PlaygamaBridgeIsLeaderboardGetScoreSupported();

        [DllImport("__Internal")]
        private static extern string PlaygamaBridgeIsLeaderboardGetEntriesSupported();

        [DllImport("__Internal")]
        private static extern void PlaygamaBridgeLeaderboardSetScore(string options);

        [DllImport("__Internal")]
        private static extern void PlaygamaBridgeLeaderboardGetScore(string options);

        [DllImport("__Internal")]
        private static extern void PlaygamaBridgeLeaderboardGetEntries(string options);

        [DllImport("__Internal")]
        private static extern void PlaygamaBridgeLeaderboardShowNativePopup(string options);
#endif

        private Action<bool> _setScoreCallback;
        private Action<bool, int> _getScoreCallback;
        private Action<bool, List<Dictionary<string, string>>> _getEntriesCallback;
        private Action<bool> _showNativePopupCallback;

        
        public void SetScore(Dictionary<string, object> options, Action<bool> onComplete)
        {
            _setScoreCallback = onComplete;
#if !UNITY_EDITOR
            PlaygamaBridgeLeaderboardSetScore(options.ToJson());
#else
            OnLeaderboardSetScoreCompleted("false");
#endif
        }

        public void GetScore(Dictionary<string, object> options, Action<bool, int> onComplete)
        {
            _getScoreCallback = onComplete;
#if !UNITY_EDITOR
            PlaygamaBridgeLeaderboardGetScore(options.ToJson());
#else
            OnLeaderboardGetScoreCompleted("false");
#endif
        }

        public void GetEntries(Dictionary<string, object> options, Action<bool, List<Dictionary<string, string>>> onComplete)
        {
            _getEntriesCallback = onComplete;
#if !UNITY_EDITOR
            PlaygamaBridgeLeaderboardGetEntries(options.ToJson());
#else
            OnLeaderboardGetEntriesCompletedFailed();
#endif
        }

        public void ShowNativePopup(Dictionary<string, object> options, Action<bool> onComplete = null)
        {
            _showNativePopupCallback = onComplete;
#if !UNITY_EDITOR
            PlaygamaBridgeLeaderboardShowNativePopup(options.ToJson());
#else
            OnLeaderboardShowNativePopupCompleted("false");
#endif
        }


        // Called from JS
        private void OnLeaderboardSetScoreCompleted(string result)
        {
            var isSuccess = result == "true";
            _setScoreCallback?.Invoke(isSuccess);
            _setScoreCallback = null;
        }

        private void OnLeaderboardGetScoreCompleted(string result)
        {
            var isSuccess = result != "false";
            var score = 0;

            if (isSuccess)
            {
                int.TryParse(result, out score);
            }

            _getScoreCallback?.Invoke(isSuccess, score);
            _getScoreCallback = null;
        }

        private void OnLeaderboardGetEntriesCompletedSuccess(string result)
        {
            var entries = new List<Dictionary<string, string>>();

            if (!string.IsNullOrEmpty(result))
            {
                try
                {
                    entries = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(result);
                }
                catch (Exception e)
                {
                    Debug.Log(e);
                }
            }

            _getEntriesCallback?.Invoke(true, entries);
            _getEntriesCallback = null;
        }

        private void OnLeaderboardGetEntriesCompletedFailed()
        {
            _getEntriesCallback?.Invoke(false, null);
            _getEntriesCallback = null;
        }

        private void OnLeaderboardShowNativePopupCompleted(string result)
        {
            var isSuccess = result == "true";
            _showNativePopupCallback?.Invoke(isSuccess);
            _showNativePopupCallback = null;
        }
    }
}
#endif