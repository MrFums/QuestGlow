# Changelog

## 1.0.0

First release.

- Gold light round quests needed for Kappa, blue round quests needed for Lightkeeper, red round lock-out quests (complete one and its rival fails for good). A quest that is more than one gets each light, up to three, chasing each other round the box.
- Shows on the trader screen's task list and on Character > Tasks (in raid too).
- The light is a thin comet: bright head, a tail that thins and fades, and a soft glow outside the box only, so it never covers the quest name.
- Each row runs a little behind the one above, so the list ripples instead of moving in lockstep.
- Everything can be changed in the F12 menu: which quests are marked, what to show for quests needed for both, colours, speed, length, thickness, glow and a faint resting outline.
- Up to three lights fit round a box with no overlap, and boxes that reach the edge of a list (Character > Tasks) keep their outline in view.
- A small server part works the list out from the server's quest data: everything leading to the Collector (Kappa) or Knock-Knock (Lightkeeper), including rival quests that unlock the same next step, plus both sides of every quest that fails when another is completed. Quests added or changed by other mods are followed.
- Without the server part the client falls back to a built-in list of 288 quests, checked against SPT 4.1's quest data.
