using System;
using System.Windows;
using System.Windows.Input;

namespace TrainersSchool.Controls
{
	public partial class AnswerButton
	{
		public AnswerButton()
		{
			InitializeComponent();
		}

		public ICommand Command
		{
			get { return (ICommand) GetValue( CommandProperty ); }
			set { SetValue( CommandProperty, value ); }
		}

		public Object CommandParameter
		{
			get { return GetValue( CommandParameterProperty ); }
			set { SetValue( CommandParameterProperty, value ); }
		}

		public String Text
		{
			get { return (String) GetValue( TextProperty ); }
			set { SetValue( TextProperty, value ); }
		}

		public AnswerButton PreviousButton
		{
			get { return (AnswerButton) GetValue( PreviousButtonProperty ); }
			set { SetValue( PreviousButtonProperty, value ); }
		}

		public AnswerButton NextButton
		{
			get { return (AnswerButton) GetValue( NextButtonProperty ); }
			set { SetValue( NextButtonProperty, value ); }
		}

		public static readonly DependencyProperty CommandProperty = DependencyProperty.Register( "Command", typeof( ICommand ),
			typeof( AnswerButton ) );
		public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.Register( "CommandParameter", typeof( Object ),
			typeof( AnswerButton ) );
		public static readonly DependencyProperty TextProperty = DependencyProperty.Register( "Text", typeof( String ),
			typeof( AnswerButton ) );
		public static readonly DependencyProperty PreviousButtonProperty = DependencyProperty.Register( "PreviousButton",
			typeof( AnswerButton ),
			typeof( AnswerButton ) );
		public static readonly DependencyProperty NextButtonProperty = DependencyProperty.Register( "NextButton",
			typeof( AnswerButton ),
			typeof( AnswerButton ) );

		private void OnMouseDown( object sender, MouseButtonEventArgs e )
		{
			Invoke();
		}

		protected override void OnMouseEnter( MouseEventArgs e )
		{
			Focus();
			base.OnMouseEnter( e );
		}

		protected override void OnGotFocus( RoutedEventArgs e )
		{
			Keyboard.Focus( this );
			base.OnGotFocus( e );
		}

		protected override void OnKeyDown( KeyEventArgs e )
		{
			if ( e.Key == Key.Left || e.Key == Key.A )
			{
				PreviousButton.Focus();
			}
			else if ( e.Key == Key.Right || e.Key == Key.D )
			{
				NextButton.Focus();
			}
			else if ( e.Key == Key.Enter || e.Key == Key.Space )
			{
				Invoke();
			}

			base.OnKeyDown( e );
		}

		private void Invoke()
		{
			ICommand command = Command;
			if ( command == null )
			{
				return;
			}

			Object parameter = CommandParameter;
			if ( command.CanExecute( parameter ) )
			{
				command.Execute( parameter );
			}
		}

		public override string ToString()
		{
			return CommandParameter.ToString();
		}
	}
}
