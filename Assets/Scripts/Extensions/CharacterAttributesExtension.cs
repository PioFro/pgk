using System.Collections.Generic;
using System.Linq;

public static class CharacterAttributesExtension
{
    public static int GetAttributeValue(this IEnumerable<CharacterAttributes> characterAttributes, string attributeName)
    {
        return characterAttributes.First(x => x.Attribute.name == attributeName).Value;
    }
}
