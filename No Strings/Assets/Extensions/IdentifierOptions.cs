using System;

namespace Extensions
{
    [Flags]
    public enum IdentifierOptions
    {
        None                    = 0b_0000_0000,
        WhitespaceAsSpace       = 0b_0000_0001,
        RemoveUnderscores       = 0b_0000_0010,
        UppercaseSingleLetters  = 0b_0000_0100,
        TwoLettersAreAcronyms   = 0b_0000_1000,
        CapitalizeWords         = 0b_0001_0000,
        All                     = 0b_0001_1111,
    }
}
