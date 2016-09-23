using System;
using VedaConnect.VedaScoreApply;

namespace VedaConnect
{
    public enum Gender
    {
        Undisclosed,
        Male,
        Female
    }

    internal static class GenderExtensions
    {
        public static gendercodeType ToCode(this Gender gender)
        {
            switch (gender)
            {
                case Gender.Male:
                    return gendercodeType.M;
                case Gender.Female:
                    return gendercodeType.F;
                case Gender.Undisclosed:
                    return gendercodeType.U;
                default:
                    throw new ArgumentOutOfRangeException(nameof(gender), gender, "Invalid Gender.");
            }
        }

        public static Gender ToGender(this string genderCode)
        {
            switch (genderCode)
            {
                case "M":
                    return Gender.Male;
                case "F":
                    return Gender.Female;
                case "U":
                    return Gender.Undisclosed;
                default:
                    throw new ArgumentOutOfRangeException(nameof(genderCode), genderCode, "Invalid gender code.");
            }
        }

        public static Gender ToGender(this gendercodeType genderCode)
        {
            switch (genderCode)
            {
                case gendercodeType.M:
                    return Gender.Male;
                case gendercodeType.F:
                    return Gender.Female;
                case gendercodeType.U:
                    return Gender.Undisclosed;
                default:
                    throw new ArgumentOutOfRangeException(nameof(genderCode), genderCode, "Invalid gender code.");
            }
        }
    }
}