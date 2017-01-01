using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TrainersSchool.Controls;

namespace TrainersSchool.Views
{
	public partial class SessionView
	{
		public SessionView()
		{
			InitializeComponent();
		}

		protected override void OnKeyDown( KeyEventArgs e )
		{
			AnswerButton button = (AnswerButton) FocusManager.GetFocusedElement( this );
			if ( e.Key == Key.Left || e.Key == Key.A )
			{
				button.PreviousButton.Focus();
			}
			else if ( e.Key == Key.Right || e.Key == Key.D )
			{
				button.NextButton.Focus();
			}
			base.OnKeyDown( e );
		}
	}
}
