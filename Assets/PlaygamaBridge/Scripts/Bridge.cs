/*
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
using Playgama.Common;
using Playgama.Modules.Advertisement;
using Playgama.Modules.Device;
using Playgama.Modules.Game;
using Playgama.Modules.Leaderboard;
using Playgama.Modules.Payments;
using Playgama.Modules.Platform;
using Playgama.Modules.Player;
using Playgama.Modules.RemoteConfig;
using Playgama.Modules.Social;
using Playgama.Modules.Storage;

namespace Playgama
{
    public class Bridge : Singleton<Bridge>
    {
        public static AdvertisementModule advertisement => instance._advertisement;
        public static GameModule game => instance._game;
        public static StorageModule storage => instance._storage; 
        public static PlatformModule platform => instance._platform; 
        public static SocialModule social => instance._social; 
        public static PlayerModule player => instance._player; 
        public static DeviceModule device => instance._device; 
        public static LeaderboardModule leaderboard => instance._leaderboard; 
        public static PaymentsModule payments => instance._payments; 
        public static RemoteConfigModule remoteConfig => instance._remoteConfig; 

        private AdvertisementModule _advertisement;
        private GameModule _game;
        private StorageModule _storage;
        private PlatformModule _platform;
        private SocialModule _social;
        private PlayerModule _player;
        private DeviceModule _device;
        private LeaderboardModule _leaderboard;
        private PaymentsModule _payments;
        private RemoteConfigModule _remoteConfig;

        protected override void Awake()
        {
            base.Awake();
            instance.name = "PlaygamaBridge";
            _platform = gameObject.AddComponent<PlatformModule>();
            _game = gameObject.AddComponent<GameModule>();
            _player = gameObject.AddComponent<PlayerModule>();
            _storage = gameObject.AddComponent<StorageModule>();
            _advertisement = gameObject.AddComponent<AdvertisementModule>();
            _social = gameObject.AddComponent<SocialModule>();
            _device = new DeviceModule();
            _leaderboard = gameObject.AddComponent<LeaderboardModule>();
            _payments = gameObject.AddComponent<PaymentsModule>();
            _remoteConfig = gameObject.AddComponent<RemoteConfigModule>();
        }
    }
}
#endif