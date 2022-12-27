
# No Smoke Stay Lit Now with Timed Torches

### Now with Ovens, Smelters, Braizers and Hottubs and Timed Torches

Keep your flames lit, items fueled and torches on a Timer.  This mod combines a few of my other mods
and creates a super simple config that allows anyone to have the features they need.

- Select the items to be fueled
- Complete customization of Torches, Smelters, HotTubs, Ovens
- Keep some torches on timers and other on all the time.
- Stop the smoke and stack those smelters
- ServerSync to push configurations to all users - Ability to turn off

### About the Mod:
I had issues keeping several mods updated with overlapping features.  I combined them into this one and
created a super simple config so everyone can easily chose the features they need for their type of gameplay.


``Turn version-check off in the config if having troubles or it is not needed for your gameplay.``

### Credits:

- Aedenthorn because without her code repository, I would have never understood how to do any of this.
- Azumatt for help with the braziers
- Red Seiko (Comfy) for guidance on the configuration file
- Marfinator for allowing me to use some code from his awesome mod "Fuel Eternal"

### Configuration:
![Settings Image](https://github.com/TastyChickenLegs/NoSmokeStayLit/blob/master/NewSettings.png)

Completely redone with everything selectable and even the ability to add custom items.


The config file is located in "<GameDirectory>\Bepinex\config" (You need to start the game at least once, with the mod installed to create the config file).

<b>To view or add items this mod affects:  </b>

See the images for a list of configurable items.


### List of supported objects:

|Config Option|Definition
|---|---|
|fe_fire_pit | Allow eternal fuel for Campfire
|fe_bonfire  | Allow eternal fuel for Bonfire
|fe_hearth    | Allow eternal fuel for Hearth
|fe_piece_walltorch | Allow eternal fuel for Sconce
|fe_piece_groundtorch | Allow eternal fuel for Standing iron torch
|fe_piece_groundtorch_wood | Allow eternal fuel for Standing wood torch
|fe_piece_groundtorch_green | Allow eternal fuel for Standing green-burning iron torch
|fe_piece_groundtorch_blue | Allow eternal fuel for Standing blue-burning iron torch
|fe_piece_brazierfloor01 | Allow eternal fuel for Standing brazier
|fe_piece_brazierceiling01 | Allow eternal fuel for Hanging brazier
|fe_piece_jackoturnip | Allow eternal fuel for Jack-o-turnip
|fe_piece_oven | Allow eternal fuel for Stone oven
|fe_piece_bathtub | Allow eternal fuel for Hot tub
|fe_smelter |Allow eternal fuel for Smelter
|fe_blastfurnace | Allow eternal fuel for Blast furnace
|fe_eitrrefinery | Allow eternal fuel for Eitr refinery
|fe_custom_instance | Allow to manage fuel for custom items added by other mods 
|fe_piece_walltorch_timer | Allow timer fuel for Sconce
|fe_piece_groundtorch_timer | Allow timer for Standing iron torch
|fe_piece_groundtorch_wood_timer | Allow timer for Standing wood torch
|fe_piece_groundtorch_green_timer |Allow timer for Standing green-burning iron torch
|fe_piece_groundtorch_blue_timer | Allow timer for Standing blue-burning iron torch
|fe_piece_brazierfloor01_timer | Allow timer for Standing brazier
|fe_piece_brazierceiling01_timer | Allow timer for Hanging brazier
|fe_piece_jackoturnip_timer | Allow timer for Jack-o-turnip
|fe_custom_instance_timer | Enable Timers for items added by other mods
|keepOnInRain | Keep fires lit even when raining and wet
|configOnTimeOne | Convert desired time to military time (24hr) and /24.  Example 6:30am is 6.30 hours /24 = .67
|configOffTimeOne | Convert desired time to military time (24hr) and /24.  Example 8:30pm is 16.30 hours /24 = .67
|configAlwaysOnInDarkBiomes | Always On In Dark Biomes or storming

<b>all fire sources can be added</b>
- Custom modded items can be added as well..



### Installation: (manual)  
Put on client and server.  Server will push down the settings and lock the config.

Extract DLL from zip file into "<GameDirectory>\Bepinex\plugins"  
Start the game.

### Version Information

2.0.0 Beta

- Complete rewrite of Mod to include Timed Torches and several other features
- Look at the Images for information regarding Config Options

1.1.6

- fixed bug in torches. They now stay lit, dry and fueled accordingly.


1.1.5

- Fixed errors in 1.1.4 resulting in object not set to an instance.  


1.1.4

- Add ability to turn off client checking for ServerSync.  If you are having problems with ServerSync turn this off

- Figured out how to turn smoke off for Braziers.  Thank you Azumatt for the help

- Removed smoke from Smelters so they can now stack on top of each other

1.13

- Fixed the packaged files. 

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

