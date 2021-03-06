﻿#region Copyright & License

// Copyright © 2012 - 2021 François Chabot
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

using Be.Stateless.BizTalk.Component;
using Be.Stateless.BizTalk.Dsl.Binding;
using Be.Stateless.BizTalk.MicroComponent;
using Be.Stateless.BizTalk.MicroPipelines;

namespace Be.Stateless.BizTalk.Dummies.Bindings
{
	internal class TwoWaySendPort : SendPortBase<string>
	{
		public TwoWaySendPort()
		{
			Name = nameof(TwoWaySendPort);
			Description = "Some Useless Two-Way Test Send Port";
			SendPipeline = new SendPipeline<PassThruTransmit>();
			ReceivePipeline = new ReceivePipeline<PassThruReceive>(
				pl => pl.Decoder<MicroPipelineComponent>(
					c => {
						c.Components = new IMicroComponent[] {
							new ContextBuilder {
								ExecutionTime = PluginExecutionTime.Deferred,
								BuilderType = typeof(DummyContextBuilder)
							}
						};
					}));
			Transport.Adapter = new DummyAdapter();
			Transport.Host = "Send Host Name";
		}
	}
}
