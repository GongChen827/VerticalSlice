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

2. Yes, I actually do think that the task steps breakdown activity helps me a lot in building my Milestone 2, and it lets me make one thing at a time, so it feels less overwhelming. At first, I kept thinking that my complicating gameplay feature is like one big system, where the player uses the ability in order to remove barriers and defeat the final enemy. But after breaking it down into multiple smaller tasks, I could just focus on one thing, or one part at a time, such as making the dark barrier disappear, how the enemy moves, and allowing the enemy to damage the player on contact, and then also making the player able to damage and kill the enemy. All of these small tasks really helped me when testing each part separately, and checking everything independently instead of finishing the whole system at once, which has many bugs.

   Also, I think both the quiz question and the activity I did in week 5 helped me to think deeper and more like I was writing my own instructions for myself. I ended almost every step that needed a clear test, such as running the game once and checking what works and what does not work, or using Debug.Log. This is really useful for me because I had several problems while coding and testing, such as the barrier tilemap just goes away all at once, and some animation states are blocking the other animations. If I am doing the breakdown again, I think I can just make my steps even more specific. For example, I can mention to make sure that "each barrier should be its own object or have its own tilemap, so only the closest barrier disappears when hit." I think I can also add more testing steps for things like using the ability while jumping, hitting only the barrier, or checking whether the enemy can be damaged by the player's ability. And by making it more specific, I think the breakdown can be more helpful for debugging instead of just building the first version.

3. For my game, I chose to call a C# method from a Visual Scripting Graph, which I think is more suitable for my game. In my player ability graph, I use the Visual scripting side for handling the input and timing for the power-up ability, and the graph will also check whether the player has collected the shrine power-up and whether the ability cooldown is ready. When all of those checks pass, it will play the particle effect, then trigger the attack animation, and also call the C# method **ActivateLightBurstHitbox()** from my **LightBurstHitDetector.cs** script.

   I really think that this bridge helped me keep my game architecture much more organized. I can use Visual Scripting for the input, cooldown, particles, and also animation timing, which is really easier than doing in code, and I can see the logic flow clearly. On the other hand, C# is cleaner for the hit detection and the object interaction, for LightBurstHitDetector.cs I added, the method can check a circle-shaped range around the player, and if it finds a DarkBarrier with **DarkBarrier.cs** added, then it will call OpenBarrier() so that the barriers can disappear (inactive). And if it finds a FinalEnemy that has **FinalEnemy.cs** added, then it will call TakeLightDamage(), which can damage the enemy and make it flash red, and finally die after enough hits. 

![My Player Ability Graph](https://github.com/user-attachments/assets/2038453b-d0ab-4c20-8708-bdaab81246f6)

4. I finished both my Tilemap and Animator systems for this milestone. And I use the Tilemap system for the platforms, hazards, and also the dark barriers, while the Animator system is used on the Player and FinalEnemy's movement, attack, hit, and also death animations. 

## Milestone 3 Devlog
1. For this Milestone, I mainly created two Shader Graph effects, which are used for my player's light ability. For the first one, it is named SG_LightBurstGlow and used on the LightBurstShaderVisual object, which can be found under the Player (SG_LightBurstGlow can also be found in my project assets, which is Assets - Shaders - ShaderGraphs). This shader will create a kind of light-blue transparent glow around the player when the ability is used by pressing the E key, which I intentionally want it to look like the player is collecting the energy before releasing the attack.

   For my first Shader Graph, SG_LightBurstGlow is using the UV nodes in order to get the 2D texture coordinates of the sprite, and I subtract (0.5, 0.5) from the UV coordinates so that the graph can measure the distance outward from the center. Then, I use a Length node for turning that offset into a distance value, which basically tells the shader how far each pixel is from the center of the sprite. And after that, I use the One Minus so that the center can become brighter and the outside will become weaker. I also use the Saturate node to clamp the value between 0 and 1, so the shader will not create any strange values outside the normal color/alpha range. With all of these, I create a kind of circular glow mask, and I multiply that mask by GlowColor for the Base Color, which makes the glow light blue,  then I also multiply the same mask by EffectAlpha for the Alpha, so that my C# script will be able to fade the effect in and out when the ability is activated by the player.

   My second shader is SG_SwordQiArc, which can also be found in the same asset folder as the first one. It is used by Mat_SwordQiArc on the SwordQiSlashVisual object under the Player, and this is the main light slash effect that will appear when the player presses E after collecting the power-up. For this graph, I also used the UV node, but instead of making the circular glow like the first one, I use the UV math in order to make a crescent shape. It basically is creating two soft circular masks, and for each one, I use the Subtract node for moving the circle center, Length for measuring the distance from that center, and also Smoothstep to make the edge soft instead of sharp. Then, I use the One Minus node in order to turn the circular distance into a visible circle mask, and after that, I use another Subtract for subtracting the inner circle from the outer circle. This will create a kind of arc shape because the inner circle is cutting into the outer circle, and leave only a curved crescent-like slash. And I also use the Saturate for the graph after the subtraction for keeping the final mask between 0 and 1. After the arc mask is created, I multiply it by the SlashColor and connect it to the Base Color, which gives the slash a blue energy color. I also multiply the same mask with EffectAlpha and connect that result to the Alpha, so I can use a script to make the slash appear and fade away during the attack.

   After doing all of these for my second graph, I set the shader to Transparent and Additive blending, because it makes the effect look brighter and more like a kind of glowing energy. I also set Render Face to Both so that when the player is facing left, the slash will flip, and that will make sure the slashes will appear from both directions. This effect will be activated from my player ability Visual Scripting Graph by calling PlaySwordQiSlash() from SwordQiSlashVFX.cs. 

Screenshot for both graphs: 
![SG_LightBurstGlow](https://github.com/user-attachments/assets/c3cd40e1-6d7a-48b5-b928-91a214766be1)

![SG_SwordQiArc](https://github.com/user-attachments/assets/8277e29a-1e5b-40f5-9c2d-9cbbfe96bf47)

2. After the last Milestone, I changed my final enemy fight again based on the playtest feedback, because the players said it was too easy for them to defeat the final enemy, and it ended too quickly and felt boring. So I adjusted the final enemy's health and its movement speed, so that the player has to land more light burst hits in order to defeat it. I also added more danger to the final arena, which includes the hazards and some extra platforms, so instead of standing still there on the platforms and keeping attacking the final enemy, I want the player to have to dodge the hazards, move around the arena, and wait for the ability cooldown. And after testing it, I think now the final fight feels much harder than before, but also feels balanced enough to be completed, even for someone new to the game. And the last thing I did was to take out some of the spikes in the level based on the Milestone 2 feedback in order to decrease the difficulty of the intro and middle part of the game, so that the player can have a higher chance of getting to the final part and finishing the game at once. 

3. Since Milestone 2, I added more content before the final fight with the final enemy, so that the main gameplay loop now feels more complete and fun. First, I added some more of the dark barriers, then I also added two smaller shadow enemies that can be defeated after landing two ability attacks. Now, the player has to use the power-up ability multiple times across the whole level, instead of just using it one to two times, and I think this will give the player more practice with the power-up before the final fight and make the level feel longer and more complete. I also added a final arena trigger (used for the health bar, and can also be used for later second sound track) and a large final enemy health bar at the top of the screen, so when the player enters the final arena (triggers the final arena trigger), the boss health bar appears as full red, and will lose red sections when the enemy takes damage from the player, and will disappear after the final enemy is defeated. 

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

[Health Bar](https://wolf-viciox.itch.io/health-bar)
