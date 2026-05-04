# My Time at Sandrock - Infinite Health And Stamina Mod

Infinite health and stamina (basically) with no extra setup!

This mod is heavily inspired by `aedenthorn` and their `Regeneration` mod: 
- Regeneration: https://www.nexusmods.com/mytimeatsandrock/mods/8
- Other mods by `aedenthorn`: https://github.com/aedenthorn/MTASMods

So i would just like to say (if `aedenthorn` would ever see this mod) thank you for creating your mod, it made it possible for me to get into modding and make this mod for my partner so she could enjoy the game without stress ❤️

## How it works
Basically, the mod hooks into the regen-system of the player, and makes a calculation everytime you take damage or spend stamina:
- get the players current Hp/Sp
- get the players max Hp/Sp
- if current is less than max:
  - calculate the difference
  - apply difference to the attribute and UI pools.

So in essence, infinite health and stamina! (Unless you would get one-shot 😅)

## Installation
- Make sure you have [BepInEx](https://github.com/BepInEx/BepInEx/releases) installed in your game folder (Where Sandrock.exe is located).
  - It should work with any version of BepInEx, but if you are having problems, I used version [5.4.22.0](https://github.com/BepInEx/BepInEx/releases/tag/v5.4.22) when making the mod.
- Download the `InfiniteHealthAndStamina.dll` file from the [latest release](https://github.com/mackans95/InfiniteHealthAndStamina/releases) and copy it into the `BepInEx/plugins/` folder inside your game folder.
- Launch the game. You should now regenerate health and stamina instantly!
  - Make sure the mod is enabled in the config file if it's not working (It should also show up in any BepInEx config manager)


## Configuration (file gets created at `BepInEx/config/MarGus.InfiniteHealthAndStamina.cfg` after launching the game once)
- `Enabled` (boolean (true/false)):
  - Enables the mod
  - Default: `true`
- `RegenerateHealth` (boolean (true/false)):
  - Turns only health regen on/off
  - Default: `true`
- `RegenerateStamina` (boolean (true/false)):
  - Turns only stamina regen on/off
  - Default: `true`
- `IsDebug` (boolean (true/false)):
  - Enables debug logging
  - Default: `false`

