using System;
using VedaConnect.VedaScoreApply;

namespace VedaConnect
{
    public enum CurrentOrPrevious
    {
        Current,
        Previous
    }

    internal static class CurrentOrPreviousExtensions
    {
        public static currentPreviousType ToCurrentPreviousType(this CurrentOrPrevious type)
        {
            switch (type)
            {
                case CurrentOrPrevious.Current:
                    return currentPreviousType.C;
                case CurrentOrPrevious.Previous:
                    return currentPreviousType.P;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, "Invalid value for CurrentOrPrevious.");
            }
        }

        public static CurrentOrPrevious ToCurrentOrPrevious(this currentPreviousType type)
        {
            switch (type)
            {
                case currentPreviousType.C:
                    return CurrentOrPrevious.Current;
                case currentPreviousType.P:
                    return CurrentOrPrevious.Previous;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, "Invalid currentPreviousType.");
            }
        }
    }
}