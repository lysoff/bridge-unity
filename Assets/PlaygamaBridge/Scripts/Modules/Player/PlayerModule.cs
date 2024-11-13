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

#if !UNITY_ANDROID
using System;
using System.Collections.Generic;
using UnityEngine;
#if !UNITY_EDITOR
using Newtonsoft.Json;
using Playgama.Common;
using System.Runtime.InteropServices;
#endif

namespace Playgama.Modules.Player
{
    public class PlayerModule : MonoBehaviour
    {
        public bool isAuthorizationSupported
        {
            get
            {
#if !UNITY_EDITOR
                return PlaygamaBridgeIsPlayerAuthorizationSupported() == "true";
#else
                return false;
#endif
            }
        }

        public bool isAuthorized
        {
            get
            {
#if !UNITY_EDITOR
                return PlaygamaBridgeIsPlayerAuthorized() == "true";
#else
                return false;
#endif
            }
        }

        public string id
        {
            get
            {
#if !UNITY_EDITOR
                var value = PlaygamaBridgePlayerId();
                if (string.IsNullOrEmpty(value)) {
                    return null;
                }

                return value;
#else
                return null;
#endif
            }
        }

        public new string name
        {
            get
            {
#if !UNITY_EDITOR
                var value = PlaygamaBridgePlayerName();
                if (string.IsNullOrEmpty(value)) {
                    return null;
                }

                return value;
#else
                return null;
#endif
            }
        }

        public List<string> photos
        {
            get
            {
#if !UNITY_EDITOR
                var json = PlaygamaBridgePlayerPhotos();
                if (string.IsNullOrEmpty(json)) {
                    return new List<string>();
                }
                
                try
                {
                    var list = JsonConvert.DeserializeObject<List<string>>(json);
                    return list;
                }
                catch (Exception e)
                {
                    return new List<string>();
                }
#else
                return new List<string>();
#endif
            }
        }

#if !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern string PlaygamaBridgeIsPlayerAuthorizationSupported();

        [DllImport("__Internal")]
        private static extern string PlaygamaBridgeIsPlayerAuthorized();

        [DllImport("__Internal")]
        private static extern string PlaygamaBridgePlayerId();

        [DllImport("__Internal")]
        private static extern string PlaygamaBridgePlayerName();

        [DllImport("__Internal")]
        private static extern string PlaygamaBridgePlayerPhotos();

        [DllImport("__Internal")]
        private static extern void PlaygamaBridgeAuthorizePlayer(string options);
#endif

        private Action<bool> _authorizationCallback;


        public void Authorize(Dictionary<string, object> options = null, Action<bool> onComplete = null)
        {
            _authorizationCallback = onComplete;

#if !UNITY_EDITOR
            PlaygamaBridgeAuthorizePlayer(options.ToJson());
#else
            OnAuthorizeCompleted("false");
#endif
        }


        // Called from JS
        private void OnAuthorizeCompleted(string result)
        {
            var isSuccess = result == "true";
            _authorizationCallback?.Invoke(isSuccess);
            _authorizationCallback = null;
        }
    }
}
#endif