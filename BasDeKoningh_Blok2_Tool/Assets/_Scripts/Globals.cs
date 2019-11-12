using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace EasyAI
{
    public enum Genre
    {
        RPG = 0,
        Survival = 1,
        Shooter = 2
    }

    public enum NPCType
    {
        Zombie = 0,
        Mage = 1,
        Warrior = 2,
        Medic = 3,
        Infantry = 4,
        Guardian = 5
    }

    public static class Globals
    {
        public static IEnumerable<SerializedProperty> GetChildren(this SerializedProperty property)
        {
            property = property.Copy();
            var nextElement = property.Copy();
            bool hasNextElement = nextElement.NextVisible(true);
            if (!hasNextElement)
            {
                nextElement = null;
            }

            property.NextVisible(true);
            while (true)
            {
                if ((SerializedProperty.EqualContents(property, nextElement)))
                {
                    yield break;
                }

                yield return property;

                bool hasNext = property.NextVisible(false);
                if (!hasNext)
                {
                    break;
                }
            }
        }
    }
}
