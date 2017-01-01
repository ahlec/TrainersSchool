using System;
using System.Windows;
using System.Windows.Markup;

namespace TrainersSchool.Controls
{
	[ContentProperty( nameof( Child ) )]
	public partial class VsBackground
	{
		public VsBackground()
		{
			InitializeComponent();
		}

		public UIElement Child
		{
			get { return (UIElement) GetValue( ChildProperty ); }
			set { SetValue( ChildProperty, value ); }
		}

		public static readonly DependencyProperty ChildProperty = DependencyProperty.Register( "Child", typeof( UIElement ), typeof( VsBackground ) );
	}
}
