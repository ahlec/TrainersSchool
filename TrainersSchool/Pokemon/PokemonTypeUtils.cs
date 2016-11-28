using System;

namespace TrainersSchool.Pokemon
{
	public static class PokemonTypeUtils
	{
		public static Boolean WasAvailableIn( this PokemonType type, Generation generation )
		{
			Generation introducedIn = type.GetAttributeOfType<IntroducedInAttribute>()?.Generation ?? Generation.I;
			return ( introducedIn <= generation );
		}

		public static Effectiveness GetEffectivenessAgainst( this PokemonType type, PokemonType againstType, Generation generation )
		{
			if ( !type.WasAvailableIn( generation ) )
			{
				throw new ArgumentException( $"The {type} type was not available in generation {generation}." );
			}

			if ( !againstType.WasAvailableIn( generation ) )
			{
				throw new ArgumentException( $"The {againstType} type was not available in generation {generation}." );
			}

			switch ( type )
			{
				case PokemonType.Normal:
					{
						switch ( againstType )
						{
							case PokemonType.Ghost:
								return Effectiveness.None;
							case PokemonType.Rock:
							case PokemonType.Steel:
								return Effectiveness.Half;
							default:
								return Effectiveness.Normal;
						}
					}
				case PokemonType.Fighting:
					{
						switch ( againstType )
						{
							case PokemonType.Ghost:
								return Effectiveness.None;
							case PokemonType.Flying:
							case PokemonType.Poison:
							case PokemonType.Bug:
							case PokemonType.Psychic:
							case PokemonType.Fairy:
								return Effectiveness.Half;
							case PokemonType.Normal:
							case PokemonType.Rock:
							case PokemonType.Steel:
							case PokemonType.Ice:
							case PokemonType.Dark:
								return Effectiveness.Double;
							default:
								return Effectiveness.Normal;
						}
					}

				case PokemonType.Flying:
					{
						switch ( againstType )
						{
							case PokemonType.Rock:
							case PokemonType.Steel:
							case PokemonType.Electric:
								return Effectiveness.Half;
							case PokemonType.Fighting:
							case PokemonType.Bug:
							case PokemonType.Grass:
								return Effectiveness.Double;
							default:
								return Effectiveness.Normal;
						}
					}

				case PokemonType.Poison:
					{
						switch ( againstType )
						{
							case PokemonType.Steel:
								return Effectiveness.None;
							case PokemonType.Poison:
							case PokemonType.Ground:
							case PokemonType.Rock:
							case PokemonType.Ghost:
								return Effectiveness.Half;
							case PokemonType.Bug:
								return ( generation == Generation.I ? Effectiveness.Double : Effectiveness.Normal );
							case PokemonType.Grass:
							case PokemonType.Fairy:
								return Effectiveness.Double;
							default:
								return Effectiveness.Normal;
						}
					}

				case PokemonType.Ground:
					{
						switch ( againstType )
						{
							case PokemonType.Flying:
								return Effectiveness.None;
							case PokemonType.Bug:
							case PokemonType.Grass:
								return Effectiveness.Half;
							case PokemonType.Poison:
							case PokemonType.Rock:
							case PokemonType.Steel:
							case PokemonType.Fire:
							case PokemonType.Electric:
								return Effectiveness.Double;
							default:
								return Effectiveness.Normal;
						}
					}

				case PokemonType.Rock:
					{
						switch ( againstType )
						{
							case PokemonType.Fighting:
							case PokemonType.Ground:
							case PokemonType.Steel:
								return Effectiveness.Half;
							case PokemonType.Flying:
							case PokemonType.Bug:
							case PokemonType.Fire:
							case PokemonType.Ice:
								return Effectiveness.Double;
							default:
								return Effectiveness.Normal;
						}
					}

				case PokemonType.Bug:
					{
						switch ( againstType )
						{
							case PokemonType.Fighting:
							case PokemonType.Flying:
							case PokemonType.Ghost:
							case PokemonType.Steel:
							case PokemonType.Fire:
							case PokemonType.Fairy:
								return Effectiveness.Half;
							case PokemonType.Poison:
								return ( generation == Generation.I ? Effectiveness.Double : Effectiveness.Half );
							case PokemonType.Grass:
							case PokemonType.Psychic:
							case PokemonType.Dark:
								return Effectiveness.Double;
							default:
								return Effectiveness.Normal;
						}
					}

				case PokemonType.Ghost:
					{
						switch ( againstType )
						{
							case PokemonType.Normal:
								return Effectiveness.None;
							case PokemonType.Dark:
								return Effectiveness.Half;
							case PokemonType.Steel:
								return ( generation < Generation.VI ? Effectiveness.Half : Effectiveness.Normal );
							case PokemonType.Psychic:
								return ( generation == Generation.I ? Effectiveness.None : Effectiveness.Double );
							case PokemonType.Ghost:
								return Effectiveness.Double;
							default:
								return Effectiveness.Normal;
						}
					}

				case PokemonType.Steel:
					{
						switch ( againstType )
						{
							case PokemonType.Steel:
							case PokemonType.Fire:
							case PokemonType.Water:
							case PokemonType.Electric:
								return Effectiveness.Half;
							case PokemonType.Rock:
							case PokemonType.Ice:
							case PokemonType.Fairy:
								return Effectiveness.Double;
							default:
								return Effectiveness.Normal;
						}
					}

				case PokemonType.Fire:
					{
						switch ( againstType )
						{
							case PokemonType.Rock:
							case PokemonType.Fire:
							case PokemonType.Water:
							case PokemonType.Dragon:
								return Effectiveness.Half;
							case PokemonType.Bug:
							case PokemonType.Steel:
							case PokemonType.Grass:
							case PokemonType.Ice:
								return Effectiveness.Double;
							default:
								return Effectiveness.Normal;
						}
					}

				case PokemonType.Water:
					{
						switch ( againstType )
						{
							case PokemonType.Water:
							case PokemonType.Grass:
							case PokemonType.Dragon:
								return Effectiveness.Half;
							case PokemonType.Ground:
							case PokemonType.Rock:
							case PokemonType.Fire:
								return Effectiveness.Double;
							default:
								return Effectiveness.Normal;
						}
					}

				case PokemonType.Grass:
					{
						switch ( againstType )
						{
							case PokemonType.Flying:
							case PokemonType.Poison:
							case PokemonType.Bug:
							case PokemonType.Steel:
							case PokemonType.Fire:
							case PokemonType.Grass:
							case PokemonType.Dragon:
								return Effectiveness.Half;
							case PokemonType.Ground:
							case PokemonType.Rock:
							case PokemonType.Water:
								return Effectiveness.Double;
							default:
								return Effectiveness.Normal;
						}
					}

				case PokemonType.Electric:
					{
						switch ( againstType )
						{
							case PokemonType.Ground:
								return Effectiveness.None;
							case PokemonType.Grass:
							case PokemonType.Electric:
							case PokemonType.Dragon:
								return Effectiveness.Half;
							case PokemonType.Flying:
							case PokemonType.Water:
								return Effectiveness.Double;
							default:
								return Effectiveness.Normal;
						}
					}

				case PokemonType.Psychic:
					{
						switch ( againstType )
						{
							case PokemonType.Dark:
								return Effectiveness.None;
							case PokemonType.Steel:
							case PokemonType.Psychic:
								return Effectiveness.Half;
							case PokemonType.Fighting:
							case PokemonType.Poison:
								return Effectiveness.Double;
							default:
								return Effectiveness.Normal;
						}
					}

				case PokemonType.Ice:
					{
						switch ( againstType )
						{
							case PokemonType.Steel:
							case PokemonType.Fire:
							case PokemonType.Water:
								return Effectiveness.Half;
							case PokemonType.Ice:
								return ( generation == Generation.I ? Effectiveness.Normal : Effectiveness.Half );
							case PokemonType.Flying:
							case PokemonType.Ground:
							case PokemonType.Grass:
							case PokemonType.Dragon:
								return Effectiveness.Double;
							default:
								return Effectiveness.Normal;
						}
					}

				case PokemonType.Dragon:
					{
						switch ( againstType )
						{
							case PokemonType.Fairy:
								return Effectiveness.None;
							case PokemonType.Steel:
								return Effectiveness.Half;
							case PokemonType.Dragon:
								return Effectiveness.Double;
							default:
								return Effectiveness.Normal;
						}
					}

				case PokemonType.Dark:
					{
						switch ( againstType )
						{
							case PokemonType.Fighting:
							case PokemonType.Dark:
							case PokemonType.Fairy:
								return Effectiveness.Half;
							case PokemonType.Steel:
								return ( generation < Generation.VI ? Effectiveness.Half : Effectiveness.Normal );
							case PokemonType.Ghost:
							case PokemonType.Psychic:
								return Effectiveness.Double;
							default:
								return Effectiveness.Normal;
						}
					}

				case PokemonType.Fairy:
					{
						switch ( againstType )
						{
							case PokemonType.Poison:
							case PokemonType.Steel:
							case PokemonType.Fire:
								return Effectiveness.Half;
							case PokemonType.Fighting:
							case PokemonType.Dragon:
							case PokemonType.Dark:
								return Effectiveness.Double;
							default:
								return Effectiveness.Normal;
						}
					}
			}

			throw new NotImplementedException();
		}
	}
}
