# GDIM33 Vertical Slice
## Milestone 1 Devlog
1. One of the main Visual Scripting Graphs I created is the player movement graph, which helps to control the player character. It is starting with the On Input System Event Vector 2 node, which can read the keyboard input for the movement, so that when the player presses either A/D or the right/left arrow keys, the graph will then receive that input as a Vector 2 value. After that, the graph will use the Get X node to take only the horizontal part of that input, because only the left and right movements are needed for my game. Then it will multiply that X value by the player’s moveSpeed variable, and also multiply the result by delta time so that the movement can stay smooth and frame-rate independent. Next, the graph will put that value into a Vector3 node as an X value, and send that value into the Transform Translate node, which is the one that can actually move the player across the level. So, in conclusion, the graph I made will read the keyboard input, turn it into a horizontal movement, and then apply that movement to the actual player character. And then the player will use the movement in order to reach any platforms, collect the power-up, and also use it to avoid any hazards later. 

2.
![New update break-down](https://github.com/user-attachments/assets/55656017-8f0d-480b-a9e0-94875035a928)

For the new breakdown I attached here, it has much more detail for every object than the old one. For my older version, it is too general, like the player is treated like one large bubble, which makes it hard to show what each part of the game was actually doing, but for the new version, I separated the player into Player Controller, Ground Check, Player State Machine, and also the Light Burst. And for the other objects, I just put them in more detail. These changes will show much more clearly the connections, for example, the Player Controller is connected to the Ground/Platforms, because the player will stand and move on them, and it is also connected to the Shrine Power-Up because the player will collide with it and pick it up. 

The main update is my Player State Machine, which has two states: one for NoPower and one for Powered. For every start of the game, the player will be in the state of NoPower, which means that the player can move and jump, but cannot use the ability yet. After the player collides with the Power-Up, the game then sets hasPower = true, and this will cause the state machine to change to the Powered state. And for this state, the player will be allowed to use the new ability, and the visual look of the character will also change in order to show that the power-up has been collected. I made this state machine part to connect with the Shrine Power-Up, because the shrine is what causes the transition, and I also connected it to UI/Feedback, because the game later will use that state to show the player that they are now powered up. 

It is also related to the other systems because it will control whether the Light Burst system can be used or not. And the Light Burst then is connected to the Dark Barrier and the Final Enemy, which means that the player's state is not just a visual change, but instead, it can actually change what gameplay actions are possible. Before the power-up, the barrier will stays closed and the enemy cannot be defeated with any ability, and after the state changes to Powered, the player then can use the light burst in order to unlock the barrier and doing damage or deafeat the enemy. And the Final Enemy is also connected to the Exit Door/Goal, which because the exit will open after it is defeated.


## Milestone 2 Devlog
Milestone 2 Devlog goes here.

## Milestone 3 Devlog
Milestone 3 Devlog goes here.

## Milestone 4 Devlog
Milestone 4 Devlog goes here.

## Final Devlog
Final Devlog goes here.

## Open-source assets
[Pixel Adventure by Pixel Frog](https://pixelfrog-assets.itch.io/pixel-adventure-1)

[Mossy Cavern by Maaot](https://maaot.itch.io/mossy-cavern)

[The Island: Parallax Ready 2D Background for Platformer](https://saurabhkgp.itch.io/the-island-parallax-background-platformer-side-scroller)

[Pixel Art Asset Pack - Sidescroller Fantasy - 16x16 Forest Sprites](https://anokolisa.itch.io/sidescroller-pixelart-sprites-asset-pack-forest-16x16)

[2D medieval castle dungeon](https://thepixelistcreator.itch.io/2d-medieval-castle-dungeon-tileset)

[ui-user-interface-medieval](https://toffeecraft.itch.io/ui-user-interface-medieval)

[SİMPLE GAME UI PACK](https://enogcestudio.itch.io/ui-pack-pxel-default-bold-vs)

[winterflowers-font](https://nest.itch.io/winterflowers-font)

[Heart pixelart asset](https://itch.io/search?q=Heart+asset)
