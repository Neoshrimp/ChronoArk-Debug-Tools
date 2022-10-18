Properly set-up CA plugin project targeting .net framework 3.5

Change properties *GameFolder* and *BepInExFolder* in .csproj file to target correct folders.<br>
Change post-build command to copy to *plugins* folder instead of *scripts* if ScriptEngine is not used.

Might have to add *https://nuget.bepinex.dev/v3/index.json* source to nuget manager for packages to be installed correctly.


template zip can be copied to *[User]\Documents\Visual Studio 20xx\Templates\ProjectTemplates* for quick project creation in vs