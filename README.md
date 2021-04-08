# WorldPredownload ![Downloads](https://img.shields.io/github/downloads/gompocp/WorldPredownload/total?color=darkgreen) ![Current Version](https://img.shields.io/github/v/release/gompocp/WorldPredownload?color=blue)<br>
- Credit to [Psychloor](https://github.com/Psychloor/AdvancedInvites/blob/master/AdvancedInvites/InviteHandler.cs) for method to convert worldID to apiWorld instance + much much more, and some of the way stuff is structured is also inspired by his utilities file
- Credit to [Ben](https://github.com/BenjaminZehowlt/DynamicBonesSafety) for xref scanning mechanism and hud icon idea
- Credit to [Knah](https://github.com/knah/) for [Enable Listener](https://github.com/knah/VRCMods/blob/master/UIExpansionKit/Components/EnableDisableListener.cs) and the [hud icon method](https://github.com/knah/VRCMods/blob/master/JoinNotifier/JoinNotifierMod.cs#L120) that I changed to use here along with the [assetbundle loading mechanism](https://github.com/knah/VRCMods/blob/master/JoinNotifier/JoinNotifierMod.cs#L61)
- fwenny for helping with some testing 
- You can hit preload on an invite, on a world page or on a friend user page
- You can see your download status bottom right of quick menu 
- Currently you can only download/preload one world at a time 
- If you go to another world while downloading, it'll cancel the download 
- Performance may degrade for split second on world enter due to you having a large cache size 

# v1.4.1 Changes
- Removed Components (VRChat cloned one of them 100 times 👀). This should make the mod just that little bit more optimized.
- Fixed Dismiss Popup XRef failing
- Added a new setting to hide "Not Downloading" text when not downloading
- Cache will only be fetched once now (on start up) (again, it makes the mod just that little bit more optimized) It'll add other worlds you download while playing through the use of magic.
- Future-proofed some stuff (for ML v0.3.1)
- Fixed "Go to world page" button on popup throwing an exception

# v1.3.6 Changes 
- Added back invite predownloading 👌

# v1.3.5 Changes
- Mod now has a hud icon you can enable or disable using uix or edit the melon prefs file

# v1.3.4 Changes 
- Mod will no longer "eat" your join/friend requests
- Mod should hopefully update text more consistently
- Slightly better performance here and there
- Yeeted the override settings because it was hurting my head 

# v1.3.2.1 Changes
- Popup will no longer show when you have a worldInfo page or userinfo open in the menu as you can see download status there 
- Mod will now check to see if the world has been downloaded previously except on invite notification menu 


