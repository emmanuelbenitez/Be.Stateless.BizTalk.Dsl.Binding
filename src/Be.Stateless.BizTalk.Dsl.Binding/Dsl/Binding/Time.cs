﻿#region Copyright & License

// Copyright © 2012 - 2020 François Chabot
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
// http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

#endregion

using System;
using System.Diagnostics.CodeAnalysis;

namespace Be.Stateless.BizTalk.Dsl.Binding
{
	[SuppressMessage("Performance", "CA1815:Override equals and operator equals on value types")]
	public readonly struct Time
	{
		#region Operators

		[SuppressMessage("Usage", "CA2225:Operator overloads have named alternates")]
		public static implicit operator Time(DateTime dateTime)
		{
			return new Time(dateTime);
		}

		[SuppressMessage("Usage", "CA2225:Operator overloads have named alternates")]
		public static implicit operator DateTime(Time time)
		{
			return time._time;
		}

		#endregion

		public Time(int hour, int minute, int second = 0)
		{
			_time = new DateTime(DateTime.MinValue.Year, DateTime.MinValue.Month, DateTime.MinValue.Day, hour, minute, second);
		}

		private Time(DateTime dateTime)
		{
			_time = dateTime;
		}

		private readonly DateTime _time;
	}
}
