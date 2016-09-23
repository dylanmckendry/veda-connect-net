using System;
using VedaConnect.VedaScoreApply;

namespace VedaConnect
{
    public enum State
    {
        ACT,
        NSW,
        NT,
        NZ,
        OS,
        OTH,
        QLD,
        SA,
        TAS,
        VIC,
        WA,
    }

    internal static class StateExtensions
    {
        public static State? ToState(this AustralianStateType state)
        {
            State value;
            if (Enum.TryParse(state.ToString(), true, out value)) return value;

            return null;
        }
        public static AustralianStateType ToAustralianStateType(this State state)
        {
            AustralianStateType value;
            if (Enum.TryParse(state.ToString(), true, out value)) return value;

            return AustralianStateType.OTH;
        }
    }
}