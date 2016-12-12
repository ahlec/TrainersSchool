using System;
using System.Collections.Generic;
using TrainersSchool.Pokemon;

namespace TrainersSchool.ViewModels.Components
{
	public sealed class QuestionAndAnswer
	{
		class QuestionAndAnswerComparer : IEqualityComparer<QuestionAndAnswer>
		{
			public bool Equals( QuestionAndAnswer a, QuestionAndAnswer b )
			{
				return ( a.CorrectAnswer == b.CorrectAnswer );
			}

			public int GetHashCode( QuestionAndAnswer a )
			{
				return a.CorrectAnswer.GetHashCode();
			}
		}

		public QuestionAndAnswer( PokemonType attackingType, PokemonType defendingType, Generation generation )
		{
			AttackingType = attackingType;
			DefendingType = defendingType;
			Generation = generation;
			CorrectAnswer = PokemonTypeUtils.GetEffectivenessAgainst( attackingType, defendingType, generation );
		}

		public PokemonType AttackingType { get; }

		public PokemonType DefendingType { get; }

		public Boolean IsGenerationImportant { get; set; } = true;

		public Generation Generation { get; }

		public Effectiveness CorrectAnswer { get; }

		public Effectiveness SubmittedAnswer { get; set; }

		public Boolean WasUserCorrect => ( CorrectAnswer == SubmittedAnswer );

		public Int32 GroupingKey => ( AttackingType.GetHashCode() ^ DefendingType.GetHashCode() );

		public static IEqualityComparer<QuestionAndAnswer> Comparer { get; } = new QuestionAndAnswerComparer();
	}
}
