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

extern alias ExplorerOM;
using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.BizTalk.Adapter.Wcf.Config;
using Microsoft.BizTalk.Component.Interop;
using Microsoft.BizTalk.Deployment.Binding;

namespace Be.Stateless.BizTalk.Dsl.Binding.Adapter
{
	[SuppressMessage("ReSharper", "UnusedMember.Global", Justification = "Public DSL API.")]
	public abstract class SBMessagingAdapter<TConfig>
		: AdapterBase,
			IAdapterConfigAddress<Uri>,
			IAdapterConfigOptionalAccessControlService,
			IAdapterConfigSasCredentials
		where TConfig : AdapterConfig,
		IAdapterConfigAddress,
		IAdapterConfigTimeouts,
		IAdapterConfigAcsCredentials,
		IAdapterConfigSasCredentials,
		new()
	{
		static SBMessagingAdapter()
		{
			_protocolType = GetProtocolTypeFromConfigurationClassId(new Guid("9c458d4a-a73c-4cb3-89c4-86ae0103de2f"));
		}

		protected SBMessagingAdapter() : base(_protocolType)
		{
			_adapterConfig = new TConfig();
		}

		#region IAdapterConfigAddress<Uri> Members

		/// <summary>
		/// Specify the URL where the Service Bus queue is deployed.
		/// </summary>
		/// <remarks>
		/// The URL is generally formatted as follows: <![CDATA[sb://<namespace>.servicebus.windows.net/<queue_name>/]]>.
		/// </remarks>
		[SuppressMessage("Naming", "CA1721:Property names should not match get methods")]
		public Uri Address { get; set; }

		#endregion

		#region IAdapterConfigOptionalAccessControlService Members

		public bool UseAcsAuthentication
		{
			get => _adapterConfig.UseAcsAuthentication;
			set => _adapterConfig.UseAcsAuthentication = value;
		}

		public Uri StsUri
		{
			get => new Uri(_adapterConfig.StsUri);
			set => _adapterConfig.StsUri = value?.ToString();
		}

		public string IssuerName
		{
			get => _adapterConfig.IssuerName;
			set => _adapterConfig.IssuerName = value;
		}

		public string IssuerSecret
		{
			get => _adapterConfig.IssuerSecret;
			set => _adapterConfig.IssuerSecret = value;
		}

		#endregion

		#region IAdapterConfigSasCredentials Members

		/// <summary>
		/// Specify the SAS key value.
		/// </summary>
		public string SharedAccessKey
		{
			get => _adapterConfig.SharedAccessKey;
			set => _adapterConfig.SharedAccessKey = value;
		}

		/// <summary>
		/// Specify the SAS key name.
		/// </summary>
		public string SharedAccessKeyName
		{
			get => _adapterConfig.SharedAccessKeyName;
			set => _adapterConfig.SharedAccessKeyName = value;
		}

		/// <summary>
		/// Whether to use Shared Access Signature for authentication.
		/// </summary>
		public bool UseSasAuthentication
		{
			get => _adapterConfig.UseSasAuthentication;
			set => _adapterConfig.UseSasAuthentication = value;
		}

		#endregion

		#region Base Class Member Overrides

		protected override string GetAddress()
		{
			return Address?.ToString();
		}

		protected override void Save(IPropertyBag propertyBag)
		{
			_adapterConfig.Save(propertyBag as ExplorerOM::Microsoft.BizTalk.ExplorerOM.IPropertyBag);
		}

		protected override void Validate()
		{
			_adapterConfig.Address = GetAddress();
			_adapterConfig.Validate();
			_adapterConfig.Address = null;
		}

		#endregion

		/// <summary>
		/// Specifies a timespan value that indicates the time for a channel close operation to complete.
		/// </summary>
		/// <remarks>
		/// It defaults to <c>1</c> minute.
		/// </remarks>
		public TimeSpan CloseTimeout
		{
			get => _adapterConfig.CloseTimeout;
			set => _adapterConfig.CloseTimeout = value;
		}

		/// <summary>
		/// Specifies a timespan value that indicates the time for a channel open operation to complete.
		/// </summary>
		/// <remarks>
		/// It defaults to <c>1</c> minute.
		/// </remarks>
		public TimeSpan OpenTimeout
		{
			get => _adapterConfig.OpenTimeout;
			set => _adapterConfig.OpenTimeout = value;
		}

		[SuppressMessage("ReSharper", "StaticMemberInGenericType")]
		private static readonly ProtocolType _protocolType;

		[SuppressMessage("Design", "CA1051:Do not declare visible instance fields")]
		protected readonly TConfig _adapterConfig;
	}
}
