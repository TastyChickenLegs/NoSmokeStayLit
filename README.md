
# No Smoke Stay Lit

### Now with braziers and smelter/kiln stacking

### About the Mod:
<b>From the aclaimed author of Timed Torches Fixed</b>

Smokeless Fire removes the smoke and keeps your flames lit.  <b>Now with Braziers and Smelter Stacking</b>

``Big thanks to Azumatt for the help with Braziers.``

Now has servsync and version check.  Place on the server and all players.  The config will be pushed down from the server so everyone has the same settings.

``Turn version-check off in the config if having troubles or it is not needed for your gameplay.``


### Background:
I created this mode because I got tired of having to modify my builds to accommodate the smoke from fire sources.  I also don't want to constantly add wood to my firepit and hearth

### Configuration:

<b>If you leave the settings alone, firepits, hearths and bonfires will never need fuel and won't have any smoke</b>



The config file is located in "<GameDirectory>\Bepinex\config" (You need to start the game at least once, with the mod installed to create the config file).

<b>To view or add items this mod affects:  </b>

- ServerConfig Locked - locks the config files so all clients have the same config and cannot change. Default true

- AffectedFireplaceSources - List of objects to be affected by the mod (see below for list of supported objects). By default vanilla firesources are added.

- NoFuelNeeded - Turns off the need to add fuel to fires.  Default is True (fires won't need fuel and will stay lit)



### List of supported objects:
  

 ``piece_brazierfloor01``<br>
`` piece_brazierceiling01``<br>
 ``fire_pit``<br>
 ``hearth``<br>
 ``bonfire``

<b>all fire sources can be added</b>
- Custom modded items can be added as well..



### Installation: (manual)  
Put on client and server.  Server will push down the settings and lock the config.

Extract DLL from zip file into "<GameDirectory>\Bepinex\plugins"  
Start the game.

### Version Information

1.1.5

- Fixed errors in 1.1.4 resulting in object not set to an instance.  


1.1.4

- Add ability to turn off client checking for ServerSync.  If you are having problems with ServerSync turn this off

- Figured out how to turn smoke off for Braziers.  Thank you Azumatt for the help

- Removed smoke from Smelters so they can now stack on top of each other

1.13

- Fixed the packaged files.  I had the wrong mod in the zip folder

1.1.2

- Update to newest release of Mistlands 0.212.7


1.1.1

- Server Sync and version check - Courtesty of Azumatt's amazing youtube tutorial
- Allowed for all firesources
- Better compatibility with my Timed Torchese mod

1.0.1
- Updated to newest Beta Valheim Version (Mistlands)
- Fixed incorrect Nexus ID and placed correct ID of 2027

1.0.0

- initial release

