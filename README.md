
# Multiplayer Box Clicking Game

## Overview
This repository contains documentation of the project task assigned as part of the internship selection process by Vyorius.

## Table of contents
* ### Introduction
* ### Packages
* ### Implementation
* ### Testing

## Introduction
In this project I have created a Multiplayer game in which player have to click on boxes as fast as they can and the first player to achieve 8 points will win the game. This project is my submission towards the task given by Vyorius regarding the selection process of internship.


## Packages
### Required packages for project are
* Netcode for GameObjects: It is the Unity's solution for the Multiplayer Implementation.
* Multiplayer Tools: It provides some extra functionalities for the Unity's Netcode. 
* TextMeshPro Essentials: This is essential package for working with the text in the UI.

## Implementation
### Created a blank Universal-3D project from unity hub.
<img width="720" src="/Screenshots/Screenshot (152).png">

### Arena and Prefabs
* Created a simple arena with walls and a base.
<img width="720" src="/Screenshots/Screenshot (153).png">

* Created a simple prefab for the box.
* Added the NetworkObject component to the box prefab. NetworkObject is for syncing the spawning and despawning of the boxes.
<img width="720" src="/Screenshots/Screenshot (162).png">

### UI elements required for game
* Added the button for hosting and for joining as client.
* Added the Input field for username.
* Added start button to start game.
* And all other text which is required.
<img width="720" src="/Screenshots/Screenshot (154).png">

### Network Manager
* Created an empty gameobject named Network Manager and then added NetworkManager and UnityTransport components from the add component section of the inspector.
* Added box prefab in the Default Network Prefabs list.
<img width="720" src="/Screenshots/Screenshot (156).png">

### Network UI Manager
* Created an empty gameobject named NetworkUI Manager and then added NetworkObject component because it requires the functionality of NetworkVariable for synchronizing players cnt.
* Created NetworkUIManager script and added the behaviour for the host and client button in Awake method. Then added the code required for updating players count in update method.
<img width="720" src="/Screenshots/Screenshot (157).png">

### Player prefab
* Created prefab for the player and then assigned this prefab in the NetworkManager's Player Prefab field.
* Now again added the player prefab in the Default Network Prefabs Lists. As it is necessary to tell NetworkManager about the prefab which are going to be spawned in game and have to be synchronised upon the network.
* Now added Network Object component to the player prefab. Network Object component synchronises the gameobject among the all instances of game connected to the host.
* Now created the PlayerController script and defined the functionalities required by the player in the script.
<img width="720" src="/Screenshots/Screenshot (161).png">

### Game Manager
* Created an empty gameobject named Game Manager and then added NetworkObject component because it requires the functionality of NetworkVariable.
* Created GameManager script and then defined all required methods and code for the game management and also implemented the spawning of the boxes.
* Implemented the game winning dialog box.
* Synchronised the game starting and the box spawning with the help of NetworkVariables and ServerRpc.
<img width="720" src="/Screenshots/Screenshot (159).png">

## Testing
* ### Ran 3 instances of my game to verify the synchronisation
<img width="720" src="/Screenshots/Screenshot (163).png">

* ### Checked the connection between host and the clients.
<img width="720" src="/Screenshots/Screenshot (164).png">

* ### Checked Box spawning and despawing.
<img width="720" src="/Screenshots/Screenshot (165).png">

* ### Checked winning mechanism.
<img width="720" src="/Screenshots/Screenshot (166).png">
