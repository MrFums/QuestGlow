# Quest Glow

See which quests matter, right in your quest list. A glowing light runs round the edge of the quest:

- 🟡 **Gold**: needed for Kappa
- 🔵 **Blue**: needed for Lightkeeper
- 🔴 **Red**: lock-out. Finish its rival quest and this one fails for good

A quest that is more than one gets more than one light.

![Trader tasks](imageExamples/trader-tasks.gif)

![Character tasks](imageExamples/character-tasks.gif)

Shows on the trader screen and on Character > Tasks, in raid too. It only draws a marker, nothing about your quests changes.

The list is worked out by a small server part from the server's own quest data, so it follows mods that add or change quests. Without the server part it falls back to a built-in list for SPT 4.1.

## Install
SPT 4.1.x. Extract the zip into your SPT folder. It puts the client part in `BepInEx\plugins\QuestGlow` and the server part in `SPT_Runtime\user\mods\QuestGlow`. Fika compatible, but don't put the client part on a headless client.

To remove it, delete both folders.

## Settings
Press **F12** in game > Fums-QuestGlow. Turn each colour on or off, pick your own colours, and change the speed, length, thickness and glow. Changes show straight away.

## Reporting issues
Please report issues in my Discord thread on the SPT server [here](https://discord.com/channels/875684761291599922/1562201063535415536), in this mods issue section, or on [GitHub issues](https://github.com/MrFums/QuestGlow/issues).

Claude helped with the code review, checking the quest list against SPT's data, and the docs.
