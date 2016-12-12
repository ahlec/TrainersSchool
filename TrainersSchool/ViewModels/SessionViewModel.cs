using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Windows.Input;
using TrainersSchool.Pokemon;
using TrainersSchool.ViewModels.Components;

namespace TrainersSchool.ViewModels
{
	public delegate void QuestionAnsweredEventHandler( QuestionAndAnswer questionAnswered );

	public class SessionViewModel : ViewModel
	{
		public SessionViewModel()
		{
			SubmitAnswerCommand = new Command( null, CommandSubmitAnswer );
			CurrentQuestion = SelectNextQuestion();
		}

		public Int32 NumberAnswersCorrect
		{
			get { return _numberAnswersCorrect; }
			set { SetProperty( ref _numberAnswersCorrect, value ); }
		}

		public Int32 NumberQuestionsAnswered
		{
			get { return _numberQuestionsAnswered; }
			set { SetProperty( ref _numberQuestionsAnswered, value ); }
		}

		public ImmutableList<QuestionAndAnswer> AnswerHistory
		{
			get { return _answerHistory; }
			private set { SetProperty( ref _answerHistory, value ); }
		}

		public QuestionAndAnswer CurrentQuestion
		{
			get { return _currentQuestion; }
			private set { SetProperty( ref _currentQuestion, value ); }
		}

		#region Commands

		public ICommand SubmitAnswerCommand { get; }

		void CommandSubmitAnswer( Object answerObj )
		{
			Effectiveness answer = (Effectiveness) answerObj;
			QuestionAndAnswer question = CurrentQuestion;
			question.SubmittedAnswer = answer;

			QuestionAnswered?.Invoke( question );

			if ( question.WasUserCorrect )
			{
				NumberAnswersCorrect++;
			}

			NumberQuestionsAnswered++;

			AnswerHistory = AnswerHistory.Add( question );
			CurrentQuestion = SelectNextQuestion();
		}

		public event QuestionAnsweredEventHandler QuestionAnswered;

		#endregion

		QuestionAndAnswer SelectNextQuestion()
		{
			// Populate the question list if it's important
			if ( _upcomingQuestions.IsEmpty )
			{
				// Generate all possible combinations
				List<QuestionAndAnswer> allCombinations = new List<QuestionAndAnswer>();
				foreach ( Generation generation in Enum.GetValues( typeof( Generation ) ) )
				{
					foreach ( PokemonType attackingType in Enum.GetValues( typeof( PokemonType ) ) )
					{
						if ( !attackingType.WasAvailableIn( generation ) )
						{
							continue;
						}

						foreach ( PokemonType defendingType in Enum.GetValues( typeof( PokemonType ) ) )
						{
							if ( !defendingType.WasAvailableIn( generation ) )
							{
								continue;
							}

							allCombinations.Add( new QuestionAndAnswer( attackingType, defendingType, generation ) );
						}
					}
				}

				// Group together by both types and effectiveness and consider if generation is important
				var builder = ImmutableList<QuestionAndAnswer>.Empty.ToBuilder();
				foreach ( IGrouping<Int32, QuestionAndAnswer> collections in allCombinations.GroupBy( question => question.GroupingKey ) )
				{
					int numberQuestionsAdded = 0;
					foreach ( QuestionAndAnswer uniqueQuestion in collections.Distinct( QuestionAndAnswer.Comparer ) )
					{
						builder.Add( uniqueQuestion );
						++numberQuestionsAdded;
					}

					if ( numberQuestionsAdded == 1 )
					{
						builder[builder.Count - 1].IsGenerationImportant = false;
					}
				}

				_upcomingQuestions = builder.ToImmutableList();
			}

			// Get the next question
			Int32 randomIndex = _random.Next( _upcomingQuestions.Count );
			QuestionAndAnswer nextQuestion = _upcomingQuestions[randomIndex];
			_upcomingQuestions = _upcomingQuestions.RemoveAt( randomIndex );
			return nextQuestion;
		}

		static readonly Random _random = new Random();
		Int32 _numberAnswersCorrect;
		Int32 _numberQuestionsAnswered;
		QuestionAndAnswer _currentQuestion;
		ImmutableList<QuestionAndAnswer> _answerHistory = ImmutableList<QuestionAndAnswer>.Empty;
		ImmutableList<QuestionAndAnswer> _upcomingQuestions = ImmutableList<QuestionAndAnswer>.Empty;
	}
}
