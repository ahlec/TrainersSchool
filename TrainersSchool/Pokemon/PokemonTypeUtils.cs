using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace TrainersSchool.Pokemon
{
	public static class PokemonTypeUtils
	{
		#region Internal classes
		private class MatchupResult : IMatchupResult
		{
			public MatchupResult( Generation generationStartingWith, Effectiveness effectiveness, String mnemonic )
			{
				GenerationStartingWith = generationStartingWith;
				Effectiveness = effectiveness;
				Mnemonic = mnemonic;
			}

			public Generation GenerationStartingWith { get; }

			public Effectiveness Effectiveness { get; }

			public String Mnemonic { get; }
		}

		private class TypeMatchup : ITypeMatchup
		{
			public TypeMatchup( PokemonType attackingType, PokemonType defendingType )
			{
				AttackingType = attackingType;
				DefendingType = defendingType;
			}

			/// <summary>
			/// Utility constructor for type matchups that haven't changed since their induction
			/// </summary>
			public TypeMatchup( PokemonType attackingType, PokemonType defendingType, Effectiveness effectiveness, String mnemonic )
				: this( attackingType, defendingType )
			{
				AddMatchup( GetFirstGenerationMatchupAvailable( attackingType, defendingType ), effectiveness, mnemonic );
			}

			public PokemonType AttackingType { get; }

			public PokemonType DefendingType { get; }

			public ImmutableList<IMatchupResult> Matchups { get; private set; } = ImmutableList<IMatchupResult>.Empty;

			public TypeMatchup AddMatchup( Generation generationStartingWith, Effectiveness effectiveness, String mnemonic )
			{
				Matchups = Matchups.Add( new MatchupResult( generationStartingWith, effectiveness, mnemonic ) );
				return this;
			}

			public override bool Equals( object obj )
			{
				TypeMatchup other = obj as TypeMatchup;
				if ( other == null )
				{
					return false;
				}

				return ( AttackingType == other.AttackingType && DefendingType == other.DefendingType );
			}

			public override int GetHashCode()
			{
				return GetHashCodeForTypeMatchup( AttackingType, DefendingType );
			}
		}
		#endregion

		public static Generation GetGenerationIntroducedIn( this PokemonType type )
		{
			return type.GetAttributeOfType<IntroducedInAttribute>()?.Generation ?? Generation.I;
		}

		public static Generation GetFirstGenerationMatchupAvailable( PokemonType typeA, PokemonType typeB )
		{
			Generation typeAGeneration = typeA.GetGenerationIntroducedIn();
			Generation typeBGeneration = typeB.GetGenerationIntroducedIn();

			if ( typeAGeneration < typeBGeneration )
			{
				return typeBGeneration;
			}

			if ( typeAGeneration > typeBGeneration )
			{
				return typeAGeneration;
			}

			return typeAGeneration;
		}

		public static Boolean WasAvailableIn( this PokemonType type, Generation generation )
		{
			Generation introducedIn = type.GetGenerationIntroducedIn();
			return ( introducedIn <= generation );
		}

		public static Int32 GetHashCodeForTypeMatchup( PokemonType attackingType, PokemonType defendingType )
		{
			return ( attackingType.GetHashCode() << 8 ) ^ defendingType.GetHashCode();
		}

		public static Effectiveness GetEffectivenessAgainst( PokemonType attackingType, PokemonType defendingType, Generation generation, out String mnemonic )
		{
			if ( !attackingType.WasAvailableIn( generation ) )
			{
				throw new ArgumentException( $"The {attackingType} type was not available in generation {generation}." );
			}

			if ( !defendingType.WasAvailableIn( generation ) )
			{
				throw new ArgumentException( $"The {defendingType} type was not available in generation {generation}." );
			}

			ITypeMatchup matchup = GetMatchupFor( attackingType, defendingType );
			if ( matchup == null )
			{
				mnemonic = null;
				return Effectiveness.Normal;
			}

			for ( Int32 index = matchup.Matchups.Count - 1; index >= 0; --index )
			{
				if ( matchup.Matchups[index].GenerationStartingWith <= generation )
				{
					mnemonic = matchup.Matchups[index].Mnemonic;
					return matchup.Matchups[index].Effectiveness;
				}
			}

			throw new ApplicationException();
		}

		public static ITypeMatchup GetMatchupFor( PokemonType attackingType, PokemonType defendingType )
		{
			Int32 requestHashCode = GetHashCodeForTypeMatchup( attackingType, defendingType );
			ITypeMatchup matchup;
			if ( !TypeMatchups.TryGetValue( requestHashCode, out matchup ) )
			{
				return null;
			}
			return matchup;
		}

		private static readonly ImmutableDictionary<Int32, ITypeMatchup> TypeMatchups = new HashSet<ITypeMatchup>
		{
			new TypeMatchup( PokemonType.Normal, PokemonType.Ghost, Effectiveness.None, "Different planes of existence" ),
			new TypeMatchup( PokemonType.Normal, PokemonType.Rock, Effectiveness.Half, "" ),
			new TypeMatchup( PokemonType.Normal, PokemonType.Steel, Effectiveness.Half, "" ),

			new TypeMatchup( PokemonType.Fighting, PokemonType.Ghost, Effectiveness.None, "You can't punch a ghost" ),
			new TypeMatchup( PokemonType.Fighting, PokemonType.Psychic, Effectiveness.Half, "Brains over brawn" ),
			new TypeMatchup( PokemonType.Fighting, PokemonType.Flying, Effectiveness.Half, "" ),
			new TypeMatchup( PokemonType.Fighting, PokemonType.Poison, Effectiveness.Half, "" ),
			new TypeMatchup( PokemonType.Fighting, PokemonType.Bug, Effectiveness.Half, "" ),
			new TypeMatchup( PokemonType.Fighting, PokemonType.Fairy, Effectiveness.Half, "" ),
			new TypeMatchup( PokemonType.Fighting, PokemonType.Rock, Effectiveness.Double, "Rock Smash breaks rocks" ),
			new TypeMatchup( PokemonType.Fighting, PokemonType.Normal, Effectiveness.Double, "" ),
			new TypeMatchup( PokemonType.Fighting, PokemonType.Steel, Effectiveness.Double, "" ),
			new TypeMatchup( PokemonType.Fighting, PokemonType.Ice, Effectiveness.Double, "" ),
			new TypeMatchup( PokemonType.Fighting, PokemonType.Dark, Effectiveness.Double, "Fighting evil is good" ),

			new TypeMatchup( PokemonType.Flying, PokemonType.Rock, Effectiveness.Half, "" ),
			new TypeMatchup( PokemonType.Flying, PokemonType.Steel, Effectiveness.Half, "" ),
			new TypeMatchup( PokemonType.Flying, PokemonType.Electric, Effectiveness.Half, "" ),
			new TypeMatchup( PokemonType.Flying, PokemonType.Fighting, Effectiveness.Double, "" ),
			new TypeMatchup( PokemonType.Flying, PokemonType.Bug, Effectiveness.Double, "" ),
			new TypeMatchup( PokemonType.Flying, PokemonType.Grass, Effectiveness.Double, "" ),

			new TypeMatchup( PokemonType.Poison, PokemonType.Steel, Effectiveness.None, "" ),
			new TypeMatchup( PokemonType.Poison, PokemonType.Poison, Effectiveness.Half, "" ),
			new TypeMatchup( PokemonType.Poison, PokemonType.Ground, Effectiveness.Half, "" ),
			new TypeMatchup( PokemonType.Poison, PokemonType.Rock, Effectiveness.Half, "" ),
			new TypeMatchup( PokemonType.Poison, PokemonType.Ghost, Effectiveness.Half, "" ),
			new TypeMatchup( PokemonType.Poison, PokemonType.Bug ).AddMatchup( Generation.I, Effectiveness.Double, "" ).AddMatchup( Generation.II, Effectiveness.Normal, "" ),
			new TypeMatchup( PokemonType.Poison, PokemonType.Grass, Effectiveness.Double, "" ),
			new TypeMatchup( PokemonType.Poison, PokemonType.Fairy, Effectiveness.Double, "" ),

			new TypeMatchup( PokemonType.Ground, PokemonType.Flying, Effectiveness.None, "" ),
			new TypeMatchup( PokemonType.Ground, PokemonType.Bug, Effectiveness.Half, "" ),
			new TypeMatchup( PokemonType.Ground, PokemonType.Grass, Effectiveness.Half, "" ),
			new TypeMatchup( PokemonType.Ground, PokemonType.Poison, Effectiveness.Double, "" ),
			new TypeMatchup( PokemonType.Ground, PokemonType.Rock, Effectiveness.Double, "" ),
			new TypeMatchup( PokemonType.Ground, PokemonType.Steel, Effectiveness.Double, "" ),
			new TypeMatchup( PokemonType.Ground, PokemonType.Fire, Effectiveness.Double, "" ),
			new TypeMatchup( PokemonType.Ground, PokemonType.Electric, Effectiveness.Double, "" ),

			new TypeMatchup( PokemonType.Rock, PokemonType.Fighting, Effectiveness.Half, "" ),
			new TypeMatchup( PokemonType.Rock, PokemonType.Ground, Effectiveness.Half, "" ),
			new TypeMatchup( PokemonType.Rock, PokemonType.Steel, Effectiveness.Half, "" ),
			new TypeMatchup( PokemonType.Rock, PokemonType.Flying, Effectiveness.Double, "Two birds with one stone" ),
			new TypeMatchup( PokemonType.Rock, PokemonType.Bug, Effectiveness.Double, "" ),
			new TypeMatchup( PokemonType.Rock, PokemonType.Fire, Effectiveness.Double, "" ),
			new TypeMatchup( PokemonType.Rock, PokemonType.Ice, Effectiveness.Double, "" ),

			new TypeMatchup( PokemonType.Bug, PokemonType.Fighting, Effectiveness.Half, "" ),
			new TypeMatchup( PokemonType.Bug, PokemonType.Flying, Effectiveness.Half, "" ),
			new TypeMatchup( PokemonType.Bug, PokemonType.Ghost, Effectiveness.Half, "" ),
			new TypeMatchup( PokemonType.Bug, PokemonType.Steel, Effectiveness.Half, "" ),
			new TypeMatchup( PokemonType.Bug, PokemonType.Fire, Effectiveness.Half, "" ),
			new TypeMatchup( PokemonType.Bug, PokemonType.Fairy, Effectiveness.Half, "" ),
			new TypeMatchup( PokemonType.Bug, PokemonType.Poison ).AddMatchup( Generation.I, Effectiveness.Double, "" ).AddMatchup( Generation.II, Effectiveness.Half, "" ),
			new TypeMatchup( PokemonType.Bug, PokemonType.Grass, Effectiveness.Double, "Bugs eat leaves" ),
			new TypeMatchup( PokemonType.Bug, PokemonType.Psychic, Effectiveness.Double, "An irrational fear of bugs" ),
			new TypeMatchup( PokemonType.Bug, PokemonType.Dark, Effectiveness.Double, "" ),

			new TypeMatchup( PokemonType.Ghost, PokemonType.Normal, Effectiveness.None, "Different planes of existence" ),
			new TypeMatchup( PokemonType.Ghost, PokemonType.Dark, Effectiveness.Half, "" ),
			new TypeMatchup( PokemonType.Ghost, PokemonType.Steel ).AddMatchup( Generation.II, Effectiveness.Half, "" ).AddMatchup( Generation.VI, Effectiveness.Normal, "" ),
			new TypeMatchup( PokemonType.Ghost, PokemonType.Psychic ).AddMatchup( Generation.I, Effectiveness.None, "" ).AddMatchup( Generation.II, Effectiveness.Double, "" ),
			new TypeMatchup( PokemonType.Ghost, PokemonType.Ghost, Effectiveness.Double, "" ),

			new TypeMatchup( PokemonType.Steel, PokemonType.Steel, Effectiveness.Half, "" ),
			new TypeMatchup( PokemonType.Steel, PokemonType.Fire, Effectiveness.Half, "" ),
			new TypeMatchup( PokemonType.Steel, PokemonType.Water, Effectiveness.Half, "" ),
			new TypeMatchup( PokemonType.Steel, PokemonType.Electric, Effectiveness.Half, "" ),
			new TypeMatchup( PokemonType.Steel, PokemonType.Rock, Effectiveness.Double, "" ),
			new TypeMatchup( PokemonType.Steel, PokemonType.Ice, Effectiveness.Double, "" ),
			new TypeMatchup( PokemonType.Steel, PokemonType.Fairy, Effectiveness.Double, "" ),

			new TypeMatchup( PokemonType.Fire, PokemonType.Rock, Effectiveness.Half, "" ),
			new TypeMatchup( PokemonType.Fire, PokemonType.Fire, Effectiveness.Half, "" ),
			new TypeMatchup( PokemonType.Fire, PokemonType.Water, Effectiveness.Half, "" ),
			new TypeMatchup( PokemonType.Fire, PokemonType.Dragon, Effectiveness.Half, "" ),
			new TypeMatchup( PokemonType.Fire, PokemonType.Bug, Effectiveness.Double, "" ),
			new TypeMatchup( PokemonType.Fire, PokemonType.Steel, Effectiveness.Double, "Smoldering forges shape metal" ),
			new TypeMatchup( PokemonType.Fire, PokemonType.Grass, Effectiveness.Double, "" ),
			new TypeMatchup( PokemonType.Fire, PokemonType.Ice, Effectiveness.Double, "Ice melts" ),

			new TypeMatchup( PokemonType.Water, PokemonType.Water, Effectiveness.Half, "" ),
			new TypeMatchup( PokemonType.Water, PokemonType.Grass, Effectiveness.Half, "" ),
			new TypeMatchup( PokemonType.Water, PokemonType.Dragon, Effectiveness.Half, "" ),
			new TypeMatchup( PokemonType.Water, PokemonType.Ground, Effectiveness.Double, "" ),
			new TypeMatchup( PokemonType.Water, PokemonType.Rock, Effectiveness.Double, "" ),
			new TypeMatchup( PokemonType.Water, PokemonType.Fire, Effectiveness.Double, "" ),

			new TypeMatchup( PokemonType.Grass, PokemonType.Flying, Effectiveness.Half, "" ),
			new TypeMatchup( PokemonType.Grass, PokemonType.Poison, Effectiveness.Half, "" ),
			new TypeMatchup( PokemonType.Grass, PokemonType.Bug, Effectiveness.Half, "" ),
			new TypeMatchup( PokemonType.Grass, PokemonType.Steel, Effectiveness.Half, "" ),
			new TypeMatchup( PokemonType.Grass, PokemonType.Fire, Effectiveness.Half, "" ),
			new TypeMatchup( PokemonType.Grass, PokemonType.Grass, Effectiveness.Half, "" ),
			new TypeMatchup( PokemonType.Grass, PokemonType.Dragon, Effectiveness.Half, "" ),
			new TypeMatchup( PokemonType.Grass, PokemonType.Ground, Effectiveness.Double, "Roots can burrow through any soil" ),
			new TypeMatchup( PokemonType.Grass, PokemonType.Rock, Effectiveness.Double, "Roots can burrow through any soil" ),
			new TypeMatchup( PokemonType.Grass, PokemonType.Water, Effectiveness.Double, "" ),

			new TypeMatchup( PokemonType.Electric, PokemonType.Ground, Effectiveness.None, "" ),
			new TypeMatchup( PokemonType.Electric, PokemonType.Grass, Effectiveness.Half, "" ),
			new TypeMatchup( PokemonType.Electric, PokemonType.Electric, Effectiveness.Half, "" ),
			new TypeMatchup( PokemonType.Electric, PokemonType.Dragon, Effectiveness.Half, "" ),
			new TypeMatchup( PokemonType.Electric, PokemonType.Flying, Effectiveness.Double, "Flying through a storm can kill you" ),
			new TypeMatchup( PokemonType.Electric, PokemonType.Water, Effectiveness.Double, "Water is a superconductor" ),

			new TypeMatchup( PokemonType.Psychic, PokemonType.Dark, Effectiveness.None, "Scared of the dark" ),
			new TypeMatchup( PokemonType.Psychic, PokemonType.Steel, Effectiveness.Half, "" ),
			new TypeMatchup( PokemonType.Psychic, PokemonType.Psychic, Effectiveness.Half, "" ),
			new TypeMatchup( PokemonType.Psychic, PokemonType.Fighting, Effectiveness.Double, "" ),
			new TypeMatchup( PokemonType.Psychic, PokemonType.Poison, Effectiveness.Double, "" ),

			new TypeMatchup( PokemonType.Ice, PokemonType.Steel, Effectiveness.Half, "" ),
			new TypeMatchup( PokemonType.Ice, PokemonType.Fire, Effectiveness.Half, "" ),
			new TypeMatchup( PokemonType.Ice, PokemonType.Water, Effectiveness.Half, "" ),
			new TypeMatchup( PokemonType.Ice, PokemonType.Ice ).AddMatchup( Generation.I, Effectiveness.Normal, "" ).AddMatchup( Generation.II, Effectiveness.Half, "" ),
			new TypeMatchup( PokemonType.Ice, PokemonType.Flying, Effectiveness.Double, "Flying through a storm can kill you" ),
			new TypeMatchup( PokemonType.Ice, PokemonType.Ground, Effectiveness.Double, "" ),
			new TypeMatchup( PokemonType.Ice, PokemonType.Grass, Effectiveness.Double, "" ),
			new TypeMatchup( PokemonType.Ice, PokemonType.Dragon, Effectiveness.Double, "" ),

			new TypeMatchup( PokemonType.Dragon, PokemonType.Fairy, Effectiveness.None, "" ),
			new TypeMatchup( PokemonType.Dragon, PokemonType.Steel, Effectiveness.Half, "" ),
			new TypeMatchup( PokemonType.Dragon, PokemonType.Dragon, Effectiveness.Double, "" ),

			new TypeMatchup( PokemonType.Dark, PokemonType.Fighting, Effectiveness.Half, "Fighting evil is good" ),
			new TypeMatchup( PokemonType.Dark, PokemonType.Dark, Effectiveness.Half, "" ),
			new TypeMatchup( PokemonType.Dark, PokemonType.Fairy, Effectiveness.Half, "" ),
			new TypeMatchup( PokemonType.Dark, PokemonType.Steel ).AddMatchup( Generation.II, Effectiveness.Half, "" ).AddMatchup( Generation.VI, Effectiveness.Normal, "" ),
			new TypeMatchup( PokemonType.Dark, PokemonType.Ghost, Effectiveness.Double, "" ),
			new TypeMatchup( PokemonType.Dark, PokemonType.Psychic, Effectiveness.Double, "Scared of the dark" ),

			new TypeMatchup( PokemonType.Fairy, PokemonType.Poison, Effectiveness.Half, "" ),
			new TypeMatchup( PokemonType.Fairy, PokemonType.Steel, Effectiveness.Half, "" ),
			new TypeMatchup( PokemonType.Fairy, PokemonType.Fire, Effectiveness.Half, "" ),
			new TypeMatchup( PokemonType.Fairy, PokemonType.Fighting, Effectiveness.Double, "" ),
			new TypeMatchup( PokemonType.Fairy, PokemonType.Dragon, Effectiveness.Double, "" ),
			new TypeMatchup( PokemonType.Fairy, PokemonType.Dark, Effectiveness.Double, "" )
		}.ToImmutableDictionary( matchup => matchup.GetHashCode() );
	}
}
