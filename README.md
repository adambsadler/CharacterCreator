# Character Creator

Authors: Adam Sadler and Zach Nichols

Due Date: December 18, 2021

Assignment: The **Eleven Fifty Academy Group API Project** requires each team to create an API utilizing Entity Framework, multiple data tables, and additional features to showcase student learning. Our group chose to create a **Dungeons & Dragons 5e** character creator API with the following classes:

- `Player` contains a unique PlayerId, Name, and list of Characters. The intent is for each registered User with a unique token would only have a single Player.

- `Character` contains a unique CharacterId, Name, and Player/PlayerId reference. It also contains the Race, CharacterClass, ability scores, references to Skills for which the Character is proficient, and a reference to the Character's Background.

- `Background` contains a unique BackgroundId, Name, Description, and associated Feature.

- `Skill` contains a unique SkillId, Name, and Description.

In addition to the API, we created a console-based User interface that calls our API. The User interface has a few notable features:

- A login screen that allows Users to register using an email and password, login with that information, and change the local host URL (since the API ran off the same computer).

- A main screen where that User (and in turn Player) could change their Player Name or navigate to screens to create Characters, view all associated Characters, delete Characters, view all Skills, or view all Backgrounds.

- The Skill/Background screens are read-only and refer Users to Postman to create new Skills/Backgrounds.

- Choosing to create a Character or view a Character (from the Character list screen) takes Users to a simplified D&D Character sheet. That screen prompts Users to change specific values on the Character sheet or save the changes. Failing to save before exiting that screen will lose any information entered, while choosing to save creates/updates the Character in the database via the API.

- Our program automatically seeds the Background and Skill tables with the appropriate information from the D&D 5e Player's Handbook.


Some stretch goals we didn't hit yet:

- `CharacterClass` and `Race` classes that contain unique IDs, Names, Descriptions, and any features/modifiers/subclasses. These would replace the string fields in the Character class.

- `Character.Level` field that tracks the Character's current level. We would also include functionality for leveling up, such as asking the User for ability score increases when necessary.

- `Modification` class that would track any buffs/debuffs/items/Features/Feats/etc. A Character would inherit these from CharacterClass, Race, Background, Skill, or leveling up. A list of these modifications would be stored for each Character, and any stat changes would be resolved upon inheriting/completing the modification. This was obviously outside the scope of our project, but maybe one day!


## Resources

- [GitHub Repository](https://github.com/adambsadler/CharacterCreator)
- [Assignment Requirements and Rubric](https://elevenfifty.instructure.com/courses/799/assignments/17170?module_item_id=72088)
- [Google Document](https://docs.google.com/document/d/1D9B7eja5Rh8hT407lgjJ4Kd6o0anSv4hyD8ibrTC6kM/edit?usp=sharing)


---
