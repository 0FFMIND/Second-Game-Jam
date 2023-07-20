Transition blocks was made by Anthony Tesija of ATesh Games LLC
http://www.ATeshGames.com
Email: ATeshGames@gmail.com
Twitter: @AntonTesh

How to use Transition Blocks
	The most basic way to add transitions to your scenes is to drag any one prefab from "TransitionBlocks/Prefabs/Example Transitioners" into your first scene. Try dragging "WiggleDiamondTransitioner" into your scene. Then instead of calling "Application.LoadLevel("LevelName");" or "SceneManager.LoadScene("LevelName");" call "Transitioner.Instance.TransitionToScene("LevelName");" or "Transitioner.Instance.TransitionToScene(levelNumber);" in your scripts. You can use any of the premade transitioners in that folder or if you want to learn how to easily customize them for an unlimited number of transitions keep reading! 
	A demo of this can be found in "TransitionBlocks/Demo". Just load the scene "Transition Block Demo" and press "t" to transition. Take a look at the script "YourGameLogic" to see how it works.
	A video showing the basics of this asset can be found here: https://youtu.be/oTr5EGezHUc
	A video going more in depth into how to set up the parts of this asset can be found here: https://www.youtube.com/watch?v=u7En8GmIiQ8
	If you're also using PlayMaker you'll see two new actions under the "level" catagory called Transition Blocks. This video shows you how to get that working: https://www.youtube.com/watch?v=klNIZuQgVMg


Transitioner
	This is the main script for this package. You only need to have one of these in your scene to be able to use the entire package. It is a singleton so you can access it from any script by calling "Transitioner.Instance." and then one of the following functions:
		"TransitionToScene(string sceneName)" This function will play the transition you have specified in the Transitioner script before changing to the scene "sceneName"
		"TransitionToScene(int sceneNumber)" Same as the previous function but it transitions to "sceneNumber" instead
		"FinishTransition()" You only need to call this if you have "Automatically Transition In" set to false on the Transitioner. This lets you control when the transition in happens in the scene you have transitioned to.

		"TransitionOutWithoutChangingScene()" This function allows you to play the transition animation without changing scenes. Use "TransitionInWithoutChangingScene" to play the animation that removes the transition from the screen.
		"TransitionInWithoutChangingScene()" Call this after "TransitionOutWithoutChangingScene" to play the animation that removes the transition from the screen.

		"CanTransition()" This function tells you if the transitioner is ready to transition (no current transition is playing)

		There is also an event you can subscribe to for when the transition is complete called "OnTransitionComplete". To do this you call Transitioner.Instance.OnTransitionComplete += YourFunction; (make sure you unsubscribe from it when you're done too Transitioner.Instance.OnTransitionComplete -= YourFunction;)

	Here is a list of all the public variables on a Transitioner script and what they do:
		Width Of Transition In Blocks: The number of transition blocks that will appear horizontally on the screen. The number that appear vertically will automatically be calculated from your screen resolution.
		
		Transition Block Prefab: The transition block that will be used in this transition. A transition block is a prefab that has a "TransitionBlock" script on it. Transition blocks know how to animate themselves over a period of time. You can find premade transition block prefabs in "TransitionBlocks/Prefabs/Transition Blocks". To learn more about transition blocks read the "Transition Block" section below.

		Transition Block Sprite: The sprite to be used for each of the transition blocks. To make a sprite bring a square image into Unity and note the width/height of it in pixels. In unity set the image Texture type to be "Sprite (2D and UI)" and the Pixels Per Unit to be the same number as the width and height of the image. There are a few examples that you can use in the folder "TransitionBlocks/Tileable Textures" 

		Transition Block Color: The color to be used for each of the transition blocks. This color will be used by the Transition Block Sprite.

		Transition Block Animation Time: The time it takes for a transition block to complete its animation.

		Transition Order Prefab: The transition order to be used when transitioning. A transition order is how the transition blocks are laid down during the transition and can be anything from checkerboard style to a side wipe. A Transition Order prefab is a game object with a transition order script on it. You can find the types of transition order scripts in "TransitionBlocks/Scripts/Transition Orders" and some premade transition order prefabs in "TransitionBlocks/Prefabs/Transition Orders". To learn more about transition orders read the "Transition Order" section below.

		Transition Time: The time it takes to place all of the transition blocks down in this transition.

		Automatically Transition In: If true this transitioner will automatically transition into the scene when the scene is loaded. If it's false the transition will stay in front of the screen until you call "Transitioner.Instance.FinishTransition();" and then the transition will finish. This will let you load assets in the background and transition in when you're ready.

		Dont Destroy On Load: If true this transitioner will be a singleton that follows you into every scene. You will only need to place one down in your first scene if this is true. Otherwise, you'll need a transitioner in each scene you make. Either way you will call it the same way using "Transitioner.Instance.TransitionToScene("LevelName");" or "Transitioner.Instance.TransitionToScene(levelNumber);".

		Skip Every X Block Updates: Only change this if you're having performance issues. This will skip every X transition block update. (If this is at 2 every other update will be skipped, at 3 every third update is skipped, etc...) Leave this at one to do nothing.

Transition Order
	A transition order is a game object with a transition order script on it (found in "TransitionBlocks/Scripts/Transition Orders"). Each script has its own options that can be tweaked. For instance the "TransitionOrderCornerWipe" script has a dropdown for the start corner with options for each corner of the screen. Changing it will change which corner of the screen the transition blocks are placed down first as the order moves to the other corner of the screen. Another good example is the "TransitionOrderSpiral" script. It has a start corner as well and a spiral direction. The spiral direction dictates which way the spiral places transition blocks down (clockwise or counter clockwise). Many of the transition order scripts have a random option in dropdowns which will pick one of the other dropdown options at random at runtime. Feel free to make a copy of any of the transition order prefabs in "TransitionBlocks/Prefabs/Transition Orders" and play around with their settings!

Transition Block
	A transition block is a game object with a transition block script on it (found in "TransitionBlocks/Scripts/Transition Blocks"). The transition block script is made up of six AnimationCurves that allow you to precisely control the animation of them over time. Clicking on the box next to the variable names will bring up a curve editor that allows you to change that property over time. The x-value of the curve is time and your animation points must be between 0.0 and 1.0 on the x-axis.
	Here are a list of the properties and what modifying the y-value does to them:
		x Scale Over Time: A multiplyer for the x-scale of the game object. You want this to end at 1.0 for the transition to look good.

		y Scale Over Time: A multiplyer for the y-scale of the game object. You want this to end at 1.0 for the transition to look good.

		x Position Over Time: A translation for the x-position of the game object. You want this to end at 0.0 for the transition to look good.

		y Position Over Time: A translation for the y-position of the game object. You want this to end at 0.0 for the transition to look good.

		Rotation Over Time: The rotation of the game object. A full rotation happens every 1.0 (1.0 is 360 degrees). You want this to end at any whole number (0.0, -1.0, 1.0, 25.0, etc...) to look good.

		Alpha Over Time: The alpha of the sprite between 0.0 and 1.0 (0.0 being transparent and 1.0 being fully visible). You want this to end at 1.0 for the transition to look good.

	To better understand how to set up a transition block feel free to make a copy of any of the transition block prefabs in "TransitionBlocks/Prefabs/Transition Blocks" and play around with their settings!


Final words
	If you feel like this documentation is lacking, have questions about this package, or find bugs feel free to email me at ATeshGames@gmail.com or message me on twitter at @AntonTesh. I'll do my best to respond in a timely manner and update the package! Thanks for buying it and I hope it helps your game look amazing!