
# Valheim - No Smoke Stay Lit

## Updated for Mistlands Beta - with ServerSync

### About the Mod:
<b>From the aclaimed author of Timed Torches Fixed</b>

ThunderStore people will note that my username has changed to TastyChickenLeg instead of TastyChickenLegs.  I deleted my Github (was going to combine with work account then decided not to)
and forgot Thunderstore needed Github to login. So I can't get into my old TastyChickenLegs Team

- My other mod TimedTorches_Fixed will remain lifeless on Thunderstore (I can't access the account) unless someone knows how to fix that problem.  No worries I rebuilt the code and published it as TimedTorchesStayLit
- I tried to get TastyChickenLegs back on Github but that's a no go too. 

### TastyChickenLegs presents another timeless mod

Smokeless Fire removes the smoke and keeps your flames lit.  

Now has servsync version check.  Place on the server and all players.  The config will be pushed down from the server so everyone has the same settings.


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
  

hearth  
bonfire  
fire_pit  


- Custom modded items can be added as well..



### Installation: (manual)  
Put on client and server.  Server will push down the settings and lock the config.

Extract DLL from zip file into "<GameDirectory>\Bepinex\plugins"  
Start the game.

### Version Information
1.0.2
- Server Sync and version check - Courtesty of Azumatt's amazing youtube tutorial

1.0.1
- Updated to newest Beta Valheim Version (Mistlands)
- Fixed incorrect Nexus ID and placed correct ID of 2027

1.0.0

- initial release


