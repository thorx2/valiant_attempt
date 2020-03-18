# Valiant Attempt
Starting with the player character, use a placeholder box or cube & create a
character with navigation controls using an on-screen joystick.
- Shooting : add a shooting ability to the character, shooting speed 2 arrows per
second
- Auto aim : if there are any enemies in the room the character should detect them
and automatically turn towards them to start shooting.
- Enemies : Make a stationary enemy type , which can be placed on the level with
shooting rate of 1 arrow per second , the enemy also has the same behaviour /
ability to detect player position / presence & start shooting in that direction.

- Life Bar : Both the player character & the enemies have HP ( health points ) , so we
need to add that as well.
  - Enemy life - has 50hp lifebar.
  - Enemy attack power - Takes 10hp off the player health per shot.
  - Player character life - 100hp
  - Player attack - 10hp damage per arrow hit
- Obstacles : create a block object , which serves as a wall on the level, that the player
character can use to hide behind and dodge enemy attacks.
- Collectables : Every time an enemy dies they drop coins as loot , which can be used
later to upgrade the stats on the player character , for this test purpose we’ll just use
the coins for score count.
- Gate logic : When all enemies in the level are dead, the player character moves to
the front of the level towards a gate which upon entering, teleports him to a new
level , for us just make the gate logic & respawn the character from the beginning of
the same level (it will work like a restart for now).
- Ability upgrades : After level completion the player gets to choose one among 3
ability upgrades to progress ahead , you need to add this feature at level
completion, before respawning the character. For this test , add just one ability
**multi shot**:<br>
It gives the character an ability to shoot 3 arrows simultaneously at a time ,
one in front & 2 on either sides ( left & right from character position )
This ability should be activated once acquired after completing the level


### External packages used.
- Unity standard assets pack.
- [Joystick Pack](https://assetstore.unity.com/packages/tools/input-management/joystick-pack-107631)
