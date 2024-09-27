# DiscordWipe

Oxide plugin for Rust. Sends map wipe and server protocol change notification to discord. 

## Upgrading to version 2.1.0
[Upgrading to version 2.1.0](https://umod.org/community/discord-wipe/24069-upgrading-to-version-210)

## Notification Examples

### Wipe

![](https://i.imgur.com/Sr7bYsR.png)  

* Note map image is only for Rust and requires RustMapApi plugin

### Protocol

![](https://i.imgur.com/J15b2ku.png)  

## RustMaps.com Support

As of version 2.3.0 you can now use RustMaps.com to generate your map. To start using RustMaps.com 
1. Set Map Image Source (None, RustMaps, RustMapApi) to "RustMaps" in the config
2. Get Your API Key @ [RustMaps.com](https://rustmaps.com/dashboard) and put it in "RustMap.com API Key" in the config.
3. Choose which RustMap image you want to use and put it in on of the image options in the embed.
 - {rustmaps.com.map}
 - {rustmaps.com.icons}
 - {rustmaps.com.thumbnail}
4. Reload the plugin.

## Configuration
# Note: To disable sending a message leave the webhook url blank or the default value
```json
{
  "Debug Level (None, Error, Warning, Info)": "Warning",
  "Command": "dw",
  "Rust Map Image Settings": {
    "Map Image Source (None, RustMaps, RustMapApi)": "None",
    "RustMaps.com Settings": {
      "RustMap.com API Key": "Get Your API Key @ https://rustmaps.com/user/profile",
      "Generate Staging Map": false
    },
    "RustMapApi Settings": {
      "Render Name": "Icons",
      "Image Resolution Scale": 0.5,
      "File Type (Jpg, Png": "Jpg"
    }
  },
  "Send wipe message when server wipes": true,
  "Wipe Webhook url": "https://support.discordapp.com/hc/en-us/articles/228383668-Intro-to-Webhooks",
  "Send protocol message when server protocol changes": true,
  "Protocol Webhook url": "https://support.discordapp.com/hc/en-us/articles/228383668-Intro-to-Webhooks",
  "Wipe messages": [
    {
      "Content": "@everyone",
      "Webhook Override (Overrides the default webhook for this message)": "https://support.discordapp.com/hc/en-us/articles/228383668-Intro-to-Webhooks",
      "Send Mode (Always, Random)": "Always",
      "Embed": {
        "Enabled": true,
        "Title": "{server.name}",
        "Description": "The server has wiped!",
        "Url": "",
        "Embed Color": "#de8732",
        "Image Url": "attachment://map.jpg",
        "Thumbnail Url": "",
        "Fields": [
          {
            "Title": "Seed",
            "Value": "[{world.seed}](https://rustmaps.com/map/{world.size}_{world.seed})",
            "Inline": true,
            "Enabled": true
          },
          {
            "Title": "Size",
            "Value": "{world.size}M ({world.size!km^2}km^2)",
            "Inline": true,
            "Enabled": true
          },
          {
            "Title": "Protocol",
            "Value": "{server.protocol.network}",
            "Inline": true,
            "Enabled": true
          },
          {
            "Title": "Click & Connect",
            "Value": "steam://connect/{server.address}:{server.port}",
            "Inline": false,
            "Enabled": true
          }
        ],
        "Footer": {
          "Icon Url": "",
          "Text": "",
          "Enabled": true
        }
      }
    }
  ],
  "Protocol messages": [
    {
      "Content": "@everyone",
      "Webhook Override (Overrides the default webhook for this message)": "https://support.discordapp.com/hc/en-us/articles/228383668-Intro-to-Webhooks",
      "Send Mode (Always, Random)": "Always",
      "Embed": {
        "Enabled": true,
        "Title": "{server.name}",
        "Description": "The server protocol has changed!",
        "Url": "",
        "Embed Color": "#de8732",
        "Image Url": "",
        "Thumbnail Url": "",
        "Fields": [
          {
            "Title": "Protocol",
            "Value": "{server.protocol.network}",
            "Inline": true,
            "Enabled": true
          },
          {
            "Title": "Previous Protocol",
            "Value": "{server.protocol.previous}",
            "Inline": true,
            "Enabled": true
          },
          {
            "Title": "Mandatory Client Update",
            "Value": "This update requires a mandatory client update in order to be able to play on the server",
            "Inline": false,
            "Enabled": true
          },
          {
            "Title": "Click & Connect",
            "Value": "steam://connect/{server.address}:{server.port}",
            "Inline": false,
            "Enabled": true
          }
        ],
        "Footer": {
          "Icon Url": "",
          "Text": "",
          "Enabled": true
        }
      }
    }
  ]
}
```


#### Hide Footer
The default footer can be disabled by changing the footer value enabled to false. 
You can also add your own custom footer which will disable to current one

## Permissions

* `discordwipe.admin` - allows players to send test messages

## Commands

### Chat

`/dw` - displays the help text  
`/dw wipe` - sends a wipe message  
`/dw protocol` - sends a protocol message  

### Console

`dw` - displays the help text  
`dw wipe` - sends a wipe message  
`dw protocol` - sends a protocol message  

## Localization

```json
{
  "NoPermission": "You do not have permission to use this command",
  "SentWipe": "You have sent a test wipe message",
  "SentProtocol": "You have sent a test protocol message",
  "Help": "Sends test message for plugin\n{0}{1} wipe - sends a wipe test message\n{0}{1} protocol - sends a protocol test message\n{0}{1} - displays this help text again"
}
```

### Available Placeholders;

#### Note not all placeholders are available for every game. Please use `placeholderapi.list` to get the most up to date list for your game

#### Placeholder Structure:
`{key:format!option}`

- Key is the value displayed in the list below  
- Format is the format to be applied to the returned value  
- Option Allows you to change the type of data being returned  

#### Placeholders  
  
date.now (Placeholder API) - Options: "local" to use local time offset, UTC (default)   
`{date.now:MM/dd/yy hh:mm:ss tt}` - Will display the current date and time using the supplied format   
`{date.now:MM/dd/yy hh:mm:ss tt!local}` - Will do the same as above but convert the time to local time   
[DateTime Formatting](https://docs.microsoft.com/en-us/dotnet/standard/base-types/custom-date-and-time-format-strings)   
 
plugins.amount (Placeholder API)   
`{plugins.amount}` - returns the number of plugins on the server   

server.address (Placeholder API)  
`{server.address}` - returns the server IP Address  

server.blueprints.wipe.last (Placeholder API) - Options: "local" to use local time offset, UTC (default)    
`{server.blueprints.wipe.last:MM/dd/yy hh:mm:ss tt}` - Will display the last blueprint wipe date in UTC time  
`{server.blueprints.wipe.last:MM/dd/yy hh:mm:ss tt!local}` - Will display the last blueprint wipe date in local time    
[DateTime Formatting](https://docs.microsoft.com/en-us/dotnet/standard/base-types/custom-date-and-time-format-strings)    

server.blueprints.wipe.next (Placeholder API) - Options: "local" to use local time offset, UTC (default)    
`{server.blueprints.wipe.next:MM/dd/yy hh:mm:ss tt}` - Will display the next blueprint wipe date in UTC time  
`{server.blueprints.wipe.next:MM/dd/yy hh:mm:ss tt!local}` - Will display the next blueprint wipe date in local time    
[DateTime Formatting](https://docs.microsoft.com/en-us/dotnet/standard/base-types/custom-date-and-time-format-strings)    

server.description (Placeholder API)  
`{server.description}` - Will display the servers description  

server.entities (Placeholder API)   
`{server.entities}` - Will return the number of entities on the server  

server.fps (Placeholder API)    
`{server.fps:0}` - Will return the current server framerate   

server.fps.average (Placeholder API)  
`{server.fps.average}` - Will return the average server framerate  

server.language.code (Placeholder API)  
`{server.language.code}` - Returns Two letter ISO language name  

server.language.name (Placeholder API)  
`{server.language.name}` - Returns the server language name  

server.map.wipe.last (Placeholder API) - Options: "local" to use local time offset, UTC (default)    
`{server.map.wipe.last:MM/dd/yy hh:mm:ss tt}` - Will display the last map wipe date in UTC time  
`{server.map.wipe.last:MM/dd/yy hh:mm:ss tt!local}` - Will display the last map wipe date in local time    
[DateTime Formatting](https://docs.microsoft.com/en-us/dotnet/standard/base-types/custom-date-and-time-format-strings)    

server.map.wipe.next (Placeholder API) - Options: "local" to use local time offset, UTC (default)   
`{server.map.wipe.next:MM/dd/yy hh:mm:ss tt}` - Will display the next map wipe date in UTC time  
`{server.map.wipe.next:MM/dd/yy hh:mm:ss tt!local}` - Will display the next map wipe date in local time    
[DateTime Formatting](https://docs.microsoft.com/en-us/dotnet/standard/base-types/custom-date-and-time-format-strings)    

server.memory.total (Placeholder API) - Options: B (default), KB, MB, GB    
`{server.memory.total:0}` - Returns the servers total available memory in bytes  
`{server.memory.total:0!kb}` - Returns the servers total available memory in kilobytes  
`{server.memory.total:0!mb}` - Returns the servers total available memory in megabytes  
`{server.memory.total:0!gb}` - Returns the servers total available memory in gigabytes  

server.memory.used (Placeholder API) - Options: B (default), KB, MB, GB    
`{server.memory.total:0.00}` - Returns the servers used available memory in bytes  
`{server.memory.total:0.00!kb}` - Returns the servers used available memory in kilobytes  
`{server.memory.total:0.00!mb}` - Returns the servers used available memory in megabytes  
`{server.memory.total:0.00!gb}` - Returns the servers used available memory in gigabytes  

server.name (Placeholder API)   
`{server.name}` - Returns the servers host name  

server.network.in (Placeholder API) - Options: B (or B/s, default) KB (or KB/s), MB (or MB/s), GB (or GB/s), Bps, Kbps, Mbps, Gbps    
`{server.network.in:0.00}` - Returns the servers network in in bytes  
`{server.network.in:0.00!KB}` - Returns the servers network in in kilobytes  
`{server.network.in:0.00!MB}` - Returns the servers network in in megabytes  
`{server.network.in:0.00!GB}` - Returns the servers network in in gigabytes  

server.network.out (Placeholder API) - Options: B (or B/s, default) KB (or KB/s), MB (or MB/s), GB (or GB/s), Bps, Kbps, Mbps, Gbps    
`{server.network.out:0.00}` - Returns the servers network out in bytes  
`{server.network.out:0.00!KB}` - Returns the servers network out in kilobytes  
`{server.network.out:0.00!MB}` - Returns the servers network out in megabytes   
`{server.network.out:0.00!GB}` - Returns the servers network out in gigabytes  

server.oxide.rust.version (Placeholder API)   
`{server.network.out}` - Returns the rust oxide version  

server.players (Placeholder API)   
`{server.players}` - Returns the current player count on the server  

server.players.loading (Placeholder API)  
`{server.players.loading}` - Returns the current number of players loading into the server  

server.players.max (Placeholder API)   
`{server.players.max}` - Returns the maximum number of players allowed on the server  

server.players.queued (Placeholder API)   
`{server.players.queued}` - Returns the number of players currently queued to join the server  

server.players.sleepers (Placeholder API)   
`{server.players.sleepers}` - Returns the current number of sleepers on the server  

server.players.stored (Placeholder API) 
`{server.players.stored}` - Returns the total number of sleepers and connected players on the server

server.players.total (Placeholder API)   
`{server.players.total}` - Returns the total number of players that have ever been on the server  

server.port (Placeholder API)  
`{server.port}` - Returns the server port  
 
server.protocol (Placeholder API)     
`{server.protocol}` - Returns the server protocol version  

server.protocol.network (Placeholder API)     
`{server.protocol.network}` - returns the server network version  

server.protocol.persistance (Placeholder API)  
`{server.protocol.persistance}` - Returns the blueprint version for the server  

server.protocol.report (Placeholder API)   
`{server.protocol.report}` - Returns the server report version  

server.protocol.save (Placeholder API)   
`{server.protocol.save}` - Returns server save version  

server.time (Placeholder API) - Current in-game time    
`{server.time:MM/dd/yy hh:mm:ss tt}` - Returns servers current time  
[DateTime Formatting](https://docs.microsoft.com/en-us/dotnet/standard/base-types/custom-date-and-time-format-strings)    

world.salt (Placeholder API)   
`{world.salt}` - Returns the servers salt  

world.seed (Placeholder API)   
`{world.seed}` - Returns the servers map seed  

world.size (Placeholder API) - Options: km, km2 (or km^2), m (default), m2 (or m^2)    
`{world.size}` - Returns map size in meters  
`{world.size!m2}` - Returns map size in meters squared  
`{world.size!km}` - Returns map size in kilometers  
`{world.size!km2}` - Returns map size in kilometers squared  

world.url (Placeholder API)   
`{world.url}` - Returns the url to download the map  

server.protocol.previous (Discord Wipe) - Displays the previous protocol version if it changed during the last restart   
`{server.protocol.previous}` - Returns the previous network version