# Typer_Defence

List of Scripts and what they do:

##1. EnemyMovement.cs

This controls the movement of enemies from their spawn point to the target(the target are list of points.
These can be found in the hierarchy under ListtargetPoints). On colliding with the tower (the target points are inside these sphere collider)
, "reachedTower" bool variable is set to true to activate the attack animation. Then the isKinematic component of the rigid body is 
made true so that they will remain in their positions and cannot be pushed by other enemies. Then a nav mesh obstacle is made around them such that
the incoming enemie's nav mesh agents will be aware of their presence and try going around them, till they collide with the tower.
Any new target points should be placed in this script, via unity(drag and drop).

##2. Typer.cs

Here you can adjust what message has to be placed on the textbox above the enemies.
startDelay and type delay was put to create a typing motion as the text appear on the enemies. these can be set to 0f
to make the text appear instantly.

##3. EnemyManager.cs

Controls the spawning of enemies. Place the spawn points created on unity to this script.Also after isntantiating the enemies, give
a required name so that they can be identified later, otherwise they will all have the same name.

##4. launchFireSpells.cs

Launches fire spells from the wizard. To launch a spell call StartCurrent() and to switch a spell call NextPrefab() or PreviousPrefab().
StartCurrent() was called from an animation event (This can be accessed by hitting 'Ctrl' + 6 on unity or Windows->Animation.Click on the 
wizard prefab and then in the animation window you can see a list of animation points and transitions. For the attack animation, I created an event to call
StartCurrent so that the animation matches with the launch of the spell).
Documentation on how to do the above:- http://docs.unity3d.com/Manual/animeditor-AnimationEvents.html.

##5. Assets/Scripts/FireSpells/*all the scripts*

Controls the FireSpells.