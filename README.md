# Project 2 Report

## Table of Contents

- [Evaluation Plan](#evaluation-plan)
- [Evaluation Report](#evaluation-report)
- [Shaders and Special Effects](#shaders-and-special-effects)
- [Summary of Contributions](#summary-of-contributions)
- [References and External Resources](#references-and-external-resources)
- [Other Details](#other-details)

## Evaluation Plan

### Evaluation Techniques

#### Observational technique

The observation should be a mostly uninterrupted playthrough with the player giving their thoughts on the game after they have played. 
If the player gets stuck or does not understand the controls, the interviewer can help out but should note down how they helped out.

Watch the player play the game for 2 - 5 minutes and take note on the following:

Does the player have a good understanding of the controls, were they easy to understand?
What upgrades did the player pick or tend to prefer? (Are they balanced or are the other upgrades not providing a meaningful impact on the gameplay).
Did they easily understand how the level up system works and how to gain exp, and did they intuitively understand which fireflies gave more exp.
Were the enemies' difficulties suited for their intended purpose? i.e are the small enemies easiest to deal with, etc.
What did they struggle with?
What did they understand well?
Did anything unexpected happen?

The aim of the observation is to take note of unexpected player behaviour and play patterns. This could include, places players get stuck on, quality of life features that would help benefit the player or they expected to be included, any bugs they have found.

#### Questionnaire/survey

Give this survey to the participant after they have had a chance to play the games and allow them to submit their answers anonymously.
The survey results should be used to measure the ease of use of the game. It will measure how easy it was to understand and use the controls.

##### Questions 

What is their age? Provide the age as a range and not an exact value for anonymity
What is their gender?
Do they play games on a regular basis?

Please answer the next questions as a scale of 1 - 10 (1 - not true at all, and 10 being completely true)

I thoroughly enjoyed the game.
I found the controls intuitive.
I found the player controls to be cumbersome.
I felt comfortable using the controls.
I learned the controls quickly.
The controls were similar to other games I have played.
I needed to learn the controls well before I could play.
Adapting to the controls was difficult.
The controls felt unnecessarily complex.
I found the controls to be inconsistent.

The following questions are open ended

What did you like about the game?
What didn’t you like about the game?
What would you like to see added to the game?
Is there anything that you would remove from the game and if so what would you remove and why?

### Participants

We should aim to have a large majority of our participants be in our target demographic of casual gamers in a 15 - 25 age range. Participants must remain anonymous. Participants outside of the demographic are ok and can be useful to gauge if we could have a potentially wider target audience.

Each team member should aim for at least 3 participants of the target audience, and at least 5 participants in total.

### Data Collection

#### Conducting the data collection:

Data collection should be done with the interviewer having a full view of the game screen while the participant is playing. This can be done in person or through an online streaming of the game. The interviewer should be able to talk to the participant via in-person or through an online call.

#### Ensuring equal and accurate results:

The participants should all playtest the exact same version of the game. To ensure this occurs, the playtests will be done in a set period of time on the main branch of the project. During this testing period there should be no updates made to the main project branch.

#### What will be used to collect results:

The results for the observational technique should be written or typed and then compiled with other results.
The survey questions will be put into an online form that the participants can fill out and submit anonymously.

### Data analysis

The results of the survey should have a SUS score calculated to determine how much of a change we will need to make. A low score will indicate a large improvement to quality of the games controls needs to be made and a high score will indicate that the game’s controls are well tuned.
The survey provides open ended questions about what the participants would like to see added to the game. This would be a good starting point to see if there are any similar answers and if they would be well suited for our game. Similarly for things that the participants didn’t like about the game, if there are enough similar answers then we should consider making an adjustment and removing that feature from the game.

### Timeline

The tests must all be conducted over the course of one week. During this time no changes should be made to the Main project branch and all tests should be conducted off of the main project branch. After this week is up or after each team member has collected all of their results we will make changes to the project as necessary.

### Responsibilities

Each team member will be responsible for collecting at least 5 participants each. Of these participants 3 must be within our target demographic of 15 - 25 year olds that are currently casual gamers.


## Evaluation Report

**Methodologies Used**

When conducting our observational technique, players were instructed to talk out loud and describe their thoughts as they were playing the game. The person conducting the interview was instructed to not help the player unless they were completely stuck or if they requested help. This was to help us understand the thought patterns of our players and discover any areas in the game that were difficult to understand. 

A survey was conducted after playing the game as part of our querying evaluation. The survey was about the games control scheme and if the controls are conventional/easy to use. The survey followed the System Usability Scale (SUS) to evaluate the usability of the game’s controls. The survey also featured some questions about what the players liked, disliked and changes they like to see be made to the game.

### Summary of Findings
**Results of the SUS survey**

Overall usability of game controls assessed based on SUS score interpretations (Bangor et al., 2009). Most users didn’t have any qualms about the controls themselves with an average score of 79.25, translating to a “Good” user-friendliness rating. It is important to note this score is only complementary to the other qualitative findings below.

**Accounting for outliers**

Some outliers in the SUS scores have been highlighted in yellow. These outliers are playtesters who are far outside of the target audience (non-gamers and 31+ years old). These outliers had no prior experience with using keyboard and mouse to play games and was their first time using WASD for movement. 

This is entirely outside our target audience who we expect to have familiarity with our controls as they are a universal game standard. It highlights to us that our game can be made more accessible by explaining the controls better so that anyone could pick up and play our game.

However, removing the outliers from the SUS score data to show our average score with players who are already familiar with similar games (AKA our target audience), gives us a score of **89.69**. This is a significant improvement and shows that the game has consistent and familiar controls for our target audience and puts the games’ usability in “excellent” on the SUS score adjective rating scale.

Notable player suggestions for additions to the game:
-   Increase enemy variety (add more types of enemies)
-   Increase weapon variety (add more types of weapons)
-   Give the large enemy a melee attack to make them more of a threat
    -   Another player had feedback that the sprint mechanic felt pointless as the player didn’t need to run away from them.
    -   Adding the melee attack can help allow the player to utilise the sprint ability for closing in and out of the large enemies range
-   Add a mini map or other tool to help with navigation
-   Having the current upgrades of the weapon displayed on the weapon upgrade screen
-   Add another use for stamina (another action that uses stamina e.g. a dash or dodge)
-   Adding a health bar to the boss enemy

Notable player feedback:
-   Improving the in-game text and item descriptions (fixing some grammar issues)

Observations made during gameplay:
-   Many players didn’t understand the light radius was the health bar at first
-   Stamina regen and max stamina seem to be the least preferred upgrades
-   Some players didn’t see a point in having a zoom in function
-   The ghost enemies spawned from the large enemies seemed weak and had low impact on the outcome of the game
-   The damage scaling on tier 1 weapons seems to be low, players seemed to avoid picking the damage upgrade on tier 1 weapons because it had low impact on their gameplay

### **Changes made**
-   Enemies no longer spawn at the first Point Of Interest (POI)
-   Added a large, slow melee attack to the large enemy
-   Changed the background colour of weapon upgrades to help distinguish them from player upgrades

**Balance Changes**

**Enemy Balance Changes**
-   **Ghost (summoned by large enemy)**
    -   Increased acceleration by 50% (by the time the player was introduced to the ghost enemies they already felt weak, increasing their acceleration should help keep them more of a threat in the late game).
    -   Decreased the amount that hitting a ghost reduced its velocity by 20% (was 33%)
-   **Small Enemy Skeletons**
    -   Reduced health to 75 (was 100)
    -   This change should make the small enemies feel the same as they currently do but slightly adjusted for the damage changes to the weapons.
-   **Large Enemies**
    -   Added a long range slow melee attack (deals 40 dmg)
-   **Boss Enemy**
    -   Increased damage dealt to 60 (was 50)
    -   Decreased the attack cooldown to 1.2 seconds (was 1.5)

**Weapon Balance Changes**
-   **Steadfast Bolt**
    -   Reduced the base damage by 20 (was 40)
    -   Increased the damage gained per upgrade to 10 (was 4)
    -   This change should make the damage upgrade feel more meaningful.
-   **Celestial Orbit**
    -   Increased the damage gained per upgrade to 5 (was 2)
    -   The celestial orbit base damage is already very low (20), so increasing the damage gained per upgrade will allow it to feel more powerful with each upgrade.
    -   Fixed bug that caused the damage upgrade to only increase after upgrading speed or projectile count, now works as intended
-   **Lightlance**
    -   Decreased the base damage to 25 (was 50)
    -   Increased the damage gained per upgrade to 12.5 (was 5)
    -   Increased the base pierce amount to 3 (was 2)
    -   This should bring the damage of the Lightlance more in line with the other weapons and increase the potency of the pierce component of the weapon.


## Shaders and Special Effects
**FogTest**

_(Assets/Shaders/FogTest.shader)_

(gif of current fog)

This shader is applied to the “fog of war” in the game. Its implementation is based on Andrew Hung’s (2018) fog of war implementation. Simply put, a special camera looks down on the entire scene that can only see a certain layer. On this layer are coloured circles (hidden from the main camera) that represent areas that should not have fog. The special camera writes what it sees to a render texture and does not clear its frame buffer. This results in the render texture being a transparent texture with coloured pixels representing areas that should be “holes” in the fog. This render texture is used in a shader that reads the alpha values of the texture and outputs a solid colour or a transparent pixel.

Hung’s implementation then used this shader on a projector that projected onto the scene, however since I wanted this fog to be a visible object in the game, I directly used the shader on a plane that spans the entire level (**UnexploredFog**). 

I also changed the render queue of this fog shader to be placed above all other transparent objects (essentially above everything else in the level except UI elements). This results in the fog being written last to the screen frame buffer and therefore “hides” everything behind the fog.

I also implemented Hung’s method of “smoothly” updating the fog’s visible areas, which essentially keeps a slightly older copy of the render texture and lerps the alpha values between the older and current textures based on a “**_Blend**” uniform. This blending is controlled by the **FogBlend.cs** script. The speed is also tied to the player’s current movement speed to avoid the fog revealing “lagging” behind the player when they have a very high speed.
The shader also includes some basic vertex manipulation in the Vertex shader stage that intends to drop visible area vertexes down so the edges of the visible areas in the fog “droop” down a little, giving it a 3D effect (an early implementation of this can be seen in the following gif), however this required the original plane mesh to have extremely high vertice counts. 

(gif of 3d)
_The vertices of visible areas being dropped down to create a vertical “wall” of fog_

This effect is not visible in the current state of the game due to this performance limitation but a future implementation could include utilising the Tessellation stage in the Direct3D 11 pipeline to increase the vertex density of nearby fog through the GPU, and then instead manipulating these vertices in the Domain-Shader stage post-tessellation. Nearby fog can be determined through another render texture (with another camera and layer) using slightly larger circles as a control map in the Hull-Shader stage.

**PlayerPulseShader**

_(Assets/Shaders/PlayerPulseShader.shader)_

(gif of pulsing)

This shader provides the player a “breathing” living effect. It manipulates the vertices of the model mesh using its normals to “inflate” the mesh and increases the brightness of the pixels based on a “Pulse Intensity” value.

The vertices are pushed along their normal direction by an amount controlled by the **_PulseSpeed** and **_PulseAmount** values and the sine of the time value. This amount will always be positive to ensure the model does not collapse inwards and instead producing a “pulsing” effect.

The output colour of the fragment shader is also controlled by an initial **_EmissionColor** and **_PulseIntensity**. The base emission colour is multiplied by a **_BaseIntensity** value which is almost always a positive value, making the colour brighter (with colour values above 1.0f) and hence make use of the bloom post-processing effect on the main camera. This makes the player glow as bloom creates fringes of light depending on if the colour values exceed 1.0f. The shader then adds an additional value to the colour that oscillates based on the sine of the time; the amplitude of this additional value is controlled by **_PulseIntensity**.

These uniforms are also parameterised to be accessed in the **PlayerShaderController.cs**, which reads the player’s current HP ratio (curr HP to max HP) and increases the frequency of pulses and lowers the brightness of the player colour based on this ratio; the closer the player is to death, the faster the pulse speed and the lower the intensity.

(gif of damaged player)
_The pulsating effect quickens and the brightness of colour values is reduced at low HP_

**Boss Altar Particle System**

_(Assets/Prefabs/POI/BossAltar.prefab → Particle System)_

(gif of sword particle effect)

The boss altar particle system consists primarily of billboard “runes” from a sprite sheet made of hand-drawn runes (can be seen in the GDD). 

The texture sheet animation of the system is configured to select a random row (out of 4) on the sprite sheet and animates each particle to cycle through the runes in the selected row at 10 FPS. The particles are then emitted with a long lifetime (10s) and from a spherical emitter at a modest 15 particles per second. This forms the basis of the particle system; a bunch of random cycling runes giving a mysterious, ancient power feel.

The particles are then given a velocity over their lifetime:
-   A constant vertical velocity of 1 (Y-Axis) makes the runes slowly float upwards
-   An orbital velocity (Y = 3, X = 1, Radial = 0.5) makes the particles orbit around a central axis. In this case the particles are intended to spiral around the sword model and as such the centre of rotation is offset to be parallel to the sword.

The colour and size of the particles are modified towards the end of their lifetime to turn black and smaller, making the particles “fade away”.

Finally the particles have noise added to their X and Z axis movement (excluding Y as we want the particles to float consistently upwards) adding an “uncontrollable, corrupted” feel to them.

(gif of in game particle)
_The particle system as seen in-game_


## Summary of Contributions

**Jeongwoo:**
-   Title screen
-   Game sounds (sound system, sound design)
-   Player stats (design concept and implementation)
    -   Particle system for HP (ring around player)
    -   Player level up system (level up UI, icon design, implementation)
-   Weapons system implementation (class design, player weapon controller)
    -   Weapons (weapon concepts, design and implementation)
    -   Weapon upgrades (weapon get/upgrade UI, icon design, implementation)
-   UI design and implementation (pause menu, stats overview, weapon overview, options, help, ending screen, game over screen)
-   Fog system (see [Other Details](#other-details))
-   Procedural level generation
    -   Points of Interest
        -   Design of POI variants
        -   POI location generation mechanics
        -   Boss altar (design and implementation)
    -   Fireflies (the exp trails and weapon upgrade pickups)
        -   Particle system design (system configuration and particle textures)
        -   Absorb animation and implementation
        -   Trail generation mechanics
-   Static level design (terrain)
-   Cloaking system (enemies/POIs being black when outside player radius) (see [Other Details](#other-details))
-   Particle effects
    -   Player level up effect
    -   Player death effect
    -   Enemy hit effect
    -   Enemy death effect
    -   Boss altar/spawn effect
-   Natural enemy spawning mechanics (player enemy spawner)
-   Player shader (design and linking to player stats)
-   Milestone 2 video
-   Maintaining GDD

**Kate:**
-   Unit health interactions (player and enemy HP)
-   Enemy implementation
    -   Finding enemy assets and animations
    -   Creating enemy animation controllers and enemy behaviour scripts
    -   Enemy spawners at POI and character
    -   Enemy and weapon interactions (damage calculations)
-   Worked on the Camera system initially
-   Player animations, movement and stamina system
-   Direction for evaluation plan

**Deyulin:**
-   Player shader implementation


## References and External Resources
Bangor, A., Kortum, P., & Miller, J. (2009). _Determining what individual SUS scores mean: adding an adjective rating scale._ Journal of Usability Studies, 4(3), 114–123.

Hung, A. (2018). _Implementing Attractive Fog of War in Unity._ Nudgie Dev Diary. https://andrewhungblog.wordpress.com/2018/06/23/implementing-fog-of-war-in-unity/

**External Resources**

https://assetstore.unity.com/packages/3d/environments/fantasy/fantasy-forest-environment-free-demo-35361

https://assetstore.unity.com/packages/3d/environments/low-poly-medieval-market-262473#reviews

https://assetstore.unity.com/packages/3d/environments/campfires-torches-models-and-fx-242552

https://assetstore.unity.com/packages/3d/environments/dungeons/low-poly-dungeons-lite-177937#description

https://assetstore.unity.com/packages/3d/environments/fantasy/halloween-pack-cemetery-snap-235573

https://assetstore.unity.com/packages/3d/props/exterior/traditional-water-well-4477

https://assetstore.unity.com/packages/3d/props/exterior/tomb-65925

https://assetstore.unity.com/packages/3d/environments/historic/medieval-barrows-and-wagons-33411

https://assetstore.unity.com/packages/3d/props/exterior/ancient-ruins-and-plants-201914

Menu Music: Soul's Departure - Darren Curtis

Boss Music: Great One's Nightmare - Dragon Clef

https://assetstore.unity.com/packages/3d/props/exterior/ancient-ruins-and-plants-201914

https://assetstore.unity.com/packages/3d/animations/melee-warrior-animations-free-165785

https://assetstore.unity.com/packages/3d/characters/creatures/golemmonster-33260

https://assetstore.unity.com/packages/3d/animations/magic-pack-36269

https://assetstore.unity.com/packages/3d/characters/low-poly-skeleton-162347

https://assetstore.unity.com/packages/3d/characters/humanoids/lowpoly-magician-rio-288942

https://assetstore.unity.com/packages/3d/characters/creatures/ghost-character-free-267003

https://assetstore.unity.com/packages/3d/animations/basic-motions-free-154271

https://assetstore.unity.com/packages/3d/characters/humanoids/humans/3d-character-dummy-178395

## Other Details
**WebGL fog implementation limitation**

The intended fog implementation included a second fog of war plane to represent the “currently” visible areas. This fog would be slightly translucent (40% opacity), allowing the player to see through it. The current fog was originally intended to be called the “unexplored” fog, as it is fully opaque and once cleared, it remains cleared. 

The combination of these effects would result in the immediate area around the player being completely clear, unexplored areas being completely black, and previously explored areas being covered in a slightly translucent fog.

While this implementation worked flawlessly in a standalone unity platform, when built on WebGL the fog implementation breaks. The issue was unable to be resolved but there are a number of reasons why I believe this may be happening:
-   WebGL automatically clears cameras
    -   According to https://docs.unity3d.com/Manual/webgl-graphics.html, Unity Web clears the drawing buffer after each frame by default, and hence this may mess with the fog visibility render textures.
    -   A lot of testing results showed that the render textures were not being updated properly (the don’t clear and clear flags were not being respected). Hence various solutions were attempted, ranging from:
        -   Adjusting WebGL build settings to preserve camera flags
        -   Adjusting render texture file formats
        -   Creating a script to manually blit the visibility camera frame buffers to render textures and clearing them using a custom shader
        -   Removing one of the fogs (camera, render texture, plane) and adding an extra fog
            -   This resulted in the entire thing breaking, and hence the second fog is actually still active within the current game, but the fog colour has an alpha of 0 to make it invisible.
    -   Lack of memory
    -   WebGL cannot process multiple cameras correctly
        -   This is the most likely issue as adding or removing extra cameras that write to render textures broke the implementation (as mentioned earlier).

I’m quite disappointed that this feature could not be implemented on WebGL as it adds a lot of atmosphere to the game and was one of the features I spent a lot of time on.

**Cloaking**

The enemies and POI models in this game are initially set to a black colour when outside the player’s “uncloaking” radius, you might notice when you reveal a new POI or find an enemy, they “fade” from black to their original colours when in range. This effect is controlled by the **EnemyUncloaker.cs** (Assets/Scripts/Fog) and **CloakPOI.cs** (Assets/Scripts/POI) scripts.

As the original materials on enemies and POI assets can vary quite greatly and have multiple children with different materials, it was a minor challenge to try and implement a “cloaked” effect on these cloakables.

The solution I came up with was to store the original materials and shader parameters (colours, metallic/smoothness values where they exist) of each material inside an enemy/POI. A separate cloak material (**CloakMaterial**) exists (a simple material using a basic unlit black colour shader **CloakShader.shader**) that is swapped onto every material within the cloakables. When the cloakable is uncloaked, it starts a coroutine swapping the materials back to their original ones and lerps the shader parameters where they exist from black to their original colour.

This effect helps sell the idea of discovering places hidden in the fog and the enemies “materialising” out of the fog.
