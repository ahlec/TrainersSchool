using System;

namespace TrainersSchool.Pokemon
{
	public static class EffectivenessUtils
	{
		public static Effectiveness Multiply( Effectiveness a, Effectiveness b )
		{
			if ( a == Effectiveness.None || b == Effectiveness.None )
			{
				return Effectiveness.None;
			}

			Int32 shiftAmount = ( b - Effectiveness.Normal ); // Effectiveness.Half => -1, Effectiveness.Quadruple => +2
			Int32 resultInt = (Int32) a + shiftAmount;
			if ( resultInt <= (Int32) Effectiveness.None || resultInt > (Int32) Effectiveness.Quadruple )
			{
				throw new InvalidOperationException( $"Encountered an invalid {nameof( Effectiveness )} value: {resultInt} created by {a} and {b}." );
			}
			return (Effectiveness) resultInt;
		}
	}
}
