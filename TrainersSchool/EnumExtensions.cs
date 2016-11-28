using System;
using System.Collections.Immutable;
using System.Reflection;

namespace TrainersSchool
{
	public static class EnumExtensions
	{
		public static T GetAttributeOfType<T>( this Enum value ) where T : Attribute
		{
			MemberInfo memberInfo;
			if ( !_enumMemberInfo.TryGetValue( value, out memberInfo ) )
			{
				memberInfo = value.GetType().GetMember( value.ToString() )[0];
				_enumMemberInfo = _enumMemberInfo.Add( value, memberInfo );
			}
			return memberInfo.GetCustomAttribute<T>();
		}

		static ImmutableDictionary<Enum, MemberInfo> _enumMemberInfo = ImmutableDictionary<Enum, MemberInfo>.Empty;
	}
}
