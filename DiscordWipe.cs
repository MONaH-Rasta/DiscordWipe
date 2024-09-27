using System.ComponentModel;
using Newtonsoft.Json;
using Oxide.Core;
using Oxide.Core.Plugins;

namespace Oxide.Plugins
{
    [Info("Discord Wipe", "MJSU", "1.0.2")]
    [Description("Sends a notification to a discord channel when the server wipes")]
    internal class DiscordWipe : CovalencePlugin
    {
        #region Class Fields
        [PluginReference] private Plugin DiscordCore;
        
        private StoredData _storedData; //Plugin Data
        
        private PluginConfig _pluginConfig;

        private bool _isWipe;
        #endregion

        #region Setup & Loading
        private void Init()
        {
            _storedData = Interface.Oxide.DataFileSystem.ReadObject<StoredData>(Name);
        }

        private void OnServerInitialized()
        {
            if (DiscordCore == null)
            {
                PrintError("Missing plugin dependency DiscordCore: https://umod.org/plugins/discord-core");
                return;
            }

            OnDiscordCoreReady();
        }

        private void OnDiscordCoreReady()
        {
            if (!(DiscordCore?.Call<bool>("IsReady") ?? false))
            {
                return;
            }

            timer.In(15f, () =>
            {
                if (_isWipe && _pluginConfig.MapWipe)
                {
                    SendChannel(_pluginConfig.WipeText);
                }

                string protocol = GetProtocol();
                if (string.IsNullOrEmpty(_storedData.Protocol))
                {
                    _storedData.Protocol = protocol;
                    SaveData();
                }
                else if (_storedData.Protocol != protocol && _pluginConfig.ProtocolChange)
                {
                    _storedData.Protocol = protocol;
                    SaveData();
                    
                    if (_pluginConfig.ProtocolChange)
                    {
                        SendChannel(string.Format(_pluginConfig.ProtocolText, protocol));
                    }
                }
            });
        }

        protected override void LoadDefaultConfig()
        {
            PrintWarning("Loading Default Config");
        }

        protected override void LoadConfig()
        {
            base.LoadConfig();
            Config.Settings.DefaultValueHandling = DefaultValueHandling.Populate;
            _pluginConfig = Config.ReadObject<PluginConfig>();
            Config.WriteObject(_pluginConfig);
        }

        private void OnNewSave(string filename)
        {
            _isWipe = true;
        }
        #endregion

        #region Helpers

        private void SendChannel(string message)
        {
            if (!string.IsNullOrEmpty(_pluginConfig.AnnouncementsChannelName))
            {
                DiscordCore.Call("SendMessageToChannel", _pluginConfig.AnnouncementsChannelName, $"{_pluginConfig.Prefix} {message}");
            }
        }

        private string GetProtocol()
        {
#if RUST
            return server.Protocol.Split('.')[0];
#else
            return server.Protocol;
#endif
        }
        #endregion
        
        #region Helper Methods
        private void SaveData() => Interface.Oxide.DataFileSystem.WriteObject(Name, _storedData);
        #endregion

        #region Classes
        private class PluginConfig
        {
            [DefaultValue("Rust Update:")]
            [JsonProperty(PropertyName = "Message Prefix")]
            public string Prefix { get; set; }
            
            [DefaultValue("announcements")]
            [JsonProperty(PropertyName = "Announcements Channel Name Or Id")]
            public string AnnouncementsChannelName { get; set; }
            
            [DefaultValue(true)]
            [JsonProperty(PropertyName = "Send on Map Wipe")]
            public bool MapWipe { get; set; }

            [DefaultValue("@everyone The SERVERNAME server has wiped! Join Now!")]
            [JsonProperty(PropertyName = "Wipe Text")]
            public string WipeText { get; set; }
            
            [DefaultValue(true)]
            [JsonProperty(PropertyName = "Send on Protocol Change")]
            public bool ProtocolChange { get; set; }
            
            [DefaultValue("The server has been updated to protocol {0}")]
            [JsonProperty(PropertyName = "Protocol Text")]
            public string ProtocolText { get; set; }
        }
        
        private class StoredData
        {
            public string Protocol { get; set; }
        }

        #endregion
    }
}
