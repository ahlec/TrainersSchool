using System;
using System.ComponentModel;

namespace TrainersSchool.Pokemon
{
	public enum Effectiveness
	{
		[Description( "0×" )]
		None,

		[Description( "¼×" )]
		Quarter,

		[Description( "½×" )]
		Half,

		[Description( "1×" )]
		Normal,

		[Description( "2×" )]
		Double,

		[Description( "4×" )]
		Quadruple
	}
}