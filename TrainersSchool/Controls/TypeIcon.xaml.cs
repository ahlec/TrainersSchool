using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using TrainersSchool.Pokemon;

namespace TrainersSchool.Controls
{
	/// <summary>
	/// Interaction logic for TypeIcon.xaml
	/// </summary>
	public partial class TypeIcon : UserControl
	{
		public TypeIcon()
		{
			InitializeComponent();
		}

		public PokemonType Type
		{
			get { return (PokemonType) GetValue( TypeProperty ); }
			set { SetValue( TypeProperty, value ); }
		}

		public static readonly DependencyProperty TypeProperty = DependencyProperty.Register( "Type", typeof( PokemonType ), typeof( TypeIcon ),
			new PropertyMetadata( PokemonType.Water ) );
	}

	class TypeToBrushConverter : IValueConverter
	{
		public Object Convert( Object value, Type targetType, Object parameter, CultureInfo culture )
		{
			switch ( (PokemonType) value )
			{
				case PokemonType.Normal:
					return NormalBrush;
				case PokemonType.Fighting:
					return FightingBrush;
				case PokemonType.Flying:
					return FlyingBrush;
				case PokemonType.Poison:
					return PoisonBrush;
				case PokemonType.Ground:
					return GroundBrush;
				case PokemonType.Rock:
					return RockBrush;
				case PokemonType.Bug:
					return BugBrush;
				case PokemonType.Ghost:
					return GhostBrush;
				case PokemonType.Steel:
					return SteelBrush;
				case PokemonType.Fire:
					return FireBrush;
				case PokemonType.Water:
					return WaterBrush;
				case PokemonType.Grass:
					return GrassBrush;
				case PokemonType.Electric:
					return ElectricBrush;
				case PokemonType.Psychic:
					return PsychicBrush;
				case PokemonType.Ice:
					return IceBrush;
				case PokemonType.Dragon:
					return DragonBrush;
				case PokemonType.Dark:
					return DarkBrush;
				case PokemonType.Fairy:
					return FairyBrush;
				default:
					throw new ArgumentException();
			}
		}

		public Object ConvertBack( Object value, Type targetType, Object parameter, CultureInfo culture )
		{
			throw new NotSupportedException();
		}

		public Brush NormalBrush { get; set; }
		public Brush FightingBrush { get; set; }
		public Brush FlyingBrush { get; set; }
		public Brush PoisonBrush { get; set; }
		public Brush GroundBrush { get; set; }
		public Brush RockBrush { get; set; }
		public Brush BugBrush { get; set; }
		public Brush GhostBrush { get; set; }
		public Brush SteelBrush { get; set; }
		public Brush FireBrush { get; set; }
		public Brush WaterBrush { get; set; }
		public Brush GrassBrush { get; set; }
		public Brush ElectricBrush { get; set; }
		public Brush PsychicBrush { get; set; }
		public Brush IceBrush { get; set; }
		public Brush DragonBrush { get; set; }
		public Brush DarkBrush { get; set; }
		public Brush FairyBrush { get; set; }
	}
}
