using System;

namespace VedaConnect
{
    public enum ProductDataLevel
    {
        Negative,
        Comprehensive
    }

    internal static class ProductDataLevelExtensions
    {
        public static string ToCode(this ProductDataLevel level)
        {
            switch (level)
            {
                case ProductDataLevel.Negative:
                    return "N";
                case ProductDataLevel.Comprehensive:
                    return "C";
                default:
                    throw new ArgumentOutOfRangeException(nameof(level), level, "Invalid ProductDataLevel.");
            }
        }

        public static ProductDataLevel ToProductDataLevel(this string code)
        {
            switch (code)
            {
                case "N":
                    return ProductDataLevel.Negative;
                case "C":
                    return ProductDataLevel.Comprehensive;
                default:
                    throw new ArgumentOutOfRangeException(nameof(code), code, "Invalid product data level code.");
            }
        }
    }
}
