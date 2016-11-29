using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.Generic;

namespace TrainersSchool.ViewModels
{
	public abstract class ViewModel : INotifyPropertyChanged
	{
		protected Boolean SetProperty<T>( ref T field, T value, [CallerMemberName] String propertyName = null )
		{
			if ( EqualityComparer<T>.Default.Equals( field, value ) )
			{
				return false;
			}

			field = value;
			OnPropertyChanged( propertyName );
			return true;
		}

		protected void OnPropertyChanged( String propertyName )
		{
			PropertyChanged?.Invoke( this, new PropertyChangedEventArgs( propertyName ) );
		}

		public event PropertyChangedEventHandler PropertyChanged;
	}
}
