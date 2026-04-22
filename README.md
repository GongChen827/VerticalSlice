# GDIM33 Vertical Slice
## Milestone 1 Devlog
1.One of the main Visual Scripting Graphs I created is the player movement graph, which helps to control the player character. It is starting with the On Input System Event Vector 2 node, which can read the keyboard input for the movement, so that when the player presses either A/D or the right/left arrow keys, the graph will then receive that input as a Vector 2 value. After that, the graph will use the Get X node to take only the horizontal part of that input, because only the left and right movements are needed for my game. Then it will multiply that X value by the player’s moveSpeed variable, and also multiply the result by delta time so that the movement can stay smooth and frame-rate independent. Next, the graph will put that value into a Vector3 node as an X value, and send that value into the Transform Translate node, which is the one that can actually move the player across the level. So, in conclusion, the graph I made will read the keyboard input, turn it into a horizontal movement, and then apply that movement to the actual player character. And then the player will use the movement in order to reach any platforms, collect the power-up, and also use it to avoid any hazards later. 

2.![New update break-down](https://github.com/user-attachments/assets/55656017-8f0d-480b-a9e0-94875035a928)



## Milestone 2 Devlog
Milestone 2 Devlog goes here.

## Milestone 3 Devlog
Milestone 3 Devlog goes here.

## Milestone 4 Devlog
Milestone 4 Devlog goes here.

## Final Devlog
Final Devlog goes here.

## Open-source assets
- Cite any external assets used here!
