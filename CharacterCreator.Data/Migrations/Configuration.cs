namespace CharacterCreator.Data.Migrations
{
    using CsvHelper;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;

    internal sealed class Configuration : DbMigrationsConfiguration<CharacterCreator.Data.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "CharacterCreator.Data.ApplicationDbContext";
        }

        protected override void Seed(CharacterCreator.Data.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.


            // Seed Skills

            List<Skill> defaultSkills = new List<Skill>();

            defaultSkills.Add(new Skill("Acrobatics", "Acrobatics covers your attempt to stay on your feet in a tricky situation, such as when you’re " +
                "trying to run across a sheet of ice, balance on a tightrope, or stay upright on a rocking ship’s deck. The GM might also call for a " +
                "Acrobatics check to see if you can perform acrobatic stunts, including dives, rolls, somersaults, and flips.", "Dexterity"));
            defaultSkills.Add(new Skill("Animal Handling", "When there is any question whether you can calm down a domesticated animal, keep a mount " +
                "from getting spooked, or intuit an animal’s intentions, the GM might call for an Animal Handling check. You also make an Animal Handling " +
                "check to control your mount when you attempt a risky maneuver.", "Wisdom"));
            defaultSkills.Add(new Skill("Arcana", "Arcana measures your ability to recall lore about spells, magic items, eldritch symbols, magical " +
                "traditions, the planes of existence, and the inhabitants of those planes.", "Intelligence"));
            defaultSkills.Add(new Skill("Athletics", "Athletics covers difficult situations you encounter while climbing, jumping, or swimming. Examples " +
                "include the following activities: • You attempt to climb a sheer or slippery cliff, avoid hazards while scaling a wall, or cling to a " +
                "surface while something is trying to knock you off.", "Strength"));
            defaultSkills.Add(new Skill("Deception", "Deception lets you convincingly hide the truth, either verbally or through your actions. This " +
                "deception can encompass everything from misleading others through ambiguity to telling outright lies. Typical situations include " +
                "trying to fasttalk a guard, con a merchant, earn money through gambling, pass yourself off in a disguise, dull someone’s suspicions " +
                "with false assurances, or maintain a straight face while telling a blatant lie.", "Charisma"));
            defaultSkills.Add(new Skill("History", "History is your ability to recall lore about historical events, legendary people, ancient kingdoms, " +
                "past disputes, recent wars, and lost civilizations.", "Intelligence"));
            defaultSkills.Add(new Skill("Insight", "Insight is the ability to determine the true intentions of a creature, such as when searching out " +
                "a lie or predicting someone’s next move. Doing so involves gleaning clues from body language, speech habits, and changes in mannerisms.", "Wisdom"));
            defaultSkills.Add(new Skill("Intimidation", "When you attempt to influence someone through overt threats, hostile actions, and physical " +
                "violence, the GM might ask you to make an Intimidation check. Examples include trying to pry information out of a prisoner, convincing " +
                "street thugs to back down from a confrontation, or using the edge of a broken bottle to convince a sneering vizier to reconsider a " +
                "decision.", "Charisma"));
            defaultSkills.Add(new Skill("Investigation", "When you look around for clues and make deductions based on those clues, you make an " +
                "Investigation check. You might deduce the location of a hidden object, discern from the appearance of a wound what kind of weapon " +
                "dealt it, or determine the weakest point in a tunnel that could cause it to collapse. Poring through ancient scrolls in search of " +
                "a hidden fragment of knowledge might also call for an Intelligence Investigation check.", "Intelligence"));
            defaultSkills.Add(new Skill("Medicine", "Medicine lets you try to stabilize a dying companion or diagnose an illness.", "Wisdom"));
            defaultSkills.Add(new Skill("Nature", "Nature measures your ability to recall lore about terrain, plants and animals, the weather, and natural cycles.", "Intelligence"));
            defaultSkills.Add(new Skill("Perception", "Your Perception lets you spot, hear, or otherwise detect the presence of something. It measures " +
                "your general awareness of your surroundings and the keenness of your senses. For example, you might try to hear a conversation through " +
                "a closed door, eavesdrop under an open window, or hear monsters moving stealthily in the forest. Or you might try to spot things that " +
                "are obscured or easy to miss, whether they are orcs lying in ambush on a road, thugs hiding in the shadows of an alley, or candlelight " +
                "under a closed secret door.", "Wisdom"));
            defaultSkills.Add(new Skill("Performance", "Performance determines how well you can delight an audience with music, dance, acting, storytelling, " +
                "or some other form of entertainment.", "Charisma"));
            defaultSkills.Add(new Skill("Persuasion", "When you attempt to influence someone or a group of people with tact, social graces, or good " +
                "nature, the GM might ask you to make a Persuasion check. Typically, you use persuasion when acting in good faith, to foster friendships, " +
                "make cordial requests, or exhibit proper etiquette. Examples of persuading others include convincing a chamberlain to let your party see " +
                "the king, negotiating peace between warring tribes, or inspiring a crowd of townsfolk.", "Charisma"));
            defaultSkills.Add(new Skill("Religion", "Religion measures your ability to recall lore about deities, rites and prayers, religious hierarchies, " +
                "holy symbols, and the practices of secret cults.", "Intelligence"));
            defaultSkills.Add(new Skill("Sleight of Hand", "Whenever you attempt an act of legerdemain or manual trickery, such as planting " +
                "something on someone else or concealing an object on your person, make a Sleight of Hand check. The GM might also call for a " +
                "Sleight of Hand check to determine whether you can lift a coin purse off another person or slip something out of another person’s " +
                "pocket.", "Dexterity"));
            defaultSkills.Add(new Skill("Stealth", "Make a Stealth check when you attempt to conceal yourself from enemies, slink past guards, " +
                "slip away without being noticed, or sneak up on someone without being seen or heard.", "Dexterity"));
            defaultSkills.Add(new Skill("Survival", "The GM might ask you to make a Survival check to follow tracks, hunt wild game, guide " +
                "your group through frozen wastelands, identify signs that owlbears live nearby, predict the weather, or avoid quicksand and " +
                "other natural hazards.", "Wisdom"));

            foreach (Skill s in defaultSkills)
            {
                context.Skills.AddOrUpdate<Skill>(s);
            }


            // Seed Backgrounds

            List<Background> defaultBackgrounds = new List<Background>();

            defaultBackgrounds.Add(new Background("Acolyte", 
                "Languages: 2; Tools: None; Skill proficiencies: Insight, Religion", 
                "Shelter of the Faithful"));
            defaultBackgrounds.Add(new Background("Charlatan", 
                "Languages: none; Tools: Disguise kit, forgery kit; Skill proficiencies: Deception (CHA), sleight of hand (DEX)", 
                "False Identity"));
            defaultBackgrounds.Add(new Background("Criminal", 
                "Languages: none; Tools: Gaming set x1, thieves' tools; Skill proficiencies: Deception (CHA), stealth (DEX)", 
                "Criminal Contact"));
            defaultBackgrounds.Add(new Background("Entertainer", 
                "Languages: none; Tools: Disguise kit, musical instrument x1; Skill proficiencies: Acrobatics (DEX), performance (CHA)", 
                "By Popular Demand"));
            defaultBackgrounds.Add(new Background("Folk Hero", 
                "Languages: none; Tools: Artisan's tools x1, vehicles (land); Skill proficiencies: Animal handling (WIS), survival (WIS)", 
                "Rustic Hospitality"));
            defaultBackgrounds.Add(new Background("Gladiator", 
                "Languages: none; Tools: Disguise kit, unusual weapon x1; Skill proficiencies: Acrobatics (DEX), performance (CHA)", 
                "Contacts"));
            defaultBackgrounds.Add(new Background("Guild Artisan", 
                "Languages: 1; Tools: Artisan's tools x1; Skill proficiencies: Insight (WIS), persuasion (CHA)", 
                "Guild Membership"));
            defaultBackgrounds.Add(new Background("Guild Merchant", 
                "Languages: 1; Tools: Navigator's tools; Skill proficiencies: Insight (WIS), persuasion (CHA)", 
                "Guild Membership"));
            defaultBackgrounds.Add(new Background("Hermit", 
                "Languages: 1; Tools: Herbalism kit; Skill proficiencies: Medicine (WIS), religion (INT)", 
                "Discovery"));
            defaultBackgrounds.Add(new Background("Knight", 
                "Languages: 1; Tools: Gaming set x1; Skill proficiencies: History (INT), persuasion (CHA)", 
                "Squire"));
            defaultBackgrounds.Add(new Background("Noble", 
                "Languages: 1; Tools: Gaming set x1; Skill proficiencies: History (INT), persuasion (CHA)", 
                "Position of Privilege"));
            defaultBackgrounds.Add(new Background("Outlander", 
                "Languages: 1; Tools: Musical instrument x1; Skill proficiencies: Athletics (STR), survival (WIS)", 
                "Wanderer"));
            defaultBackgrounds.Add(new Background("Pirate", 
                "Languages: none; Tools: Navigator's tools, vehicles (water); Skill proficiencies: Athletics (STR), perception (WIS)", 
                "No Honour"));
            defaultBackgrounds.Add(new Background("Sage", 
                "Languages: 2; Tools: None; Skill proficiencies: Arcana (INT), history (INT)", 
                "Researcher"));
            defaultBackgrounds.Add(new Background("Sailor", 
                "Languages: none; Tools: Navigator's tools, vehicles (water); Skill proficiencies: Athletics (STR), perception (WIS)", 
                "Ship's Passage"));
            defaultBackgrounds.Add(new Background("Soldier", 
                "Languages: none; Tools: Gaming set x1, vehicles (land); Skill proficiencies: Athletics (STR), intimidation (CHA)", 
                "Military Rank"));
            defaultBackgrounds.Add(new Background("Spy", 
                "Languages: none; Tools: Gaming set x1, thieves' tools; Skill proficiencies: Deception (CHA), stealth (DEX)", 
                "Spy Contact"));
            defaultBackgrounds.Add(new Background("Urchin", 
                "Languages: none; Tools: Disguise kit, thieves' tools; Skill proficiencies: Sleight of hand (DEX), stealth (DEX)", 
                "City Secrets"));

            foreach (Background b in defaultBackgrounds)
            {
                context.Backgrounds.AddOrUpdate<Background>(b);
            }


            // Save changes

            base.Seed(context);
            context.SaveChanges();
        }
    }
}
