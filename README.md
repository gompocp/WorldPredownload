# WorldPredownload ![Downloads](https://img.shields.io/github/downloads/gompocp/WorldPredownload/total?color=darkgreen) ![Current Version](https://img.shields.io/github/v/release/gompocp/WorldPredownload?color=blue)<br>

-Credit to [Psychloor](https://github.com/Psychloor/AdvancedInvites/blob/master/AdvancedInvites/InviteHandler.cs) for method to convert worldID to apiWorld instance + much much more, and some of the way stuff is structured is also inspired by his utilities file<br>
-Credit to [Ben](https://github.com/BenjaminZehowlt/DynamicBonesSafety) for xref scanning mechanism and hud icon idea<br>
-Credit to [Knah](https://github.com/knah/) for [enable disable listener](https://github.com/knah/VRCMods/blob/master/UIExpansionKit/Components/EnableDisableListener.cs) and the [hud icon method](https://github.com/knah/VRCMods/blob/master/JoinNotifier/JoinNotifierMod.cs#L120) that I changed to use here along with the [assetbundle loading mechanism](https://github.com/knah/VRCMods/blob/master/JoinNotifier/JoinNotifierMod.cs#L61)  <br>
-fwenny for helping with some testing <br>
-You can hit predownload on a world page or on a friend <br>
-You can see your download status bottom right of quick menu <br>
-Currently you can only download/predownload one world at a time <br>
-If you go to another world while downloading, it'll cancel the download <br>
-Performance may degrade for split second on world enter due to you having a large cache size <br>

# v1.3.5.6 Changes <br>
-Invite predownloading has been removed for now including mod settings related to it till I figure something out<br>
-Messages on hud should clear now and not stay anymore<br>
-Fixed DownloadStatus failing to clone a gameobject<br>

# v1.3.5 Changes <br>
-Mod now has a hud icon you can enable or disable using uix or edit the melon prefs file<br>

# v1.3.4 Changes <br>
-Mod will no longer "eat" your join/friend requests<br>
-Mod should hopefully update text more consistently<br>
-Slightly better performance here and there<br>
-Yeeted the override settings because it was hurting my head <br>
<img src="https://cdn.discordapp.com/attachments/769569340856991754/796786175510708234/animethumbsdown7.jpg"/> <br>

# v1.3.2.1 Changes <br>
-Popup will no longer show when you have a worldInfo page or userinfo open in the menu as you can see download status there <br>
-Mod will now check to see if the world has been downloaded previously except on invite notification menu <br><br>


