# Colour-Switch-Project
Colour Switch

No known bugs

- The Player is represented by a white ball sprite with a PlayerController script attached.

- The Player has Sprite Renderer, Rigidbody 2D and Circle Collider 2D components attached also.

- The Player is constrained on the X and Z axis via the Rigidbody 2D component so that the The Player can only move on the Y axis.

- To move the Player, I created a method called MovePlayer() (called in Update()) which checks for left mouse button clicks and if there are then the Player Rigidbody 2D velocity is multiplied by an upwardsForce of 7.5f on the Y access.

- The Player is affected by gravity by setting the Gravity Scale in the Player Rigidbody 2D to 2.5f.

- 7.5f for the upwardsForce and 2.5f seemed to me to be a good balance for allowing the player to control the Player effectively, predictably, and fairly.

- The Main Camera has a script attached called called Camera Movement containing a variable for playerTransform which, in Update(), checks if the playerTransform.position.y is greater than the MainCamera.transform.position.y and if it is, sets a new Main Camera transform to the playerTransform meaning the camera, essentially, follows the Player location upwards yet not downwards.  Seems to be an effective way to handle upwards camera movement.

- In the PlayerController script, there is a method called PlayerFall() (called in Update()) which checks the position of the Player against the position of the MainCamera and if the falls below the visible are shown by the camera, the Player is destroyed.

- On Start(), the Player is assigned one colour from a possible of four (Magenta, Blue, Green or Red) by calling the GiveRandomColour() method from the GameManager.  The GiveRandomColour() method gets a random number (0, 4) which is used in a Switch statement of 4 cases. Each cases is set to one of the colours set in the Editor with corresponding tags.  Magenta = "Magenta" etc so in case 0 a random number of 0 has been rolled so the playerColour is set to "Magenta" which then sets the Sprite Renderer of the PlayerController on the Player to the Magenta colour and so on for the other colours.  There is also a check to make sure the previousPlayerColour is not the same as the new playerColour and if it is then the GiveRandomColour() is called again meaning that you cannot get 2 of the same colours in a row.  At first I used several if else statements to solve the colour issue then, in a search of documentation and message boards, I found references to Switch Statements which are easier to read than using many if else statements.  In addition, I had never used a Switch Statement before so I wanted to try something new.

- The obstacles, I created from the supplied sprites adding a Polygon Collider 2D component to each, assigning the corresponding individual tag, arranging them in circles, blades, squares then adding those sprites into an empty game object with a Rotate Object script attached. I created the obstacles to be of similar size to make the spawning of the obstacles easier to manage in terms of distance.

- In the PlayerController script, within the OnTriggerEnter2D() method there are checks within an if else statement that checks for the playerColour tag matching the obstacle tag.  If the colours/tags matched, then the Player could pass through the obstacle.  If not, then the Player cannot pass through the obstacle and so is destroyed.  Checking through tags seemed like a very simple way to solve the colour issue.

- Star Pickups were created from the supplied sprites. I added a Polygon Collider 2D component and a tag of "StarPickup".  I added the Star Pickup to be in a prefab with other Obstacles so that in the middle of every Obstacle there would be a Star Pickup.  I did it this way to make it easier in spawning of the Stars and Obstacles.  In the PlayerController script, with the OnTriggerEnter2D() method, there is a check that if the Player collides with a "StarPickup" then the Star Pickup is destroyed, a collection effect is instantiated, an audio plays from the AudioManager, +1 score is added to the UI and new Obstacle is spawned at a set distance from the Player using the Spawn Manager.

- The Colour Switches were created from the supplied sprites. I added a Polygon Collider 2D component and a tag of "ColourSwitch".  In the PlayerController script, with the OnTriggerEnter2D() method, there is a check that if the Player collides with a "ColourSwitch" then the Colour Switch is destroyed, a new colour is assigned by calling the GiveRandomColour() method from the GameManager, and a new Colour Switch is instantiated at a set distance from the Player using the SpawnManager.  This was done since it seemed to be a very straightforward way to solve the problem.

