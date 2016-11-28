using System;

namespace TrainersSchool.Pokemon
{
	public enum Generation
	{
		I,
		II,
		III,
		IV,
		V,
		VI,
		VII
	}

	[AttributeUsage( AttributeTargets.Field )]
	public class IntroducedInAttribute : Attribute
	{
		public IntroducedInAttribute( Generation generation )
		{
			Generation = generation;
		}

		public Generation Generation { get; }
	}
}
