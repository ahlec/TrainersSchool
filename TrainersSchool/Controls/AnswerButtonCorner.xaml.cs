using System;
using System.Windows;

namespace TrainersSchool.Controls
{
	public partial class AnswerButtonCorner
	{
		public AnswerButtonCorner()
		{
			InitializeComponent();
		}

		public Double Rotation
		{
			get { return (Double) GetValue( RotationProperty ); }
			set { SetValue( RotationProperty, value ); }
		}

		public static readonly DependencyProperty RotationProperty = DependencyProperty.Register( "Rotation", typeof( Double ), typeof( AnswerButtonCorner ) );
	}
}
