// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

namespace Microsoft.Azure.IIoT.OpcUa.Api.Registry.Models {
    using System.Runtime.Serialization;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Discoverer registration model
    /// </summary>
    [DataContract]
    public class DiscovererApiModel {

        /// <summary>
        /// Discoverer id
        /// </summary>
        [DataMember(Name = "id")]
        [Required]
        public string Id { get; set; }

        /// <summary>
        /// Site of the discoverer
        /// </summary>
        [DataMember(Name = "siteId",
            EmitDefaultValue = false)]
        public string SiteId { get; set; }

        /// <summary>
        /// Whether the discoverer is in discovery mode
        /// </summary>
        [DataMember(Name = "discovery",
            EmitDefaultValue = false)]
        public DiscoveryMode? Discovery { get; set; }

        /// <summary>
        /// Discoverer configuration
        /// </summary>
        [DataMember(Name = "discoveryConfig",
            EmitDefaultValue = false)]
        public DiscoveryConfigApiModel DiscoveryConfig { get; set; }

        /// <summary>
        /// Requested discovery mode
        /// </summary>
        [DataMember(Name = "requestedMode",
            EmitDefaultValue = false)]
        public DiscoveryMode? RequestedMode { get; set; }

        /// <summary>
        /// Requested discoverer configuration
        /// </summary>
        [DataMember(Name = "requestedConfig",
            EmitDefaultValue = false)]
        public DiscoveryConfigApiModel RequestedConfig { get; set; }

        /// <summary>
        /// Current log level
        /// </summary>
        [DataMember(Name = "logLevel",
            EmitDefaultValue = false)]
        public TraceLogLevel? LogLevel { get; set; }

        /// <summary>
        /// Whether the registration is out of sync between
        /// client (module) and server (service) (default: false).
        /// </summary>
        [DataMember(Name = "outOfSync",
            EmitDefaultValue = false)]
        public bool? OutOfSync { get; set; }

        /// <summary>
        /// Whether discoverer is connected on this registration
        /// </summary>
        [DataMember(Name = "connected",
            EmitDefaultValue = false)]
        public bool? Connected { get; set; }
    }
}
