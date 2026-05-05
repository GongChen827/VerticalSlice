# GDIM33 Vertical Slice
## Milestone 1 Devlog
1. One of the main Visual Scripting Graphs I created is the player movement graph, which helps to control the player character. It is starting with the On Input System Event Vector 2 node, which can read the keyboard input for the movement, so that when the player presses either A/D or the right/left arrow keys, the graph will then receive that input as a Vector 2 value. After that, the graph will use the Get X node to take only the horizontal part of that input, because only the left and right movements are needed for my game. Then it will multiply that X value by the player’s moveSpeed variable, and also multiply the result by delta time so that the movement can stay smooth and frame-rate independent. Next, the graph will put that value into a Vector3 node as an X value, and send that value into the Transform Translate node, which is the one that can actually move the player across the level. So, in conclusion, the graph I made will read the keyboard input, turn it into a horizontal movement, and then apply that movement to the actual player character. And then the player will use the movement in order to reach any platforms, collect the power-up, and also use it to avoid any hazards later. 

2.
![New update break-down](https://github.com/user-attachments/assets/55656017-8f0d-480b-a9e0-94875035a928)

For the new breakdown I attached here, it has much more detail for every object than the old one. For my older version, it is too general, like the player is treated like one large bubble, which makes it hard to show what each part of the game was actually doing, but for the new version, I separated the player into Player Controller, Ground Check, Player State Machine, and also the Light Burst. And for the other objects, I just put them in more detail. These changes will show much more clearly the connections, for example, the Player Controller is connected to the Ground/Platforms, because the player will stand and move on them, and it is also connected to the Shrine Power-Up because the player will collide with it and pick it up. 

The main update is my Player State Machine, which has two states: one for NoPower and one for Powered. For every start of the game, the player will be in the state of NoPower, which means that the player can move and jump, but cannot use the ability yet. After the player collides with the Power-Up, the game then sets hasPower = true, and this will cause the state machine to change to the Powered state. And for this state, the player will be allowed to use the new ability, and the visual look of the character will also change in order to show that the power-up has been collected. I made this state machine part to connect with the Shrine Power-Up, because the shrine is what causes the transition, and I also connected it to UI/Feedback, because the game later will use that state to show the player that they are now powered up. 

It is also related to the other systems because it will control whether the Light Burst system can be used or not. And the Light Burst then is connected to the Dark Barrier and the Final Enemy, which means that the player's state is not just a visual change, but instead, it can actually change what gameplay actions are possible. Before the power-up, the barrier will stays closed and the enemy cannot be defeated with any ability, and after the state changes to Powered, the player then can use the light burst in order to unlock the barrier and doing damage or deafeat the enemy. And the Final Enemy is also connected to the Exit Door/Goal, which because the exit will open after it is defeated.


## Milestone 2 Devlog
1. For this Milestone, I will do my best to build the complicating gameplay feature, which is that the player can use the power-up that they will get during the gameplay to remove dark barriers that show in the latter part of the level and defeat a final enemy at the end. So basically, after I am done with this milestone, the player can first collect the shrine power-up, then use the new ability in order to progress through some blocked path, and finally uses the that ability for defeating the final enemy.

   1. **Make the dark barrier respond to the light burst.**
      1. First, I will create a dark barrier object or tilemap section after the shrine area, so the player can clearly see that the path is blocked. And then I will run the game and confirm that the player cannot walk through the barrier.
      2. Next, I will create a script for the dark barrier that can remove the barrier. Then I will test to confirm that the barrier disappears.
      3. Then I will connect the player’s existing light burst ability graph to the C# method, so pressing E after collecting the power-up can remove the barrier, after doing that, I will try to collect the power-up in the game, and press E near the barrier, and test whether or not that the barrier disappears after using the ability.
         
   2. **Build the final enemy with movement and contact damage.**
      1. For the final enemy, I can begin by putting a final enemy in the last area of the level and giving it a collider.
      2. Next, I can add a simple enemy movement script, such as pacing left and right between two single points in the last area. And then I can see by playing the game and making sure that the enemy moves back and forth.
      3. Then I can make the enemy damage the player on contact by calling the player’s health script. And I will walk into the enemy and check that the player loses one heart.
      
   3. **Make the final enemy only take damage from the light burst and open the exit after defeat.**
      1. I can add health to the enemy, such as 3 hits per death, and then test it by using Debug Log to print the enemy’s current health whenever it is hit.
      2. And then I can make the light burst detect the enemy and call the enemy’s damage method. For playtesting, I will press E near the enemy and use my ability and check that the enemy loses health.
      3. Then I can add enemy hit feedback, for example, a red flash when the enemy is damaged and a death animation or disappearance when health reaches 0 just like the player's character. And I will hit the enemy until it dies and make sure that it is able to disappears.
      4. After all is done, I also want to add a short UI message that says something like "The final exit is opened!" and unlock the final exit. And for testing this, I can defeat the enemy in the game and try to see whether or not that message appears and the exit is no longer blocked. 

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

[Heart pixelart asset](https://rollinrock.itch.io/hearts-pixelart-assets)
