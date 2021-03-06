// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

namespace Microsoft.Azure.IIoT.OpcUa.Subscriber.Handlers {
    using Microsoft.Azure.IIoT.OpcUa.Subscriber;
    using Microsoft.Azure.IIoT.OpcUa.Subscriber.Models;
    using Microsoft.Azure.IIoT.Hub;
    using Opc.Ua;
    using Opc.Ua.PubSub;
    using Serilog;
    using System;
    using System.IO;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Publisher message handling
    /// </summary>
    public sealed class PubSubNetworkMessageBinaryHandler : IDeviceTelemetryHandler {

        /// <inheritdoc/>
        public string MessageSchema => Core.MessageSchemaTypes.NetworkMessageUadp;

        /// <summary>
        /// Create handler
        /// </summary>
        /// <param name="handlers"></param>
        /// <param name="logger"></param>
        public PubSubNetworkMessageBinaryHandler(IEnumerable<ISubscriberMessageProcessor> handlers, ILogger logger) {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _handlers = handlers?.ToList() ?? throw new ArgumentNullException(nameof(handlers));
        }

        /// <inheritdoc/>
        public async Task HandleAsync(string deviceId, string moduleId,
            byte[] payload, IDictionary<string, string> properties, Func<Task> checkpoint) {
            var json = Encoding.UTF8.GetString(payload);
            using (var stream = new MemoryStream(payload)) {
                var context = new ServiceMessageContext();
                try {
                    using (var decoder = new BinaryDecoder(stream, context)) {
                        var networkMessage = decoder.ReadEncodeable(null, typeof(NetworkMessage)) as NetworkMessage;
                        foreach (var dataSetMessage in networkMessage.Messages) {
                            var dataset = new DataSetMessageModel {
                                PublisherId = networkMessage.PublisherId,
                                MessageId = networkMessage.MessageId,
                                DataSetClassId = networkMessage.DataSetClassId,
                                DataSetWriterId = dataSetMessage.DataSetWriterId,
                                SequenceNumber = dataSetMessage.SequenceNumber,
                                Status = StatusCode.LookupSymbolicId(dataSetMessage.Status.Code),
                                MetaDataVersion = $"{dataSetMessage.MetaDataVersion.MajorVersion}"+
                                    $".{dataSetMessage.MetaDataVersion.MinorVersion}",
                                Timestamp = dataSetMessage.Timestamp,
                                Payload = new Dictionary<string, DataValueModel>()
                            };
                            foreach (var datapoint in dataSetMessage.Payload) {
                                dataset.Payload[datapoint.Key] = new DataValueModel() {
                                    Value = datapoint.Value?.Value,
                                    Status = (datapoint.Value?.StatusCode.Code == StatusCodes.Good)
                                        ? null : StatusCode.LookupSymbolicId(datapoint.Value.StatusCode.Code),
                                    SourceTimestamp = (datapoint.Value?.SourceTimestamp == DateTime.MinValue)
                                        ? null : (DateTime?)datapoint.Value?.SourceTimestamp,
                                    ServerTimestamp = (datapoint.Value?.ServerTimestamp == DateTime.MinValue)
                                        ? null : (DateTime?)datapoint.Value?.ServerTimestamp
                                };
                            }
                            await Task.WhenAll(_handlers.Select(h => h.HandleMessageAsync(dataset)));
                        }
                    }
                }
                catch (Exception ex) {
                    _logger.Error(ex, "Subscriber binary network message handling failed - skip");
                }
            }
        }

        /// <inheritdoc/>
        public Task OnBatchCompleteAsync() {
            return Task.CompletedTask;
        }

        private readonly ILogger _logger;
        private readonly List<ISubscriberMessageProcessor> _handlers;
    }
}
