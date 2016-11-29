using System;
using System.Windows.Input;
using TrainersSchool.Pokemon;

namespace TrainersSchool.ViewModels
{
	public class SessionViewModel : ViewModel
	{
		public SessionViewModel()
		{
			SubmitAnswerCommand = new Command( null, CommandSubmitAnswer );
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

		public ICommand SubmitAnswerCommand { get; }
		
		void CommandSubmitAnswer( Object answerObj )
		{
			Effectiveness answer = (Effectiveness) answerObj;

			if ( answer == Effectiveness.Double )
			{
				NumberAnswersCorrect++;
			}

			NumberQuestionsAnswered++;
		}

		Int32 _numberAnswersCorrect;
		Int32 _numberQuestionsAnswered;
	}
}