- A simple UI was created to display the score and the Game Over screen.  Whenever the Player collided with a Star Pickup, a score of 1 would be passed through the AddScore() method on the GameManager which would then update the score on the UI using the UIManager.  Whenever the Player was destroyed, I called a GameOver() method from the GameManager which set the Game Over state to be true and called the GameOverCoroutine which waited 2.5 seconds, allowing the player to see the death effect and realise they failed, to show the gameOverPanel which shows the score followed by a further 0.75 second wait, then prompted the player to click on the screen to restart the game.  I added a delay to being able to restart the game so that the click cannot be pressed rapidly or by mistake by the player.  

- For spawning the obstacles, I created an ObstacleSpawn() method in SpawnManager which randomised a number (0, 11) which corresponded to the number of obstacles in the obstacle[] array set up in the Editor then used that random number to Instantiate the corresponding obstacle in the game at a set distance from the Player.  Using an array in this situation seemed appropriate and efficient to use.

- I used Singletons in my solution since there would only be one instance of them in this small game and it also allowed me to break up the code/ spread it out over several scripts instead of having all the code, say, in PlayerController.

- I think that the code is functional and works as intended with no apparent bugs thereby meeting the requirements set out in the challenge.  I feel the code style is appropriate and understandable/ readable where the comments are informative allowing others to continue work.  However, I am always looking to improve and so would appreciate any feedback or improvement suggestions.

Tell us what features would you like to add to the gameplay and how would you go about and implement them.

I have added some simple features to the game which I feel add to the gameplay.

- At the start of the game I set the Gravity Scale to 0f which allows the player to enter the game without immediately having to click.  Upon clicking, the Gravity Scale is set back to 2.5.  I feel this is a simple addition which prevents player frustration from potential immediate death. I also added a simple diamond 2D game object to show the starting position for the player.

- I added level boundaries on the left and right side of the level with a Box Collider 2D component added. I then changed the PlayerDeath particle system collisions to interact with the world which allows the spawned particles to interact with the edges of the level, more in keeping with the actual Color Switch game.  I think this adds a small but fun element to the gameplay which enhances the death of the Player.

- I added a high score UI element on the Game Over screen by setting an integer in PlayerPrefs if the high score was greater than the current score.  I think this adds re-playability to the game through additional challenge.

- I added a simple DestroyOnInvisible script to the obstacle prefabs which checks if the obstacles are out of view of the camera, based on position, and if they are those obstacles are destroyed.  This does not add anything to gameplay but might be good practice for cleaning out the hierarchy.

- I added a simple animation to the Star Pickup which animates the scale of the object to simulate the object getting bigger and smaller. I feel this adds a dynamic layer to the visual aspect of the gameplay and suggests to the player that the Star Pickup is something to interact with.

- The main addition I would make to this version of the game to improve gameplay would be to remove the random spawning of obstacles and generate the level manually. I feel this would allow for more varied obstacles, more predictable difficulty that would increase over time, and overall, more fun.  There could be levels for the game and thereby a "win" scenario when the Player beats all the obstacles.  Additional UI elements could be made such as "Congratulations" etc.

- I would add background music by having the file as an object in the hierarchy in the AudioManager which would Play On Awake and even loop or have multiple background music tracks that would change depending on difficulty, score or time played. I think this adds more atmosphere to the gameplay and perhaps a sense of urgency depending on the use.  In addition, I could add more sound effects, for example, to the Colour Switch collision for added immersion.

- Different Colour Switches could be implemented which would assign only 2 or 3 colours depending on what obstacles are present.  I would implement this through having different Colour Switches having different tags such as "ColourSwitchNoMagenta" which then triggers a different colour assignment method specific to the needed colours.  Creating levels manually would be the easiest way to implement these new Colour Switches and obstacles.