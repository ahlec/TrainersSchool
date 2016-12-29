using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrainersSchool.Pokemon;

namespace TrainersSchoolTests
{
	[TestClass]
	public class PokemonTypeUtilsTests
	{
		[TestMethod]
		[TestCategory( nameof( PokemonTypeUtils ) )]
		[Description( "Ensures that for every two types defined in the type chart, there is a type matchup for every generation both types existed in" )]
		public void MatchupsHaveFullCoverage()
		{
			foreach ( PokemonType attackingType in Enum.GetValues( typeof( PokemonType ) ) )
			{
				foreach ( PokemonType defendingType in Enum.GetValues( typeof( PokemonType ) ) )
				{
					ITypeMatchup matchup = PokemonTypeUtils.GetMatchupFor( attackingType, defendingType );
					if ( matchup == null )
					{
						continue;
					}

					Generation firstGeneration = PokemonTypeUtils.GetFirstGenerationMatchupAvailable( attackingType, defendingType );
					Assert.AreEqual( firstGeneration, matchup.Matchups[0].GenerationStartingWith, $"Matchup expected {firstGeneration} but was met with {matchup.Matchups[0].GenerationStartingWith} for the matchup of {attackingType}x{defendingType}" );
				}
			}
		}

		[TestMethod]
		[TestCategory( nameof( PokemonTypeUtils ) )]
		[Description( "Ensures that there is a mnemonic for every type matchup that isn't 1x" )]
		public void MatchupsAllHaveMnemonics()
		{
			foreach ( PokemonType attackingType in Enum.GetValues( typeof( PokemonType ) ) )
			{
				foreach ( PokemonType defendingType in Enum.GetValues( typeof( PokemonType ) ) )
				{
					Generation firstGeneration = PokemonTypeUtils.GetFirstGenerationMatchupAvailable( attackingType, defendingType );

					foreach ( Generation generation in Enum.GetValues( typeof( Generation ) ) )
					{
						if ( generation < firstGeneration )
						{
							continue;
						}

						String mnemonic;
						Effectiveness effectiveness = PokemonTypeUtils.GetEffectivenessAgainst( attackingType, defendingType, generation, out mnemonic );
						if ( effectiveness == Effectiveness.Normal )
						{
							continue;
						}

						Assert.IsFalse( String.IsNullOrEmpty( mnemonic ), $"There is no mnemonic for the Gen{generation} type matchup of {attackingType}x{defendingType}" );
					}
				}
			}
		}
	}
}
