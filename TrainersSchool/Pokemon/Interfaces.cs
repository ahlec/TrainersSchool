using System;
using System.Collections.Immutable;

namespace TrainersSchool.Pokemon
{
	public interface ITypeMatchup
	{
		PokemonType AttackingType { get; }

		PokemonType DefendingType { get; }

		ImmutableList<IMatchupResult> Matchups { get; }
	}

	public interface IMatchupResult
	{
		Generation GenerationStartingWith { get; }
		
		Effectiveness Effectiveness { get; }

		String Mnemonic { get; }
	}
}
