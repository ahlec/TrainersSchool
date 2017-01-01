﻿using System;
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

		public static readonly DependencyProperty CommandProperty = DependencyProperty.Register( "Command", typeof( ICommand ),
			typeof( AnswerButton ) );
		public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.Register( "CommandParameter", typeof( Object ),
			typeof( AnswerButton ) );
		public static readonly DependencyProperty TextProperty = DependencyProperty.Register( "Text", typeof( String ),
			typeof( AnswerButton ) );

		private void OnMouseDown( object sender, MouseButtonEventArgs e )
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
	}
}
