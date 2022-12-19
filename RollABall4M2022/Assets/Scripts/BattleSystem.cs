using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystem : MonoBehaviour
{
    void Start()
    {
// create player party
        List<Character> playerCharacters = new List<Character>();
        for (int i = 0; i < 4; i++)
        {
            playerCharacters.Add(new Character());
        }
            // create enemy party
    List<Character> enemyCharacters = new List<Character>();
    for (int i = 0; i < 9; i++)
    {
        enemyCharacters.Add(new Character());
    }

    // create list of all characters
    List<Character> allCharacters = new List<Character>();
    allCharacters.AddRange(playerCharacters);
    allCharacters.AddRange(enemyCharacters);

    // initialize character stats
    foreach (Character character in allCharacters)
    {
        character.name = "Character " + (allCharacters.IndexOf(character) + 1);
        character.hp = UnityEngine.Random.Range(50, 100);
        character.attack = UnityEngine.Random.Range(5, 15);
        character.magicAttack = UnityEngine.Random.Range(10, 20);
        character.defense = UnityEngine.Random.Range(5, 10);
        character.magicDefense = UnityEngine.Random.Range(5, 10);
        character.agility = UnityEngine.Random.Range(40, 60);
        character.dexterity = UnityEngine.Random.Range(40, 60);
        character.mainElement = (MagicElement)UnityEngine.Random.Range(0, 6);
    }

    // create queue for turn order
    List<Character> turnQueue = new List<Character>(allCharacters);

    // sort queue by speed
    turnQueue.Sort((x, y) => x.agility.CompareTo(y.agility));

    // loop through each character's turn
    while (playerCharacters.Count > 0 && enemyCharacters.Count > 0)
    {
        // get current character and remove from queue
        Character currentCharacter = turnQueue[0];
        turnQueue.RemoveAt(0);

        // print character stats
        Console.WriteLine(currentCharacter.name + " - HP: " + currentCharacter.hp + ", Attack: " + currentCharacter.attack + ", Magic Attack: " + currentCharacter.magicAttack + ", Defense: " + currentCharacter.defense + ", Magic Defense: " + currentCharacter.magicDefense + ", Agility: " + currentCharacter.agility + ", Dexterity: " + currentCharacter.dexterity + ", Element: " + currentCharacter.mainElement);

        // determine if character is in player party or enemy party
        List<Character> currentParty;
        List<Character> targetParty;
        if (playerCharacters.Contains(currentCharacter))
        {
            currentParty = playerCharacters;
            targetParty = enemyCharacters;
        }
        else
        {
            currentParty = enemyCharacters;
            targetParty = playerCharacters;
        }

        // choose action for current character
        int action = UnityEngine.Random.Range(0, 3);
        if (action == 0)
        {
                    Attack(currentCharacter, targetParty[UnityEngine.Random.Range(0, targetParty.Count)]);
        }
        else if (action == 1)
        {
            Magic(currentCharacter, targetParty[UnityEngine.Random.Range(0, targetParty.Count)]);
        }
        else
        {
            Defend(currentCharacter);
        }

        // remove any dead characters from parties
        foreach (Character character in allCharacters)
        {
            if (character.hp <= 0)
            {
                playerCharacters.Remove(character);
                enemyCharacters.Remove(character);
                turnQueue.Remove(character);
            }
        }

        // sort queue by speed
        turnQueue.Sort((x, y) => x.agility.CompareTo(y.agility));
    }

    // print outcome of battle
    if (playerCharacters.Count > 0)
    {
        Console.WriteLine("Player party wins!");
    }
    else
    {
        Console.WriteLine("Enemy party wins!");
    }
}

private void Attack(Character caster, Character target)
{
    // calculate chance to hit
    float hitChance = (caster.dexterity + UnityEngine.Random.Range(1, 100)) / (target.agility + UnityEngine.Random.Range(1, 100));

    // calculate critical hit chance
    float critChance = caster.dexterity / 100f;

    if (hitChance > 0.5f)
    {
        // calculate damage
        int damage = caster.attack - target.defense;

        // apply critical hit bonus
        if (critChance < 0.1f)
        {
            Console.WriteLine(caster.name + " lands a critical hit!");
            damage *= 2;
        }

        // apply damage to target
        target.hp -= damage;
        Console.WriteLine(caster.name + " deals " + damage + " damage to " + target.name);
    }
    else
    {
        Console.WriteLine(caster.name + "'s attack misses!");
    }
}

private void Magic(Character caster, Character target)
{
    // calculate chance to hit
    float hitChance = (caster.dexterity + UnityEngine.Random.Range(1, 100)) / (target.magicDefense + UnityEngine.Random.Range(0, 100));
        if (hitChance > 0.5f)
    {
        // calculate damage
        int damage = caster.magicAttack - target.magicDefense;

        // calculate bonus damage for weakness
        if (target.mainElement == GetWeakness(caster.mainElement, 1) || target.mainElement == GetWeakness(caster.mainElement, 2))
        {
            damage *= 2;
            Console.WriteLine(caster.name + " takes advantage of " + target.name + "'s weakness to " + caster.mainElement + " magic!");
        }

        // apply damage to target
        target.hp -= damage;
        Console.WriteLine(caster.name + " deals " + damage + " magic damage to " + target.name);
    }
    else
    {
        Console.WriteLine(caster.name + "'s magic attack misses!");
    }
}

private void Defend(Character character)
{
    Console.WriteLine(character.name + " defends");
}

private MagicElement GetWeakness(MagicElement element, int weakness)
{
    if (element == MagicElement.Fire)
    {
        if (weakness == 1)
        {
            return MagicElement.Water;
        }
        else
        {
            return MagicElement.Ice;
        }
    }
    else if (element == MagicElement.Water)
    {
        if (weakness == 1)
        {
            return MagicElement.Lightning;
        }
        else
        {
            return MagicElement.Fire;
        }
    }
    else if (element == MagicElement.Lightning)
    {
        if (weakness == 1)
        {
            return MagicElement.Wind;
        }
        else
        {
            return MagicElement.Water;
        }
    }
    else if (element == MagicElement.Wind)
    {
        if (weakness == 1)
        {
            return MagicElement.Earth;
        }
        else
        {
            return MagicElement.Lightning;
        }
    }
    else if (element == MagicElement.Earth)
    {
        if (weakness == 1)
        {
            return MagicElement.Ice;
        }
        else
        {
            return MagicElement.Wind;
        }
    }
    else
    {
        if (weakness == 1)
        {
            return MagicElement.Fire;
        }
        else
        {
            return MagicElement.Earth;
        }
    }
}

}

public class Character
{
    public string name;
    public int hp;
    public int attack;
    public int magicAttack;
    public int defense;
    public int magicDefense;
    public int agility;
    public int dexterity;
    public MagicElement mainElement;
}

public enum MagicElement
{
    Fire,
    Water,
    Lightning,
    Wind,
    Earth,
    Ice
}
