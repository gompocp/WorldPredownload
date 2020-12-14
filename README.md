# WorldPredownload <br>
-Credit to [Psychloor](https://github.com/Psychloor/AdvancedInvites/blob/master/AdvancedInvites/InviteHandler.cs) for method to convert worldID to apiWorld instance + more, and some of the way stuff is structured is also inspired by his utilities file<br>
-Credit to [Ben](https://github.com/BenjaminZehowlt/) for xref scanning mechanism from https://github.com/BenjaminZehowlt/DynamicBonesSafety <br>
-Credit to [KeafyisHere](https://github.com/KeafyIsHere/Custom-MonoBehaviours-example) for custom monobehaviour example <br>
-You can hit preload on an invite, on a world page or on a friend <br>
-You can see your download status bottom right of quick menu <br>
-Currently you can only download/preload one world at a time <br>
-If you go to another world while downloading, it'll cancel the download <br>
-Performance may degrade for split second on world enter due to you having a large cache size <br>

# Todo <br>
-Possibly Add a 15 second timer on the preload option for invites to prevent api spam <br>

# Changes <br>
-Popup will no longer show when you have a worldInfo page or userinfo open in the menu as you can see download status there <br>
-Mod will now check to see if the world has been downloaded previously using asset hashing <br>

