using System;
using System.ComponentModel;

namespace VedaConnect
{
    public enum PermissionType
    {
        Consumer,
        ConsumerPlusCommercial,
        CommercialPlusConsumer,
        Commercial
    }

    internal static class PermissionTypeExtensions
    {
        public static string ToCode(this PermissionType type)
        {
            switch (type)
            {
                case PermissionType.Consumer:
                    return "X";
                case PermissionType.ConsumerPlusCommercial:
                    return "XY";
                case PermissionType.CommercialPlusConsumer:
                    return "YX";
                case PermissionType.Commercial:
                    return "Y";
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, "Invalid PermissionType.");
            }
        }

        public static PermissionType ToPermissionType(this string typeCode)
        {
            switch (typeCode)
            {
                case "X":
                    return PermissionType.Consumer;
                case "XY":
                    return PermissionType.ConsumerPlusCommercial;
                case "YX":
                    return PermissionType.CommercialPlusConsumer;
                case "Y":
                    return PermissionType.Commercial;
                default:
                    throw new ArgumentOutOfRangeException(nameof(typeCode), typeCode, "Invalid type code.");
            }
        }
    }
}
