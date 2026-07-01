## Project Specification: Cyborg Dungeon Crawler

### Core Loop
* Run Phase: Top-down tactical combat inside underground bug nests. The player fights swarms, gathers scrap, and loots raw data processing chips.
* Rebuild Phase: Upon destruction, the player resets to the workshop. Scrap is spent on physical chassis upgrades; data chips unlock new execution nodes.

---

### Movement Subsystem (Legs Firmware)
* Input Routing: Digital inputs map directly to directional force vectors. 
* Logic Integration: Modifiers sit between the key bind node and the execution node. 
* Behavioral Modifiers: 
  * Shift Key -> Sprint Node -> Increases speed multiplier but injects a subtraction vector into the central battery pool.
  * Double Tap -> Dash Node -> Triggers short-burst velocity with an internal cooldown clock.

---

### Ballistics Subsystem (Arms & Weapon Scripts)
* Execution Order: Guns process logic sequentially starting from an execution trigger node.
* Memory Constraints: High-impact nodes consume greater amounts of system RAM, limiting the complexity of the weapon script.
* Conditional Modifiers:
  * Left Click Input -> RNG Splitter (50/50) -> Success branches to Double Damage; failure branches to Nullify Bullet.
  * Magazine Counter <= 10% -> Frequency Multiplier -> Increases fire rate dynamically as ammo depletes.

---

### Core Chassis Subsystem (Torso & Life Support)
* Resource Balancing: Dictates the interaction between Hull Integrity (HP), Battery capacity (Energy), and total system RAM.
* Hardware Mounts: Upgraded chests unlock secondary physical hardpoints for shoulder turrets or auxiliary limbs, allowing separate script loops to run concurrently.
* Defensive Automation:
  * Hull Sensor < 20% -> Gate Opener -> Redirects energy from weapons to activate a temporary plasma barrier.
  * Heavy Armor Plate -> Adds flat damage reduction but increases mass, requiring high-torque movement nodes to maintain base speed.