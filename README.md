# MonkeToggler
MonkeToggler is a simple [BepInEx](https://github.com/BepInEx/BepInEx) mod toggler made for [Gorilla Tag](https://store.steampowered.com/app/1533390/Gorilla_Tag/).

[![Github All Releases](https://img.shields.io/github/downloads/pl2w/MonkeToggler/total.svg)]()

![Gorilla_Tag_6alGsnPxt8](https://github.com/pl2w/MonkeToggler/assets/137610832/6f431579-48f5-403b-a0e9-e069626c84f1)

## Installation
To install the mod, make sure you have BepInEx installed. You can install it through [MonkeModManager](https://github.com/DeadlyKitten/MonkeModManager) or manually install it.
Once you have BepInEx installed, drag and drop the 'MonkeToggler.dll' file into your BepInEx plugins folder (Gorilla Tag\BepInEx\plugins).

![installationTutorial](https://github.com/pl2w/MonkeToggler/assets/137610832/587be3a0-79d3-4875-abdd-9e872abc71c6)

## Making your mod compatible
To make your mod compatible with MonkeToggler, you must add ```OnEnable()``` and ```OnDisable()``` methods into your BaseUnityPlugin file.
```cs
using BepInEx;

namespace ExampleMod
{
    [BepInPlugin("pl2w.examplemod", "ExampleMod", "1.0.0")]
    public class Plugin : BaseUnityPlugin
    {
        void OnEnable()
        {
             Debug.Log("This gets called when your mod gets enabled, or when the mod is first initialized.");
        }

        void OnDisable()
        {
            Debug.Log("This gets called when your mod gets disabled.");
        }
    }
}

```


## Disclaimer

This product is not affiliated with Gorilla Tag or Another Axiom LLC and is not endorsed or otherwise sponsored by Another Axiom LLC. Portions of the materials contained herein are property of Another Axiom LLC. © 2021 Another Axiom LLC.
